using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringScript : MonoBehaviour
{
    public Camera Camera;
    public float SpeedT = 5;
    public float SpeedR = 50;
    public string HAxisName = "Horizontal";
    public string VAxisName = "Vertical";

    public float LerpSpeed = 1;
    public float HoveringDistance = 5;

    private float HVal;
  
    void Update()
    {


        HVal += Input.GetAxis(HAxisName) * Time.deltaTime * SpeedR;
        float VVal = Input.GetAxis(VAxisName) * Time.deltaTime * SpeedT;

        Quaternion q = Quaternion.Euler(0, HVal, 0);
        transform.Translate(0, 0, VVal);



        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, 30);
        float distToTerrain = (hitInfo.point - transform.position).magnitude;
        if (distToTerrain != HoveringDistance)
        {
        float deltaDist = HoveringDistance - distToTerrain;
        Vector3 newPos = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y+deltaDist, transform.position.z), LerpSpeed*Time.deltaTime);
        transform.position = newPos;
        }

        Vector3 normal = hitInfo.normal;
        Vector3 newForward = Vector3.Cross(Vector3.right, normal).normalized;
        transform.rotation = q*Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(newForward, normal), LerpSpeed*Time.deltaTime);


        Debug.DrawRay(transform.position, hitInfo.point-transform.position, Color.yellow);
        Debug.DrawLine(hitInfo.point, hitInfo.point+normal*5, Color.green);
        Debug.DrawLine(hitInfo.point, hitInfo.point+newForward*5, Color.blue);


    }
}
