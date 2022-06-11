using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InstantiateSphereEdit : MonoBehaviour
{
    public Transform parent;
    public int n;
    public bool inside, randomRotation, randomScale, randomUniformScale;
    public float randomMinScale, randomMaxScale;
    public float r;
    public GameObject prefab;

    public bool Create, DestoryAll;

    void Perform_instantiate()
    {
        for (int i = 0; i < n; i++)
        {
            GameObject go;
            if (parent != null)
                go = Instantiate(prefab, parent);
            else
                go = Instantiate(prefab);
            if (inside) 
                go.transform.localPosition = Random.insideUnitSphere * r;
            else
                go.transform.localPosition = Random.onUnitSphere * r;

            if (randomRotation)
                go.transform.localRotation = Random.rotation;
            if (randomScale) {
                if (randomUniformScale) {
                    float scale = Random.Range(randomMinScale, randomMaxScale);
                    go.transform.localScale = new Vector3(scale, scale, scale);
                }
                else
                    go.transform.localScale = new Vector3(  Random.Range(randomMinScale, randomMaxScale),
                                                            Random.Range(randomMinScale, randomMaxScale),
                                                            Random.Range(randomMinScale, randomMaxScale));
            }
        }
    }

    private void Update()
    {
        if (Create) {
            Create = false;
            Perform_instantiate();
        }
        if (DestoryAll)
        {
            DestoryAll = false;
            int n = transform.childCount;
            List<Transform> children = new List<Transform>();
            for (int i = n - 1; i >= 0; i--)
            {
                if (Application.isPlaying)
                    Destroy(transform.GetChild(i).gameObject);
                else
                    DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
