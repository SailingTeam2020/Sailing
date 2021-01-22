/*スクリプト名：LpoginUserData.cs
 *作成者      ：足立拓海
 *作成日      ：2021/01/18
 *概要        ：ログインをする際の処理。PHPに送るデータなどやり取りをする。
 *外部参照変数：UserData→Loginをした際、PlayerPrefsにデータを保存する。
 *              
 *
 *
 */

using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


namespace Sailing.Server {
    public class LoginUserData : MonoBehaviour
    {
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text passWordText;

        [SerializeField]
        private Button loginButton;

        [SerializeField]
        private string playerId;

        private void Awake()
        {
            playerId = PlayerPrefs.GetString(UserDataKey.UserID_Key, UserDataKey.UserID_Default);

        }

        public void LoginUser()
        {
            StartCoroutine("Login");

        }

        private IEnumerator Login()
        {
            WWWForm form = new WWWForm();

            
            form.AddField("id", playerId);
            form.AddField("name", nameText.text);
            form.AddField("password", passWordText.text);

            //Post内で指定したURLに、formの中身を送る。
            UnityWebRequest request = UnityWebRequest.Post(ServerData.LoginUser, form);

            request.timeout = ServerData.MaxWaitTime;
            yield return request.SendWebRequest();


            ResponseLog(request.responseCode);

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("http Post NG: " + request.error);
                gameObject.GetComponent<Button>().interactable = true;
                Debug.Log("ログインに失敗しました");
                yield break;
            }

            writeUserData(request.downloadHandler.text);

            Debug.Log("ログインが完了しました");

            UserData userData = gameObject.AddComponent<UserData>();

            // 登録が完了したらIDを端末に保持するし、ボタンを押せなくする
            //userData.LoginUserData(nameText.text, int.Parse(passWordText.text), prefList.value, sb.ToString());
            //registerButton.interactable = false;
            
            loginButton.interactable = true;


        }

        void writeUserData(string userData)
        {
            Debug.Log(userData);


        }




        /// <summary>
        /// @brief 下記サイトのレスポンス結果をLogに出力する
        /// https://developer.mozilla.org/ja/docs/Web/HTTP/Status
        /// </summary>
        /// <param>@note RegisterUserData.csからまるコピ</param>
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
