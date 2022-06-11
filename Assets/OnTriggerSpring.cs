using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSpring : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<SpringJoint>().spring = 10;
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<SpringJoint>().spring = 0;
    }
}
