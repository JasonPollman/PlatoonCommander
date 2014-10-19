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

		//yield return new WaitForSeconds (0.5f); 
		StartCoroutine (TestCoroutine(soldier));



	}
	

	IEnumerator TestCoroutine(GameObject soldier)
	{
		var speed = soldier.GetComponent<move> ().getSpeed ();
		var defaultRotation = soldier.GetComponent<move> ().getDefaultRotation ();

		yield return new WaitForSeconds(2f);
		switch (dir) {
		case direction.down:
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
			soldier.rigidbody2D.velocity = new Vector2(0f, -1*speed);
			break;
		case direction.up:
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 5f);
			soldier.rigidbody2D.velocity = new Vector2(0f, speed);
			break;
		case direction.left:
			soldier.transform.rotation = defaultRotation;
			soldier.transform.rotation = Quaternion.Euler(0f, 0f, 100f);
			soldier.rigidbody2D.velocity = new Vector2(-1*speed, 0f);
			break;
		case direction.right:
			soldier.transform.rotation = defaultRotation;
			soldier.rigidbody2D.velocity = new Vector2(speed, 0f);
			break;
		}
	}



}
