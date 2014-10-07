using UnityEngine;
using System.Collections;

public class RandomPattonQuote : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UILabel Quote = GameObject.Find("Quote").GetComponent<UILabel>();
		Quote.text = GameVars.GetQuote ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
