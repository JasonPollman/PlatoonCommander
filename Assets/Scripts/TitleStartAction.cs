using UnityEngine;
using System.Collections;

public class TitleStartAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// On Mouse Down
	void OnMouseDown() {
		Application.LoadLevel("Map-1");
	}
}
