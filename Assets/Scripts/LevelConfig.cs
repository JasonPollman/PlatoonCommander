using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConfig : MonoBehaviour {

	public string levelName = "LevelName";

	// The length before "game over" in seconds
	public int LevelDuration = 900; 

	// The mission title
	public string MissionTitle = "Mission Title";

	// The mission objective
	public string MissionObjective = " It's about damn time. We've got a situation that we need you to attend to. Seeing as how this is your first mission, we'll make it an easy one. \n \nYour orders are to take your platoon and head to Baquba, Iraq  and destroy the Diyala Bridge at all costs. The enemy had been getting a steady steam of suppilies through this route and we need you to stop it! Just beware the route to the bride is hot, and an enemy tower is defending the position...\n\nGood Luck!";

	// The mission description (longer than objective... appears in game start box).
	public string MissionDescription = "Vestibulum id ligula porta felis euismod semper. Cras mattis consectetur purus sit amet fermentum. Nulla vitae elit libero, a pharetra augue. Aenean lacinia bibendum nulla sed consectetur. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Donec id elit non mi porta gravida at eget metus. Vestibulum id ligula porta felis euismod semper.";

	public int numRifilers = 17;
	public int numFirefighter = 0;
	public int numLDU = 0;
	public int numCommando = 0;
	public int numSpecial = 0;

	// The number of each type of unit for this level
	public static Dictionary<string, int> NumberOfUnits = new Dictionary<string, int> {
		{"bomber", 			1  },
		{"rifiler", 		17 },
		{"firefighter", 	0  },
		{"ldu", 			0  },
		{"special forces", 	0  },
		{"commando", 		0  }
	};




	// <--------------------------------------- DO NOT EDIT BELOW HERE, IF MAKING NEW LEVEL ---------------------------------------> //

	// Game Labels we need in order to update...
	private UILabel RemainingTroopsValuesLabel, MissionTitleLabel, MissionObjectiveLabel, MissionDescriptionLabel;

	public GameObject winBox;


	void Start () {

		GameVars.PlayerCurrentLevelName = levelName;

		// We *must* do this for some reason.
		MissionObjectiveLabel.text = MissionObjective.ToString().Replace("\\n", "\n");
		MissionTitleLabel.text = MissionTitle.ToUpper();
		MissionDescriptionLabel.text = MissionDescription.Replace("\\n", "\n");

		foreach(KeyValuePair<string, Unit[]> e in GameVars.Squads) {
			for(int i = 0; i < e.Value.Length; i++) {
				e.Value[i] = null;
			}
		}

	} // End Start()


	void Awake () {

		NumberOfUnits ["bomber"] 		 = 1;
		NumberOfUnits ["rifiler"] 		 = numRifilers;
		NumberOfUnits ["firefighter"] 	 = numFirefighter;
		NumberOfUnits ["ldu"] 			 = numLDU;
		NumberOfUnits ["special forces"] = numSpecial;
		NumberOfUnits ["commando"] 		 = numCommando;

		GameVars.ResetGameVars ();

		// Get the UILabels we need
		MissionTitleLabel = GameObject.Find ("GameStartBox/MessageTitle").GetComponent<UILabel>();
		MissionObjectiveLabel = GameObject.Find ("GameStartBox/MessageText").GetComponent<UILabel>();
		MissionDescriptionLabel = GameObject.Find ("UserGUI/MissionObjectiveDescrip").GetComponent<UILabel>();
		RemainingTroopsValuesLabel = GameObject.Find ("RemainingTroopsValues").GetComponent<UILabel>();

		// Get the win dialog box
		winBox = GameObject.Find ("MissionCompleteBox");

	} // End Awake()
	

	void Update () {

		// Update the number of remaining units on screen...
		string temp = "";

		foreach(KeyValuePair<string, int> entry in GameVars.UnitsRemaining) temp += entry.Key.ToUpper() + "..." + entry.Value.ToString() + "\n";
		RemainingTroopsValuesLabel.text = temp;

		// <------------------------------------- VARIOUS WIN CHECKING CONDITIONS -------------------------------------> //

		// If the bomber dies, then game over:
		if(GameVars.BomberDeployed && GameVars.BomberUnit != null && GameVars.BomberUnit.GameObj.GetComponent<UnitObject>().Alive == false) { // Game Over, the Bomber is Dead...
			GameVars.GameOverReason = "Your bomber was destroyed!";
			StartCoroutine(DeathNextLevel());
			GameVars.GameInPlay = false;
			return;
		}

		// Popup the win box if the level has been defeated...
		if(GameVars.LevelWon == true) {
			NGUITools.SetActive(winBox, true);
			return;
		}


		// If all squads deployed, game over:

		if(GameVars.SquadsDeployed.IndexOf("alpha") > -1 &&
		   GameVars.SquadsDeployed.IndexOf("beta")  > -1 &&
		   GameVars.SquadsDeployed.IndexOf("omega") > -1 ) {

			bool gameO = true;

			foreach(KeyValuePair<string, Unit[]> x in GameVars.Squads) {
				foreach(Unit u in x.Value) {
					if(u != null && u.GameObj && u.GameObj.GetComponent<UnitObject>().Alive == true && (u.GameObj.rigidbody2D.velocity != Vector2.zero || GameVars.Stop == true)) {
						gameO = false;
					}
				}
			}

			if(gameO == true) {
				GameVars.GameOverReason = "You failed to destroy the target with three squads!";
				StartCoroutine(DeathNextLevel());
				GameVars.GameInPlay = false;
				return;
			}

		} // End outer if block


		// If the time is up:
		if(GameVars.PlayerReady == true) {

			if(GameTimer.TimeRemaining <= 0) {
				GameVars.GameOverReason = "Mission Failed: Time is up!";
				StartCoroutine(DeathNextLevel());
				GameVars.GameInPlay = false;
				return;
			}
			
		} // End if block


	} // End Update()


	IEnumerator DeathNextLevel() {
		yield return new WaitForSeconds (1);
		Application.LoadLevel("GameOver");
	}

} // End LevelConfig class
