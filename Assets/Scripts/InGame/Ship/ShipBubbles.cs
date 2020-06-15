using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Online
{

    public class ShipBubbles : MonoBehaviour
    {
        ParticleSystem.EmissionModule EmObj1;
        ParticleSystem.EmissionModule EmObj2;
        ParticleSystem.EmissionModule EmObj3;

        private ShipMove shipMove;
        private float speed;

        void Start()
        {
            //"Particle1"オブジェクトから ParticleSystemコンポーネントを取得 
            ParticleSystem ParticleObj1 = transform.Find("Particle1").GetComponent<ParticleSystem>();
            //↓最終目的である rateOverTime にアクセスするために必要な emission を取得し格納
            EmObj1 = ParticleObj1.emission;
            ParticleSystem ParticleObj2 = transform.Find("Particle2").GetComponent<ParticleSystem>();
            EmObj2 = ParticleObj2.emission;
            ParticleSystem ParticleObj3 = transform.Find("Particle3").GetComponent<ParticleSystem>();
            EmObj3 = ParticleObj3.emission;

            shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();
            speed = 0;
        }



        private float mCount = 0;       //←時間計測用
        void Update()
        {

            speed = shipMove.MoveSpeed;
            mCount = mCount + Time.deltaTime;   //←時間計測中
            if (mCount >= 1.0f)
            { //経過する度に if 成立
                mCount = 0; // 時間計測用変数を初期化
                            //スピードに対してN個放出する
                    EmObj1.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
                    EmObj2.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
                    EmObj3.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
            }
        }
    }
}