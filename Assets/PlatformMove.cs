using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{

    public Transform StartPos, EndPos;
    public float Speed=1;

    bool direction = true;

    public float DistToEnd { get { return (transform.position - EndPos.position).magnitude; } }
    public float DistToStart { get { return (transform.position - StartPos.position).magnitude; } }
    float deltaDist = 0.2f;


    private void Start()
    {
        transform.position = StartPos.position ;
    }
    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
        transform.position=  Vector3.Lerp(transform.position, EndPos.position, Speed * Time.deltaTime);
            if ( DistToEnd<=deltaDist)
            {
                direction = false;
            }

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, StartPos.position, Speed * Time.deltaTime);
            if (DistToStart <= deltaDist)
            {
                direction = true;
            }
        }

        



    }
}
