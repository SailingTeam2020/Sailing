/*
 * 
 * 長嶋
 * 
 */

using Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sailing.Server
{

    public class RegisterUserData : MonoBehaviour
    {

        [SerializeField]
        private Text  nameText;
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
            form.AddField("pref", prefList.value);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(yearText.text).Append(monthText.text).Append(dayText.text);
            form.AddField("birthday", sb.ToString());

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
            userData.WriteUserData(id, nameText.text, prefList.value, sb.ToString());
            registerButton.interactable = false;

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