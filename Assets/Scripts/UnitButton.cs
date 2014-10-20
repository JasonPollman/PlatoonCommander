using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UnitButton : MonoBehaviour {

	public string UnitType;
	public AudioClip ErrorClip;

	private int ThisUnitSpot;

	private bool hovering;

	private UILabel UnitNameLabel, AboutUnit, HP, DP, BomberDeployedLabel;
	private GameObject AddUnitBox;
	AudioClip originalClip;
	// Use this for initialization
	void Start () {
		BomberDeployedLabel.text = GameVars.BomberDeployed ? "YES" : "NO";
		UnitNameLabel.text = GameVars.UnitTypes["bomber"].Name.ToUpper();
		AboutUnit.text = GameVars.UnitTypes["bomber"].About;
		DP.text = "DP: " + GameVars.UnitTypes["bomber"].DP.ToString();
		HP.text = "HP: " + GameVars.UnitTypes["bomber"].HP.ToString();

		originalClip = gameObject.GetComponent<UIButtonSound>().audioClip;
	}
	
	// Update is called once per frame
	void Update () {

		if(GameVars.UnitsRemaining[UnitType.ToLower()] <= 0) {
			gameObject.GetComponent<UIButtonSound>().audioClip = ErrorClip;
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(.3f, .3f, .3f, 1f);
		}
		else if(hovering == false) {
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(1f, 1f, 1f);
		}

	}

	void Awake () {
		AddUnitBox = GameObject.Find ("AddUnitPopup");
		UnitNameLabel = GameObject.Find("UnitName").GetComponent<UILabel>();
		AboutUnit = GameObject.Find("AboutUnit").GetComponent<UILabel>();
		HP = GameObject.Find ("HP").GetComponent<UILabel>();
		DP = GameObject.Find ("DP").GetComponent<UILabel>();
		BomberDeployedLabel = GameObject.Find("BomberDeployed").GetComponent<UILabel>();
	}

	void OnClick () {

		GameVars.UnitTypeClicked = UnitType.ToLower();
		ThisUnitSpot = GameVars.UnitNumberClicked;

		bool addSuccess = false;
		gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(1f, 1f, 1f);

		if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0 && GameVars.SpotFilled[ThisUnitSpot - 1] == null) {

			addSuccess = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);
			GameVars.SpotFilled[ThisUnitSpot - 1] = UnitType.ToLower();
		}
		else if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0) {
			bool remSuccess = GameVars.RemoveUnitFromSquad(GameVars.SpotFilled[ThisUnitSpot - 1], GameVars.SquadClicked);
			addSuccess = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);

		}

		if(addSuccess) { // Change the + Button to the Unit Type

			UIImageButton AddTileButton = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString()).GetComponent<UIImageButton>();
			AddTileButton.normalSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			AddTileButton.disabledSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			AddTileButton.hoverSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			UISprite AddTile = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/Background").GetComponent<UISprite>();


			AddTile.spriteName = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			AddTile.MakePixelPerfect();
			AddTile.MarkAsChanged();
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