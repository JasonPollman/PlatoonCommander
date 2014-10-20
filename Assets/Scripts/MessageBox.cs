using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour {

	public string NextLevel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick () {
		NGUITools.SetActive (gameObject.transform.parent.gameObject, false);
		Application.LoadLevel (NextLevel);
	}
}
