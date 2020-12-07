﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sailing.Online
{
    public class ShipBubbles : MonoBehaviour
    {
        ParticleSystem.EmissionModule EmObj1;
        ParticleSystem.EmissionModule EmObj2;
        ParticleSystem.EmissionModule EmObj3;
        ParticleSystem.EmissionModule EmObj4;

        private ShipMove shipMove;
        private CpushipMove CpushipMove;
        private float speed;
        public GameObject root;
        GameObject Ship;
        GameObject CPUship;
        bool flg;
        // string player;
        void Start()
        {
            //"Particle1"オブジェクトから ParticleSystemコンポーネントを取得 
            ParticleSystem ParticleObj1 = transform.Find("Particle1").GetComponent<ParticleSystem>();
            EmObj1 = ParticleObj1.emission;         //最終目的である rateOverTime にアクセスするために必要な emission を取得し格納
            ParticleSystem ParticleObj2 = transform.Find("Particle2").GetComponent<ParticleSystem>();
            EmObj2 = ParticleObj2.emission;
            ParticleSystem ParticleObj3 = transform.Find("Particle3").GetComponent<ParticleSystem>();
            EmObj3 = ParticleObj3.emission;
            ParticleSystem ParticleObj4 = transform.Find("Particle4").GetComponent<ParticleSystem>();
            EmObj4 = ParticleObj4.emission;


            shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();
            Ship = GameObject.Find("Ship");
            if (SceneManager.GetActiveScene().name == "InGame")
            {
                CpushipMove = GameObject.Find("CPUShip").GetComponent<CpushipMove>();
                CPUship = GameObject.Find("CPUShip");
            }
            speed = 0;
            root = transform.root.gameObject;
            //player = "Ship";
            if (root == Ship)
            {
                //speed = shipMove.MoveSpeed;
                flg = true;

            }

            if (root == CPUship)
            {
                //speed = CpushipMove.Cpu_Speed;
                flg = false;
            }

            Debug.Log("root" + root);
            Debug.Log("GameObject" + shipMove);
            Debug.Log("Flg " + flg);

        }

        private float mCount = 0;       //←時間計測用
        void Update()
        {
            if (flg == false)
            {
                speed = CpushipMove.Cpu_Speed;
                Debug.Log("speed" + root + " " + speed);
            }
            else
            {
                speed = shipMove.MoveSpeed;
                Debug.Log("speed" + root + " " + speed);
            }


            mCount = mCount + Time.deltaTime;   //←時間計測中
            if (mCount >= 1.0f)
            { //経過する度に if 成立
                mCount = 0; // 時間計測用変数を初期化
                            //スピードに対してN個放出する
                EmObj1.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
                EmObj2.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
                EmObj3.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
                EmObj4.rateOverTime = new ParticleSystem.MinMaxCurve(speed);
            }
        }
    }
}
