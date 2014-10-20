using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DeployButton : MonoBehaviour {

	public string squadName = "alpha";

	private GameObject[] pathArrows;

	private GameObject selectText;

	void Awake () {
		selectText = GameObject.Find ("SelectPath");
	}

	// Update is called once per frame
	void Update () {
	}

	void OnClick () {

		if (UICamera.currentTouchID == -1 && GameVars.Squads[squadName].Count > 0) { // Only on a left-click & must have units in squad to deploy...

			// Set the squad clicked to the squadName
			GameVars.SquadDeployClicked = this;

			// Get all the path arrows on the map...
			foreach(GameObject a in GameVars.PathArrows) NGUITools.SetActive (a, true);

			// Show the "Select A Path" Sprite...
			NGUITools.SetActive(selectText, true);
		}
	}

	public void UnitFactory(List<Unit> squad, Vector3 pos, Quaternion rot) {
		StartCoroutine(StartFactory(squad, pos, rot));
	}
	
	
	public IEnumerator StartFactory(List<Unit> squad, Vector3 pos, Quaternion rot) {
		
		foreach(Unit x in squad) {

			GameObject unitObject = x.InstantiateUnit(pos, rot);
			unitObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
			
			if(x.Type.Name.ToLower() == "bomber") {
				GameVars.BomberUnit = x;
				GameVars.BomberDeployed = true;
			}
			
			yield return new WaitForSeconds(x.GameObj.GetComponent<move>().getAdjustedSpeed());
			
		}
	}
}
