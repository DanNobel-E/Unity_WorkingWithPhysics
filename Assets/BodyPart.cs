using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BodyPart : MonoBehaviour
{
    public RectTransform Target;
    public bool BackToStart { get; protected set; }
    Vector3 currPos;
    Vector3 startPos;
    Animator Anim;
    Camera targetCam;
    float lerpDuration = 0.1f;
    float timer;
    bool follow;
    public ParticleSystem Particle { get; protected set; }

    void Start()
    {
        Anim = GetComponent<Animator>();
        startPos = transform.position;
        Anim.SetFloat("Cycle", Random.Range(0f, 1f));
        Particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (BackToStart)
        {
            timer += Time.deltaTime;
            float fraction = timer / lerpDuration;
            transform.position = Vector3.Lerp(currPos, startPos, fraction);

            if(timer >= lerpDuration)
            {
                transform.position = startPos;
                timer = 0;
                BackToStart = false;
            }
        }


        if (follow)
        {
            Vector3 dir = (targetCam.transform.position-Target.position).normalized;
            Target.rotation = Quaternion.LookRotation(-dir, Target.up);
        }
    }

    public void ReturnToStartPos()
    {
        currPos = transform.position;
        BackToStart = true;
    }

    public void TargetFollow(bool state, Camera cam)
    {
        Target.gameObject.SetActive(state);
        follow = state;
        if (state)
            targetCam = cam;
    }

    public void SetActiveParticle(bool b)
    {
        EmissionModule e = Particle.emission;
        e.enabled = b;
    }
}
