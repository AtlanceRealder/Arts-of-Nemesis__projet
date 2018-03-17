using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysConfig : MonoBehaviour {
	// Commandes du clavier et de la souris.
	public KeyCode forward, back, left, right, jump; // Déplacement.
	public KeyCode sprint, crouch; // Modes de déplacement.
	public KeyCode wpnFire, wpnReload, wpnAim; // Armes.

	public float mouseSensitivity; // Sensibilité de la souris.

	void Start () {
		// Lock le curseur au milieu de la fenêtre et le cache.
		Cursor.lockState = CursorLockMode.Locked;

		// Assignation pour les tests.
		// Les contrôles seront sûrement contenus dans un fichier config et modifiables IG.
		forward = KeyCode.Z;
		back = KeyCode.S;
		left = KeyCode.Q;
		right = KeyCode.D;
		jump = KeyCode.Space;

		sprint = KeyCode.X;
		crouch = KeyCode.W;

		wpnFire = KeyCode.Mouse0;
		wpnAim = KeyCode.Mouse1;
		wpnReload = KeyCode.R;

		mouseSensitivity = 10;
	}
}
