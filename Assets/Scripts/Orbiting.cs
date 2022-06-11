using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public Transform Origin;
    public string HAxis = "Horizontal";
    public string VAxis = "Vertical";
    public float SpeedO=1;
    public float SpeedR=1;

    private float alpha;
    private float beta;
    private float radius;



    // Start is called before the first frame update
    void Start()
    {
        radius = Origin.localScale.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        //alpha += -Input.GetAxis(HAxis) * SpeedO * Time.deltaTime;
        alpha = Input.GetAxis(VAxis) * SpeedO * Time.deltaTime;
        beta = -Input.GetAxis(HAxis) * SpeedR * Time.deltaTime;






        //Vector3 newDir = new Vector3(Mathf.Cos(signedAlpha), Mathf.Sin(signedAlpha));
        //Vector3 newAxis = new Vector3(Mathf.Sin(signedAlpha+(Mathf.PI/2)), Mathf.Cos(signedAlpha + (Mathf.PI / 2)), 0);
        //Vector3 distToOrigin =  Origin.position-transform.position;
        //Vector3 transformForward = new Vector2(transform.fo, Vector3.up.y);

        //Quaternion qy = Quaternion.AngleAxis(alpha, Vector3.up);
        Quaternion qx = Quaternion.AngleAxis(alpha, transform.right);
        Quaternion qz = Quaternion.AngleAxis(beta, transform.forward.normalized) * transform.rotation;



        Debug.DrawLine(Origin.transform.position, transform.right * 50, Color.red);


        transform.rotation = qx * qz;
        transform.position = Origin.position - (transform.rotation * Vector3.forward * (radius + (transform.localScale.x / 2)));
    }
}
