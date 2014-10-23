using UnityEngine;
using System.Collections;

public class AddUnitToSquad : MonoBehaviour {

	private GameObject AddUnitPanel;
	public int UnitNumber;
	public string Squad;

	void Awake () {

		// Get the add unit popup window
		AddUnitPanel = GameObject.Find ("AddUnitPopup");
	}
	
	// Update is called once per frame
	void Update () {

		// If the game isn't in play don't let users add units to squads (e.g. disable the buttons)
		if(GameVars.GameInPlay == false) gameObject.GetComponent<UIImageButton>().isEnabled = false;
	}

	void OnClick() {

		// Set the UnitNumberClicked
		GameVars.UnitNumberClicked = UnitNumber;

		// Set the squad that was clicked
		GameVars.SquadClicked = Squad;

		// If the player has clicked the ready box, popup the add unit context menu...
		AddUnitPanel.transform.position = new Vector3(gameObject.transform.position.x + .3f, AddUnitPanel.transform.position.y, AddUnitPanel.transform.position.z);
		if(GameVars.PlayerReady) NGUITools.SetActive(AddUnitPanel, true);

	} // End OnClick()
	
} // End AddUnitToSquad class
