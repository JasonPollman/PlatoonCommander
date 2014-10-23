using UnityEngine;
using System.Collections;

public class TimerTint : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		// Get the timer tint game object
		UISprite timerTint = GameObject.Find("TimerTint").GetComponent<UISprite>();

		// If the time remaining is between 30-21% of the original time, make the tint yellow
		if(GameTimer.TimeRemaining <= Mathf.Ceil(GameTimer.InitialTime / 3) && GameTimer.TimeRemaining > Mathf.Ceil(GameTimer.InitialTime / 5)) {
			timerTint.color = new Color (92, 70, 0, 0.5f);
		}
		// If the time remaining is < 20% of the original time, make the tint red
		else if(GameTimer.TimeRemaining <= Mathf.Ceil(GameTimer.InitialTime / 5)) {

			timerTint.color = new Color (92, 0, 0, 1f);
		}

	} // End Update()

} // End TimerTint class
