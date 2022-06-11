using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FollowedObj : MonoBehaviour
{
    public int Points;
    public List<GameObject> FollowingObjects;
    public Material Material;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < FollowingObjects.Count; i++)
        {
            if (FollowingObjects[i].GetComponent<Collider>().enabled)
            {
                FollowingObjects[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void OnTrigger(Collider c)
    {

        
        if (FollowingObjects.Count == 0)
            c.gameObject.AddComponent<FollowingObj>().Follower = gameObject;
        else
            c.gameObject.AddComponent<FollowingObj>().Follower = FollowingObjects[FollowingObjects.Count - 1];


        c.gameObject.GetComponent<FollowingObj>().Origin = gameObject.GetComponent<Orbiting>().Origin;
        c.gameObject.GetComponent<FollowingObj>().ChangeMaterial();
        FollowingObjects.Add(c.gameObject);
        gameObject.GetComponent<Points>().AddPoint(Points);

        BodyPart b = c.GetComponent<BodyPart>();
        if (b != null)
        {
            b.Target.gameObject.SetActive(false);
            b.SetActiveParticle(false);
        }

        c.GetComponent<Animator>().enabled = false;
        
    }
}
