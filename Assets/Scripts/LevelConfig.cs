using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConfig : MonoBehaviour {

	public static int LevelDuration = 900; // The length before "game over" in seconds

	public string MissionTitle = "Mission Title";
	public string MissionObjective = "Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Integer posuere erat a ante venenatis dapibus posuere velit aliquet.";
	public string MissionDescription = "Vestibulum id ligula porta felis euismod semper. Cras mattis consectetur purus sit amet fermentum. Nulla vitae elit libero, a pharetra augue. Aenean lacinia bibendum nulla sed consectetur. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Donec id elit non mi porta gravida at eget metus. Vestibulum id ligula porta felis euismod semper.";

	public static Dictionary<string, int> NumberOfUnits = new Dictionary<string, int> {
		{"bomber", 			1  },
		{"rifiler", 		17 },
		{"firefighter", 	0  },
		{"ldu", 			0  },
		{"special forces", 	0  },
		{"commando", 		0  }
	};
	
	private UILabel RemainingTroopsValuesLabel, MissionTitleLabel, MissionObjectiveLabel, MissionDescriptionLabel;
	
	// Use this for initialization
	void Start () {
		MissionObjectiveLabel.text = MissionObjective;
		MissionTitleLabel.text = MissionTitle.ToUpper();
		MissionDescriptionLabel.text = MissionDescription;
	}

	void Awake () {
		MissionTitleLabel = GameObject.Find ("GameStartBox/MessageTitle").GetComponent<UILabel>();
		MissionObjectiveLabel = GameObject.Find ("GameStartBox/MessageText").GetComponent<UILabel>();
		MissionDescriptionLabel = GameObject.Find ("UserGUI/MissionObjectiveDescrip").GetComponent<UILabel>();
		RemainingTroopsValuesLabel = GameObject.Find ("RemainingTroopsValues").GetComponent<UILabel>();
	}
	

	void Update () {

		// Check to ensure that the bomber unit is alive, if deployed...

		//Debug.Log (GameVars.UnitTypes);

		// Update the number of remaining units on screen...
		string temp = "";

		foreach(KeyValuePair<string, int> entry in GameVars.UnitsRemaining)
		{
			temp += entry.Key.ToUpper() + "..." + entry.Value.ToString() + "\n";
		}

		RemainingTroopsValuesLabel.text = temp;
	}

}
