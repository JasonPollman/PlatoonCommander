using UnityEngine;
using System.Collections;

public class HideSpriteOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NGUITools.SetActive (gameObject, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
