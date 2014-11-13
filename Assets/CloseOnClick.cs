using UnityEngine;
using System.Collections;

public class CloseOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		Debug.Log ("HERE");
		Destroy (gameObject);
	}
}
