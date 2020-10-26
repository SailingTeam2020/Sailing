//2020/10/19　宮崎

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
    }*/
    }
}