using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace Sailing.Server
{
    public class GetUserMoney : MonoBehaviour
    {
        string User_id;//自身のUser_idを格納する変数
        string TextSent;//送信されてきたテキストを格納する
        string EraseForward;//帰ってきたテキストからいらない文字を消す(前方)
        string EraseBack;//帰ってきたテキストからいらない文字を消す(後方)
        float Conversion_float;//string型をfloat型に変更する
        float ProductPrice;//商品の価格を格納
        float ResultMoney;//自身が持っているお金からProductPriceを引いた値を格納する
        string Conversion_string;//float型をstring型に変更する

        // Start is called before the first frame update
        void Start()
        {
            ProductPrice = 100;
        }

        public void OnClickButton()
        {
            User_id = PlayerPrefs.GetString(UserDataKey.UserID_Key);
            //Debug.Log("プレイヤーID" + User_id);
            StartCoroutine(Method(User_id));
        }
        private IEnumerator Method(string user_id)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", user_id);
            //1.UnityWebRequestを生成
            UnityWebRequest RequestUser_id = UnityWebRequest.Post(ServerData.GetUserMoney, form);

            //2.SendWebRequestを実行し、送受信開始
            yield return RequestUser_id.SendWebRequest();


            //3.isNetworkErrorとisHttpErrorでエラー判定
            if (RequestUser_id.isHttpError || RequestUser_id.isNetworkError)
            {
                //4.エラー確認
                Debug.Log("検索に失敗しました");
            }
            else
            {
                //4.結果確認
                Debug.Log("検索に成功しました" + RequestUser_id.downloadHandler.text);
                TextSent = RequestUser_id.downloadHandler.text;
                EraseForward = TextSent.Replace("[{\"money\":\"", "");
                EraseBack = EraseForward.Replace("\"}]", "");
                //Debug.Log("テキスト: " + EraseBack);
                Conversion_float = float.Parse(EraseBack);
                //ResultMoney = 1000;
                ResultMoney = Conversion_float - ProductPrice;
                //Debug.Log("計算結果を送る " + ResultMoney);
                Conversion_string = ResultMoney.ToString();
                //Debug.Log("String計算" + Conversion_string);

                WWWForm form2 = new WWWForm();
                form2.AddField("id", user_id);
                form2.AddField("money", Conversion_string);
                UnityWebRequest Post_money = UnityWebRequest.Post(ServerData.SetUserMoney, form2);
                yield return Post_money.SendWebRequest();
                if (Post_money.isHttpError || Post_money.isNetworkError)
                {
                    //4.エラー確認
                    Debug.Log("送信に失敗しました");
                }
                else
                {
                    Debug.Log("送信に成功しました"+Post_money.downloadHandler.text);
                }
            }

        }

    }
}