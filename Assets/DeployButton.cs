using UnityEngine;
using System.Collections;

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

		if (UICamera.currentTouchID == -1) { // Only on a left-click

			// Set the squad clicked to the squadName
			GameVars.SquadDeployClicked = this;

			// Get all the path arrows on the map...
			foreach(GameObject a in GameVars.PathArrows) NGUITools.SetActive (a, true);

			// Show the "Select A Path" Sprite...
			NGUITools.SetActive(selectText, true);
		}
	}
}
