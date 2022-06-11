using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggersEvents : MonoBehaviour {

	//This is called as soon as the collision starts
	void OnTriggerEnter(Collider c) {

		gameObject.GetComponent<FollowedObj>().OnTrigger(c);

	}
	//This is called if the Trigger c remains the same of the previous frame
	void OnTriggerStay(Collider c) {
	}
	//This is called as soon as the collision ends
	void OnTriggerExit(Collider c) {
	}
}
