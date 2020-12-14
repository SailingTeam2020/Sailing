//2020/06/18　宮崎

using System.Collections;
using UnityEngine;
using System.Linq;
using Unity;

namespace Sailing
{
    public class CpushipMove : MonoBehaviour
    {
        public float angle;
        public float axis;
        public Transform[] sorted = new Transform[0];
        GameObject Goal;
        bool isSort = false;        
        private int i;
        public  float Cpu_Speed;
        private float Cpu_Rotate;
        GameObject obj;
        public bool Cpu_Move
        {
            get;
            private set;
        }
        public ShipObject CPUShipMove
        {
            get;
            private set;
        }

        public CPUShipObject CPUShipObject;
        void Start()
        {
            Goal = GameObject.FindGameObjectWithTag("GoalNavPoint");
            //Debug.Log("ゴール " + Goal);
            // InCheck = false;
            i = 0;
            Cpu_Speed = 0; //0.05f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
                              //Invoke("NextMarkerSet", 10.0f);
            Cpu_Move = false;//動けるかどうかのフラグ
            obj = GameObject.Find("Ship");//ShipObject内のIsCPUMoveを呼び出す
            CPUShipMove = obj.GetComponent<ShipObject>();
       }

        void Update()
        {
            //フラグがtrueになるまで呼び出す
            if (Cpu_Move == true)  Cpu_Speed = 0.1f;
            else  Cpu_Move = CPUShipMove.IsCPUMove;
            if (!isSort)
            {
                Transform[] nextMarker = GameObject.FindGameObjectsWithTag("NavPoint").Select(marker => marker.transform).ToArray();
                sorted = nextMarker.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToArray();
                isSort = true;

            }
            if (i == sorted.Length)
            {
                var diff = Goal.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定

                var axis = Vector3.Cross(transform.forward, diff);
                var angle = Vector3.Angle(transform.forward, diff);
                var ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整

                this.gameObject.transform.Translate(0, 0, Cpu_Speed);
                if (ship_direction.y < -2f)//船が右にいる
                {
                    this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                }
                else if (ship_direction.y > 2f)//船が左にいる
                {
                    this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                }
            }
            else
            {
                var diff = sorted[i].transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定

                var axis = Vector3.Cross(transform.forward, diff);
                var angle = Vector3.Angle(transform.forward, diff);
                var ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整

                this.gameObject.transform.Translate(0, 0, Cpu_Speed);
                if (ship_direction.y < -2f)//船が右にいる
                {
                    this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                }
                else if (ship_direction.y > 2f)//船が左にいる
                {
                    this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                }
            }
        }


        void OnTriggerEnter(Collider other)
        {
            if (i<sorted.Length)
            {
                i++;
            }          
        }
    }
}



