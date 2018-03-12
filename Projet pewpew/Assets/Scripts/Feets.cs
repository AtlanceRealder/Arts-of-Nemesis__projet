using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feets : MonoBehaviour {

	PlayerController player;

	void Start () {
		player = transform.parent.GetComponent<PlayerController> ();
	}

	void OnTriggerEnter (Collider coll) {
		player.isTouchingTheGround = true;
	}

	void OnTriggerExit (Collider coll) {
		player.isTouchingTheGround = false;
	}
}
