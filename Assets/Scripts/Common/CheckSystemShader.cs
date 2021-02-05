using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSystemShader : MonoBehaviour
{
    [SerializeField]
    GameObject Water;

    [SerializeField]
    GameObject Ocean;

    [SerializeField]
    GameObject OceanInputs;

    private void Awake()
    {

        //Sceneが遷移しても破棄されないようにする。
        //DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {

        try
        {
            Water = GameObject.Find("Water");
            Ocean = GameObject.Find("Ocean");
            OceanInputs = GameObject.Find("OceanInputs");

            if (SystemInfo.supportsComputeShaders)
            {
                Water.SetActive(false);
            }
            else
            {
                Water.SetActive(true);
            }

        }
        catch
        {

        }


    }

    bool checkSystem()
    {
        if (!SystemInfo.supportsComputeShaders) return false;
        if (!SystemInfo.supports2DArrayTextures) return false;

        return true;
    }

}
