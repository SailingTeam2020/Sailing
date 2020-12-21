//2020/12/21　宮崎
using UnityEngine;
using System.Collections.Generic;

namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private int NextMarkerNumber;//マーカーの番号
        public bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private int i;
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い
        private bool direction;//向きフラグ
        float dist;//距離用
        private Vector3 ship_direction;//
        public enum ShipState//船の状態
        {
            senkai,//旋回
            tilyokusen,//ジグザグ
            straight,//直進
            dodge//避ける
        }
        public ShipState shipstate;
        private bool Cpu_Move;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;
        public static float Angle;


        void Start()
        {
            shipstate = ShipState.senkai;
            isGoal = false;
            isInsert = false;
            direction = false;
            Progress = false;
            NextMarkerNumber = 1;

            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.2f, 1.0f));

            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {
            this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true && isGoal == false)
            {
                Move(courseManager.WindManager.GetInfluence(transform));//風を適用

                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }

                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var angle = Vector3.Angle(transform.forward, diff);
                    ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    dist = Vector3.Distance(NextPoint.transform.position, this.gameObject.transform.position);//距離取得

                    switch (shipstate)//船の動作状態
                    {
                        case ShipState.senkai://目的地に向かせる
                            if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                            }
                            else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                            }
                            else//旋回完了
                            {
                                shipstate = ShipState.tilyokusen;
                            }
                            break;

                        case ShipState.tilyokusen://ジグザグ
                            if (direction == false)
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                                if (ship_direction.y < -3500f)
                                {
                                    direction = true;
                                }

                            }
                            if (direction == true)
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                                if (ship_direction.y > 3500f)
                                {
                                    direction = false;
                                }
                            }
                            if (Cpu_Speed >= 0.2f)//速くなったらジグザグをやめる
                            {
                                if (ship_direction.y <= 300f && ship_direction.y >= -300f)
                                {
                                    shipstate = ShipState.straight;
                                }
                            }
                            if (dist <= 30)
                            {
                                shipstate = ShipState.senkai;
                            }
                            break;
                        case ShipState.straight://直進
                            if (ship_direction.y > 300f || ship_direction.y < -300f)
                            {
                                shipstate = ShipState.senkai;
                            }
                            if (dist <= 30)
                            {
                                shipstate = ShipState.senkai;
                            }
                            break;
                        case ShipState.dodge://船が
                            Cpu_Speed = 0.02f;
                            break;
                    };
                }
                else if (isGoal == true) { Cpu_Move = false; }
            }
            if (NextPoint != null)
            {

            }
        }

        private void Awake()
        {
            Cpu_Speed = 0.0f;
            WindInfluence = 0.0f;
            FrameCnt = 0.0f;
            Acceleration = 0.25f;
            BeforeCpu_Speed = 0.0f;
            SpeedDifference = 0.0f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "FinishLine" || Progress == false && NextMarkerNumber == courseManager.MakerManager.MakerObjectList.Count)//フィニッシュラインに当たった&&最後に当たったのがアウトライン&&現在のマーカーナンバーがリストの最大
            {
                isGoal = true;
            }
        }
        public void CollisionDetected(CpuShipSensor cpushipsensor)//船が前にいる
        {
            Mathf.Lerp(Cpu_Speed, 0, Time.deltaTime);
            shipstate = ShipState.dodge;

        }
        public void CollisionExitDetected(CpuShipSensor cpushipsensor)//船が前にいない
        {
            shipstate = ShipState.senkai;
        }

        public void Move(float influence)//風システム
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.01f)
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
                FrameCnt = 2f;
            }
            WindInfluence = influence;
            if (SpeedDifference <= 0)
            {
                Cpu_Speed = Acceleration * WindInfluence;
            }
            else
            {
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.01f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;
        }
    }
}


/*/
//2020/12/14　宮崎
using UnityEngine;
using System.Collections.Generic;

namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private int NextMarkerNumber;//マーカーの番号
        private bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private int i;
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い
        private bool direction;//向きフラグ
        float dist;//距離用
        private Vector3 ship_direction;//
        public enum ShipState//船の状態
        {
            senkai,//旋回
            tilyokusen,//ジグザグ
            straight//直進
        }
        public ShipState shipstate;
        private bool Cpu_Move;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;
        public static float Angle;


        void Start()
        {
            shipstate = ShipState.senkai;
            isGoal = false;
            isInsert = false;
            direction = false;
            Progress = false;
            NextMarkerNumber = 1;

            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.2f, 1.0f));

            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {
            this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true && isGoal == false)
            {
                Move(courseManager.WindManager.GetInfluence(transform));//風を適用

                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }

                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var angle = Vector3.Angle(transform.forward, diff);
                    ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    dist = Vector3.Distance(NextPoint.transform.position, this.gameObject.transform.position);//距離取得

                    switch (shipstate)//船の動作状態
                    {
                        case ShipState.senkai://目的地に向かせる
                            if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                            }
                            else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                            }
                            else//旋回完了
                            {
                                shipstate = ShipState.tilyokusen;
                            }
                            break;

                        case ShipState.tilyokusen://ジグザグ
                            if (direction == false)
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                                if (ship_direction.y < -3500f)
                                {
                                    direction = true;
                                }
                            }
                            if (direction == true)
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                                if (ship_direction.y > 3500f)
                                {
                                    direction = false;
                                }
                            }
                            if (Cpu_Speed >= 0.2f)//速くなったらジグザグをやめる
                            {
                                if (ship_direction.y <= 300f && ship_direction.y >= -300f)
                                {
                                    shipstate = ShipState.straight;
                                }
                            }
                            if (dist <= 30)
                            {
                                shipstate = ShipState.senkai;
                            }
                            break;
                        case ShipState.straight://直進
                            if (ship_direction.y > 300f || ship_direction.y < -300f)
                            {
                                shipstate = ShipState.senkai;
                            }
                            if (dist <= 30)
                            {
                                shipstate = ShipState.senkai;
                            }
                            break;
                    };
                }
                else if (isGoal == true) { Cpu_Move = false; }
            }
            if (NextPoint != null)
            {

            }
        }

        private void Awake()
        {
            Cpu_Speed = 0.0f;
            WindInfluence = 0.0f;
            FrameCnt = 0.0f;
            Acceleration = 0.25f;
            BeforeCpu_Speed = 0.0f;
            SpeedDifference = 0.0f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "FinishLine" && Progress == false && NextMarkerNumber == courseManager.MakerManager.MakerObjectList.Count)//フィニッシュラインに当たった&&最後に当たったのがアウトライン&&現在のマーカーナンバーがリストの最大
            {
                isGoal = true;
            }
        }

        public void Move(float influence)//風システム
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.01f)
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
                FrameCnt = 2f;
            }
            WindInfluence = influence;
            if (SpeedDifference <= 0)
            {
                Cpu_Speed = Acceleration * WindInfluence;
            }
            else
            {
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.01f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;

        }
    }
}/**/
/*//2020/12/07　宮崎

using UnityEngine;
using System.Collections.Generic;

namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private int NextMarkerNumber;//マーカーの番号
        private bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private int i;
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い
        private bool direction;//向きフラグ
        float dist;
        private Vector3 ship_direction;
        public enum ShipState
        {
            senkai,
            tilyokusen
        }
        public ShipState shipstate;
        private bool Cpu_Move;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;
        public static float Angle;


        void Start()
        {
            direction = false;
            shipstate = ShipState.senkai;
            isGoal = false;
            isInsert = false;
            NextMarkerNumber = 1;
            Progress = false;

            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.2f, 1.0f));

            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {
            Debug.Log(ship_direction.y);
            this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true && isGoal == false)
            {
                Move(courseManager.WindManager.GetInfluence(transform));//風を適用

                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }

                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var angle = Vector3.Angle(transform.forward, diff);
                    ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    dist = Vector3.Distance(NextPoint.transform.position, this.gameObject.transform.position);//距離取得

                    switch (shipstate)//船の動作状態
                    {
                        case ShipState.senkai://目的地に向かせる
                            if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                            }
                            else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                            }
                            else//旋回完了
                            {
                                shipstate = ShipState.tilyokusen;
                            }
                            break;

                        case ShipState.tilyokusen://ジグザグ
                            if (direction == false)
                            {
                                this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                                if (ship_direction.y < -1500f)
                                {
                                    direction = true;
                                }
                            }
                            if (direction == true)
                            {
                                this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                                if (ship_direction.y > 1500f)
                                {
                                    direction = false;
                                }
                            }
                            if (dist <= 25)//ブイの直前でジグザグをやめる
                            {
                                shipstate = ShipState.senkai;
                            }
                            break;
                    };
                }
                else if (isGoal == true) { Cpu_Move = false; }
            }
            if (NextPoint != null)
            {

            }
        }

        private void Awake()
        {
            Cpu_Speed = 0.0f;
            WindInfluence = 0.0f;
            FrameCnt = 0.0f;
            Acceleration = 0.25f;
            BeforeCpu_Speed = 0.0f;
            SpeedDifference = 0.0f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
                shipstate = ShipState.senkai;
            }
            if (other.gameObject.name == "FinishLine" && Progress == false && NextMarkerNumber == courseManager.MakerManager.MakerObjectList.Count)//フィニッシュラインに当たった&&最後に当たったのがアウトライン&&現在のマーカーナンバーがリストの最大
            {
                isGoal = true;
            }
        }

        public void Move(float influence)//風システム
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.01f)
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
                FrameCnt = 2f;
            }
            WindInfluence = influence;
            if (SpeedDifference <= 0)
            {
                Cpu_Speed = Acceleration * WindInfluence;
            }
            else
            {
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.01f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;

        }
    }
}*/
/*//2020/11/16　宮崎

using UnityEngine;
namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private Transform HalfPoint;//中間地点
        private int NextMarkerNumber;//マーカーの番号
        public bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private int i;
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い

        private bool Cpu_Move;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;

        void Start()
        {
            isGoal = false;
            isInsert = false;
            NextMarkerNumber = 1;
            Progress = false;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Speed = 0.05f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.2f, 1.0f));

            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {
            this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true && isGoal == false)
            {
                Move(courseManager.WindManager.GetInfluence(transform));//風
                                                                        //{//スタート
                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }


                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var diffharf = diff / 2;
                    var angle = Vector3.Angle(transform.forward, diff);
                    var ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    //this.gameObject.transform.Translate(0, 0, Cpu_Speed);

                    if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                    {
                        this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                    }
                    else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                    {
                        this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                    }
                    if (Cpu_Speed < 0.3f)
                    {

                        this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);

                    }

                }
                else if (isGoal == true) { Cpu_Move = false; }
            }
        }

        private void Awake()
        {
            Cpu_Speed = 0.0f;
            WindInfluence = 0.0f;
            FrameCnt = 0.0f;
            Acceleration = 0.12f;
            BeforeCpu_Speed = 0.0f;
            SpeedDifference = 0.0f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
            }
            if (other.gameObject.name == "FinishLine" && Progress == false && NextMarkerNumber == courseManager.MakerManager.MakerObjectList.Count)//フィニッシュラインに当たった&&最後に当たったのがアウトライン&&現在のマーカーナンバーがリストの最大
            {
                isGoal = true;
            }
        }

        public void Move(float influence)//風システム
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.01f)
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
                FrameCnt = 2f;
            }
            WindInfluence = influence;
            if (SpeedDifference <= 0)
            {
                Cpu_Speed = Acceleration * WindInfluence;
            }
            else
            {
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.01f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;

        }
    }
}*/
/*// 2020 / 11 / 2 宮崎

using UnityEngine;


namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private int NextMarkerNumber;//マーカーの番号
        private bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い

        private bool Cpu_Move;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;

        void Start()
        {
            isGoal = false;
            isInsert = false;
            NextMarkerNumber = 1;
            Progress = false;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Speed = 0.05f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.6f, 0.8f));

            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {
            this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true && isGoal == false)
            {
                Move(courseManager.WindManager.GetInfluence(transform));//風
                                                                        //{//スタート
                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }


                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var diffharf = diff / 2;
                    var angle = Vector3.Angle(transform.forward, diff);
                    var ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    this.gameObject.transform.Translate(0, 0, Cpu_Speed);

                    if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                    {
                        this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                    }
                    else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                    {
                        this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                    }
                }
            }
        }

        private void Awake()
        {
            Cpu_Speed = 0.0f;
            WindInfluence = 0.0f;
            FrameCnt = 0.0f;
            Acceleration = 0.07f;
            BeforeCpu_Speed = 0.0f;
            SpeedDifference = 0.0f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
            }
            if (other.gameObject.name == "FinishLine" && Progress == false && NextMarkerNumber == courseManager.MakerManager.MakerObjectList.Count)//フィニッシュラインに当たった&&最後に当たったのがアウトライン&&現在のマーカーナンバーがリストの最大
            {
                isGoal = true;
            }
        }

        public void Move(float influence)//風システム
        {
            FrameCnt -= Time.deltaTime;
            if (Acceleration < 0.01f)
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
                SpeedDifference = SpeedDifference - (SpeedDifference - 0.01f);
                Cpu_Speed = SpeedDifference;
            }
            transform.Translate(gameObject.transform.forward * Cpu_Speed * Time.deltaTime, Space.World);
        }

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;
        }
    }
}

*/
/*
namespace Sailing
{

    public class CpushipMove : MonoBehaviour
    {
        private CourseManager courseManager;
        private Transform NextPoint;//次のマーカーの座標
        private int NextMarkerNumber;//マーカーの番号
        private bool isGoal;//ゴールしたかどうかを判定
        private bool isInsert;//最初のマーカーを設定したかを判定  
        private string CheckPoint;//どこを通ったか
        private bool Progress;
        //船の動き
        public float Cpu_Speed;//CPUのスピード
        public float Cpu_Rotate;//CPUの回転
        //風
        private float Acceleration;//加速
        private float WindInfluence;//風の影響
        private float BeforeCpu_Speed;//以前のCPUスピード
        private float FrameCnt;//フレーム数
        private float SpeedDifference;//速度の違い

        private bool Cpu_Move ;//動けるかどうかのフラグ
        public ShipObject CPUShipMove;

        void Start()
        {   
            isGoal = false;
            isInsert = false;
            NextMarkerNumber = 1;
            Progress = false;
            courseManager = GameObject.Find("CourseManager").GetComponent<CourseManager>();
            Cpu_Speed = 0f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Speed = 0.05f; //テスト的に設定　プレイヤーに合わせて変える必要あり
            //Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
            Cpu_Rotate = (Random.Range(0.1f, 1.5f));
            Acceleration = 0.11f;
            WindInfluence = 0.0f;
            BeforeCpu_Speed = 0.0f;
            FrameCnt = 0.0f;
            SpeedDifference = 0.0f;
            Cpu_Move = false;
            CPUShipMove = GameObject.Find("Ship").GetComponent<ShipObject>();
        }

        void Update()
        {

            //フラグがtrueになるまで呼び出す
            Cpu_Move = CPUShipMove.IsCPUMove;
            if (Cpu_Move == true) Cpu_Speed = 0.1f;

            if (Time.time > 13.5f)
            {//スタート
                if (courseManager.MakerManager.MakerObjectList.Count == NextMarkerNumber && CheckPoint == "Out")//最後のマーカーに、かつアウトラインに当たったら
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "FinishLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                }
                if (!isInsert)
                {
                    NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                    NextPoint = FindChild(NextPoint, "NavPoint");
                    isInsert = true;
                }
                this.gameObject.transform.Translate(0, 0, Cpu_Speed);//前進
                Move(courseManager.WindManager.GetInfluence(transform));//風
                if (!isGoal)
                {
                    //目標に向かう
                    var diff = NextPoint.transform.position - this.gameObject.transform.position; //CPUとブイ距離を判定
                    var axis = Vector3.Cross(transform.forward, diff);
                    var angle = Vector3.Angle(transform.forward, diff);
                    var ship_direction = angle * axis; //ブイに対してCPUがどの角度にいるのかを、左右判定できるように180から-180に調整
                    this.gameObject.transform.Translate(0, 0, Cpu_Speed);

                    if (ship_direction.y < -1f && ship_direction.y < 0)//船が右にいる
                    {
                        this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
                    }
                    else if (ship_direction.y > 1f && ship_direction.y > 0)//船が左にいる
                    {
                        this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
                    }
                }

            }

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "EnterLine" && Progress == false)//エンターラインに当たった
            {

                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "OutLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Out";//アウトに設定しておく
                Progress = true;
            }
            if (other.gameObject.name == "OutLine" && Progress == true)//アウトラインに当たった
            {
                NextMarkerNumber++;//ブイの出口に当たったので、次のブイの番号にする。
                NextPoint = FindChild(courseManager.MakerManager.MakerObjectList[NextMarkerNumber].gameObject.transform, "EnterLine");
                NextPoint = FindChild(NextPoint, "NavPoint");
                CheckPoint = "Enter";//エンターに設定する
                Progress = false;
            }
            if (other.gameObject.name == "FinishLine")//フィニッシュラインに当たった
            {
                isGoal = true;
            }
        }

        public void Move(float influence)//風システム
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

        private Transform FindChild(Transform transform, string str)//子オブジェクトを探す
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == str)
                {
                    return transform.GetChild(i);
                }
            }
            return null;
        }

        /*public void MovePattern(int rank,int markernum)//順位とマーカー番号によってCPUの動きを変える
        {
 
         方法
 
         2,3,4,5,6,7,8
 
         順位１　マジ遅い
　　　　　　順位２　　　遅い
         順位３　　　普通
         順位４　    早い   コース 4    遅い
 
         コース 1　　遅い
         コース 2　　遅い
         コース 3    遅い
         コース 4    遅い
 
         難易度を貰ってそれに対応した回転速度にする
 
         switch (rank) {
             case 1:
                 switch (markernum)
                 {
                     case 1:
                         //速い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //遅い
                         break;
                     case 4:
                         //マジ遅い
                         break;
                 }
                 break;
             case 2:
                 switch (markernum)
                 {
                     case 1:
                         //速い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //遅い
                         break;
                     case 4:
                         //遅い
                         break;
                 }
                 break;
             case 3:
                 switch (markernum)
                 {
                     case 1:
                         //普通
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //速い
                         break;
                 }
                 break;
             case 4:
                 switch (markernum)
                 {
                     case 1:
                         //遅い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //マジ速い
                         break;
                 }
                 break;
             case 5:
                 switch (markernum)
                 {
                     case 1:
                         //遅い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //マジ速い
                         break;
                 }
                 break;
             case 6:
                 switch (markernum)
                 {
                     case 1:
                         //遅い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //マジ速い
                         break;
                 }
             case 7:
                 switch (markernum)
                 {
                     case 1:
                         //遅い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //マジ速い
                         break;
                 }
                 break;
             case 8:
                 switch (markernum)
                 {
                     case 1:
                         //遅い
                         break;
                     case 2:
                         //速い
                         break;
                     case 3:
                         //速い
                         break;
                     case 4:
                         //マジ速い
                         break;
                 }
                 break;
         }
    }
    }
}*/