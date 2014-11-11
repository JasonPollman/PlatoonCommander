using UnityEngine;
using System.Collections;

public class PathChangerButton : MonoBehaviour {

	GameObject PathChanger;
	PathChanger PCScript;
	string direction;

	// Use this for initialization
	void Start () {

		PathChanger = gameObject.transform.parent.Find("Path").transform.gameObject; 
		PCScript = PathChanger.GetComponent<PathChanger>();

		if(PCScript.directionCount > 1) {
			
			// Rotate to match sprite to start direction
			switch(PCScript.direction) {
			case "up":
				gameObject.transform.rotation = Quaternion.Euler (0, 0, 90);
				break;
			case "down":
				gameObject.transform.rotation = Quaternion.Euler (0, 0, 270);
				break;
			case "left":
				gameObject.transform.rotation = Quaternion.Euler (0, 0, 180);
				break;
				
			} // End switch block
			
		}
		else {
			
			// Only 1 direction, hide the sprite...
			var btnSprite = gameObject.GetComponent<UIImageButton>();
			btnSprite.hoverSprite = btnSprite.normalSprite = btnSprite.disabledSprite = btnSprite.pressedSprite = "blank";
			btnSprite.transform.Find("Background").GetComponent<UISprite>().spriteName = "blank";

			// Now kill the button
			NGUITools.SetActive(gameObject, false);
			
			
		} // End if(directionCount > 1)/else
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		
		Debug.Log ("HERE");

		int directionCount = PathChanger.GetComponent<PathChanger> ().directionCount;
		string direction = PathChanger.GetComponent<PathChanger> ().direction;


		bool up = PathChanger.GetComponent<PathChanger> ().up;
		bool down = PathChanger.GetComponent<PathChanger> ().down;
		bool left = PathChanger.GetComponent<PathChanger> ().left;
		bool right = PathChanger.GetComponent<PathChanger> ().right;

		if(directionCount > 1) {
			
			Quaternion newRotation = Quaternion.Euler (0, 0, 0);
			
			// Undo last rotation
			gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
			
			// Figure out new rotation based on settings...
			switch(direction) {
				
			case "up":
				
				if(right) {
					direction = "right";
					newRotation = Quaternion.Euler (0, 0, 0);
				}
				else if(down) {
					direction = "down";
					newRotation = Quaternion.Euler (0, 0, 270);
				}
				else if(left) {
					direction = "left";
					newRotation = Quaternion.Euler (0, 0, 180);
				}
				break;
				
			case "right":
				
				if(down) {
					direction = "down";
					newRotation = Quaternion.Euler (0, 0, 270);
				}
				else if(left) {
					direction = "left";
					newRotation = Quaternion.Euler (0, 0, 180);
				}
				else if(up) {
					direction = "up";
					newRotation = Quaternion.Euler (0, 0, 90);
				}
				break;
				
			case "down":
				
				if(left) {
					direction = "left";
					newRotation = Quaternion.Euler (0, 0, 180);
				}
				else if(up) {
					direction = "up";
					newRotation = Quaternion.Euler (0, 0, 90);
				}
				else if(right) {
					direction = "right";
					newRotation = Quaternion.Euler (0, 0, 0);
				}
				break;
				
			case "left":
				
				if(up) {
					direction = "up";
					newRotation = Quaternion.Euler (0, 0, 90);
				}
				else if(right) {
					direction = "right";
					newRotation = Quaternion.Euler (0, 0, 0);
				}
				else if(down) {
					direction = "down";
					newRotation = Quaternion.Euler (0, 0, 270);
				}
				break;
				
				
			} // End long switch block
			
			// Rotate the object...
			PathChanger.GetComponent<PathChanger> ().direction = direction;
			gameObject.transform.rotation = newRotation;


		} // End if(directionCount > 1)
		
	} // End OnClick()
}
