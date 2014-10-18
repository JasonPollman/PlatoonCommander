using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameVars : MonoBehaviour {

	public static string UCFirst (string s) {
		return char.ToUpper(s[0]) + s.Substring(1);
	}

	public static int SquadMaxUnits = 6;
	
	public static Dictionary<string, UnitType> UnitTypes =  new Dictionary<string, UnitType> {
		{"bomber"			, new UnitType("bomber"		 	 , 3, 1, null, new Color(.5f, .5f, .5f), "This is the Demolitions Expert", true,  "UnitType1") },
		{"ldu"				, new UnitType("ldu"			 , 2, 2, null, new Color(0, 0, 0), "This is the Demolitions Expert", false, "UnitType2") },
		{"rifiler"			, new UnitType("rifiler"		 , 1, 3, null, new Color(0, 0, 1), "This is the Demolitions Expert", false, "UnitType3") },
		{"special forces"	, new UnitType("special forces"  , 4, 4, null, new Color(255, 255, 255), "This is the Demolitions Expert", false, "UnitType4") },
		{"firefighter"		, new UnitType("firefighter"	 , 5, 5, null, new Color(255, 255, 255), "This is the Demolitions Expert", false, "UnitType5") },
		{"commando"			, new UnitType("commando"		 , 3, 6, null, new Color(255, 255, 255), "This is the Demolitions Expert", false, "UnitType6") }
	};

	public static string SquadClicked;


	public static Dictionary<string, int> UnitsRemaining = new Dictionary<string, int> {
		{ "bomber"			, LevelConfig.NumberOfUnits["bomber"] 			},
		{ "ldu"				, LevelConfig.NumberOfUnits["ldu"] 				},
		{ "rifiler"			, LevelConfig.NumberOfUnits["rifiler"] 			},
		{ "special forces"	, LevelConfig.NumberOfUnits["special forces"] 	},
		{ "firefighter"		, LevelConfig.NumberOfUnits["firefighter"] 		},
		{ "commando"		, LevelConfig.NumberOfUnits["commando"] 		},

	};

	// False until the user clicks the ready button to begin the game.
	public static bool PlayerReady = false;

	public static List<Unit> AllUnits = new List<Unit>();

	public static string UnitTypeClicked = "";

	// The "unit box" the user clicked on to add a unit.
	private static int UnitBoxClicked = -1;

	// Indicates whether or not the bomber has been deployed...
	public static string BomberDeployed = "NO";

	// If A unit has been put in a spot in a squad that spot will be true here:
	public static string[] SpotFilled = new string[18]; 


	// Get the box number that the user clicked on to add a unit.
	public static int UnitNumberClicked {

		get { return UnitBoxClicked;  }
		set { UnitBoxClicked = value; }
	}

	public static Dictionary<string, List<Unit>> Squads = new Dictionary<string, List<Unit>> {
		{ "alpha", new List<Unit>() },
		{ "beta" , new List<Unit>() },
		{ "omega", new List<Unit>() },
	};

	// General Patton Quotes
	private static string[] PattonQuotes = new string[5] {
		"\"Good tactics can save even the worst strategy. Bad tactics will destroy even the best strategy.\"",
		"\"A good plan, violently executed now, is better than a perfect plan next week.\"",
		"\"Lead me, follow me, or get out of my way.\"",
		"\"Never tell people how to do things. Tell them what to do and they will surprise you with their ingenuity.\"",
		"\"In war the only sure defense is offense, and the efficiency of the offense depends on the warlike souls of those conducting it.\""
	};

	// Get a General Patton Quote...
	public static string GetQuote () {
		return PattonQuotes[(int) Mathf.Floor(Random.Range(0, PattonQuotes.Length))];
	}


	public static bool AddUnitToSquad (string type, string squad) {

		
		if(!Squads.ContainsKey(squad.ToLower())) throw new UnityException("Unknown Squad");

		if(Squads[squad.ToLower()].Count <= SquadMaxUnits) {
			Unit NewUnit = new Unit (UnitTypes[type.ToLower()]);
			Squads[squad.ToLower()].Add(NewUnit);
			UnitsRemaining[type.ToLower()]--;
			Console.Push ("Unit " + UCFirst (type) + " added to squad " + UCFirst (squad) + ".");
			return true;
		}

		return false;
	}

	public static bool RemoveUnitFromSquad (string type, string squad) {

		bool removed = false;

		if(!Squads.ContainsKey(squad.ToLower())) throw new UnityException("Unknown Squad");

		foreach(Unit x in Squads[squad.ToLower()]) {
			if(x.Type.Name.Equals(type)) {
				Squads[squad.ToLower()].Remove(x);
				removed = true;
				break;
			}
		}

		UnitsRemaining[type.ToLower()]++;
		Console.Push ("Unit " + UCFirst (type) + " removed from squad " + UCFirst (squad) + ".");

		return (removed) ? true : false;
	}

}
