using UnityEngine;

namespace Sailing.Server
{

	public class UserData : MonoBehaviour
	{

		public string UserID {
			get;
			private set;
		}

		public string UserName {
			get;
			private set;
		}

		public int Prefecture {
			get;
			private set;
		}

		public string Birthday {
			get;
			private set;
		}


		private void Awake()
		{

			ReadUserData();

		}

		public void ReadUserData()
		{

			UserID = PlayerPrefs.GetString(UserDataKey.UserID_Key, UserDataKey.UserID_Default);
			UserName = PlayerPrefs.GetString(UserDataKey.UserName_Key, UserDataKey.UserName_Default);
			Prefecture = PlayerPrefs.GetInt(UserDataKey.UserPref_Key, UserDataKey.UserPref_Default);
			Birthday = PlayerPrefs.GetString(UserDataKey.UserBirth_Key, UserDataKey.UserBirth_Default);

		}

		public void WriteUserData(string id, string name, int pref, string birth)
		{

			PlayerPrefs.SetString(UserDataKey.UserID_Key, id);
			PlayerPrefs.SetString(UserDataKey.UserName_Key, name);
			PlayerPrefs.SetInt(UserDataKey.UserPref_Key, pref);
			PlayerPrefs.SetString(UserDataKey.UserBirth_Key, birth);

			PlayerPrefs.Save();

		}

		public void DeleteUserData()
		{

			PlayerPrefs.DeleteAll();

		}

	}

}