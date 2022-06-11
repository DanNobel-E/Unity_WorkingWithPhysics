using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayCaster : MonoBehaviour
{
    public List<Camera> Cameras;
    public GameObject Prefab;
    public GameObject Model;
    public Transform Root;
    public int Nx = 30;
    public int Ny = 30;
    public bool UseNormal;

    // Start is called before the first frame update
    void Start()
    {
        float deltaX = Screen.width / Nx;
        float deltaY = Screen.height / Ny;

        for (int i = 0; i < Cameras.Count; i++)
        {

            for (float y = 0; y + deltaY < Screen.height; y += deltaY)
            {
                for (float x = 0; x + deltaX < Screen.width; x += deltaX)
                {

                    Vector3 screenPos = new Vector3(x, y, 0);
                    Ray ray = Cameras[i].ScreenPointToRay(screenPos);
                    RaycastHit hitPoint;
                    if (Physics.Raycast(ray, out hitPoint, 500))
                    {
                        Vector3 objPos = hitPoint.point;
                        Quaternion normalRot = Quaternion.LookRotation(hitPoint.normal);
                        Instantiate(Prefab, objPos, UseNormal ? normalRot : Quaternion.identity, Root);


                    }



                }


            }
        }
        Model.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
