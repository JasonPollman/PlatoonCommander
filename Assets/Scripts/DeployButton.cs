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


	void OnClick () {

		if (UICamera.currentTouchID == -1 && GameVars.Squads[squadName].Length > 0) { // Only on a left-click & must have units in squad to deploy...

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
	public void UnitFactory(Unit[] squad, Vector3 pos, Quaternion rot) {
		StartCoroutine(StartFactory(squad, pos, rot));
	}
	
	/**
	 * Coroutine for UnitFactory
	 */
	public IEnumerator StartFactory(Unit[] squad, Vector3 pos, Quaternion rot) {
		
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

	} // End StartFactory()

} // End DeployButton Class
