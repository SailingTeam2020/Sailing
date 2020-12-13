using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{



    void Start()
    {

    }

    void Update()
    {

    }

    public void onClick()
    {
        //Time.timeScale=0の場合停止する
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}