using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{

    public Camera Camera;
    public GameObject Ball;
    public GameObject BackWall;
    public Transform Parent;

    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = Ball.transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {


        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        float rayMagnitude = (transform.position - BackWall.transform.position).magnitude;
        Debug.DrawRay(transform.position, ray.direction*rayMagnitude, Color.red);

        RaycastHit hitInfo;
        if (Physics.SphereCast(ray, radius, out hitInfo))
        {



            if (hitInfo.collider == BackWall.GetComponent<BoxCollider>())
            {
                Debug.DrawRay(transform.position, ray.direction * rayMagnitude, Color.green);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Vector3 distToWall = hitInfo.point - transform.position;
                    Quaternion rot = Quaternion.LookRotation(distToWall.normalized);
                    Instantiate(Ball, transform.position, rot, Parent);
                }



            }


        }
    }
}
