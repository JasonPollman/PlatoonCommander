using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class UnitButton : MonoBehaviour {

	public string UnitType;
	public AudioClip ErrorClip;

	private int ThisUnitSpot;

	private Unit ThisButtonsUnit;

	private bool hovering;

	private UILabel UnitHPLabel, UnitDPLabel;

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

		if(ThisButtonsUnit != null && ThisButtonsUnit.Instantiated) {
			Debug.Log ("IN UPDATE");
			UnitHPLabel.text = "HP :" + ThisButtonsUnit.GameObj.GetComponent<UnitObject>().HP;
			UnitDPLabel.text = "DP :" + ThisButtonsUnit.GameObj.GetComponent<UnitObject>().DP;
		}

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

		gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(1f, 1f, 1f);

		if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0 && GameVars.SpotFilled[ThisUnitSpot - 1] == null) {

			ThisButtonsUnit = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);
			GameVars.SpotFilled[ThisUnitSpot - 1] = UnitType.ToLower();
		}
		else if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0) {
			bool remSuccess = GameVars.RemoveUnitFromSquad(GameVars.SpotFilled[ThisUnitSpot - 1], GameVars.SquadClicked);
			ThisButtonsUnit = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);

		}
		else {
			ThisButtonsUnit = null;
		}

		if(ThisButtonsUnit != null) { // Change the + Button to the Unit Type

			UIImageButton AddTileButton = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString()).GetComponent<UIImageButton>();
			AddTileButton.normalSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = AddTileButton.disabledSprite = "AddUnitBlank";

			UISprite AddTile = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/Background").GetComponent<UISprite>();
			AddTile.spriteName = "AddUnitBlank";
			AddTile.MakePixelPerfect();
			AddTile.MarkAsChanged();

			Destroy(GameObject.Find ("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/UnitBKG"));

			GameObject unitRef = ((GameObject) Instantiate(Resources.Load<GameObject>("BlankSprite")));
			unitRef.gameObject.name = "UnitBKG";
			UISprite unitRefSprite = unitRef.GetComponent<UISprite>();
			unitRefSprite.spriteName = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;

			unitRef.transform.parent = AddTileButton.transform;
			unitRef.transform.localPosition = new Vector3(-16, 25, 0);
			unitRefSprite.MakePixelPerfect();
			unitRefSprite.MarkAsChanged();

			// Add Unit Statistics for HP

			GameObject UnitHPLabelObject = new GameObject();
			Instantiate(UnitHPLabelObject);
			UnitHPLabel = UnitHPLabelObject.AddComponent<UILabel>();
			UnitHPLabel.transform.parent = AddTileButton.transform;
			UnitHPLabel.text = "HP: " + ThisButtonsUnit.Type.HP;
			UnitHPLabel.name = "HP Label";
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
			UnitDPLabel.text = "DP: " + ThisButtonsUnit.Type.DP;
			UnitDPLabel.name = "DP Label";
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