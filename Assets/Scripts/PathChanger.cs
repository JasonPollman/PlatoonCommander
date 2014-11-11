using UnityEngine;
using System.Collections;

public class PathChanger : MonoBehaviour {
	
	public bool up;
	public bool down;
	public bool left;
	public bool right;
	
	public string startDirection;
	
	public string direction;
	public int directionCount = 0;
	private GameObject background;
	private GameObject button;
	
	
	void Awake() {
		background = gameObject.transform.parent.transform.FindChild("Button/Background").gameObject;
		button  = gameObject.transform.parent.transform.FindChild("Button").gameObject;
	}
	
	void Start() {
		
		startDirection = startDirection.ToLower();
		
		if(startDirection == "up" && !up)       startDirection = "right";
		if(startDirection == "right" && !right) startDirection = "down";
		if(startDirection == "down" && !down)   startDirection = "left";
		if(startDirection == "left" && !left)   startDirection = "up";
		
		if(up    == true) directionCount++;
		if(down  == true) directionCount++;
		if(left  == true) directionCount++;
		if(right == true) directionCount++;
		
		direction = startDirection;
		
		if(directionCount < 1) throw new UnityException("You must pick a direction!");
		
		if(directionCount > 1) {
			
			// Rotate to match sprite to start direction
			switch(direction) {
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
			var btnSprite = button.GetComponent<UIImageButton>();
			btnSprite.hoverSprite = btnSprite.normalSprite = btnSprite.disabledSprite = btnSprite.pressedSprite = "blank";
			btnSprite.transform.Find("Background").GetComponent<UISprite>().spriteName = "blank";
			
			// Now kill the button
			NGUITools.SetActive(gameObject.transform.parent.transform.Find ("Button").gameObject, false);
			
			
		} // End if(directionCount > 1)/else
		
	} // End Start()
	
	
	void OnTriggerStay2D(Collider2D other) {
		
		Debug.Log ("HERE");
		Debug.Log (direction);
		
		GameObject soldier = other.gameObject;
		if (soldier.tag != "unit") return;
		
		move     	move  = soldier.GetComponent<move>();
		float    	speed = (float) move.speed;
		Quaternion  defaultRotation = move.getDefaultRotation ();
		
		switch (direction) {
		case "down":
			if((Mathf.Round(soldier.transform.position.x * 1000f) / 1000f) == (Mathf.Round(gameObject.GetComponent<BoxCollider2D>().transform.position.x * 1000f) / 1000f)) {
				soldier.transform.rotation = defaultRotation;
				soldier.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
				soldier.rigidbody2D.velocity = new Vector2(0f, -1*speed);
			}
			break;
		case "up":
			if((Mathf.Round(soldier.transform.position.x * 1000f) / 1000f) == (Mathf.Round(gameObject.GetComponent<BoxCollider2D>().transform.position.x * 1000f) / 1000f)) {
				soldier.transform.rotation = defaultRotation;
				soldier.transform.rotation = Quaternion.Euler(0f, 0f, 5f);
				soldier.rigidbody2D.velocity = new Vector2(0f, speed);
			}
			break;
		case "left":
			if((Mathf.Round(soldier.transform.position.y * 1000f) / 1000f) == (Mathf.Round(gameObject.GetComponent<BoxCollider2D>().transform.position.y * 1000f) / 1000f)) {
				soldier.transform.rotation = defaultRotation;
				soldier.transform.rotation = Quaternion.Euler(0f, 0f, 100f);
				soldier.rigidbody2D.velocity = new Vector2(-1*speed, 0f);
			}
			break;
		case "right":
			if((Mathf.Round(soldier.transform.position.y * 1000f) / 1000f) == (Mathf.Round(gameObject.GetComponent<BoxCollider2D>().transform.position.y * 1000f) / 1000f)) {
				soldier.transform.rotation = defaultRotation;
				soldier.rigidbody2D.velocity = new Vector2(speed, 0f);
			}
			break;
			
		} // End switch block
		
	} // End OnTriggerEnter2D
	
}
