using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Sailing.Online
{
    public class ShipBubbles : MonoBehaviour
    {
        ParticleSystem.MinMaxCurve EmObj1;
        ParticleSystem.MinMaxCurve EmObj2;
        ParticleSystem.MinMaxCurve EmObj3;
        ParticleSystem.MinMaxCurve EmObj4;

        private ShipMove shipMove;
        private CpushipMove CpushipMove;
        private float speed;
        GameObject Ship;
        GameObject CPUship;
        bool flg;
        GameObject rootGameObject;
        // string player;
        void Start()
        {

            rootGameObject = transform.root.gameObject;

            if (rootGameObject.name == "CPUShip")
            {
                CpushipMove = GameObject.Find("CPUShip").GetComponent<CpushipMove>();
                CPUship = GameObject.Find("CPUShip");
                flg = false;
            }
            else
            {
                shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();
                Ship = GameObject.Find("Ship");
                flg = true;
            }
            speed = 0;

            //Debug.Log("root" + rootGameObject);
            //Debug.Log("GameObject" + shipMove);
            //Debug.Log("Flg " + flg);

            //"Particle1"オブジェクトから ParticleSystemコンポーネントを取得 
            ///最初にエラーを吐くが無視すること
            EmObj1 = transform.Find("Particle1").GetComponent<ParticleSystem>().emission.rateOverTime;
            EmObj2 = transform.Find("Particle2").GetComponent<ParticleSystem>().emission.rateOverTime;
            EmObj3 = transform.Find("Particle3").GetComponent<ParticleSystem>().emission.rateOverTime;
            EmObj4 = transform.Find("Particle4").GetComponent<ParticleSystem>().emission.rateOverTime;
        }

        private float mCount = 0;       //←時間計測用
        void Update()
        {
            if (flg == false)
            {
                speed = CpushipMove.Cpu_Speed;
                //Debug.Log("speed" + rootGameObject + " " + speed);
            }
            else
            {
                speed = shipMove.MoveSpeed;
                //Debug.Log("speed" + rootGameObject + " " + speed);
            }


            mCount = mCount + Time.deltaTime;   //←時間計測中
            if (mCount >= 1.0f)
            { //経過する度に if 成立
                mCount = 0; // 時間計測用変数を初期化
                            //スピードに対してN個放出する
                            //EmObj1.rateOverTime = speed;
                EmObj1 = speed;
                EmObj2 = speed;
                EmObj3 = speed;
                EmObj4 = speed;
            }
        }
    }
}
