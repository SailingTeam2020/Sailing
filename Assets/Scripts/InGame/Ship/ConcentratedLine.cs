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
        private GameObject Player;
        private Vector3 offset;
        void Start()
        {
            main = GetComponent<ParticleSystem>().main;
            shipMove = GameObject.Find("Ship").GetComponent<ShipMove>();
            Player = GameObject.Find("Ship");
            speed = 0;
            offset = transform.position - Player.transform.position;

        }

        // Update is called once per frame
        void Update()
        {
            speed = shipMove.MoveSpeed;
            transform.position = Player.transform.position + offset;
            //transform.rotation = Player.transform.rotation;
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