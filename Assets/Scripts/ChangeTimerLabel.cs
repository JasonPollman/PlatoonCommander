using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ChangeTimerLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer.TimeRemainingFormatted();
	}
	
	// Update is called once per frame
	void Update () {
		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer.TimeRemainingFormatted();

		if(GameTimer.TimeRemaining <= 10 && !audio.isPlaying && GameVars.PlayerReady) {
			audio.Play();
		}
	}
	
}
