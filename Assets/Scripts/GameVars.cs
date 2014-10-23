using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameVars : MonoBehaviour {

	/**
	 * Utility function to capitalize the first letter of a string
	 * @param s - The string to capitalize.
	 */
	public static string UCFirst (string s) {
		return char.ToUpper(s[0]) + s.Substring(1);
	}

	// The types of units available in the game
	public static Dictionary<string, UnitType> UnitTypes =  new Dictionary<string, UnitType> {
		{"bomber"			, new UnitType("bomber"		 	 , 5, 1, null, new Color(1f, .18f, .18f), "The Demolitions Expert—your bomber and most important unit. This guy likes to blow stuff up, and it's your job to get him to the target.", true,  "UnitType1") },
		{"ldu"				, new UnitType("ldu"			 , 4, 2, null, new Color(.4f, .97f, .4f), "The Laser Defense Unit. This unit harnesses the strength of his laser impenetrable steel armor to protect against laser fire. A laser tower will only cause a fourth of the damage to the LDU.", false, "UnitType2") },
		{"rifiler"			, new UnitType("rifiler"		 , 3, 3, null, new Color(.4f, .97f, .4f), "Fresh out of boot camp, the Rifiler is a standard rifle carrying unit. What this guy lacks in experience, he makes up for in taking hits for your bomber.", false, "UnitType3") },
		{"special forces"	, new UnitType("special forces"  , 6, 4, null, new Color(.4f, .97f, .4f), "These grunts are veterans—tough as nails and ready to fight. They have high HP and DP. They can slide by some towers unscathed.", false, "UnitType4") },
		{"firefighter"		, new UnitType("firefighter"	 , 3, 3, null, new Color(.4f, .97f, .4f), "With their fire retardant uniforms, Firefighters are nearly invincible to Flamethrowing Towers. Although, up against other towers—they’re not the strongest units in the platoon.", false, "UnitType5") },
		{"commando"			, new UnitType("commando"		 , 4, 6, null, new Color(.4f, .97f, .4f), "Commandos, like Special Forces are tough, elite units. They can take damage like no other unit, but unlike special forces, have almost no special abilities.", false, "UnitType6") }
	};


	// The number of each unit type remaining
	public static Dictionary<string, int> UnitsRemaining = new Dictionary<string, int> {
		{ "bomber"			, LevelConfig.NumberOfUnits["bomber"] 			},
		{ "ldu"				, LevelConfig.NumberOfUnits["ldu"] 				},
		{ "rifiler"			, LevelConfig.NumberOfUnits["rifiler"] 			},
		{ "special forces"	, LevelConfig.NumberOfUnits["special forces"] 	},
		{ "firefighter"		, LevelConfig.NumberOfUnits["firefighter"] 		},
		{ "commando"		, LevelConfig.NumberOfUnits["commando"] 		},
		
	};

	// The maxium number of units a squad can have.
	public static int SquadMaxUnits = 6;
	
	// The squad clicked on when the user clicks an add unit button
	public static string SquadClicked = null;

	public static string PlayerCurrentLevelName;

	// Has the level been beat?
	public static bool LevelWon = false;

	// Is the game in play?
	public static bool GameInPlay = true;

	// The reason for GameOver
	public static string GameOverReason = "";

	// False until the user clicks the ready button to begin the game.
	public static bool PlayerReady = false;

	// The arrows that you click to choose a path.
	public static List<GameObject> PathArrows = new List<GameObject>();

	// All game units
	public static List<Unit> AllUnits = new List<Unit>();

	// The bomber unit! //
	public static Unit BomberUnit = new Unit(UnitTypes["bomber"]);

	// The unit type clicked when a user clicks the icon to add a unit to a squad.
	public static string UnitTypeClicked = "";

	// The "unit box" the user clicked on to add a unit.
	private static int UnitBoxClicked = -1;

	// Indicates whether or not the bomber has been deployed...
	public static bool BomberDeployed = false;

	// The squads deployed
	public static List<string> SquadsDeployed = new List<string>();

	// If A unit has been put in a spot in a squad that spot will be true here:
	public static string[] SpotFilled = new string[18]; 
	
	// The Deploy Button Clicked
	public static DeployButton SquadDeployClicked = null;

	// Get the box number that the user clicked on to add a unit.
	public static int UnitNumberClicked {

		get { return UnitBoxClicked;  }
		set { UnitBoxClicked = value; }
	}

	// The squads in the game
	public static Dictionary<string, Unit[]> Squads = new Dictionary<string, Unit[]> {
		{ "alpha", new Unit[SquadMaxUnits] },
		{ "beta" , new Unit[SquadMaxUnits] },
		{ "omega", new Unit[SquadMaxUnits] },
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

	public static Dictionary<int, bool> DeployingPath = new Dictionary<int, bool> ();

	/**
	 * Adds a unit to a squad (Unit class object)
	 * @param type - The unit type as a string
	 * @param squad - The squad to add the unit to.
	 * @param buttonNum - The button clicked where the user wants the unit.
	 */
	public static Unit AddUnitToSquad (string type, string squad, int buttonNum) {

		// Squad must be "alpha", "beta", or "omega"
		if(!Squads.ContainsKey(squad.ToLower())) throw new UnityException("Unknown Squad");

		// To get the right array index
		if(squad.ToLower().Equals("beta"))  { buttonNum -= SquadMaxUnits; }
		if(squad.ToLower().Equals("omega")) { buttonNum -= SquadMaxUnits * 2; }

		if(Squads[squad.ToLower()].Length <= SquadMaxUnits) {

			// Create a new unit
			Unit NewUnit = new Unit (UnitTypes[type.ToLower()]);

			// Set the squad position to this unit
			Squads[squad.ToLower()][buttonNum - 1] = NewUnit;

			// Decrement the number of units remaining for this type
			UnitsRemaining[type.ToLower()]--;

			// Notify the user that we added a unit
			Console.Push ("Unit " + UCFirst (type) + " added to squad " + UCFirst (squad) + ".");


			return NewUnit;
		}

		return null;

	} // End AddUnitToSquad()


	/**
	 * Remove a unit from a squad
	 * @param type      - The unit type as a string
	 * @param squad     - The squad to remove the unit from.
	 * @param buttonNum - The button clicked to get the index to remove the unit.
	 */
	public static bool RemoveUnitFromSquad (string type, string squad, int buttonNum) {

		// Squad must be "alpha", "beta", or "omega"
		if(!Squads.ContainsKey(squad.ToLower())) throw new UnityException("Unknown Squad");

		// To get the right array index
		if(squad.ToLower().Equals("beta"))  { buttonNum -= SquadMaxUnits; }
		if(squad.ToLower().Equals("omega")) { buttonNum -= SquadMaxUnits * 2; }

		bool removed = false;

		// Set the unit to null at this index
		Squads[squad.ToLower()][buttonNum - 1] = null;

		// Increment the number of units remaining for this type
		UnitsRemaining[type.ToLower()]++;

		// Notify the user that we removed a unit
		Console.Push ("Unit " + UCFirst (type) + " removed from squad " + UCFirst (squad) + ".");

		return (removed) ? true : false;

	} // End RemoveUnitFromSquad()


	/**
	 * Reset all game variables (except PlayerCurrentLevelName) to start a new level...
	 */
	public static void ResetGameVars () {
		SquadClicked = null;
		LevelWon = false;
		GameInPlay = true;
		GameOverReason = "";
		PlayerReady = false;
		PathArrows = new List<GameObject>();
		AllUnits = new List<Unit>();
		BomberUnit = new Unit(UnitTypes["bomber"]);
		UnitTypeClicked = "";
		UnitBoxClicked = -1;
		BomberDeployed = false;
		SquadsDeployed = new List<string>();
		SpotFilled = new string[18];
		SquadDeployClicked = null;

		Squads = new Dictionary<string, Unit[]> {
			{ "alpha", new Unit[SquadMaxUnits] },
			{ "beta" , new Unit[SquadMaxUnits] },
			{ "omega", new Unit[SquadMaxUnits] },
		};

		UnitsRemaining = new Dictionary<string, int> {
			{ "bomber"			, LevelConfig.NumberOfUnits["bomber"] 			},
			{ "ldu"				, LevelConfig.NumberOfUnits["ldu"] 				},
			{ "rifiler"			, LevelConfig.NumberOfUnits["rifiler"] 			},
			{ "special forces"	, LevelConfig.NumberOfUnits["special forces"] 	},
			{ "firefighter"		, LevelConfig.NumberOfUnits["firefighter"] 		},
			{ "commando"		, LevelConfig.NumberOfUnits["commando"] 		},
			
		};

		DeployingPath = new Dictionary<int, bool> ();
		
	} // End ResetGameVars()


} // End GameVars Class
