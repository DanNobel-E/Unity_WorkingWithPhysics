using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    public Camera Camera;
    public float Power = 1;
    public float JumpPower = 1;
    public float BoostPower = 2;



    string HAxis = "Horizontal";
    string VAxis = "Vertical";
    float HVal;
    float VVal;
    Rigidbody rb;

    private bool jump=false;
    float jumpTimer;
    bool setJumpTimer = false;
    float jumpDuration = 0.8f;

    private bool boost=false;
    private float boostTimer;
    private bool setBoostTimer=false;
    float boostDuration = 1.5f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Vector3 cameraFwd = Camera.transform.forward.normalized;
        Vector3 cameraR = Camera.transform.right.normalized;
        cameraFwd.Scale(new Vector3(HVal,HVal,HVal));
        cameraR.Scale(new Vector3(VVal, VVal, VVal));


        Vector3 force = cameraFwd+cameraR;
        Debug.DrawLine(transform.position, transform.position+(force.normalized*2), Color.red);
        rb.AddTorque(force,ForceMode.Impulse);

        if (jump)
        {
            rb.AddForce(Vector3.up.normalized*JumpPower, ForceMode.Impulse);
            jump = false;
        }

        if (boost)
        {
            rb.AddForce(Camera.transform.forward.normalized * BoostPower, ForceMode.Impulse);
            boost = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HVal = -Input.GetAxis(HAxis) * Power * Time.deltaTime;
        VVal = Input.GetAxis(VAxis) * Power * Time.deltaTime;

        if (setJumpTimer)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (setBoostTimer)
        {
            boostTimer -= Time.deltaTime;
        }

        if (jumpTimer < 0)
        {
            jumpTimer = 0;
            setJumpTimer = false;

        }


        if (boostTimer < 0)
        {
            boostTimer = 0;
            setBoostTimer = false;

        }


        if (Input.GetKeyDown(KeyCode.Space) && !setJumpTimer)
        {

            jumpTimer = jumpDuration;
            setJumpTimer = true;
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !setBoostTimer)
        {

            boostTimer = boostDuration;
            setBoostTimer = true;
            boost = true;
        }
    }
}
