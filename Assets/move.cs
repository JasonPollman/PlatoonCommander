using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public double speed;
	private Quaternion defaultRotation;
	// Use this for initialization
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (getSpeed(),0f);
		defaultRotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getSpeed(){
		return(float)speed;
	}
	public Quaternion getDefaultRotation(){
		return defaultRotation;
	}
}
