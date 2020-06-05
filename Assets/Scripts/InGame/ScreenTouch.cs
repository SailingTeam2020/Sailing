using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Online
{

    public class ScreenTouch : MonoBehaviour
    {
        Vector2 m;
        Vector3 mousePosition;
        private GameObject ship;
        private float ShipRotation;
        private float timeElapsed;
        private float timeOut;

        // Start is called before the first frame update
        void Start()
        {
            m = new Vector2(Screen.width / 2, Screen.height / 2);
            ship = GameObject.Find("Ship");
            ShipRotation = 0;
            timeOut = 0.05f;
        }

        // Update is called once per frame
        void Update()
        {
            timeElapsed += Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                if (timeElapsed >= timeOut)
                {
                    if (mousePosition.x < m.x)
                    {
                        ShipRotation = (-0.5f);
                        //Debug.Log("左" + ShipRotation);
                    }
                    else
                    {
                        ShipRotation = 0.5f;
                        //Debug.Log("右" + ShipRotation);
                    }

                    timeElapsed = 0.0f;
                }
             ship.transform.Rotate(0f, ShipRotation, 0f);
           }
            else
            {
                //何もしない
            }
        }
    }
}