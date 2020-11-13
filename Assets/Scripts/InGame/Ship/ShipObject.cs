
// 2020/05/15 小林更新

using UnityEngine;
using Photon.Pun;
using Sailing.SingletonObject;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace Sailing
{

    public class ShipObject : MonoBehaviour
    {

        private PhotonView photonView;
        private CourseManager courseManager;
        private GameObject Concentratobj;
        private GameObject Splashesobj;
        private GameObject Concentrat;
        private GameObject Splashes;
        private GameObject Afterimage;
        private GameObject Afterimageobj;
        public GameObject Player;
        public bool IsMove {
            get;
            private set;
        }
        public bool IsCPUMove
        {
            get;
            private set;
        }

        public bool IsRotate {
            get;
            private set;
        }

        public bool IsHint
        {
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

        public float HintEnableTime
        {
            get;
            private set;
        }

        private void Awake()
        {
            //this.gameObject.AddComponent<BoatAlignNormal>();
            photonView = PhotonView.Get(this);

            IsHint = true;
            IsMove = false;
            IsCPUMove = false;
            IsRotate = false;
            IsGoal = false;
            PassEnterMaker = false;
            NextMakerNumber = 1;
            HintEnableTime = 10.0f;

            if (photonView.IsMine)
            {
                gameObject.tag = "Player";
                ShipMove = gameObject.AddComponent<ShipMove>();
                ShipController = gameObject.AddComponent<ShipController>();

                GameObject child = new GameObject();
                child.transform.parent = gameObject.transform;
                child.name = "GameViewCamera";
                child.AddComponent<Camera>();
                child.GetComponent<Camera>().cullingMask = ~(1 << 8);
                child.transform.position = gameObject.transform.position + new Vector3(0.0f, 4.0f, -10.0f);
                child.transform.LookAt(gameObject.transform);

                child.AddComponent<PostProcessLayer>();//shipにPostProcessLayerを追加する
                child.GetComponent<PostProcessLayer>().volumeLayer = 1<<9;//9番レイヤーのみ指定
                child.GetComponent<PostProcessLayer>().volumeTrigger = child.gameObject.transform;//TargetをGameViewCameraに指定
                
                //child = (GameObject)Resources.Load("ChangeCameraAngle");//カメラ角度移動

                Concentrat = (GameObject)Resources.Load("ConcentratObject");  //集中線
                Splashes = (GameObject)Resources.Load("SplashesEffect");      //水しぶき
                Afterimage = (GameObject)Resources.Load("ShipAfterimage");//残像
                Player = GameObject.Find("Ship");
                Debug.Log("オブジェクト　", Afterimage);
                Concentratobj = Instantiate(Concentrat, this.transform.position, Quaternion.identity);
                Splashesobj = Instantiate(Splashes, this.transform.position, Quaternion.identity);
                Afterimageobj = Instantiate(Afterimage, new Vector3(3.0f, 0.0f, 0.0f), Quaternion.identity);

                Concentratobj.transform.SetParent(child.transform, true);
                Splashesobj.transform.SetParent(Player.transform, true);
                Afterimageobj.transform.SetParent(Player.transform, true);

                Splashesobj.transform.rotation = Quaternion.Euler(0, 180, 0);
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

            HintEnableTime -= Time.deltaTime;

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

            if(HintEnableTime <= 0.0f && !IsGoal)
            {
                IsHint = true;
                HintEnableTime = 10.0f;
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
                IsHint = false;
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
                IsHint = false;
                NextMakerNumber++;
                PassEnterMaker = false;
                SoundManager.Instance.PlaySE("Makerend");

                return;

            }
            
            if (collision.tag == "Finish")
            {

                IsHint = false;
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

        public void ChangeShipControlFlag(bool isMove,bool isRotate,bool isCPUMove)
        {

            IsMove = isMove;
            IsRotate = isRotate;
            IsCPUMove = isCPUMove;
        }

        public void GoalShipMoveStop()
        {

            IsMove = false;

        }

        public bool HintEnable(bool HintFlg)
        {
            HintFlg = IsHint;

            return HintFlg;
        }

    }

}