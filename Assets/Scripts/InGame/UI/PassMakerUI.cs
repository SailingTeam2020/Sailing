using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

    public class PassMakerUI : MonoBehaviour
    {
        
        [SerializeField]
        private ShipManager shipManager;

        private Text makerText;
        private int makerNum;

        private void Start()
        {

            makerText = GetComponent<Text>();
            makerNum = GameObject.Find("CourseManager").GetComponent<CourseManager>().MakerManager.MakerNum;

        }

        private void Update()
        {

            makerText.text = (shipManager.MainShipObject.NextMakerNumber - 1).ToString() + " / " + makerNum;

        }

    }

}