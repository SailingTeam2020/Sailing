//2020/06/18　宮崎

using System.Collections;
using UnityEngine;
using System.Linq;
using Unity;

namespace Sailing
{
    public class CpushipMove : MonoBehaviour
    {
        //public MakerManager maker;

        public float angle;
        public float axis;
        public Transform[] sorted = new Transform[0];
        Vector3 Goal;
        //GameObject Goal;
        bool isSort = false;        
        private int i;
        public float Cpu_Speed;
        private float Cpu_Rotate;
        void Start()
        {
            //仮
            //maker = gameObject.GetComponent<MakerManager>();


            Goal = GameObject.FindGameObjectWithTag("GoalNavPoint").transform.position;
           // InCheck = false;
            i = 0;
            Cpu_Speed = 0.05f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
                              //Invoke("NextMarkerSet", 10.0f);
        }

        void Update()
        {
            if (!isSort)
            {
                Transform[] nextMarker = GameObject.FindGameObjectsWithTag("NavPoint").Select(marker => marker.transform).ToArray();
                sorted = nextMarker.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToArray();
                isSort = true;

            }
            if (i == sorted.Length)
            {

                Debug.Log("ゴールポジ");
                Debug.Log(Goal);
                Debug.Log("CPUポジ");
                Debug.Log(this.gameObject.transform.position);
                var diff = Goal- this.gameObject.transform.position; //CPUとブイ距離を判定
                //var diff = Goal.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定

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



