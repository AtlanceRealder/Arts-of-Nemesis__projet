using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {
	// 'fireDelay' définit le temps requis entre chaque tirs; 'reloadTime' le temps requis pour recharger. (Tous deux en secondes).
	public float fireDelay, reloadTime;
	// 'bodyDamages' et 'headDamages' définissent les dégats infligés lors de touches au corps ou à la tête. 'range' définit la portée de l'arme. 'bulletsPerShot' le nombre de projectiles tirés par tir.
	public int bodyDamages, headDamages, range, bulletsPerShot;
	// 'recoil' définit le recul, 'dispersion' l'angle maximal de dispersion sur un axe, et 'bulletDrop' la retombée de la balle en fonction de la distance.
	public float recoil, dispersion, bulletDrop;
	// 'magazinesRemaining' définit le nombre de chargeurs restants. 'magazineCapacity' définit le nombre de munitions dans un chargeur.
	public int magazinesRemaining, magazineCapacity;
	// Définit si arme de melée ou arme à feu.
	public bool meleeWeapon;

	public AudioClip fireAudio, reloadAudio, bulletShellAudio;

	private Transform head, aimer; // Les transforms des différentes parties du corps utiles au tir.

	private bool canFire;
	private int bulletsRemaining; // Le nombre de balles restantes dans le chargeur en cours d'utilisation.
	private int currentRecoil = 1; // Le coefficient de recul actuel.

	private AudioSource audioSource;

	// HUD de tests.
	private Text ammo, mag;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		head = transform.parent.parent.parent.parent;
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

			if (Physics.Raycast (ray, out hit, range)) { // Créer un Raycast pour récupérer le point visé par le joueur.
				for (int i = 0; i < bulletsPerShot; i++) {
					// En fonction du point récupéré, calcule la direction de ce point par rapport au bout du canon de l'arme: 'dir'.
					Vector3 heading = hit.point - aimer.transform.position, dir = heading / heading.magnitude;

					float currentBulletDrop = Vector3.Distance (aimer.position, hit.point) * bulletDrop / 100;

					// Calcule la dispersion.
					Vector3 disp = new Vector3 (Random.Range (0, dispersion) / 50, Random.Range (0, dispersion) / 50 - currentBulletDrop, Random.Range (0, dispersion) / 50);

					// La direction du tir prenant en compte la dispersion.
					Vector3 dispDir = dir + disp;


				
					if (Physics.Raycast (aimer.position, dispDir, out hit2)) {
						// Quelque chose est touché.
					}
				
					Debug.DrawLine (aimer.position, hit2.point, Color.red, 20f); // Debug le tir.
					Debug.Log(hit2.point + "  " + disp + "  " + dir + "  " + dispDir);
				}
			}
				
			bulletsRemaining--; // Enlève la balle tirée du chargeur.
			UpdateHUD (); // Met a jour le HUD.

			// Applique le l'effet de recul.
			head.transform.Rotate (Random.Range (0, -1 * recoil * currentRecoil * 0.5f), 0, 0);
			head.parent.Rotate (0, Random.Range (-1 * recoil * currentRecoil * 0.5f, 1 * recoil * currentRecoil * 0.5f), 0);

			if (currentRecoil < 5) // Ajoute du recul.
				currentRecoil++;

			yield return new WaitForSeconds (fireDelay);
			canFire = true;

			yield return new WaitForSeconds (0.8f);

			if (currentRecoil > 1) // Enlève du recul.
				currentRecoil--;
		}
	}
}