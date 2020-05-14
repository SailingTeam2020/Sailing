using UnityEngine;

namespace Sailing
{

    public class ShipController : MonoBehaviour
    {

        private Vector3 RotateDirection {
            get {
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    return Vector3.up;
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    return Vector3.down;
                }

                return Vector3.zero;
            }
        }

        public void Rotate()
        {

            transform.Rotate(RotateDirection, Space.Self);

            Gyro();

            //Swipe();

        }

        #region Gyro

        public float GyroParam {
            get;
            private set;
        }

        private void Gyro()
        {

            GyroParam = Input.acceleration.x;

            if (GyroParam < -0.2f || 0.2f < GyroParam)
            {
                this.transform.Rotate(0.0f, GyroParam, 0.0f);
            }

        }

        #endregion

        #region Swipe

        private bool isFlick;
        private bool isClick;
        private Vector3 touchStartPos;
        private Vector3 touchEndPos;

        public void Swipe()
        {

            Vector3 mousePos = Input.mousePosition;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                isFlick = true;
                touchStartPos = new Vector3(mousePos.x, mousePos.y, mousePos.z);

                //0.2秒後にFlickOff処理
                Invoke("FlickOff", 0.2f);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                touchEndPos = new Vector3(mousePos.x, mousePos.y, mousePos.z);

                float directionX = (touchEndPos.x - touchStartPos.x) * 0.003f;
                if (directionX >= 1.0f)
                {
                    directionX = 1.0f;
                }
                if (directionX <= -1.0f)
                {
                    directionX = -1.0f;
                }

                transform.Rotate(0, directionX, 0);

                if (touchStartPos != touchEndPos)
                {
                    ClickOff();
                }
            }

        }

        private void FlickOff()
        {

            isFlick = false;

        }

        private bool IsFlick()
        {

            return isFlick;
        }

        private void ClickOn()
        {

            isClick = true;
            Invoke("ClickOff", 0.2f);

        }

        private bool IsClick()
        {

            return isClick;
        }

        private void ClickOff()
        {

            isClick = false;

        }

        #endregion

    }

}