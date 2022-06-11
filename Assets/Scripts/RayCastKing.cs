using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastKing : MonoBehaviour
{
    public Transform[] Soldiers = new Transform[4];
    public float DistanceFromKing=1;
    public float DistanceFromWall = 5;

    public float LerpMultiplier=1;

    private Ray[] rays = new Ray[4];

    // Start is called before the first frame update
    void Start()
    {

        Soldiers[0].position=transform.position;
        Soldiers[1].position=transform.position;
        Soldiers[2].position=transform.position;
        Soldiers[3].position = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos0 = transform.position + (transform.forward.normalized * DistanceFromKing);
        Vector3 newPos1=transform.position + (transform.right.normalized * DistanceFromKing);
        Vector3 newPos2=transform.position + (-transform.forward.normalized * DistanceFromKing);
        Vector3 newPos3=transform.position + (-transform.right.normalized * DistanceFromKing);



        Soldiers[0].position = Vector3.Lerp(Soldiers[0].position, newPos0, LerpMultiplier * Time.deltaTime);
        Soldiers[1].position = Vector3.Lerp(Soldiers[1].position, newPos1, LerpMultiplier * Time.deltaTime);
        Soldiers[2].position = Vector3.Lerp(Soldiers[2].position, newPos2, LerpMultiplier * Time.deltaTime);
        Soldiers[3].position = Vector3.Lerp(Soldiers[3].position, newPos3, LerpMultiplier * Time.deltaTime);

        for (int i = 0; i < Soldiers.Length; i++)
        {
            Soldiers[i].rotation = Quaternion.LookRotation(transform.forward, transform.up);
        }

        rays[0] = new Ray(transform.position, transform.forward);
        rays[1] = new Ray(transform.position, transform.right);
        rays[2] = new Ray(transform.position, -transform.forward);
        rays[3] = new Ray(transform.position, -transform.right);

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(transform.position, rays[i].direction*DistanceFromKing, Color.yellow);
        }

        for (int i = 0; i < rays.Length; i++)
        {
            RaycastHit hitPoint;
            if (Physics.Raycast(rays[i], out hitPoint, DistanceFromKing + DistanceFromWall))
            {
                Vector3 newPos = hitPoint.point + (hitPoint.normal.normalized * DistanceFromWall);
                Soldiers[i].position = newPos;

                Vector3 distToKing = transform.position - Soldiers[i].position;
                Quaternion newRot = Quaternion.LookRotation(distToKing.normalized, Vector3.up);
                Soldiers[i].rotation = newRot;
            }
        }



    }
}
