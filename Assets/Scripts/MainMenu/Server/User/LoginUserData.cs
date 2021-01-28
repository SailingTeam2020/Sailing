/*スクリプト名：LpoginUserData.cs
 *作成者      ：足立拓海
 *作成日      ：2021/01/18
 *概要        ：ログインをする際の処理。PHPに送るデータなどやり取りをする。
 *外部参照変数：PlayerPrefs→ユーザーのデータを登録。         
 *参考サイト　：https://docs.unity3d.com/Manual/UnityWebRequest.html
 *              https://qiita.com/phi/items/914bc839b543988fc0ec   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using MiniJSON;

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

            //Debug.Log(request.downloadHandler.data);

            if (request.isHttpError || request.isNetworkError || !writeUserData(request.downloadHandler.text))
            {
                Debug.LogError("http Post NG: " + request.error);
                gameObject.GetComponent<Button>().interactable = true;
                Debug.Log("ログインに失敗しました");
                yield break;
            }

            
            Debug.Log("ログインが完了しました");

            UserData userData = gameObject.AddComponent<UserData>();

            // 登録が完了したらIDを端末に保持するし、ボタンを押せなくする
            
            loginButton.interactable = true;


        }

        bool writeUserData(string userData)
        {
            //Debug.Log(userData);

            var userList = Json.Deserialize(userData) as Dictionary<string, object>;
            
            //Debug　//確認用
            //Debug.Log((string)userList["id"]);
            //Debug.Log((string)userList["name"]);
            //Debug.Log((string)userList["password"]);
            //Debug.Log((string)userList["pref_id"]);

            string birth = (string)userList["birthday"];
            birth = birth.Replace("-", "");

            Debug.Log(birth);

            PlayerPrefs.SetString(UserDataKey.UserID_Key, (string)userList["id"]);
            PlayerPrefs.SetString(UserDataKey.UserName_Key, (string)userList["name"]);
            PlayerPrefs.SetInt(UserDataKey.UserPassWord_Key, int.Parse((string)userList["password"]));
            PlayerPrefs.SetInt(UserDataKey.UserPref_Key, int.Parse((string)userList["pref_id"]));
            PlayerPrefs.SetString(UserDataKey.UserBirth_Key, birth);

            PlayerPrefs.Save();

            return true;
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
