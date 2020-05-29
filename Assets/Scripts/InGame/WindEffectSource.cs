using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectSource : MonoBehaviour
{    

    public float WindDirection
    {
        get;
        private set;
    }
    
    public float WindSpeed
    {
        get;
        private set;
    }

    public bool DestroyFlg
    {
        get;
        private set;
    }

    private void Awake()
    {
        
        WindDirection = FindObjectOfType<WindEffect>().SetAng();
        WindSpeed = 4.0f;
        DestroyFlg = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.Translate((WindDirection * WindSpeed), 0, (WindDirection * WindSpeed));
        //Debug.Log(WindDirection);
        if(transform.position.z < -100)
        {
            DestroyFlg = true;
            Destroy(this.gameObject);
        }

    }

    public int ReturnFlg()
    {
        //Debug.Log(DestroyFlg);
        if (DestroyFlg)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

}
