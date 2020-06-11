using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Online
{

    public class ConcentratedLine : MonoBehaviour
    {
        private ShipMove shipMove;
        private float speed;
        ParticleSystem.MainModule main;
        void Start()
        {
            main = GetComponent<ParticleSystem>().main;
            shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();
            speed = 0;

        }

        // Update is called once per frame
        void Update()
        {
            speed = shipMove.MoveSpeed;
             //船のスピードに応じて切り替える
            if (speed >= 18)
            {
                main.startColor = Color.white;
            }
            else
            {
                main.startColor = Color.clear;
            }
        }
    }
}