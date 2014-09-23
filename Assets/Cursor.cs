using UnityEngine;
using System.Collections;

public class SetCursor : MonoBehaviour {

	Texture2D 	cursorTexture 	= (Texture2D)Resources.Load("Cursor.png");
	CursorMode 	cursorMode 		= CursorMode.Auto;
	Vector2 	hotSpot 		= Vector2.zero;

	// Use this for initialization
	void Start () {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
