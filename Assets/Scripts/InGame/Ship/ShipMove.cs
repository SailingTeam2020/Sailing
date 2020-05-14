using UnityEngine;

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

        private void Awake()
        {

            MoveSpeed = 0.0f;
            Acceleration = 20.0f;
            WindInfluence = 0.0f;

        }

        public void Move(float influence)
        {

            if (Acceleration < 0.1f) { 
                Acceleration = 0.0f;
            }

            WindInfluence = influence;
            MoveSpeed = Acceleration * WindInfluence;
            transform.Translate(gameObject.transform.forward * MoveSpeed * Time.deltaTime, Space.World);

        }

    }

}