using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public KeysConfig cfg;
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			// Mouvement.
			if (Input.GetKey (cfg.forward)) {

			}
			if (Input.GetKey (cfg.back)) {

			}
			if (Input.GetKey (cfg.left)) {

			}
			if (Input.GetKey (cfg.right)) {

			}
			if (Input.GetKey (cfg.jump)) {

			}
		}
	}
}
