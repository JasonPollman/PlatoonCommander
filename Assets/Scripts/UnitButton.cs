using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UnitButton : MonoBehaviour {

	// The unit's type
	public string UnitType;

	// The original sound clip when the button is pressed.
	public AudioClip OriginalClip;

	// The error sound clip that plays when there's no more of this unit type. 
	public AudioClip ErrorClip;

	// The "spot" or "slot" of the unit from 1-18, 1-6 = Alpha Squad, 7-12 = Beta, etc. etc.
	private int ThisUnitSpot;

	// The Unit class of the Game Unit
	private Unit GameUnit;

	// If we are hovering over the button or not.
	private bool hovering;

	// The labels that show up on the unit button that show it's HP and DP.
	private UILabel UnitHPLabel, UnitDPLabel;

	// The labels within the Unit Popup Box...
	private UILabel UnitNameLabel, AboutUnit, HP, DP, BomberDeployedLabel;

	// The Add Unit Popup Menu
	private GameObject AddUnitBox = null;

	private GameObject AddTileObject = null;
	

	// Use this for initialization
	void Start () {

		// Set the bomber deployed label to "NO"
		BomberDeployedLabel.text = GameVars.BomberDeployed ? "YES" : "NO";

		// Unit Popup Menu Labels...

		// Set the Unit's Name label to the Bomber on start...
		UnitNameLabel.text = GameVars.UnitTypes["bomber"].Name.ToUpper();

		// Set the About Unit label to the Bomber on start...
		AboutUnit.text = GameVars.UnitTypes["bomber"].About;

		// Set the HP & DP
		DP.text = "DP: " + GameVars.UnitTypes["bomber"].DP.ToString();
		HP.text = "HP: " + GameVars.UnitTypes["bomber"].HP.ToString();

		// Set the originalClip
		OriginalClip = gameObject.GetComponent<UIButtonSound>().audioClip;

	} // End Start() Method


	// Update is called once per frame
	void Update () {

		// Gray out the Unit Sprites and and set the audio clip to the error clip if the number of units remaining for this type is <= 0.
		if(GameVars.UnitsRemaining[UnitType.ToLower()] <= 0) {
			gameObject.GetComponent<UIButtonSound>().audioClip = ErrorClip;
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(.3f, .3f, .3f, 1f);
		}
		else if(hovering == false) {
			gameObject.GetComponent<UIButtonSound>().audioClip = OriginalClip;
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(1f, 1f, 1f);
		}

	} // End Update()


	void Awake () {

		// Set the AddUnitBox GameObject Label
		AddUnitBox = GameObject.Find ("AddUnitPopup");

		// Set the UnitNameLabel GameObject Label
		UnitNameLabel = GameObject.Find("UnitName").GetComponent<UILabel>();

		// Set the AboutUnit GameObject Label
		AboutUnit = GameObject.Find("AboutUnit").GetComponent<UILabel>();

		// Set the HP & DP GameObject Labels
		HP = GameObject.Find ("HP").GetComponent<UILabel>();
		DP = GameObject.Find ("DP").GetComponent<UILabel>();

		// Set the BomberDeployedLabel GameObject Label
		BomberDeployedLabel = GameObject.Find("BomberDeployed").GetComponent<UILabel>();

	} // End Awake()


	void OnClick () {

		GameVars.UnitTypeClicked = UnitType.ToLower();

		// Get the unit spot clicked...
		ThisUnitSpot = GameVars.UnitNumberClicked;

		// Find the background and change it to white on hover...
		gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(1f, 1f, 1f);

		if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0 && GameVars.SpotFilled[ThisUnitSpot - 1] == null) { // If the spot isn't already filled and we have units of this type:

			// Add the unit to the squad for this button
			GameUnit = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked, ThisUnitSpot);
			// Indicate that this spot is filled
			GameVars.SpotFilled[ThisUnitSpot - 1] = UnitType.ToLower();
		}
		else if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0) { // If we have units of this type remaining...

			// Remove the last unit that was put in this spot
			GameVars.RemoveUnitFromSquad(GameVars.SpotFilled[ThisUnitSpot - 1], GameVars.SquadClicked, ThisUnitSpot);

			// Add the new unit
			GameUnit = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked, ThisUnitSpot);
			GameVars.SpotFilled[ThisUnitSpot - 1] = UnitType.ToLower();

		}
		else { // No unit was added

			GameUnit = null;

		} // End if/else block

		Debug.Log (GameUnit);

		if(GameUnit != null) { // If a unit was added...

			// ---------------------- Change the + Button to the Unit Type ---------------------- //

			// Grab the Add Tile Button
			AddTileObject = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString());
			UIImageButton AddTileButton = AddTileObject.GetComponent<UIImageButton>();


			GameUnit.UnitButton = AddTileObject;

			// Change the sprite button's sprites to the blank version
			AddTileButton.normalSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = AddTileButton.disabledSprite = "AddUnitBlank";

			// Change the background tile to the blank sprite
			UISprite AddTile = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/Background").GetComponent<UISprite>();
			AddTile.spriteName = "AddUnitBlank";
			AddTile.MakePixelPerfect();
			AddTile.MarkAsChanged();


			// Destroy and previous unit sprites that may have been added here:
			Destroy(GameObject.Find ("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/UnitBKG"));

			// Instantiate a blank sprite game object
			GameObject unitRef = ((GameObject) Instantiate(Resources.Load<GameObject>("BlankSprite")));

			// Change the object's name
			unitRef.gameObject.name = "UnitBKG";

			// Get the actual sprite
			UISprite unitRefSprite = unitRef.GetComponent<UISprite>();

			// Change it to the unit's UI sprite
			unitRefSprite.spriteName = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;

			// Make it's parent the AddTileButton
			unitRef.transform.parent = AddTileButton.transform;

			// Put it in the correct place
			unitRef.transform.localPosition = new Vector3(-16, 25, 0);

			// Update the sprite
			unitRefSprite.MakePixelPerfect();
			unitRefSprite.MarkAsChanged();


			// Add Unit Statistics for HP

			GameObject UnitHPLabelObject = new GameObject();
			Instantiate(UnitHPLabelObject);
			UnitHPLabel = UnitHPLabelObject.AddComponent<UILabel>();
			UnitHPLabel.transform.parent = AddTileButton.transform;
			UnitHPLabel.text = "HP: " + GameUnit.Type.HP;
			UnitHPLabel.name = "HPLabel";
			UnitHPLabel.color = new Color((191f / 255f), 0, 0);
			UnitHPLabel.font = (UIFont) Resources.Load<UIFont>("UIFontSmall");
			UnitHPLabel.depth = 50;
			UnitHPLabel.maxLineCount = 1;
			UnitHPLabel.multiLine = false;
			UnitHPLabel.pivot = UIWidget.Pivot.Right;
			
			UnitHPLabel.MakePixelPerfect();
			UnitHPLabel.MarkAsChanged();
			
			UnitHPLabel.transform.localPosition = new Vector3(34, 25, 0);


			// Add Unit Statistics for DP

			GameObject UnitDPLabelObject = new GameObject();
			Instantiate(UnitDPLabelObject);
			UnitDPLabel = UnitDPLabelObject.AddComponent<UILabel>();
			UnitDPLabel.transform.parent = AddTileButton.transform;
			UnitDPLabel.text = "DP: " + GameUnit.Type.DP;
			UnitDPLabel.name = "DPLabel";
			UnitDPLabel.color = new Color(0, (191f / 255f), (17f / 255f));
			UnitDPLabel.font = (UIFont) Resources.Load<UIFont>("UIFontSmall");
			UnitDPLabel.depth = 50;
			UnitDPLabel.maxLineCount = 1;
			UnitDPLabel.multiLine = false;
			UnitDPLabel.pivot = UIWidget.Pivot.Right;
			
			UnitDPLabel.MakePixelPerfect();
			UnitDPLabel.MarkAsChanged();
			
			UnitDPLabel.transform.localPosition = new Vector3(34, 10, 0);


		}

		// Close the Add Unit Menu...
		NGUITools.SetActive(AddUnitBox, false);

	} // End OnClick()

	void OnHover (bool isOver) {

		if(isOver) {

			hovering = true;

			if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0) {
				gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = GameVars.UnitTypes[UnitType.ToLower()].UnitColor;
			}

			UnitNameLabel.text = GameVars.UnitTypes[UnitType.ToLower()].Name.ToUpper();
			AboutUnit.text = GameVars.UnitTypes[UnitType.ToLower()].About;
			DP.text = "DP: " + GameVars.UnitTypes[UnitType.ToLower()].DP.ToString();
			HP.text = "HP: " + GameVars.UnitTypes[UnitType.ToLower()].HP.ToString();
			BomberDeployedLabel.text = (GameVars.BomberDeployed) ? "YES" : "NO";
		}
		else {
			hovering = false;
		}
	}


}