using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Keeps track of the time spent playing in each level
public class Timer : MonoBehaviour {

	public Text timerText;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time - startTime;

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");

		timerText.text = minutes + ":" + seconds;
	}
}
