/*クラス名：PlayerInfo.cs
 * 作成日：01/08
 * 作成者：小林凱
 */

using Common;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sailing.Server
{
    public class PlayerInfo : MonoBehaviour
    {

        Scene loadScene;

        [SerializeField]
        private Text userNameText;

        [SerializeField]
        private Text HaveMoneyText;

        public string User_id;
        string TextSent;//送信されてきたテキストを格納する
        string EraseForward;//帰ってきたテキストからいらない文字を消す(前方)
        string EraseBack;//帰ってきたテキストからいらない文字を消す(後方)


        public void Awake()
        {

            loadScene = SceneManager.GetActiveScene();

            User_id = PlayerPrefs.GetString(UserDataKey.UserID_Key);
            StartCoroutine(Method(User_id));
        }

        private void Start()
        {

            userNameText = ObjectFind.ChildFind("UserData", ObjectFind.ChildFind("Name", transform)).gameObject.GetComponent<Text>();
            HaveMoneyText = ObjectFind.ChildFind("HaveMoney", ObjectFind.ChildFind("Money", transform)).gameObject.GetComponent<Text>();

            GetUserData();

        }

        public void GetUserData()
        {

            //SceneManager.LoadScene(loadScene.name);

            if (PlayerPrefs.GetString(UserDataKey.UserName_Key) ==
               PlayerPrefs.GetString(UserDataKey.UserName_Default))
            {
                userNameText.text = "ゲストさん";
            }
            else
            {
                userNameText.text = PlayerPrefs.GetString(UserDataKey.UserName_Key) + "さん";
            }

            if (PlayerPrefs.GetString(UserDataKey.UserMoney_Key) == 
                PlayerPrefs.GetString(UserDataKey.UserMoney_Default))
            {
                HaveMoneyText.text = PlayerPrefs.GetString(UserDataKey.UserName_Default);
                Debug.Log(PlayerPrefs.GetString(UserDataKey.UserName_Default));
            }
            else
            {
                HaveMoneyText.text = PlayerPrefs.GetString(UserDataKey.UserMoney_Key);
                Debug.Log("else");
            }

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
                PlayerPrefs.SetString(UserDataKey.UserMoney_Key, EraseBack);
            }

        }

    }
}
