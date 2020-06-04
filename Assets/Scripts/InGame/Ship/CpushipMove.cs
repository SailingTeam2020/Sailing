using System.Collections;
using UnityEngine;

public class CpushipMove : MonoBehaviour
{

    public GameObject[] targetObj;
    public float angle;
    public float axis;

    private float Cpu_Speed;
    private float Cpu_Rotate;
    private int marker_num;//マーカーの番号

    void Start()
    {
        Cpu_Speed = 0.1f;//テスト的に設定　プレイヤーに合わせて変える必要あり
        Cpu_Rotate = 0.5f;//テスト的に設定　プレイヤーに合わせて変える必要あり
        marker_num = 0;
    }

    void Update()
    {
        var diff = targetObj[marker_num].transform.position - this.gameObject.transform.position; //ターゲットと
        var axis = Vector3.Cross(transform.forward, diff);
        var angle = Vector3.Angle(transform.forward, diff);
        var ship_direction = angle * axis;


        this.gameObject.transform.Translate(0, 0, Cpu_Speed);
        Debug.Log(ship_direction / 180);
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
        if (targetObj[marker_num+1] != null)
        {
            marker_num++;
        }
    }
}
