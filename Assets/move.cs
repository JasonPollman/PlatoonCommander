using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public double speed;
	// Use this for initialization
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (getSpeed(),0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getSpeed(){
		return(float)speed;
	}
}
