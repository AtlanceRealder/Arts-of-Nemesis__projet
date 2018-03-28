using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feets : MonoBehaviour {

	PlayerController player;

	void Start () {
		player = transform.parent.GetComponent<PlayerController> ();
	}

	void OnTriggerEnter (Collider coll) { // Notifie PlayerController que le joueur est en contact avec le sol.
		player.isTouchingTheGround = true;
	}

	void OnTriggerExit (Collider coll) { // Notifie PlayerController que le joueur n'est pas en contact avec le sol.
		player.isTouchingTheGround = false;
	}
}