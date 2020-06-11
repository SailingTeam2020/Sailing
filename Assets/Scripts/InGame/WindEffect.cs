using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{

    [SerializeField]
    public GameObject WindObject;

    [SerializeField]
    public GameObject WindEffectObject;

    public Vector3 WindObjectPosition
    {
        get;
        private set;
    }

    public float WindDirection
    {
        get;
        private set;
    }

    public float MoveSpeed
    {
        get;
        private set;
    }

    public int EffectCount
    {
        get;
        private set;
    }

    public int EffectMax
    {
        get;
        private set;
    }

    public float GenerateTime
    {
        get;
        private set;
    }

    public int DestroyFlg
    {
        get;
        private set;
    }

    private void Awake()
    {

        //WindObject = GameObject.Find("WindObject");
        WindDirection = 0.0f;
        MoveSpeed = 500.0f;
        EffectCount = 0;
        EffectMax = 45;
        GenerateTime = 3.0f;
        DestroyFlg = 0;

    }

    private void Start()
    {
        WindObjectPosition = WindObject.transform.position;
        WindDirection = 0.0f;
    }

    private void FixedUpdate()
    {

        GenerateTime -= Time.deltaTime;
        WindEffectGenerate();
        //this.gameObject.transform.Translate(WindDirection, 0, WindDirection);
        //DestroyFlg = FindObjectOfType<WindEffectSource>().ReturnFlg();
        if(GenerateTime <= 0.0f)
        {
            EffectCount = 0;
            GenerateTime = 3.0f;
        }

    }

    public void WindEffectGenerate()
    {
        if (EffectCount == EffectMax) return;
        float EffectPos_x = Random.Range(-50.0f, 300.0f);
        float EffectPos_z = Random.Range(200.0f, 290.0f);
        Vector3 EffectPosition = new Vector3(EffectPos_x, 2.7f, EffectPos_z);

        Instantiate (WindEffectObject, EffectPosition, Quaternion.identity);
        EffectCount++;
    }

    public float GetAng(Vector3 ThisPosition, Vector3 WindPosition)
    {

        float dx = WindPosition.x - ThisPosition.x;
        //float dy = WindPosition.y - ThisPosition.y;
        float dz = WindPosition.z - ThisPosition.z;
        float rad = Mathf.Atan2(dz, dx);
        float angle = rad * Mathf.Deg2Rad;

        //Debug.Log("GetAng = " + angle);

        return angle;
    }

    public float SetAng()
    {
        float WD = 0.0f;
        WindDirection = GetAng(this.transform.position, WindObjectPosition);
        WD = WindDirection;
        return WD;
    }

}
