//2020/06/22宮崎
using System.Collections;
using UnityEngine;
using System.Linq;
using Unity;

namespace Sailing
{
    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        public Transform[] sorted = new Transform[0];
        GameObject Goal;
        bool isSort = false;        
        private int i;
        public float Cpu_Speed;
        public float Cpu_Rotate;
        public Transform Cpuship;
        

        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い
        private bool isMove;
        void Start()
        {
            isMove = false;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Goal = GameObject.FindGameObjectWithTag("GoalNavPoint");
            i = 0;
            Cpu_Speed = 0f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = 0.4f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Acceleration = 0.12f;
            WindInfluence = 0.0f;
            BeforeCpu_Speed = 0.0f;
            FrameCnt = 0.0f;
            SpeedDifference = 0.0f;           
        }
        void Update()
        {
            Invoke("isMovecheck", 14);//テスト的にプレイヤーとスタート時間を合わせている①
            if (isMove)//①
            {
                Move(courseManager.WindManager.GetInfluence(transform));
                if (!isSort)
                {
                    Transform[] nextMarker = GameObject.FindGameObjectsWithTag("NavPoint").Select(marker => marker.transform).ToArray();
                    sorted = nextMarker.OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).ToArray();
                    isSort = true;
                }
                if (i == sorted.Length)//
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
            }

        public void Move(float influence)//風によってスピードを変える
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.1f)
            {
                Acceleration = 0.0f;
            }
            if (SpeedDifference <= 0)
            {
                BeforeCpu_Speed = Cpu_Speed;
            }
            if (FrameCnt <= 0.0f)
            {
                SpeedDifference = BeforeCpu_Speed - Cpu_Speed;
                FrameCnt = 2.0f;
            }
            WindInfluence = influence;
            if (SpeedDifference <= 0)
            {
                Cpu_Speed = Acceleration * WindInfluence;
            }
            else
            {
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.1f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        public void isMovecheck()//①
        {
            isMove = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" || other.gameObject.name == "OutLine") {//エンターラインかアウトラインに当たった場合
                if (i < sorted.Length)
                {
                    i++;
                }
            }
        }
    }
}



