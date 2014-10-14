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

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Hit");
		foreach (ContactPoint2D contact in col.contacts) {
			Debug.Log (contact.collider.tag);
			if(contact.collider.tag == "unit"){
				switch (dir) {
				case direction.down:
					//182
					contact.collider.transform.Rotate(0f, 0f, 0f);
					contact.collider.transform.Rotate(0f, 0f, 182f);
					contact.collider.rigidbody2D.velocity = new Vector2(0f, -0.05f);
						break;
				case direction.up:
					contact.collider.transform.Rotate(0f, 0f, 45f);
					contact.collider.rigidbody2D.velocity = new Vector2(0f, 0.05f);
						break;
				case direction.left:
					//-270
					contact.collider.transform.Rotate(0f, 0f, 0f);
					contact.collider.transform.Rotate(0f, 0f, -270f);
					contact.collider.rigidbody2D.velocity = new Vector2(-0.05f, 0f);
						break;
				case direction.right:
					//270
					contact.collider.transform.Rotate(0f, 0f, 0f);
					contact.collider.transform.Rotate(0f, 0f, 270f);
					contact.collider.rigidbody2D.velocity = new Vector2(0.05f, 0f);
						break;
				}
			}
		}
	}


}
