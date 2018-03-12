using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysConfig : MonoBehaviour {
	public KeyCode forward, back, left, right, jump; // Touches directionnelles.

	void Start () {
		// Pour les tests.
		forward = KeyCode.Z;
		back = KeyCode.S;
		left = KeyCode.Q;
		right = KeyCode.D;
		jump = KeyCode.Space;
	}
}
