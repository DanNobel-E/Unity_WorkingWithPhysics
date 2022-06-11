using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceAtPosition : MonoBehaviour {
    public float Power = 5;
    Vector3 ForceToApply;
    Rigidbody rb;
    RaycastHit hitInfo;
    RaycastHit[] hitsInfo;
    Ray customRay;
    bool fire = false;
    public Camera cam;
    public Transform DebugTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            fire = true;
    }
    void FixedUpdate () {
        customRay = cam.ScreenPointToRay(Input.mousePosition);

        if (fire && Physics.Raycast(customRay, out hitInfo))
        {
            fire = false;
            rb = hitInfo.collider.transform.GetComponent<Rigidbody>();
            if (rb == null)
                return;
            DebugTarget.position = hitInfo.point;
            ForceToApply = (hitInfo.point - cam.transform.position).normalized * Power;
            rb.AddForceAtPosition(ForceToApply, hitInfo.point);
        }
    }
}
