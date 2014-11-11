using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {

	// The enemy to fire at
	private GameObject enemy = null;

	// The list of objects tagged with "unit".
	private GameObject[] enemyList;

	// The "flames" GameObject which holds the "flames" sprite.
	private GameObject fire1, fire2, fireEffect, hit, miss;

	// The total number of objects tagged with "unit" on the map.
	private int totalEnemies;

	// The UnitObject script on the closest unit
	private UnitObject UO;

	public GameObject end;

	// The Rotation Speed of the Tower
	public float RotationSpeed;

	// The "boom" sound effect made on fire...
	public AudioClip sound;

	// The "Projectile Type"
	public string ProjectileType;

	// Whether or not the closest unit is in range.
	private bool inRange;

	// The Turret Color
	public Color TurretColor = new Color (104, 255, 0);

	// The FireRate...
	public float TimeBetweenShotsInSec;


	// The Hit Ratio
	public float HitPercentage;

	// The tower's range
	public float Range;

	// How many HP's a single hit will deal. Also for the future...
	public int HPOnHit;

	// The tower's type
	public string TowerType;
	public int TowerClass;

	// Don't shoot while rotating!
	private bool RotationReady = false;
	private Quaternion RotQ = new Quaternion (0, 0, 0, 0);

	private GameObject closest;
	
	void Awake () {
		fire1 = gameObject.transform.Find ("SpriteFire_1").gameObject;
		fire2 = gameObject.transform.Find ("SpriteFire_2").gameObject;
		NGUITools.SetActive (fire1, false);
		NGUITools.SetActive (fire2, false);
		GetTowerType ();
		end = GameObject.Find ("Target");
		closest = null;
		enemyList = GameObject.FindGameObjectsWithTag ("unit");
		hit = gameObject.transform.parent.transform.Find("Hit").gameObject;
		miss = gameObject.transform.parent.transform.Find("Miss").gameObject;
		NGUITools.SetActive (fireEffect, false);
		NGUITools.SetActive (hit, false);
		NGUITools.SetActive (miss, false);
		totalEnemies = enemyList.Length;
		gameObject.transform.Find("Background").GetComponent<UISprite>().color = TurretColor;
		InvokeRepeating("Shoot", 1, TimeBetweenShotsInSec);
		InvokeRepeating("Fire", 1.1f, TimeBetweenShotsInSec);

	} // End Start()

	public void setTowerTint(Color color) {
		gameObject.transform.Find("Background").GetComponent<UISprite>().color = color;
	}

	void GetTowerType(){

		//Detects the tower type and sets the variables accordingly
		switch (TowerClass) {
		case 1:
			RotationSpeed = 5;
			ProjectileType = "Bullets";
			TurretColor = new Color (1, .85f, .6f);
			TimeBetweenShotsInSec = 0.15f;
			HitPercentage = 0.2f;
			Range = 0.1f;
			HPOnHit = 1;
			sound = (AudioClip)Resources.Load ("MachineGun");
			audio.clip = sound;
			fireEffect = fire1;
			TowerType = "Machine Gunner";
			break;
		case 2:
			RotationSpeed = 3;
			ProjectileType = "Shells";
			TurretColor = new Color (1f, .55f, 1f);
			TimeBetweenShotsInSec = 5.0f;
			HitPercentage = 0.75f;
			Range = 1f;
			HPOnHit = 2;
			sound = (AudioClip)Resources.Load ("Boom");
			audio.clip = sound;
			fireEffect = fire1;
			TowerType = "Cannon";
			break;
		case 3:
			RotationSpeed = 6;
			ProjectileType = "Bullets";
			TurretColor = new Color (1, .85f, .6f);
			TimeBetweenShotsInSec = 0.5f;
			HitPercentage = 0.6f;
			Range = 0.25f;
			HPOnHit = 3;
			sound = (AudioClip)Resources.Load ("RifleShot");
			audio.clip = sound;
			fireEffect = fire1;
			TowerType = "Rifleman";
			break;
		case 4:
			RotationSpeed = 7;
			ProjectileType = "Fire";
			TurretColor = new Color (1f, .2f, .2f);
			TimeBetweenShotsInSec = 1.2f;
			HitPercentage = .8f;
			Range = 0.1f;
			HPOnHit = 100;
			sound = (AudioClip)Resources.Load ("Flamethrower");
			audio.clip = sound;
			fireEffect = fire2;
			TowerType = "Flamethrower";
			break;
		}
	}
	
	void Update () {

		// Make sure we don't fire while rotating
		if((RotQ.eulerAngles - transform.rotation.eulerAngles).sqrMagnitude < Range * 1000) {
			RotationReady = true;
		}
		else {
			RotationReady = false;
		}

		// Grab all the units on the map
		enemyList = GameObject.FindGameObjectsWithTag ("unit");

		// The number of units on the map
		totalEnemies = enemyList.Length;

		if(totalEnemies > 0){

			// Find the closest one...
			enemy = FindTarget () as GameObject;

			if(enemy != null) { // If there is a closest one...

				// Rotate towards the unit
				RotQ = Rotate();

				// Get the unit's script
				UO = enemy.GetComponent<UnitObject>();

			} // End if block
		}
		else {
			enemy = null; // No units were found...
		}

	} // End Update()


	/**
	 * Finds the closest enemy and targets it...
	 */
	private GameObject FindTarget() {

		// Set the unit out of range
		inRange = false;

		// ------------------------ ERIK'S ALGORITHM FOR FINDING THE CLOSEST ENEMY AND LOCKING ON ------------------------ //

		float distanceBetween = Mathf.Infinity;
		Vector3 thisPosition = transform.position;

		foreach (GameObject go in enemyList) { // Iterate through the enemyList to find the closest

			Vector3 diff = go.transform.position - thisPosition;
			Vector3 distanceToTarget = end.transform.position - go.transform.position;
			float curDistance = diff.sqrMagnitude;
			float tempDistance = distanceToTarget.sqrMagnitude;

			if (curDistance < Range && tempDistance < distanceBetween) {
				closest = go;
				inRange = true;
				distanceBetween = tempDistance;
			}

		} // End foreach loop

		return closest;

	} // End FindTarget()


	/**
	 * Rotates to face the target
	 */
	Quaternion Rotate() {

		Vector3 vectorToTarget = enemy.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);

		return q;

	} // End Rotate()


	/**
	 * "Shoots" the target, reducing its HP
	 * */
	void Shoot(){

		float rand = Random.Range(0f, 1f);

		if(totalEnemies > 0 && enemy != null && inRange == true && RotationReady == true){

			// Turn on the "cannon flames"
			NGUITools.SetActive (fireEffect, true);

			// Play the "boom" clip
			audio.PlayOneShot (sound);

			if(rand <= HitPercentage) {

				// Get the UnitObject script
				UO = enemy.GetComponent<UnitObject>();

				// Make the unit take the hit
				UO.TakeHit (HPOnHit, TowerType, gameObject.GetComponent<TurretControl>());

				Debug.Log (gameObject.transform.parent.name);
				NGUITools.SetActive(hit, true);
				StartCoroutine(Hide(hit));
			}
			else {
				NGUITools.SetActive(miss, true);
				StartCoroutine(Hide(miss));
			}

		} // End if block

	} // End Shoot()

	IEnumerator Hide(GameObject o) {
		yield return new WaitForSeconds (TimeBetweenShotsInSec - .1f);
		NGUITools.SetActive(o, false);
	}
	void Fire(){ 
		NGUITools.SetActive (fireEffect, false); 
	}

} // End TurretControl Class
