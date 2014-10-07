using UnityEngine;
using System.Collections;

public class AddUnitToSquad : MonoBehaviour {

	private GameObject AddUnitPanel;
	public int UnitNumber;
	public string Squad;

	// Use this for initialization
	void Start () {
	}

	void Awake () {
		AddUnitPanel = GameObject.Find ("AddUnitPopup");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnClick() {

		GameVars.UnitNumberClicked = UnitNumber;
		GameVars.SquadClicked = Squad;

		// If the player has clicked the ready box, popup the add unit context menu...
		AddUnitPanel.transform.position = new Vector3(gameObject.transform.position.x + .3f, AddUnitPanel.transform.position.y, AddUnitPanel.transform.position.z);
		if(GameVars.PlayerReady) NGUITools.SetActive(AddUnitPanel, true);
	}


}
