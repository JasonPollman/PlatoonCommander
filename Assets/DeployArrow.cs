using UnityEngine;
using System.Collections;

public class DeployArrow : MonoBehaviour {

	private GameObject selectText;

	// Use this for initialization
	void Start () {
		// Hide the Arrow, until a User Clicks Deploy Squad...
		GameVars.PathArrows.Add (gameObject);
		NGUITools.SetActive (gameObject, false);
	}

	void Awake () {
		selectText = GameObject.Find ("SelectPath");
	}
	
	// Update is called once per frame
	void Update () {

		// Cancel on right click...
		if (Input.GetMouseButtonDown (1)) {
			NGUITools.SetActive (gameObject, false);
		}
	
	}

	IEnumerator Wait1Sec () {
		yield return new WaitForSeconds(1);
	}
	
	void OnClick () {

		if (UICamera.currentTouchID == -1) { // Only on a left-click

			// Determine the "Deploy" Button Clicked
			string whichSquad = GameVars.SquadDeployClicked.squadName;

			// If Null, return.
			if(GameVars.SquadDeployClicked == null) return;

			// If the squad name isn't "alpha", "beta", or "omega" throw an exception...
			if(GameVars.Squads[whichSquad] == null) throw new UnityException("Unknown Squad");

			// Instantiate the squad's units...
			if(GameVars.Squads[whichSquad].Count > 0) {

				foreach(Unit x in GameVars.Squads[whichSquad]) {
					Wait1Sec();
					x.InstantiateUnit(gameObject.transform.position, new Quaternion(0,0,0,0));
					if(x.Type.Name.ToLower() == "bomber") GameVars.BomberDeployed = true;
				}

				// Disable the deploy button now,
				GameVars.SquadDeployClicked.GetComponent<UIImageButton>().isEnabled = false;

				// Now hide the Arrows...
				foreach(GameObject g in GameVars.PathArrows) NGUITools.SetActive (g, false);

				int squadStartI = 0;

				switch(whichSquad) {
					case "alpha":
						squadStartI = 1;
						break;
					case "beta":
						squadStartI = 7;
						break;
					case "omega":
						squadStartI = 13;
						break;
				}

				// Now disable the add buttons for this squad
				for(int i = squadStartI; i <= GameVars.SquadMaxUnits + squadStartI - 1; i++) {
					UIImageButton plusButton = GameObject.Find ("AddUnit" + i).GetComponent<UIImageButton>();

					if (plusButton.disabledSprite == "AddUnit") {
						plusButton.disabledSprite = "NoAdd";
					}

					plusButton.isEnabled = false;
				}


				// Now hide the "Select A Path" Sprite...
				NGUITools.SetActive(selectText, false);
			}

		} // End if left click

	} // End OnClick
}
