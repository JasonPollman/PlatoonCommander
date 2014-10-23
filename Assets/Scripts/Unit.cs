using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit {

	// The UnitType of the Unit (Class Unit Type)
	public UnitType Type;

	// This Unit's GameObject
	public GameObject GameObj;

	// If the unit has been instantiated, this will be true.
	public bool Instantiated = false;

	// The UI Button which correlates to this unit (The Add Squad Button)...
	public GameObject UnitButton;


	// Constructor
	public Unit (UnitType type) {

		Type = type;

		// Add the unit to the AllUnits static variable
		GameVars.AllUnits.Add(this);

	} // End constructor


	/*
	 * Instantiate the Unit and add him to the game board.
	 * @param position - The position to instantiate the unit.
	 * @param rotation - The rotation of the unit.
	 */
	public GameObject InstantiateUnit(Vector3 position, Quaternion rotation) {

		GameObj = (GameObject) UnityEngine.MonoBehaviour.Instantiate(Type.SpriteAnimation, position, rotation);
		GameObj.GetComponent<UnitObject> ().UnitButton = UnitButton;
		Instantiated = true;

		// Set the unit's tint
		GameObj.GetComponent<SpriteRenderer> ().color = Type.UnitColor;

		// Set the "UnitObject" script attached to the Unit's arguments.

		UnitObject UO = GameObj.GetComponent<UnitObject> ();

		UO.Alive 	= true;
		UO.HP 		= Type.HP;
		UO.DP 		= Type.DP;
		UO.Type 	= Type.Name;

		// Return the GameObject of the actual unit...
		return GameObj;

	} // End InstantiateUnit


} // End Unit Class
