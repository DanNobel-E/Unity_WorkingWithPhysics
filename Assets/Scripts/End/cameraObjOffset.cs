using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class  cameraObjOffset: MonoBehaviour {
	public Transform parentObj;
	public Vector3 offset;
    public float LerpMultiplier=1;

    private void Start()
    {
        transform.position = parentObj.position + offset;
    }

    void Update () {
        if(parentObj)
		transform.position = Vector3.Lerp(transform.position, parentObj.position + offset, LerpMultiplier*Time.deltaTime);
	}
}
