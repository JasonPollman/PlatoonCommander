using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitType {

	// The unit type's name (description)
	public string Name;

	// The number of HP for this unit type
	public int HP = 1;

	// The number of DP for this unit type
	public int DP = 0;

	// Any special abilities this unit has
	public string[] SpecialAbilities;

	// The about this unit text
	public string About;

	// The "solider" resource
	public GameObject SpriteAnimation;

	// If set to true, the game will be over if this unit type dies
	public bool GameOverOnDeath = false;

	// The GUI Sprite (Add Unit Popup sprite) for this unit type
	public string GUISprite;

	// The unit types color
	public Color UnitColor;

	public UnitType(string UnitName, int UnitHP, int UnitDP, string[] UnitSpecialAbilities, Color color, string AboutThisUnit, bool GameOverOnUnitDeath, string GUISpriteName) {

		// Set all values...
		Name 				= UnitName;
		HP 					= UnitHP;
		DP 					= UnitDP;
		SpriteAnimation 	= Resources.Load<GameObject>("Soldier");
		SpecialAbilities 	= UnitSpecialAbilities;
		About 				= AboutThisUnit;
		GameOverOnDeath 	= GameOverOnUnitDeath;
		GUISprite 			= GUISpriteName;
		UnitColor 			= color;

	} // End Constructor

} // End UnitType class
