using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing
{

    public class ChangeCameraAngle : MonoBehaviour
    {

        private GameObject Player;
        private Transform TargetPoint;
        private GameObject MainCamera;

        private ShipObject shipObject;
        private CourseManager courseManager;
        public NextMakerNavi nextMakerNavi
        {
            get;
            private set;
        }
        private float RotationSpeed;
        float AngleValue;

        // Start is called before the first frame update
        void Start()
        {
            shipObject = GameObject.Find("ShipManager").GetComponent<ShipManager>().MainShipObject;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            nextMakerNavi = GameObject.Find("NextLine").GetComponent<NextMakerNavi>();
            Player = GameObject.Find("Ship");
            MainCamera = GameObject.Find("GameViewCamera");

            RotationSpeed = 0;
            AngleValue = 0;

        }

        // Update is called once per frame
        void Update()
        {
            TargetPoint = nextMakerNavi.nextMakerLine;
            //TargetPoint = courseManager.MakerManager.MakerObjectList[shipObject.NextMakerNumber].gameObject.transform;
            TargetLook(Player.transform.position, TargetPoint.transform.position, MainCamera.transform.position);
        }

        /**
 * プレイヤーからカメラまでのベクトルと、プレイヤーからブイまでのベクトルから
 * 角度を計算する。
 * 計算した角度は-180～180の値で帰ってくる。
 * この角度が180°になる場所がカメラのいてほしい角度となる
 */
        void TargetLook(Vector3 FromPoint, Vector3 ToPoint_Point, Vector3 ToPoint_Camera)
        {

            Vector3 FromVector = ToPoint_Point - FromPoint;//0°のライン
            Vector3 ToVector = ToPoint_Camera - FromPoint;//目的の角度までのライン
            //Debug.Log("FromVector" + FromVector);
            //Debug.Log("ToVector" + ToVector);
            AngleValue = Vector3.SignedAngle(FromVector, ToVector, Vector3.up);
            //Debug.Log("角度数値" + AngleValue);
            float AngleVector = Vector3.Distance(FromPoint, ToPoint_Point);
            if (AngleValue < 0)
            {
                //RotationSpeed = (-158 - AngleValue) * aaa;
                RotationSpeed = -20;
                //Debug.Log("マイナス" + ((-158  - AngleValue) * aaa));
            }
            else
            {
                //RotationSpeed = (180 + AngleValue) * aaa;
                RotationSpeed = 20;
                //Debug.Log("プラス" + ((180 + AngleValue) * aaa));
            }
            if (AngleVector > 30)
            {
                MainCamera.transform.RotateAround(FromPoint, Vector3.up, RotationSpeed * Time.deltaTime);
            }
        }

    }
}
