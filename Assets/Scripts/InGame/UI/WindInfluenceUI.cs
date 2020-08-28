using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

    public class WindInfluenceUI : MonoBehaviour
    {
        //値でゲージの長さの可変を行う
        [SerializeField]
        private ShipManager shipManager;
        [SerializeField]
        private Slider windSlider;

        private ShipMove shipMove;

        private void Start()
        {
            // shipManager内、MainShipObject内のShipMoveクラスを取得
            shipMove = shipManager.MainShipObject.GetComponent<ShipMove>();

        }

        void Update()
        {
            /*************************************
               小出 7/6着手

            *************************************/
            // shipMoveのWindInfluenceの値をwindSlider.valueに代入
            windSlider.value = shipMove.WindInfluence;  //揚力の計算？

            //7/24　足立から指示
            windSlider.value = shipMove.MoveSpeed;  //船の速さを代入
            //windSlider.value = GetAllWindForce();

        }
        /*************************************
        private float GetAllWindForce()
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            throw new NotImplementedException();
        }
            *************************************/
    }

}