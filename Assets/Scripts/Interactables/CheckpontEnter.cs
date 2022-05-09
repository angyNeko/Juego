using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpontEnter : MonoBehaviour
{
    GameObject chekcpoint;

    private void Start()
    {
        chekcpoint = GameObject.Find("Checkpoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(chekcpoint);
    }
}
