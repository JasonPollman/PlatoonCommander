using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DeployButton : MonoBehaviour {

	// The squad name
	public string squadName = "alpha";

	// The arrows that you click to deploy a squad (GameObject)
	private GameObject[] pathArrows;

	// The "Select A Path" game obkect
	private GameObject selectText;


	void Awake () {

		// Get the "Select A Path" object
		selectText = GameObject.Find ("SelectPath");
	}

	void Update () {
		// If the game isn't in play don't let users add units to squads (e.g. disable the buttons)
		if(GameVars.GameInPlay == false) {
			gameObject.GetComponent<UIImageButton>().isEnabled = false;
		}
	}


	void OnClick () {

		if (GameVars.PlayerReady && UICamera.currentTouchID == -1 && GameVars.Squads[squadName].Length > 0) { // Only on a left-click & must have units in squad to deploy...

			// Set the squad clicked to the squadName
			GameVars.SquadDeployClicked = this;

			// Get all the path arrows on the map...
			foreach(GameObject a in GameVars.PathArrows) NGUITools.SetActive (a, true);

			// Show the "Select A Path" Sprite...
			NGUITools.SetActive(selectText, true);
		}

	} // End OnClick()

	/**
	 * Pumps out units one by one when deployed
	 */
	public void UnitFactory(int path, Unit[] squad, Vector3 pos, Quaternion rot) {
		StartCoroutine(StartFactory(path, squad, pos, rot));
	}
	
	/**
	 * Coroutine for UnitFactory
	 */
	public IEnumerator StartFactory(int path, Unit[] squad, Vector3 pos, Quaternion rot) {

		if(!GameVars.DeployingPath.ContainsKey(path)) {
			GameVars.DeployingPath.Add (path, true);
		}
		else {
			GameVars.DeployingPath[path] = true;
		}
		
		foreach(Unit x in squad) {

			// Can't instantiate a null unit
			if(x == null) continue; 

			// Instantiate the unit
			GameObject unitObject = x.InstantiateUnit(pos, rot);

			// Set it's initial rotation
			unitObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);

			// If the unit is the bomber, we need to set some additional values.
			if(x.Type.Name.ToLower() == "bomber") {
				GameVars.BomberUnit = x;
				GameVars.BomberDeployed = true;
			}
			
			yield return new WaitForSeconds(x.GameObj.GetComponent<move>().getAdjustedSpeed());
			
		} // End foreach loop

		GameVars.DeployingPath[path] = false;

	} // End StartFactory()

} // End DeployButton Class
