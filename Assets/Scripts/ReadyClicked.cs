using UnityEngine;
using System.Collections;

public class ReadyClicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameVars.PlayerReady = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick () {
		NGUITools.SetActive (gameObject.transform.parent.gameObject, false);
		GameVars.PlayerReady = true;
	}
}
