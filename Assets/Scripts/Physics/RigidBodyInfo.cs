using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyInfo : MonoBehaviour
{
    public Transform CenterOfMass;
    Rigidbody rb;
    string info;
    public bool displayInfo = true;
    public Vector2 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 vel = rb.velocity;
        Vector3 aVel = rb.angularVelocity;
        bool isSleeping = rb.IsSleeping();


        //rb.ResetCenterOfMass();
        //Vector3 currPos = transform.position;
        //Debug.DrawLine(currPos, currPos + rb.velocity, Color.red);
        //Debug.DrawLine(currPos, currPos + rb.angularVelocity, Color.yellow);
        //if(CenterOfMass)
        //    CenterOfMass.localPosition = rb.centerOfMass;

        info = string.Format("RigidBodyInfo\nvelocity: {0}\nangularVelocity: {1}\nIsSleeping: {2}",
            vel, aVel, isSleeping);
    }

    void OnGUI()
    {
        if (!displayInfo) return;
        GUILayout.BeginHorizontal();
        GUILayout.Space(offset.x);
        GUILayout.BeginVertical();
        GUILayout.Space(offset.y);
        GUILayout.BeginVertical("box");
        GUILayout.Label(info);
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}
