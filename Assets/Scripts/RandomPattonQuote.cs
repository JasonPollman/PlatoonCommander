using UnityEngine;
using System.Collections;

public class RandomPattonQuote : MonoBehaviour {
	
	void Start () {
		UILabel Quote = GameObject.Find("Quote").GetComponent<UILabel>();
		Quote.text = GameVars.GetQuote ();
	}

}
