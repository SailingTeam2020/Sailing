using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 2点(プレイヤーの座標と目標地点の座標)から角度を求め、
 その角度に180°追加した角度にカメラを移動させる。
 目標地点-プレイヤー-カメラ　という一直線になる
 またカメラも回転させ目標地点を映す。
 疑似的にプレイヤーと目標地点をカメラに映すことができる
 */
namespace Sailing
{

    public class ChangeCameraAngle : MonoBehaviour
    {

        private GameObject Player;
        private Transform TargetPoint;
        private GameObject MainCamera;

        private ShipObject shipObject;
        private CourseManager courseManager;
        private float RotationSpeed;
        private bool FirstRotation;
        // Start is called before the first frame update
        void Start()
        {
            shipObject = GameObject.Find("ShipManager").GetComponent<ShipManager>().MainShipObject;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();

            Player = GameObject.Find("Ship");
            MainCamera = GameObject.Find("GameViewCamera");

            RotationSpeed = 0;
            FirstRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            TargetPoint = courseManager.MakerManager.MakerObjectList[shipObject.NextMakerNumber].gameObject.transform;

            float TargetAngle = GetAngle(Player.transform.position, TargetPoint.transform.position);
            //Debug.Log("角度" + TargetAngle);
            float CameraAngle = GetInversionAngle(TargetAngle);
            //Debug.Log("反転角度" + CameraAngle);

                CameraTransform(Player.transform.position, MainCamera.transform.position, CameraAngle);
        }
        //2点の座標から角度を計算する
        //プレイヤーと目標地点の角度計算
        private float GetAngle(Vector3 start, Vector3 target)
        {
            Vector3 dt = target - start;
            float rad = Mathf.Atan2(dt.z, dt.x);
            float degree = rad * Mathf.Rad2Deg;
            return degree;
        }
        //カメラの目標角度計算
        private float GetInversionAngle(float cameraAngle)
        {
            float CameraAngle = 0;
            if (cameraAngle > 0)
            {
                CameraAngle = cameraAngle - 180;
            }
            else 
            {
                CameraAngle = cameraAngle + 180;
            }
            return CameraAngle;
        }


        //カメラの位置変更・プレイヤーを中心にして回転
        private void CameraTransform(Vector3 Player, Vector3 Camera, float Point)
        {//float Pointに格納された数字がカメラのいてほしい角度

            //プレイヤーからみた現在のカメラの角度を求める
            Vector3 dt = Camera - Player;
            float rad = Mathf.Atan2(dt.z, dt.x);
            float degree = rad * Mathf.Rad2Deg;
            //Debug.Log("現在のカメラの角度 "+degree);
            if (FirstRotation == true)
            {
                if (degree > Point +1)
                {
                    RotationSpeed = 1f;
                }
                else if (degree < Point -1)
                {
                    RotationSpeed = -1f;
                }
                else
                {
                    RotationSpeed = 0f;
                    FirstRotation = false;
                }

            }
            else
            {
                if (degree > Point + 20)
                {
                    //Debug.Log("角度"+ (Point - 5) + " 条件"+ Point+"　1");
                    RotationSpeed = 1f;
                }
                else if (degree < Point - 20)
                {
                    //Debug.Log("角度" + (Point + 5) + " 条件" + Point +"　₋1");
                    RotationSpeed = -1f;
                }
                else
                {
                    RotationSpeed = 0f;
                    //Debug.Log("静止");
                }
                //Debug.Log( " カメラの位置" + Point + "回転" + RotationSpeed);
            }
            Vector3 angle = new Vector3(RotationSpeed * 2, 0, 0);

            //transform.RotateAround()をしようしてメインカメラを回転させる
            MainCamera.transform.RotateAround(Player, Vector3.up, angle.x);
        }
    }

}
