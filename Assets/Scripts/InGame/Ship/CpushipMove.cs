using System.Collections;
using UnityEngine;

public class CpushipMove : MonoBehaviour
{
    public float angle;
    public float axis;
    private GameObject nextMarker;//次の目的地

    private float Cpu_Speed;
    private float Cpu_Rotate;

    void Start()
    {
        Cpu_Speed = 0.05f;//テスト的に設定　プレイヤーに合わせて変える必要あり
        Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
        //Invoke("NextMarkerSet", 10.0f);
        nextMarker = serchTag(gameObject, "EnterMarker");
    }

    void Update()
    {
        var diff = nextMarker.transform.position - this.gameObject.transform.position; //ターゲットと
        var axis = Vector3.Cross(transform.forward, diff);
        var angle = Vector3.Angle(transform.forward, diff);
        var ship_direction = angle * axis;

        this.gameObject.transform.Translate(0, 0, Cpu_Speed);
        if (ship_direction.y < -2f)//船が右にいる
        {
            this.gameObject.transform.Rotate(0, -Cpu_Rotate, 0);
        }
        else if (ship_direction.y > 2f)//船が左にいる
        {
            this.gameObject.transform.Rotate(0, Cpu_Rotate, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {        
        if(other.tag == "EnterMarker")
        {
            nextMarker = serchTag(gameObject, "OutMarker");
        }
        else if(other.tag == "OutMarker") {
            if (GameObject.FindWithTag("EnterMarker") == true)
            {
                nextMarker = serchTag(gameObject, "EnterMarker");
            }
            else
            {
                nextMarker = serchTag(gameObject, "GoalMarker");
            }
        }
        other.tag = "Passing";
     }
     
   
    //マーカーの中で最も近いものを取得
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;       
        float nearDis = 0;        
        GameObject targetObj = null;
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }
        }
        return targetObj;
    }
}

