using UnityEngine;

namespace Sailing.Online
{

    public class CameraController : MonoBehaviour
    {

        private float scale;
        public float cameraSpeed;
        private Vector3 prevPlayerPos;
        private Vector3 posVector;

        public GameObject PlayerShip {
            get;
            set;
        }

        void Start()
        {

            scale = 8.0f;
            cameraSpeed = 0.5f;

            prevPlayerPos = transform.position;

        }


        void Update()
        {
            if (!PlayerShip)
            {
                return;
            }

            Vector3 currentPlayerPos = PlayerShip.transform.position;
            Vector3 backVector = (prevPlayerPos - currentPlayerPos).normalized;

            posVector = (backVector == Vector3.zero) ? posVector : backVector;

            Vector3 targetPos = currentPlayerPos + scale * posVector;

            targetPos.y = targetPos.y + 3.5f;

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                cameraSpeed * Time.deltaTime
                );

            transform.LookAt(PlayerShip.transform.position);
            prevPlayerPos = PlayerShip.transform.position;

        }

    }

}