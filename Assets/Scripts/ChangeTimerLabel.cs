using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ChangeTimerLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Set the time remaining in correct format
		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer.TimeRemainingFormatted();

	} // End Start()
	
	// Update is called once per frame
	void Update () {

		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer.TimeRemainingFormatted();

		// Play the countdown beeping clip when time is <= 10 seconds
		if(GameTimer.TimeRemaining <= 10 && !audio.isPlaying && GameVars.PlayerReady) audio.Play();

	} // End Update()
	
} // End ChangeTimerLabel class
