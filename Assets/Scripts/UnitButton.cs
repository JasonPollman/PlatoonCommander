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
		BomberDeployedLabel.text = GameVars.BomberDeployed;
		UnitNameLabel.text = GameVars.UnitTypes["bomber"].Name.ToUpper();
		AboutUnit.text = GameVars.UnitTypes["bomber"].About;
		DP.text = "DP: " + GameVars.UnitTypes["bomber"].DP.ToString();
		HP.text = "HP: " + GameVars.UnitTypes["bomber"].HP.ToString();

		originalClip = gameObject.GetComponent<UIButtonSound>().audioClip;
	}
	
	// Update is called once per frame
	void Update () {

		if(hovering && GameVars.UnitsRemaining[UnitType.ToLower()] <= 0) {
			gameObject.GetComponent<UIButtonSound>().audioClip = ErrorClip;
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(10,10,10);
		}
		else if(!hovering) {
			gameObject.GetComponent<UIButtonSound>().audioClip = originalClip;
			gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = new Color(255, 255, 255);
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

		string LastUnitType = GameVars.UnitTypeClicked.ToLower();
		GameVars.UnitTypeClicked = UnitType.ToLower();

		int LastUnitSpot = ThisUnitSpot;
		ThisUnitSpot = GameVars.UnitNumberClicked;

		bool addSuccess = false;

		if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0 && LastUnitSpot != ThisUnitSpot) {
			addSuccess = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);
		}
		else if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0 && LastUnitType.ToLower() != UnitType.ToLower()) {
			GameVars.RemoveUnitFromSquad(LastUnitType.ToLower(), GameVars.SquadClicked);
			addSuccess = GameVars.AddUnitToSquad(UnitType.ToLower(), GameVars.SquadClicked);

		}

		if(addSuccess) {
			UIImageButton AddTileButton = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString()).GetComponent<UIImageButton>();
			AddTileButton.normalSprite = AddTileButton.hoverSprite = AddTileButton.pressedSprite = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			UISprite AddTile = GameObject.Find("AddUnit" + GameVars.UnitNumberClicked.ToString() + "/Background").GetComponent<UISprite>();


			AddTile.spriteName = GameVars.UnitTypes[UnitType.ToLower()].GUISprite;
			AddTile.MakePixelPerfect();
			AddTile.MarkAsChanged();
			Debug.Log (AddTile.spriteName);
		}

		NGUITools.SetActive(AddUnitBox, false);
	}

	void OnHover (bool isOver) {

		Debug.Log ("JASON");

		if(isOver) {
			hovering = true;

			if(GameVars.UnitsRemaining[UnitType.ToLower()] > 0) {
				gameObject.transform.FindChild("Background").GetComponent<UISprite>().color = GameVars.UnitTypes[UnitType.ToLower()].UnitColor;
			}

			UnitNameLabel.text = GameVars.UnitTypes[UnitType.ToLower()].Name.ToUpper();
			AboutUnit.text = GameVars.UnitTypes[UnitType.ToLower()].About;
			DP.text = "DP: " + GameVars.UnitTypes[UnitType.ToLower()].DP.ToString();
			HP.text = "HP: " + GameVars.UnitTypes[UnitType.ToLower()].HP.ToString();
			BomberDeployedLabel.text = GameVars.BomberDeployed;
		}
		else {
			Debug.Log ("EXITING");
			hovering = false;
		}
	}


}