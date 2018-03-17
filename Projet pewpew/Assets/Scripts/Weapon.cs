using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
	public Transform arm, head, aimer;
	// fireDelay définit le temps requis entre chaque tirs; reloadTime le temps requis pour recharger. (Tous deux en secondes).
	public float fireDelay, reloadTime;
	// bodyDamages et headDamages définissent les dégats infligés lors de touches au corps ou à la tête.
	// range définit la portée de l'arme, recoil le recul et dispersion l'angle maximal de dispersion sur un axe.
	public int bodyDamages, headDamages, range, recoil, dispersion;
	// magazinesRemaining définit le nombre de chargeurs restants. magazineCapacity définit le nombre de munitions dans un chargeur.
	public int magazinesRemaining, magazineCapacity;
	// Définit si arme de melée ou arme à feu.
	public bool meleeWeapon;

	public AudioClip fireAudio, reloadAudio, bulletShellAudio;

	private bool canFire;
	private int bulletsRemaining; // The amount of remaining bullets in the currently used magazine.
	private AudioSource audioSource;

	public GameObject prefabTest;

	private Text ammo, mag;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		arm = transform.parent.parent;
		head = arm.parent.parent;
		aimer = transform.GetChild (0);

		canFire = true;

		// Affichage HUD de test.
		ammo = GameObject.Find ("INFOammo").GetComponent<Text> ();
		mag = GameObject.Find ("INFOmag").GetComponent<Text> ();

		bulletsRemaining = magazineCapacity;
		UpdateHUD ();
	}

	// Update les infos du HUD.
	void UpdateHUD () {
		ammo.text = bulletsRemaining + "";
		mag.text = magazinesRemaining + "";
	}

	// Recharge.
	public IEnumerator Reload () {
		if (!meleeWeapon && canFire && magazinesRemaining > 0) { // Si il reste des chargeurs et que l'arme n'est pas de melêe.
			canFire = false;
			magazinesRemaining--;
			bulletsRemaining = 0;
			UpdateHUD ();

			audioSource.PlayOneShot (reloadAudio);

			yield return new WaitForSeconds (reloadTime);
			bulletsRemaining = magazineCapacity;
			UpdateHUD ();
			canFire = true;
		}
	}

	// Tire.
	public IEnumerator Shoot () {
		if (canFire && bulletsRemaining > 0) { // Si il reste des balles.
			canFire = false;

			// Audio.
			audioSource.PlayOneShot (fireAudio);
			audioSource.PlayOneShot (bulletShellAudio);

			RaycastHit hit, hit2;
			Camera cam = head.GetComponent<Camera> ();
			Ray ray = cam.ScreenPointToRay (new Vector3 (cam.pixelWidth / 2, cam.pixelHeight / 2, 0));

			Vector3 disp = new Vector3 (Random.Range (0, dispersion), Random.Range (0, dispersion), 0); // Système de dispersion a chier parce que fait en 5min a 3h du matin.
			ray.direction += disp;

			if (Physics.Raycast (ray, out hit, range)) {
				Vector3 heading = hit.point - aimer.transform.position, dir = heading / heading.magnitude;

				if (Physics.Raycast (aimer.position, dir, out hit2)) {
					// Pan! Touché!
				}

				Debug.DrawLine (aimer.position, hit2.point, Color.red, 20f);
			}

			bulletsRemaining--;
			UpdateHUD ();

			head.transform.Rotate (Random.Range (-recoil * 1.5f, -recoil), 0, 0);
			head.parent.Rotate (0, Random.Range (-recoil * 1.5f, recoil * 1.5f), 0);

			yield return new WaitForSeconds (fireDelay);
			canFire = true;
		}
	}
}
