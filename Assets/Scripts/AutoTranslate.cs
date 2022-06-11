using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTranslate: MonoBehaviour {
	public bool performUpdate;
	public Vector3 move;

	void Update () {
		if (performUpdate) {
			transform.Translate (move.x * Time.deltaTime, move.y * Time.deltaTime, move.z * Time.deltaTime);
		}
	}
}
