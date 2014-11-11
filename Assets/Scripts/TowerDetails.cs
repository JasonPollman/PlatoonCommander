using UnityEngine;
using System.Collections;

public class TowerDetails : MonoBehaviour {
	
	private GameObject Tower, TD, ROFL, HPDealtL, HitPercentL, RangeL, TypeL, NameL, RotationL;
	
	// Use this for initialization
	void Awake () {
		
		TD = gameObject.transform.parent.transform.Find ("TowerDetails").gameObject;
		
		ROFL 		= gameObject.transform.parent.transform.Find("TowerDetails/RateOfFire").gameObject;
		HPDealtL 	= gameObject.transform.parent.transform.Find("TowerDetails/HPDealt").gameObject;
		HitPercentL = gameObject.transform.parent.transform.Find("TowerDetails/HitPercentage").gameObject;
		RangeL 		= gameObject.transform.parent.transform.Find("TowerDetails/AttackRange").gameObject;
		TypeL		= gameObject.transform.parent.transform.Find("TowerDetails/Projectile").gameObject;
		NameL		= gameObject.transform.parent.transform.Find("TowerDetails/TowerName").gameObject;
		RotationL 	= gameObject.transform.parent.transform.Find("TowerDetails/Rotation").gameObject;
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
		
		if(hovering && GameVars.PlayerReady) {
			NGUITools.SetActive (TD, true);
		}
		else {
			NGUITools.SetActive (TD, false);
		}
	}
}
