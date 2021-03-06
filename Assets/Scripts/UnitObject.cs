﻿using UnityEngine;
using System.Collections;

public class UnitObject : MonoBehaviour {

	// Is the unit alive?
	public bool Alive = true;

	// The unit's HP as defined by the UnitType class
	public int HP;

	// The unit's DP as defined by the UnitType class
	public int DP;

	// The unit's type
	public string Type;

	// The UI Button which correlates to this unit (The Add Squad Button)...
	public GameObject UnitButton;

	public float speed = -1f;
	private Vector3 lastPosition = Vector3.zero;

	
	void Update() {

		// Update the HP & DP on the Unit's GUI Button
		if(UnitButton != null) {

			UILabel UnitHPLabel = GameObject.Find (UnitButton.name + "/HPLabel").GetComponent<UILabel>();
			UILabel UnitDPLabel = GameObject.Find (UnitButton.name + "/DPLabel").GetComponent<UILabel>();
			
			UnitHPLabel.text = "HP :" + HP;
			UnitDPLabel.text = "DP :" + DP;

			UnitHPLabel.MakePixelPerfect();
			UnitDPLabel.MakePixelPerfect();

			UnitHPLabel.MarkAsChanged();
			UnitDPLabel.MarkAsChanged();

		} // End if block

		speed = gameObject.rigidbody2D.velocity.magnitude;

	} // End Update()


	/**
	 * Kills the unit
	 */
	public void KillUnit () {
	
		// So the HP & DP will update again before this object is set inactive...
		Update ();

		// Add the KIA sprite to the UnitButton
		GameObject KIA = Instantiate (Resources.Load<GameObject>("BlankSprite")) as GameObject;
		KIA.name = "KIASprite";
		KIA.transform.parent = UnitButton.transform;
		KIA.transform.position = UnitButton.transform.position;
		KIA.transform.localPosition = new Vector3 (0, 35, 0);
		UISprite KIASprite = KIA.GetComponent<UISprite> ();
		KIASprite.spriteName = "KIA";
		KIASprite.depth = 60;
		KIASprite.MakePixelPerfect ();
		NGUITools.SetActive (KIA, true);

		// Gray out the BKG Sprites...
		GameObject.Find (UnitButton.name + "/UnitBKG").GetComponent<UISprite> ().color = new Color (.4f, .4f, .4f);
		GameObject.Find (UnitButton.name + "/HPLabel").GetComponent<UILabel>  ().color = new Color (.6f, .6f, .6f);
		GameObject.Find (UnitButton.name + "/DPLabel").GetComponent<UILabel>  ().color = new Color (.6f, .6f, .6f);


		// Notify the player that the unit has been killed...
		Console.Push ("Unit " + GameVars.UCFirst(Type) + " has been killed!");
	
		// Set alive to false
		Alive = false;

		// Deactivate the game object...
		NGUITools.SetActive (gameObject, false);

	} // End KillUnit()


	/**
	 * Removes HP from the unit
	 * @param hp - The amount of hp to take.
	 * @param towerType - The type of tower dealing the damage.
	 * */
	public void TakeHit (int hp, string towerType, TurretControl turretControl) {


		// Flash the unit's button red on hit
		GameObject.Find (UnitButton.name + "/UnitBKG").GetComponent<UISprite> ().color = new Color (.9f, .1f, .1f);
		StartCoroutine(ChangeLabelColorBack());

		// Push a notification to the console:
		Console.Push ("Unit " + GameVars.UCFirst(Type) + " has been hit!");

		Debug.Log(Type);
		// We will use this for special abilities. ;)
		switch(towerType) {

			case "Flamethrower":



				if(Type == "firefighter") {
					Debug.Log ("HERE");
					turretControl.RotationSpeed = 0;
					turretControl.HitPercentage = 0;
					turretControl.HPOnHit = 0;
					turretControl.Range = 0;
					turretControl.setTowerTint(new Color(.3f, .3f, .3f));
				}

				break;

			default:
				break;

		} // End switch

		if(HP > 0) StartCoroutine(TakeHP(hp));
	
	} // End TakeHit()


	IEnumerator ChangeLabelColorBack() {
		yield return new WaitForSeconds(.1f);
		GameObject.Find (UnitButton.name + "/UnitBKG").GetComponent<UISprite> ().color = Color.white;
	}

	IEnumerator TakeHP(int hp) {
		yield return new WaitForSeconds(.2f);
		HP -= hp;
		// Kill the unit if it's HP <= 0...
		if(HP <= 0) KillUnit();
	}


} // End UnitObject Class
