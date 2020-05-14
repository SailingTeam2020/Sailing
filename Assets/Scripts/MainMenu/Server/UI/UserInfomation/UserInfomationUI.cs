using Common;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Server
{

    public class UserInfomationUI : MonoBehaviour
    {

        private Text userNameText;
        private Text userBirthText;
        private Text userPrefText;

        private void Start()
        {

            userNameText = ObjectFind.ChildFind("UserData", ObjectFind.ChildFind("Name", transform)).gameObject.GetComponent<Text>();
            userBirthText = ObjectFind.ChildFind("UserData", ObjectFind.ChildFind("Birth", transform)).gameObject.GetComponent<Text>();
            userPrefText = ObjectFind.ChildFind("UserData", ObjectFind.ChildFind("Pref", transform)).gameObject.GetComponent<Text>();

            GetUserData();

        }

        private void GetUserData()
        {

            userNameText.text = PlayerPrefs.GetString(UserDataKey.UserName_Key);

            string birth = PlayerPrefs.GetString(UserDataKey.UserBirth_Key);
            StringBuilder sb = new StringBuilder();
            sb.Append(birth.Substring(0, 4)).Append("年").Append(birth.Substring(4, 2)).Append("月").Append(birth.Substring(6, 2)).Append("日");
            userBirthText.text = sb.ToString();

            int pref = PlayerPrefs.GetInt(UserDataKey.UserPref_Key);
            userPrefText.text = PrefectureNameList.PrefName[pref];

        }

    }

}