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

		private void Awake()
		{

			registerUI = ObjectFind.ChildFind("Register", transform);
			noRegisterUI = ObjectFind.ChildFind("UserInfomation", transform);

		}

		private void OnEnable()
		{

			UserData userData = gameObject.AddComponent<UserData>();
			Debug.Log(userData.UserID);

			if (userData.UserID == UserDataKey.UserID_Default || userData.UserID == null)
			{
				registerUI.gameObject.SetActive(true);
				noRegisterUI.gameObject.SetActive(false);
			}
			else
			{
				registerUI.gameObject.SetActive(false);
				noRegisterUI.gameObject.SetActive(true);
			}

		}

	}

}