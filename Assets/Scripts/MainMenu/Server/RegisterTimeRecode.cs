/*
 * 
 * 長嶋
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace Sailing.Server
{

    public class RegisterTimeRecode : MonoBehaviour
    {

        public float RecodeTime {
            get;
            set;
        }

        private UserData userData;

        private void Awake()
        {

            userData = gameObject.AddComponent<UserData>();
            Debug.Log(userData.UserID);
            RecodeTime = 0.0f;

        }

        /// <summary>
        /// @brief PostDataコルーチンを開始する
        /// </summary>
        public void Register()
        {

            StartCoroutine(Push());

        }

        /// <summary>
        /// @brief 入力されたユーザーの情報をPHPに送信する
        /// </summary>
        /// <returns></returns>
        public IEnumerator Push()
        {

            //IDが存在しない場合登録しない
            if (userData.UserID == UserDataKey.UserID_Default || userData.UserID == null)
            {
                Debug.Log("IDが存在しないため、登録できませんでした");
                yield break;
            }

            WWWForm form = new WWWForm();

            form.AddField("id", userData.UserID);
            form.AddField("time", RecodeTime.ToString());

            UnityWebRequest request = UnityWebRequest.Post(ServerData.RegisterRanking, form);

            request.timeout = ServerData.MaxWaitTime;
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);

            ResponseLog(request.responseCode);

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("http Post NG: " + request.error);
                Debug.Log("登録に失敗しました");
                yield break;
            }

            Debug.Log("登録が完了しました");

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