/*
 * 
 * 長嶋
 * https://qiita.com/nmxi/items/9da751e88e0b6aefaa62
 * http://kan-kikuchi.hatenablog.com/entry/UnityWebRequest
 * 上記サイトを参考に作成
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;

namespace Sailing.Server
{

    public class UserCatchData : MonoBehaviour
    {
        /// <summary>
        /// @brief Getコルーチンを開始する
        /// </summary>
        public void Start()
        {

            StartCoroutine("Get");

        }

        /// <summary>
        /// @brief ランキングに必要なデータを取得し、テキストに出力する
        /// </summary>
        /// <param name="request"></param>
        private void GetUserData(UnityWebRequest request)
        {
            string jsonData = request.downloadHandler.text;
            IList userList = (IList)Json.Deserialize(jsonData);

            int index = 0;
            foreach (IDictionary data in userList)
            {
                string id = (string)data["id"];
                string name = (string)data["name"];
                Debug.Log(id); 
                Debug.Log(name);
                /*string rank = (string)data["rank"];
                string name = (string)data["name"];
                float time = float.Parse((string)data["time"]);*/

            }

        }

        public string ConvertStringTime(float time)
        {

            int m = (int)(time / 60.0f);
            int s = (int)(time % 60.0f);
            int ss = (int)((time - Mathf.Floor(time)) * 100);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(m.ToString("D2"));
            sb.Append(":");
            sb.Append(s.ToString("D2"));
            sb.Append(".");
            sb.Append(ss.ToString("D2"));

            return sb.ToString();
        }

        #region UnityWebRequest

        /// <summary>
        /// @brief データベースからデータを取得するためにPHPにアクセスする
        /// </summary>
        /// <returns></returns>
        private IEnumerator Get()
        {

            UnityWebRequest request = UnityWebRequest.Get(ServerData.GetUserData);

            request.timeout = ServerData.MaxWaitTime;
            yield return request.SendWebRequest();

            ResponseLog(request.responseCode);

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError("http Post NG: " + request.error);
                yield break;
            }

            GetUserData(request);

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

        #endregion

        #region WWW(旧式のためコメントアウト)

        /*[System.Obsolete]
        private IEnumerator PostWWW(string url, Dictionary<string, string> post)
        {

            WWWForm form = new WWWForm();

            foreach (KeyValuePair<string, string> postArg in post)
            {
                form.AddField(postArg.Key, postArg.Value);
            }

            WWW www = new WWW(url, form);

            yield return StartCoroutine(CheckTimeOut(www, 3f));

            //このエラーが通る場合、接続ができていない
            if (www.error != null)
            {
                Debug.LogError("http Post NG: " + www.error);
            }
            else if (www.isDone)
            {
                //データをテキストに反映
                resultText.GetComponent<Text>().text = www.text;
            }

            Debug.Log("Post");

        }

        /// <summary>
        /// @brief WWWのタイムアウト処理を行う
        /// </summary>
        /// <param name="www">WWW</param>
        /// <param name="timeOut">タイムアウトする時間</param>
        /// <returns>null</returns>
        [System.Obsolete]
        private IEnumerator CheckTimeOut(WWW www, float timeOut)
        {

            float requestTime = Time.time;

            while (!www.isDone)
            {
                if (Time.time - requestTime < timeOut)
                {
                    yield return null;
                }
                else
                {
                    Debug.LogError("TimeOut");
                    break;
                }
            }

            yield return null;
        }*/

        #endregion

    }

}
