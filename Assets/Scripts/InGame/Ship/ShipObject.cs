
// 2020/05/15 小林更新

using UnityEngine;
using Photon.Pun;
using Sailing.SingletonObject;

namespace Sailing
{

    public class ShipObject : MonoBehaviour
    {

        private PhotonView photonView;
        private CourseManager courseManager;
        private GameObject obj;
        public bool IsMove {
            get;
            private set;
        }

        public bool IsRotate {
            get;
            private set;
        }

        public bool PassEnterMaker {
            get;
            private set;
        }

        public int NextMakerNumber {
            get;
            private set;
        }

        public bool IsGoal {
            get;
            private set;
        }

        public ShipMove ShipMove {
            get;
            private set;
        }

        public ShipController ShipController {
            get;
            private set;
        }

        public GameTimer GameTimer {
            get;
            set;
        }

        private void Awake()
        {

            photonView = PhotonView.Get(this);
            
            IsMove = false;
            IsRotate = false;
            IsGoal = false;
            NextMakerNumber = 1;

            if (photonView.IsMine)
            {
                gameObject.tag = "Player";
                ShipMove = gameObject.AddComponent<ShipMove>();
                ShipController = gameObject.AddComponent<ShipController>();

                GameObject child = new GameObject();
                child.transform.parent = gameObject.transform;
                child.name = "Camera";
                child.AddComponent<Camera>();
                child.GetComponent<Camera>().cullingMask = ~(1 << 8);
                child.transform.position = gameObject.transform.position + new Vector3(0.0f, 4.0f, -10.0f);
                child.transform.LookAt(gameObject.transform);

                GameObject obj = (GameObject)Resources.Load("ConcentratObject");
                Instantiate(obj, this.transform.position, Quaternion.identity);
            }
            else
            {
                gameObject.tag = "Opponent";
            }

        }

        // Start is called before the first frame update
        private void Start()
        {

            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();

            /*var properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add(PlayerPropertyKey.PlayerObject, this.gameObject);
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);*/

        }

        private void FixedUpdate()
        {

            if (!photonView.IsMine)
            {
                return;
            }

            if (IsMove)
            {
                ShipMove.Move(courseManager.WindManager.GetInfluence(transform));
            }

            if (IsRotate)
            {
                ShipController.Rotate();
            }

        }

        private void OnTriggerEnter(Collider collision)
        {

            OnHitMaker(collision);

        }

        private void OnHitMaker(Collider collision)
        {

            GameObject hitMaker = collision.gameObject.transform.parent.gameObject;  // 当たったオブジェクトの親オブジェクトを参照
            if (!hitMaker)
            {
                return;
            }

            if (collision.tag == "Enter" && !PassEnterMaker)
            {

                if (!courseManager.MakerManager.PassMaker(NextMakerNumber, hitMaker))
                {
                    return;
                }

                PassEnterMaker = true;
                SoundManager.Instance.PlaySE("Makerstart");

                return;
            }
            
            if (collision.tag == "Out" && PassEnterMaker)
            {

                if (!courseManager.MakerManager.PassMaker(NextMakerNumber, hitMaker))
                {
                    return;
                }

                NextMakerNumber++;
                PassEnterMaker = false;
                SoundManager.Instance.PlaySE("Makerend");

                return;
            }
            
            if (collision.tag == "Finish")
            {

                if (!courseManager.MakerManager.PassMaker(NextMakerNumber, hitMaker))
                {
                    return;
                }

                NextMakerNumber++;
                IsRotate = false;
                IsGoal = true;
                Invoke("GoalShipMoveStop", 3.0f);
                SoundManager.Instance.PlaySE("Goal");
                GameTimer.TimerStop();

                Debug.Log("ゴール");

                return;
            }
            
        }

        public void ChangeShipControlFlag(bool isMove,bool isRotate)
        {

            IsMove = isMove;
            IsRotate = isRotate;

        }

        public void GoalShipMoveStop()
        {

            IsMove = false;

        }

    }

}