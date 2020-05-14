using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

    public class WindInfluenceUI : MonoBehaviour
    {

        [SerializeField]
        private ShipManager shipManager;
        [SerializeField]
        private Slider windSlider;

        private ShipMove shipMove;

        private void Start()
        {

            shipMove = shipManager.MainShipObject.GetComponent<ShipMove>();

        }

        void Update()
        {

            windSlider.value = shipMove.WindInfluence;
        
        }

    }

}