using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpring : MonoBehaviour
{

    public GameObject SpringPlatform1, SpringPlatform2;
    public float RestoreDuration = 2;
    public RigidbodyConstraints SpringAxis;

    Vector3 spring1StartPos, spring2StartPos;
    Rigidbody rb1, rb2;
    private bool restore;
    float restoreTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();

        spring1StartPos = SpringPlatform1.transform.position;
        rb1 = SpringPlatform1.GetComponent<Rigidbody>();
        rb1.constraints = RigidbodyConstraints.FreezeAll;

        if(SpringPlatform2!= null)
        {
            spring2StartPos = SpringPlatform2.transform.position;
            rb2 = SpringPlatform2.GetComponent<Rigidbody>();
            rb2.constraints = RigidbodyConstraints.FreezeAll;

        }






    }

    private void OnTriggerEnter(Collider other)
    {
        if (!restore)
        {
            if(other.gameObject!=SpringPlatform1 && other.gameObject!= SpringPlatform2)
            {
                rb1.constraints &= ~SpringAxis;

                if (SpringPlatform2 != null)
                {
                    rb2.constraints &= ~SpringAxis;
                }
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!restore)
        {
            if (other.gameObject != SpringPlatform1 && other.gameObject != SpringPlatform2)
            {
                rb1.constraints = RigidbodyConstraints.FreezeAll;
                if (SpringPlatform2 != null)
                {

                    rb2.constraints = RigidbodyConstraints.FreezeAll;
                }
                restore = true;
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (restore)
        {
            restoreTimer += Time.deltaTime;


            SpringPlatform1.transform.position = Vector3.Lerp(SpringPlatform1.transform.position, spring1StartPos, restoreTimer / RestoreDuration);

            if (SpringPlatform2 != null)
            {

                SpringPlatform2.transform.position = Vector3.Lerp(SpringPlatform2.transform.position, spring2StartPos, restoreTimer / RestoreDuration);

            }


            if (restoreTimer >= RestoreDuration)
            {
                

                ResetTimer();
                restore = false;

            }




        }
        
    }

    void ResetTimer()
    {

        restoreTimer = 0;


    }
}
