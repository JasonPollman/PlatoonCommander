using UnityEngine;
using System.Collections;

public class TimerTint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UISprite timerTint = GameObject.Find("TimerTint").GetComponent<UISprite>();

		if(GameTimer15Min.TimeRemaining <= Mathf.Ceil(GameTimer15Min.InitialTime / 3) &&
		   GameTimer15Min.TimeRemaining > Mathf.Ceil(GameTimer15Min.InitialTime / 5)) {
			timerTint.color = new Color (92, 70, 0, 0.5f);
		}
		else if(GameTimer15Min.TimeRemaining <= Mathf.Ceil(GameTimer15Min.InitialTime / 5)) {

			timerTint.color = new Color (92, 0, 0, 1f);
		}

	}
}
