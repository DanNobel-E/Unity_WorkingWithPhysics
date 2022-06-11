using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float SpeedT = 1;
    public float SpeedR = 1;
    public float Power = 10;

    private string YawAxis = "SSYaw";
    private string PitchAxis = "SSPitch";
    private string RollAxis = "SSRoll";
    private string PowerAxis = "SSPower";
    private string HAxis = "SSHorizontal";
    private string VAxis = "SSVertical";

    private float YawVal;
    private float PitchVal;
    private float RollVal;
    private float PowerVal;
    private float HVal;
    private float VVal;

    private Rigidbody rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.AddRelativeTorque(PitchVal,YawVal,RollVal, ForceMode.Force);

        rigidbody.AddRelativeForce(0,0,PowerVal, ForceMode.Acceleration);
        rigidbody.AddRelativeForce(HVal,VVal,0, ForceMode.Acceleration);




    }

    // Update is called once per frame
    void Update()
    {
      YawVal=  Input.GetAxis(YawAxis)*SpeedR*Time.deltaTime;
      PitchVal=  -Input.GetAxis(PitchAxis) * SpeedR * Time.deltaTime;
      RollVal=  -Input.GetAxis(RollAxis) * SpeedR * Time.deltaTime;
      PowerVal=  Input.GetAxis(PowerAxis) * Power * Time.deltaTime;
      HVal=  Input.GetAxis(HAxis) * SpeedT * Time.deltaTime;
      VVal=  Input.GetAxis(VAxis) * SpeedT * Time.deltaTime;


    }
}
