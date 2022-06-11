using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {
	public bool performUpdate;
	public Vector3 angle;

	void Update () {
		if (performUpdate) {
			transform.Rotate (angle.x * Time.deltaTime, angle.y * Time.deltaTime, angle.z * Time.deltaTime);
		}
	}
}
