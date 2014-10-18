using UnityEngine;
using System.Collections;

public class PathChanger : MonoBehaviour {
	public enum direction {up,down,left,right};
	public direction dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {


		var soldier = other.gameObject;
		if (soldier.tag != "unit") return;
		var speed = soldier.GetComponent<move> ().getSpeed ();
		switch (dir) {
		case direction.down:
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.rigidbody2D.velocity = new Vector2(0f, -1*speed);
			break;
		case direction.up:
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.rigidbody2D.velocity = new Vector2(0f, speed);
			break;
		case direction.left:
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.rigidbody2D.velocity = new Vector2(-1*speed, 0f);
			break;
		case direction.right:
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.transform.Rotate(0f, 0f, 0f);
			soldier.rigidbody2D.velocity = new Vector2(speed, 0f);
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Hit");
		foreach (ContactPoint2D contact in col.contacts) {
			Debug.Log (contact.collider.tag);
			if(contact.collider.tag == "unit"){

			}
		}
	}


}
