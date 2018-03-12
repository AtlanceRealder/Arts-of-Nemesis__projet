using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysConfig : MonoBehaviour {
	public KeyCode forward, back, left, right, jump; // Touches directionnelles.
	public KeyCode sprint, crouch; // Modes de déplacement.

	public float mouseSensitivity; // Sensibilité de la souris.

	void Start () {
		// Assignation pour les tests.
		forward = KeyCode.Z;
		back = KeyCode.S;
		left = KeyCode.Q;
		right = KeyCode.D;
		jump = KeyCode.Space;

		sprint = KeyCode.R;
		crouch = KeyCode.T;

		mouseSensitivity = 10;
	}
}
