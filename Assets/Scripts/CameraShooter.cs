using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour
{
    public GameObject QuadPrefab;
    public GameObject Cube;
    public float Power = 1;

    private Rigidbody rb;
    private Vector3 force;
    private bool fire;


    // Start is called before the first frame update
    void Start()
    {
        rb = Cube.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (fire == true)
        {
            fire = false;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 300))
            {
                if (hitInfo.collider == Cube.GetComponent<BoxCollider>())
                {
                    Quaternion rot = Quaternion.LookRotation(-hitInfo.normal);
                    Vector3 pos = hitInfo.point + (hitInfo.normal * 0.01f);
                    GameObject quad = Instantiate(QuadPrefab, hitInfo.point, rot, Cube.transform);

                    float quadOffset = quad.transform.localScale.x*1.001f;
                    float cubeOffset = Cube.transform.localScale.x * 0.5f;

                    Vector3 faceCenter = Cube.transform.position + (hitInfo.normal.normalized * cubeOffset);
                    Vector3 distToFaceCenter = quad.transform.position - faceCenter;
                    Vector3 distToCenter = quad.transform.position - Cube.transform.position;
                    float distToQuadVertex = Mathf.Sqrt(2) * quadOffset;

                    Vector3 distToRight = Cube.transform.right.normalized * cubeOffset;
                    Vector3 distToUp = Cube.transform.up.normalized * cubeOffset;
                    Vector3 distToForward = Cube.transform.forward.normalized * cubeOffset;
                    Vector3 distToVertex = distToRight + distToUp + distToForward;
                    float distToVertexMagnitude = distToVertex.magnitude;


                    if (distToCenter.magnitude+ distToQuadVertex > distToVertexMagnitude)
                    {

                        quad.transform.position = quad.transform.position + (-distToFaceCenter.normalized * distToQuadVertex);



                    }


                    if (distToFaceCenter.magnitude + quadOffset > cubeOffset)
                    {

                        quad.transform.position = quad.transform.position + (-distToFaceCenter.normalized * quadOffset);
                    }
                    force = ray.direction.normalized * Power;
                    rb.AddForceAtPosition(force, hitInfo.point, ForceMode.Impulse);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
             fire = true;
        }


    }
}
