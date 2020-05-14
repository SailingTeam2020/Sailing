using UnityEngine;

namespace Sailing.Server
{

    public class UserDataDebug : MonoBehaviour
    {

        [SerializeField,Header("チェックを入れると起動時にユーザーデータを削除します")]
        private bool isDeleteUserData;

        private void Awake()
        {

            if (isDeleteUserData)
            {
                UserData userData = gameObject.AddComponent<UserData>();
                userData.DeleteUserData();
                Debug.Log("ユーザーデータを削除しました");
            }

        }

    }

}