using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public KeysConfig cfg; // La config des touches.
	private Transform head, arm; // Les Transforms de la tête (caméra principale) et du bras (pour les armes) du player.

	public float walkSpeed, crouchSpeed, sprintSpeed, jumpPower; // Les vitesses en mode normal, accroupi, sprint et la puissance du saut.
	private float speed; // La vitesse actuelle.

	public bool isTouchingTheGround; // Si le player touche le sol (via Feets).

	void Start () {
		head = transform.Find ("Head");
		arm = head.GetChild (0).GetChild (0);
	}

	void Update () {
		if (Input.anyKey)
			MovePlayer ();

		MoveCamera ();
	}

	void MoveCamera () { // Oriente la caméra en fonction des mouvements de la souris.
		float speed = cfg.mouseSensitivity * Time.deltaTime;

		transform.Rotate (0, Input.GetAxis ("Mouse X") * speed, 0); // Pivote le corps du joueur sur l'axe Y.
		head.Rotate (-Input.GetAxis ("Mouse Y") * speed, 0, 0); // Pivote la tête du joueur sur l'axe X.
	}

	void MovePlayer () {
		if (Input.GetKeyDown (cfg.wpnReload))
			StartCoroutine (arm.GetChild (0).GetChild (0).GetComponent<Weapon> ().Reload ()); // Recharge.
		else if (Input.GetKey (cfg.wpnFire))
			StartCoroutine (arm.GetChild (0).GetChild (0).GetComponent<Weapon> ().Shoot ()); // Tire.

		// Modes de mouvement.
		if (isTouchingTheGround) {
			if (Input.GetKey (cfg.crouch)) {
				// Accroupi
				speed = crouchSpeed * Time.deltaTime;
			} else if (Input.GetKey (cfg.sprint)) {
				// Sprint
				speed = sprintSpeed * Time.deltaTime;
			} else {
				// Marche
				speed = walkSpeed * Time.deltaTime;
			}
		}

		// Mouvement.
		if (Input.GetKey (cfg.forward)) {
			// Avant
			transform.Translate (Vector3.forward * speed);
		}
		if (Input.GetKey (cfg.back)) {
			// Arrière
			transform.Translate (Vector3.back * speed);
		}
		if (Input.GetKey (cfg.left)) {
			// Gauche
			transform.Translate (Vector3.left * speed);
		}
		if (Input.GetKey (cfg.right)) {
			// Droite
			transform.Translate (Vector3.right * speed);
		}
		if (Input.GetKeyDown (cfg.jump) && isTouchingTheGround) {
			// Saut
			GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpPower, ForceMode.Impulse);
		}
	}
}