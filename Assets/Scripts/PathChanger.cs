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
		
	} // End Start()

	
	void OnTriggerEnter2D(Collider2D other) {
		
		GameObject soldier = other.gameObject;

		if (soldier.tag != "unit") return;

		Debug.Log (other.name);
		
		StartCoroutine (TestCoroutine(soldier, soldier.transform.position));
		
	}
	
	
	IEnumerator TestCoroutine(GameObject soldier, Vector3 startPos)
	{
		float speed = soldier.GetComponent<move> ().getSpeed ();
		Quaternion defaultRotation = soldier.GetComponent<move> ().getDefaultRotation ();

		Vector3 colPos = gameObject.transform.position;

		Vector3 diff = colPos - startPos;

		float rotation = soldier.transform.rotation.eulerAngles.z;

		if(rotation == 90 || rotation == 270) {
			yield return new WaitForSeconds(Mathf.Abs(diff.x/speed));
		}
		else {
			yield return new WaitForSeconds(Mathf.Abs(diff.y/speed));
		}

		switch (direction) {
		case "down":
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
			soldier.rigidbody2D.velocity = new Vector2(0f, -1*speed);
			break;
		case "up":
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 5f);
			soldier.rigidbody2D.velocity = new Vector2(0f, speed);
			break;
		case "left":
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 100f);
			soldier.rigidbody2D.velocity = new Vector2(-1*speed, 0f);
			break;
		case "right":
			soldier.transform.rotation = defaultRotation;
			soldier.rigidbody2D.velocity = new Vector2(speed, 0f);
			break;
		}
	}
	
}
