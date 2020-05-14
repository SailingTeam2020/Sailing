using UnityEngine;

namespace Sailing.Online
{

    public class NameDirection : MonoBehaviour
    {

        [SerializeField]
        private Camera cameraObject;

        private void LateUpdate()
        {

            // 回転をカメラと同期させる
            transform.rotation = cameraObject.transform.rotation;

        }

    }

}