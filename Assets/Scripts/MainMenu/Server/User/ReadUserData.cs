using Common;
using UnityEngine;

namespace Sailing.Server
{

	public class ReadUserData : MonoBehaviour
	{

		[SerializeField]
		private Transform registerUI = null;
		[SerializeField]
		private Transform noRegisterUI = null;
		[SerializeField]
		private Transform userLoginInterfaceUI = null;


		private void Awake()
		{

			registerUI = ObjectFind.ChildFind("Register", transform);
			noRegisterUI = ObjectFind.ChildFind("UserInfomation", transform);
			userLoginInterfaceUI = ObjectFind.ChildFind("UserLoginInterface", transform);

		}

		private void OnEnable()
		{

			UserData userData = gameObject.AddComponent<UserData>();
			Debug.Log(userData.UserID);
			if (userData.UserID == UserDataKey.UserID_Default )//|| userData.UserName == UserDataKey.UserName_Default)
			{//ユーザー登録画面
				registerUI.gameObject.SetActive(true);
				noRegisterUI.gameObject.SetActive(false);
				userLoginInterfaceUI.gameObject.SetActive(false);
			}
			else if (userData.UserName != UserDataKey.UserName_Default)// || userData.UserName != null)
			{//ログアウト画面
				registerUI.gameObject.SetActive(false);
				noRegisterUI.gameObject.SetActive(true);
				userLoginInterfaceUI.gameObject.SetActive(false);
			}
			else
			{//ユーザーログイン画面
				registerUI.gameObject.SetActive(false);
				noRegisterUI.gameObject.SetActive(false);
				userLoginInterfaceUI.gameObject.SetActive(true);
			}

			/*if (userData.UserName == UserDataKey.UserName_Default || userData.UserName == null)
			{
				registerUI.gameObject.SetActive(true);
				noRegisterUI.gameObject.SetActive(false);
			}
			else
			{
				registerUI.gameObject.SetActive(false);
				noRegisterUI.gameObject.SetActive(true);
			}*/

		}

	}

}