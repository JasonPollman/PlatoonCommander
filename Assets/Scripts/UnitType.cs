using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitType {

	public string Name;
	public int HP = 1;
	public int DP = 0;
	public string[] SpecialAbilities;
	public string About;
	public Object SpriteAnimation;
	public bool GameOverOnDeath = false;
	public string GUISprite;
	public Color UnitColor;

	public GameObject UnitObj;

	public UnitType(string UnitName, int UnitHP, int UnitDP, string[] UnitSpecialAbilities, Color color, string AboutThisUnit, bool GameOverOnUnitDeath, string GUISpriteName) {

		Name = UnitName;
		HP = UnitHP;
		DP = UnitDP;
		SpriteAnimation = Resources.Load ("Soldier");
		SpecialAbilities = UnitSpecialAbilities;
		About = AboutThisUnit;
		GameOverOnDeath = GameOverOnUnitDeath;
		GUISprite = GUISpriteName;
		UnitColor = color;

	} // End Constructor

}
