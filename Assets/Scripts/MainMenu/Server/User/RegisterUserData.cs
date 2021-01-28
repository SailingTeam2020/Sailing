/*スクリプト名     ：
 * 作成者          ：長嶋
 * 作成日          ：不明
 * 概要            ：Userのデータを登録する為に
 * 
 * 更新者          ：足立拓海
 * 更新日          ：2021/01/15
 * 更新内容        ：ログアウトボタンの追加
 * 
 * 更新者          ：足立拓海
 * 更新日          ：2021/01/18
 * 更新内容        ：パスワードの追加
 */

using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Sailing.Server
{

    public class RegisterUserData : MonoBehaviour
    {

        private Scene loadScene;

        [SerializeField]
        private Text  nameText;
        [SerializeField]
        private Text passWordText;
        [SerializeField]
        private Dropdown prefList;
        [SerializeField]
        private Text yearText;
        [SerializeField]
        private Text monthText;
        [SerializeField]
        private Text dayText;
        [SerializeField]
        private Button registerButton;

        //追記
        [SerializeField]
        private Button LogOutButton;


        [SerializeField] private string id = "None";


        private void Start()
        {

            //将来的なバグの温床になると思うからアタッチすることを推奨する
            /*Transform input = ObjectFind.ChildFind("Input", transform);
            nameText = ObjectFind.ChildFind("Text", ObjectFind.ChildFind("Name", input)).GetComponent<Text>();
            prefList = ObjectFind.ChildFind("Prefectures", input).GetComponent<Dropdown>();
            Transform birth = ObjectFind.ChildFind("Birthday", input);
            yearText = ObjectFind.ChildFind("Label", ObjectFind.ChildFind("Year", birth)).GetComponent<Text>();
            monthText = ObjectFind.ChildFind("Label", ObjectFind.ChildFind("Month", birth)).GetComponent<Text>();
            dayText = ObjectFind.ChildFind("Label", ObjectFind.ChildFind("Day", birth)).GetComponent<Text>();
            registerButton = ObjectFind.ChildFind("Push", transform).GetComponent<Button>();*/

        }

        /// <summary>
        /// @brief PostDataコルーチンを開始する
        /// </summary>
        public void Register()
        {

            StartCoroutine("Push");

        }

        /// <summary>
        /// @brief 入力されたユーザーの情報をPHPに送信する
        /// </summary>
        /// <returns></returns>
        private IEnumerator Push()
        {

            WWWForm form = new WWWForm();

            id = IDCreater.IDOutPut();

            form.AddField("id", id);
            form.AddField("name", nameText.text);
            form.AddField("password", int.Parse(passWordText.text));
            form.AddField("pref", prefList.value);

            //登録日の登録用変数
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(yearText.text).Append(monthText.text).Append(dayText.text);
            form.AddField("birthday", sb.ToString());

            //Post内で指定したURLに、formの中身を送る。
            UnityWebRequest request = UnityWebRequest.Post(ServerData.RegisterUserData, form);

            request.timeout = ServerData.MaxWaitTime;
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);

            ResponseLog(request.responseCode);

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("http Post NG: " + request.error);
                gameObject.GetComponent<Button>().interactable = true;
                Debug.Log("登録に失敗しました");
                yield break;
            }

            Debug.Log("登録が完了しました");

            UserData userData = gameObject.AddComponent<UserData>();
            
            // 登録が完了したらIDを端末に保持するし、ボタンを押せなくする
            userData.WriteUserData(id, nameText.text, int.Parse(passWordText.text),prefList.value, sb.ToString());
            registerButton.interactable = false;

            LogOutButton.interactable = true;
        }

        public void LogOut()
        {
            //UserDataDelete userDataDelete = GetComponent<UserDataDelete>();
            loadScene = SceneManager.GetActiveScene();
            if (UserDataDelete.Instance.PlayerPresDateDelete())
            {
                Debug.Log("データの削除が完了しました。");
                //Debug.Log(loadScene.name);
            }
            else
            {
                Debug.Log("データが削除できませんでした。");
            }
            registerButton.interactable = true;
            LogOutButton.interactable = false;

            SceneManager.LoadScene(loadScene.name);

        }


        /// <summary>
        /// @brief 下記サイトのレスポンス結果をLogに出力する
        /// https://developer.mozilla.org/ja/docs/Web/HTTP/Status
        /// </summary>
        /// <param name="code"></param>
        private void ResponseLog(long code)
        {

            switch (code)
            {
                case 200:
                    Debug.Log("success");
                    break;
                case 404:
                    Debug.LogWarning("not found");
                    break;
                case 500:
                    Debug.LogWarning("server error");
                    break;
                default:
                    break;
            }

        }

    }

}