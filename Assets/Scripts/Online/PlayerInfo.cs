/*クラス名：PlayerInfo.cs
 * 作成日：01/08
 * 作成者：小林凱
 */

using Common;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Server
{
    public class PlayerInfo : MonoBehaviour
    {

        [SerializeField]
        private Text userNameText;
        //private Text userBirthText;
        //private Text userPrefText;

        private void Start()
        {

            userNameText = ObjectFind.ChildFind("UserData", ObjectFind.ChildFind("Name", transform)).gameObject.GetComponent<Text>();

            GetUserData();

        }

        private void GetUserData()
        {

            if (PlayerPrefs.GetString(UserDataKey.UserName_Key) ==
               PlayerPrefs.GetString(UserDataKey.UserName_Default))
            {
                userNameText.text = "ゲストさん";
            }
            else
            {
                userNameText.text = PlayerPrefs.GetString(UserDataKey.UserName_Key) + "さん";
            }

        }
    }
}
