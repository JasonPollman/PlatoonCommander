using UnityEngine;
using System.Collections;

public class KeypressUnits : MonoBehaviour {

	UnitButton ubScript1;
	UnitButton ubScript2;
	UnitButton ubScript3;
	UnitButton ubScript4;
	UnitButton ubScript5;
	UnitButton ubScript6;

	string last = "";

	// The original sound clip when the button is pressed.
	AudioClip OriginalClip;
	
	// The error sound clip that plays when there's no more of this unit type. 
	AudioClip ErrorClip;

	AudioSource src;

	string oldSquad = "";

	// Use this for initialization
	void Awake () {
		OriginalClip = Resources.Load ("Click2") as AudioClip;
		ErrorClip = Resources.Load ("Error") as AudioClip;
		src = gameObject.AddComponent<AudioSource>();
		src.playOnAwake = false;
		src.clip = OriginalClip;

		ubScript1 = GameObject.Find ("AddUnitPopup/Unit" + (1).ToString()).GetComponent<UnitButton>();
		ubScript2 = GameObject.Find ("AddUnitPopup/Unit" + (2).ToString()).GetComponent<UnitButton>();
		ubScript3 = GameObject.Find ("AddUnitPopup/Unit" + (3).ToString()).GetComponent<UnitButton>();
		ubScript4 = GameObject.Find ("AddUnitPopup/Unit" + (4).ToString()).GetComponent<UnitButton>();
		ubScript5 = GameObject.Find ("AddUnitPopup/Unit" + (5).ToString()).GetComponent<UnitButton>();
		ubScript6 = GameObject.Find ("AddUnitPopup/Unit" + (6).ToString()).GetComponent<UnitButton>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.inputString.ToString() != last && GameVars.PlayerReady && !GameVars.LevelWon) {

			switch(Input.inputString.ToString()) {

				case "a":
					GameVars.LastSquadKey = "Alpha";
					break;
				case "b":
					GameVars.LastSquadKey = "Beta";
					break;
				case "o":
					GameVars.LastSquadKey = "Omega";
					break;


				case "1":
					setup();
					if(ubScript1.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
				case "2":
					setup();
					if(ubScript2.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
				case "3":
					setup();
					if(ubScript3.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
				case "4":
					setup();
					if(ubScript4.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
				case "5":
					setup();
					if(ubScript5.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
				case "6":
					setup();
					if(ubScript6.OnClick()) {
						src.clip = OriginalClip;
					}
					else {
						src.clip = ErrorClip;
					}
					src.Play ();
					break;
			}

			last = Input.inputString.ToString();
		}


	}


	void setup() {

		if(GameVars.UnitNumberClicked == -1) GameVars.UnitNumberClicked = 1;

		if(oldSquad.ToLower() != GameVars.LastSquadKey.ToLower()) {

			switch(GameVars.LastSquadKey.ToLower()) {
			case "alpha":
				GameVars.UnitNumberClicked = 1;
				break;
			case "beta":
				GameVars.UnitNumberClicked = 7;
				break;
			case "omega":
				GameVars.UnitNumberClicked = 13;
				break;
			}
		}

		GameVars.SquadClicked = GameVars.LastSquadKey.ToLower();
		
		switch(GameVars.LastSquadKey) {
		case "Alpha":
			if(GameVars.UnitNumberClicked > 6) GameVars.UnitNumberClicked = 1;
			break;
		case "Beta":
			if(GameVars.UnitNumberClicked > 12) GameVars.UnitNumberClicked = 7;
			break;
		case "Omega":
			if(GameVars.UnitNumberClicked > 18) GameVars.UnitNumberClicked = 13;
			break;
		}

		oldSquad = GameVars.LastSquadKey;
	}


}
