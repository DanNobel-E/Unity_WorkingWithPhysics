using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupManager : MonoBehaviour
{
    public Transform Player1, Player2;
    public string p1Layer, p2Layer;
    public Camera Cam1;
    public Camera Cam2;

    CullingGroup cullingGroup1;
    CullingGroup cullingGroup2;

    Transform[] Group;
    BoundingSphere[] bounds;
    float[] distances = new float[] { 15, 25, 40 };
    int layer1, layer2, NV;
    
// Start is called before the first frame update
void Start()
    {
        layer1 = LayerMask.NameToLayer(p1Layer);
        layer2= LayerMask.NameToLayer(p2Layer);
        NV = LayerMask.NameToLayer("NotVisible");


        Cam1.cullingMask &= ~LayerMask.GetMask("NotVisible");
        Cam2.cullingMask &= ~LayerMask.GetMask("NotVisible");

        Group = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            Group[i] = transform.GetChild(i);
            Group[i].gameObject.layer = NV;
        }

        cullingGroup1 = new CullingGroup();
        cullingGroup2 = new CullingGroup();

        cullingGroup1.targetCamera = Cam1;
        cullingGroup2.targetCamera = Cam2;

        cullingGroup1.SetDistanceReferencePoint(Player1);
        cullingGroup2.SetDistanceReferencePoint(Player2);

        cullingGroup1.SetBoundingDistances(distances);
        cullingGroup2.SetBoundingDistances(distances);

        bounds = new BoundingSphere[200];
        for (int i = 0; i < Group.Length; i++)
        {
            bounds[i] = new BoundingSphere(Group[i].position, 1);
        }
        cullingGroup1.SetBoundingSpheres(bounds);
        cullingGroup2.SetBoundingSpheres(bounds);

        cullingGroup1.SetBoundingSphereCount(Group.Length);
        cullingGroup2.SetBoundingSphereCount(Group.Length);

        cullingGroup1.onStateChanged = OnStateChange1;
        cullingGroup2.onStateChanged = OnStateChange2;


    }

    private void Update()
    {
        for (int i = 0; i < Group.Length; i++)
        {
            bounds[i].position = Group[i].position;
        }
    }

    public void OnStateChange1(CullingGroupEvent cEvent)
    {
        if (!IsFallowing(Group[cEvent.index]))
        {

           

            switch (cEvent.currentDistance)
            {
                case 0:
                    ManageActivation(Group[cEvent.index], true, layer1, layer2);
                    ManageAnimation(Group[cEvent.index], true);
                    ManageTarget(Group[cEvent.index], true, layer1, layer2,Cam1);
                    ManageParticles(Group[cEvent.index], true);
                    break;
                case 1:
                    ManageActivation(Group[cEvent.index], true, layer1, layer2);
                    ManageAnimation(Group[cEvent.index], false);
                    ManageTarget(Group[cEvent.index], false, layer1, layer2);
                    ManageParticles(Group[cEvent.index], false);
                    break;
                case 2:
                    ManageActivation(Group[cEvent.index], false, layer1, layer2);
                    ManageAnimation(Group[cEvent.index], false);
                    ManageTarget(Group[cEvent.index], false, layer1, layer2);
                    ManageParticles(Group[cEvent.index], false);
                    break;
            }

        }
        
    }

    public bool IsFallowing(Transform t)
    {
        FollowingObj f;
        return t.TryGetComponent<FollowingObj>(out f);
          
    }

    public void OnStateChange2(CullingGroupEvent cEvent)
    {
        if (!IsFallowing(Group[cEvent.index]))
        {
            switch (cEvent.currentDistance)
            {
                case 0:
                    ManageActivation(Group[cEvent.index], true, layer2, layer1);
                    ManageAnimation(Group[cEvent.index], true);
                    ManageTarget(Group[cEvent.index], true, layer2, layer1, Cam2);
                    ManageParticles(Group[cEvent.index], true);
                    break;
                case 1:
                    ManageActivation(Group[cEvent.index], true, layer2, layer1);
                    ManageAnimation(Group[cEvent.index], false);
                    ManageTarget(Group[cEvent.index], false, layer2, layer1);
                    ManageParticles(Group[cEvent.index], false);
                    break;
                case 2:
                    ManageActivation(Group[cEvent.index], false, layer2, layer1);
                    ManageAnimation(Group[cEvent.index], false);
                    ManageTarget(Group[cEvent.index], false, layer2, layer1);
                    ManageParticles(Group[cEvent.index], false);
                    break;
            }
            
        }

    }

    public void ManageActivation(Transform t, bool isVisible, int layer, int layer2)
    {
        if (isVisible)
        {
            t.gameObject.SetActive(isVisible);
            if (t.gameObject.layer == NV)
                t.gameObject.layer = layer;
        }
        else
        {
            if (t.gameObject.layer == layer) 
                t.gameObject.layer = NV;
        }


    }

    public void ManageParticles(Transform t, bool state)
    {
        BodyPart b;
        if(t.TryGetComponent<BodyPart>(out b))
        {
            b.SetActiveParticle(state);
        }

    }

    public void ManageAnimation(Transform t, bool state)
    {
        t.GetComponent<Animator>().enabled = state;
        
        if(!state)
            t.GetComponent<BodyPart>().ReturnToStartPos();
    }

    public void ManageTarget(Transform t, bool isVisible, int layer, int layer2, Camera cam=null)
    {
        if (isVisible)
        {

            if (t.GetChild(0).gameObject.layer == NV)
            {
                t.GetChild(0).gameObject.layer = layer;
                t.GetComponent<BodyPart>().TargetFollow(isVisible, cam);
            }
        }
        else
        {
            if (t.GetChild(0).gameObject.layer == layer)
            {
                t.GetChild(0).gameObject.layer = NV;
                t.GetComponent<BodyPart>().TargetFollow(isVisible, cam);

            }
        }


    }

    private void OnDestroy()
    {
        cullingGroup1.Dispose();
        cullingGroup2.Dispose();

    }
}
