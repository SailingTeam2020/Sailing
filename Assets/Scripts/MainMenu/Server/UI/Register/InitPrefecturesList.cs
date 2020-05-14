using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{

    public class InitPrefecturesList : MonoBehaviour
    {

        private Dropdown dropList;

        // Start is called before the first frame update
        private void Start()
        {

            dropList = GetComponent<Dropdown>();

            List<string> list = new List<string>();
            list.AddRange(PrefectureNameList.PrefName);

            dropList.ClearOptions();
            dropList.AddOptions(list);
            dropList.value = 0;

        }

    }

}