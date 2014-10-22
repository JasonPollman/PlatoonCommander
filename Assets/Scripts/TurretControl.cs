using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
	
	private Vector3 position;
	private GameObject enemy = null;
	private GameObject[] enemyList;
	private GameObject flames;
	private int totalEnemies;
	private UnitObject uo;
	public GameObject end;
	public AudioClip boom;
	private bool inRange;

	// Added a few public variables so that we can easily change the Tower Prefab...

	// The Turret Color
	public Color TurretColor = new Color (104, 255, 0);

	// The FireRate... Alan you can do something with this and different Tower Types
	// in the future...
	public int FireSpeed = 1;

	// How many HP's a single hit will deal. Also for the future...
	public int HPOnHit = 1;

	private GameObject closest;

	// Use this for initialization
	void Start () {
		closest = null;
		enemyList = GameObject.FindGameObjectsWithTag ("unit");
		flames = GameObject.Find ("SpriteFire_1");
		NGUITools.SetActive (flames, false);
		totalEnemies = enemyList.Length;
		gameObject.GetComponent<UISprite> ().color = TurretColor;
		InvokeRepeating("Shoot", 1, 1);
		InvokeRepeating ("Fire", 1.1f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		enemyList = GameObject.FindGameObjectsWithTag ("unit");
		totalEnemies = enemyList.Length;
		if(totalEnemies > 0){
			enemy = FindTarget () as GameObject;
			if(enemy != null) {
				position = enemy.transform.position;
				Rotate();
				uo = enemy.GetComponent<UnitObject>();
			}
		}
		else {
			enemy = null;
		}
	}
	//Finds the closest enemy and targets it
	private GameObject FindTarget() {

		inRange = false;

		float distance = 0.1f; //May have to play with the distance, default is 0.4f but I changed it for testing on my laptop-Erik
		float distanceBetween = Mathf.Infinity;
		Vector3 thisPosition = transform.position;
		foreach (GameObject go in enemyList) {
			Vector3 diff = go.transform.position - thisPosition;
			Vector3 distanceToTarget = end.transform.position - go.transform.position;
			float curDistance = diff.sqrMagnitude;
			float tempDistance = distanceToTarget.sqrMagnitude;
			if (curDistance < distance && tempDistance < distanceBetween) {
				closest = go;
				inRange = true;
				distanceBetween = tempDistance;
			}
		}

		return closest;
	}
	//Rotates to face the target
	void Rotate() {
		Vector3 vectorToTarget = enemy.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 8);
	}
	void Shoot(){

		if(totalEnemies > 0 && enemy != null && inRange == true){
			uo = enemy.GetComponent<UnitObject>();
			audio.PlayOneShot (boom);
			NGUITools.SetActive (flames, true);
			uo.TakeHit ();
			Console.Push ("Unit " + uo.Type.ToString()+ " was hit!");
		}
	}
	void Fire(){
			NGUITools.SetActive (flames, false);
	}
}