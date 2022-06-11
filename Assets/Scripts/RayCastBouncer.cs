using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastBouncer : MonoBehaviour
{
    public Camera Camera;
    public float MaxDistance=100;
    public int Bounces = 3;

    private RaycastHit hitInfo;
    private Ray prevRay;
    private LineRenderer lineRenderer;
    private Vector3 currentMousePos;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        currentMousePos = Input.mousePosition;
        ResetLine();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if(currentMousePos != Input.mousePosition)
        {
            ResetLine();
        }

        if(Physics.Raycast(ray, out hitInfo, MaxDistance))
        {
            prevRay = ray;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1, hitInfo.point);

            for (int i = 0; i < Bounces; i++)
            {
                Ray reflectedRay = new Ray(hitInfo.point, Vector3.Reflect(prevRay.direction, hitInfo.normal).normalized);
                if(Physics.Raycast(reflectedRay, out hitInfo, MaxDistance))
                {
                    Debug.DrawLine(hitInfo.point, hitInfo.point + (hitInfo.normal * 2), Color.green);
                    prevRay = reflectedRay;
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitInfo.point);

                }
                else
                {
                    return;
                }



            }





        }

    }

    public void ResetLine()
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }
}
