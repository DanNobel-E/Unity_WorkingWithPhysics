using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    string HAxis = "Horizontal2";
    public Transform Target;
    public Vector3 Offset;
    public float SpeedT = 1;
    public float SpeedR = 1;
    float rotVal;

    // Update is called once per frame
    void LateUpdate()
    {
        rotVal += Input.GetAxis(HAxis) * SpeedR * Time.deltaTime;

        transform.position = Offset;
        Quaternion rot = Quaternion.AngleAxis(rotVal, Vector3.up);
        transform.position =  rot * transform.position;
        transform.position = Target.position + transform.position;

        Vector3 disttoTarget = Target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(disttoTarget.normalized, Vector3.up);
    }
}
