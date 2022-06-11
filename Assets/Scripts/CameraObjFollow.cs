using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float SpeedT=1;
    public float SpeedR=1;


    // Update is called once per frame
    void LateUpdate()
    {

          Vector3 finalPos = Target.position+ Target.rotation*Offset;
          transform.position = Vector3.Lerp(transform.position, finalPos, SpeedT * Time.deltaTime);

           
          Quaternion finalRot= Target.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, SpeedR * Time.deltaTime);
    }
}
