using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class OnlineSpeedTextUI : MonoBehaviour
    {

        [SerializeField]
        private ShipManager shipManager;
        [SerializeField]
        private Text speedText;

        private ShipMove shipMove;

        private void Start()
        {

            shipMove = shipManager.MainShipObject.GetComponent<ShipMove>();

        }

        private void Update()
        {

            float speed = shipMove.MoveSpeed;
            speed = Mathf.Floor(speed);
            speedText.text = speed.ToString();

        }

    }

}