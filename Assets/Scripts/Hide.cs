using UnityEngine;
using System.Collections;

public class Hide : MonoBehaviour {

	public GameObject ItemToClose;

	public bool HiddenInitially = false;
	
	// Use this for initialization
	void Start () {
		if(HiddenInitially) NGUITools.SetActive(ItemToClose, false);
	}

	void OnClick () {
		NGUITools.SetActive(ItemToClose, false);
	}

} // End Hide class
