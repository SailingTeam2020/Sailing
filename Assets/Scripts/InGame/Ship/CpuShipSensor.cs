using Sailing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CpuShipSensor : MonoBehaviour
{
    GameObject cpuship;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ship") || collider.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<CpushipMove>().CollisionDetected(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<CpushipMove>().CollisionExitDetected(this);
    }
}
