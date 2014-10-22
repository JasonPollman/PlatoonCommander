using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {
	
	private Vector3 position;
	private GameObject enemy;
	private GameObject[] enemyList;
	private int totalEnemies;

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
		closest = new GameObject ();
		enemyList = GameObject.FindGameObjectsWithTag ("unit");
		totalEnemies = enemyList.Length;

		gameObject.GetComponent<UISprite> ().color = TurretColor;
	}
	
	// Update is called once per frame
	void Update () {
		enemyList = GameObject.FindGameObjectsWithTag ("unit");
		totalEnemies = enemyList.Length;

		enemy = FindTarget ().gameObject;
		if(totalEnemies > 0 && enemy != null){
			position = enemy.transform.position;
			Rotate();
		}
	}
	
	//Finds the closest enemy and targets it
	private GameObject FindTarget() {

		float distance = .1f;
		Vector3 thisPosition = transform.position;
		foreach (GameObject go in enemyList) {
			Vector3 diff = go.transform.position - thisPosition;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
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
		
		/*Quaternion targetLoc = Quaternion.LookRotation (position-transform.position);
		//secting the X and Z roations to zero so the player only rotaes on the Y-axis
		targetLoc.x = targetLoc.y = 0f;	
		//rotates the player from the current direction it is facing to the new location 
		transform.rotation = Quaternion.Slerp (transform.rotation, targetLoc, Time.deltaTime*8);*/
	}
	
}