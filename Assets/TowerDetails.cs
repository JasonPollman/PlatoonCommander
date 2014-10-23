using UnityEngine;
using System.Collections;

public class TowerDetails : MonoBehaviour {

	private GameObject Tower, TD, ROFL, HPDealtL, HitPercentL, RangeL, TypeL, NameL, RotationL;

	// Use this for initialization
	void Awake () {

		Tower = gameObject.transform.parent.gameObject;

		TD = GameObject.Find (Tower.name + "/TowerDetails");

		ROFL 		= GameObject.Find (Tower.name + "/" + TD.name + "/RateOfFire");
		HPDealtL 	= GameObject.Find (Tower.name + "/" + TD.name + "/HPDealt");
		HitPercentL = GameObject.Find (Tower.name + "/" + TD.name + "/HitPercentage");
		RangeL 		= GameObject.Find (Tower.name + "/" + TD.name + "/AttackRange");
		TypeL		= GameObject.Find (Tower.name + "/" + TD.name + "/Projectile");
		NameL		= GameObject.Find (Tower.name + "/" + TD.name + "/TowerName");
		RotationL 	= GameObject.Find (Tower.name + "/" + TD.name + "/Rotation");
	}

	void Start () {

		ROFL.GetComponent<UILabel> ().text 			= "Rate of Fire: " 		+ (gameObject.GetComponent<TurretControl> ().TimeBetweenShotsInSec * 100).ToString();
		HPDealtL.GetComponent<UILabel> ().text 		= "HP Dealt on Hit: " 	+ gameObject.GetComponent<TurretControl> ().HPOnHit.ToString();
		HitPercentL.GetComponent<UILabel> ().text 	= "Hit Percentage: " 	+ (gameObject.GetComponent<TurretControl> ().HitPercentage * 100).ToString () + "%";
		RangeL.GetComponent<UILabel> ().text 		= "Attack Range: " 		+ (gameObject.GetComponent<TurretControl> ().Range * 100).ToString() + "M";
		TypeL.GetComponent<UILabel> ().text 		= "Projectile Type: " 	+ gameObject.GetComponent<TurretControl> ().ProjectileType.ToString();
		NameL.GetComponent<UILabel> ().text 		= gameObject.GetComponent<TurretControl> ().TowerType.ToString();
		RotationL.GetComponent<UILabel> ().text 	= "Rotation Speed: " + gameObject.GetComponent<TurretControl> ().RotationSpeed.ToString();

		NGUITools.SetActive (TD, false);

	}
	
	void OnHover(bool hovering) {

		Debug.Log ("HERE");

		if(hovering) {
			NGUITools.SetActive (TD, true);
		}
		else {
			NGUITools.SetActive (TD, false);
		}
	}
}
