/*using UnityEngine;

namespace Sailing
{

    public class ShipMove : MonoBehaviour
    {

        public float MoveSpeed {
            get;
            private set;
        }

        public float Acceleration {
            get;
            private set;
        }

        public float WindInfluence {
            get;
            private set;
        }

        public float BeforeWindInfluence {
            get;
            private set;
        }

        public float WindDifference {
            get;
            private set;
        }

        public float FrameCnt{
            get;
            private set;
        }

        private void Awake()
        {

            MoveSpeed = 0.0f;
            Acceleration = 20.0f;
            WindInfluence = 0.0f;
            BeforeWindInfluence = 0.0f;
            WindDifference = 0.1f;
            FrameCnt = 2.0f;

        }

        public void Move(float influence)
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.1f) { 
                Acceleration = 0.0f;
            }

            WindInfluence = influence;
            //WindDifference = WindInfluence - BeforeWindInfluence;
            if(FrameCnt <= 0.0f)
            {
                BeforeWindInfluence = WindInfluence;
                WindDifference = WindInfluence - BeforeWindInfluence;
                FrameCnt = 2.0f;
            }
            if (WindDifference == 0)
            {
                MoveSpeed = Acceleration * ( WindInfluence - WindDifference);
                FrameCnt = 0.0f;
            }
            else
            {
                WindDifference = WindDifference - (WindDifference - 0.05f);
                MoveSpeed = Acceleration * (WindInfluence - WindDifference);
            }
            
            transform.Translate(gameObject.transform.forward * MoveSpeed * Time.deltaTime, Space.World);

        }

    }

}*/

// 2020/05/15 小林更新

using UnityEngine;

namespace Sailing
{

    public class ShipMove : MonoBehaviour
    {

        public float MoveSpeed
        {
            get;
            private set;
        }

        public float Acceleration
        {
            get;
            private set;
        }

        public float WindInfluence
        {
            get;
            private set;
        }

        public float BeforeMoveSpeed
        {
            get;
            private set;
        }

        public float FrameCnt
        {
            get;
            private set;
        }

        public float MoveSpeedDifference
        {
            get;
            private set;
        }


        private void Awake()
        {

            MoveSpeed = 0.0f;
            Acceleration = 20.0f;
            WindInfluence = 0.0f;
            BeforeMoveSpeed = 0.0f;
            FrameCnt = 0.0f;
            MoveSpeedDifference = 0.0f;

        }

        public void Move(float influence)
        {

            FrameCnt -= Time.deltaTime;

            if (Acceleration < 0.1f)
            {
                Acceleration = 0.0f;
            }

            if (MoveSpeedDifference <= 0)
            {
                BeforeMoveSpeed = MoveSpeed;
            }
            if(FrameCnt <= 0.0f)
            {
                MoveSpeedDifference = BeforeMoveSpeed - MoveSpeed;
                FrameCnt = 2.0f;
            }

            WindInfluence = influence;
            if (MoveSpeedDifference <= 0)
            {
                MoveSpeed = Acceleration * WindInfluence;
            }
            else
            {
                MoveSpeedDifference = MoveSpeedDifference - (MoveSpeedDifference - 0.1f);
                MoveSpeed = MoveSpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * MoveSpeed * Time.deltaTime, Space.World);

        }

    }

}