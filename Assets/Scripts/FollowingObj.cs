using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObj : MonoBehaviour
{
    public Transform Origin;
    public GameObject Follower;

    public float dist=2;
    public float LerpSpeed = 4;

    


    // Update is called once per frame
    void Update()
    {
        Vector3 distToOrigin =  Origin.position-transform.position;
        transform.rotation = Quaternion.LookRotation(distToOrigin.normalized);




        if (Vector3.Distance(transform.position, Follower.transform.position) > dist)
        {

            transform.position = Vector3.Slerp(transform.position, Follower.transform.position, LerpSpeed * Time.deltaTime);
            
        }

        //if(Vector3.Distance(transform.position, Origin.position) < radius)
        //{
        //    float deltaRadius = radius-distToOrigin.magnitude;
        //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z-deltaRadius);

        //}
    }

    public void ChangeMaterial()
    {
        FollowedObj component;
        if (Follower.TryGetComponent<FollowedObj>(out component))
        {
            gameObject.GetComponent<MeshRenderer>().material = component.Material;

        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = Follower.GetComponent<MeshRenderer>().material;

        }

    }
}
