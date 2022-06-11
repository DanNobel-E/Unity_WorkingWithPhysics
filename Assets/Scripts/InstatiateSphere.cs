using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InstatiateSphere : MonoBehaviour
{
    
    public GameObject Prefab;
    public int N;
    public Transform Parent,World;

    public bool InstantiateN, DestroyAll;
    public bool InstantiatePrefab;


    public float Radius;
    public bool Inside;


    //Rotation
    public bool RandomRotation;

    //Scale
    public bool RandomUniformScale;
    public bool RandomScale;
    public float RandomMinScale;
    public float RandomMaxScale;
    void Update()
    {
        if (InstantiateN)
        {

            InstantiateN = false;
            PerformInstantiate();


        }


        if (DestroyAll)
        {
            DestroyAll = false;
            PerformDestroy();

        }


    }

    private void PerformDestroy()
    {
        int n = transform.childCount;

        List<Transform> children = new List<Transform>();
        for (int i = n - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            else
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }

    void PerformInstantiate()
    {
        if (Inside)
        {
            for (int i = 0; i < N; i++)
            {

                GameObject obj = Instantiate(Prefab, Parent.transform.position, Parent.transform.rotation, Parent);
                Vector3 position = Random.insideUnitSphere * Radius;
                obj.transform.position = position;
                Vector3 dir = (obj.transform.position - World.position).normalized;
                obj.transform.rotation = Quaternion.LookRotation(dir);

                RandomRotate(obj);

                RandomScaling(obj);


            }



        }
        else
        {

            for (int i = 0; i < N; i++)
            {

                GameObject obj = Instantiate(Prefab, Parent.transform.position, Parent.transform.rotation, Parent);

                Vector3 position = (Random.onUnitSphere * (Radius+(obj.transform.localScale.x/2)));
                obj.transform.position = position;
                Vector3 dir = (obj.transform.position - World.position).normalized;
                obj.transform.rotation = Quaternion.LookRotation(dir);



                RandomRotate(obj);

                RandomScaling(obj);
            }


        }
        void RandomRotate(GameObject obj)
        {
            if (RandomRotation)
            {
                obj.transform.Rotate(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));

            }
        }

        void RandomScaling(GameObject obj)
        {
            if (RandomScale || RandomUniformScale)
            {
                if (RandomUniformScale)
                {
                    float random = Random.Range(RandomMinScale, RandomMaxScale);
                    obj.transform.localScale = new Vector3(random, random, random);

                }
                else
                {
                    obj.transform.localScale = new Vector3(Random.Range(RandomMinScale, RandomMaxScale), Random.Range(RandomMinScale, RandomMaxScale), Random.Range(RandomMinScale, RandomMaxScale));

                }

            }
        }

    }

}
