using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Online
{

    public class ShipAfterimage : MonoBehaviour
    {
        //Time変更用の変数
        private float AfterimageObj_Time;

        //各本体のTrailRenderer
        private TrailRenderer AfterimageObj1;
        private TrailRenderer AfterimageObj2;
        private TrailRenderer AfterimageObj3;
        private TrailRenderer AfterimageObj4;

        private ShipMove Ship;
        //
        bool Afterimage = false;
        private float speed;

        // Start is called before the first frame update
        void Start()
        {
            //子オブジェクトのTrailRendererを取得
            AfterimageObj1 = transform.Find("Afterimage1").GetComponent<TrailRenderer>();
            AfterimageObj2 = transform.Find("Afterimage2").GetComponent<TrailRenderer>();
            AfterimageObj3 = transform.Find("Afterimage3").GetComponent<TrailRenderer>();
            AfterimageObj4 = transform.Find("Afterimage4").GetComponent<TrailRenderer>();

            Ship = GameObject.Find("Ship").GetComponent<ShipMove>();
        }

        // Update is called once per frame
        void Update()
        {
            speed = Ship.MoveSpeed;
            //船のスピードX or うまくいっているとき　分岐
            if (speed >= 18) AfterimageObj_Time = 2;
            else
            {
                AfterimageObj_Time -= 0.01f;
                if(AfterimageObj_Time <= 0)
               AfterimageObj_Time = 0;
            }
            //残像なし
            //残像あり→変更
            AfterimageObj1.time = AfterimageObj_Time;
            AfterimageObj2.time = AfterimageObj_Time;
            AfterimageObj3.time = AfterimageObj_Time;
            AfterimageObj4.time = AfterimageObj_Time;

        }
    }
}
