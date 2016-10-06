using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadioController : MonoBehaviour {
	public List<RadioStation> stations = new List<RadioStation> ();
	public int currentStationIndex;
	public AudioSource audio;

	public RadioStation currentStation{
		get { return stations [currentStationIndex];}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Press r to play radio

		bool isGamePaused = GameController.getPausedStatus ();
		if (!audio.isPlaying && !isGamePaused && Input.GetKeyDown (KeyCode.R)) {
			PlayAudio ();
			return;
		}
		if (isGamePaused || Input.GetKeyDown (KeyCode.R)) {
			audio.Pause ();
		}
		if (Input.GetKeyDown (KeyCode.KeypadPlus) && audio.volume < 1) {
			audio.volume += 0.1f;
		}
		if (Input.GetKeyDown (KeyCode.KeypadMinus) && audio.volume > 0) {
			audio.volume -= 0.1f;
		}
		if (Input.GetKeyDown(KeyCode.B) && currentStationIndex > 0){
			Debug.Log ("Changing radio station");
			currentStationIndex --;
			audio.Stop ();
			PlayAudio ();
		}
		if (Input.GetKeyDown(KeyCode.N) && currentStationIndex < stations.Count - 1){
			Debug.Log ("Changing radio station");
			currentStationIndex ++;
			audio.Stop ();
			PlayAudio ();
		}
	}

	void PlayAudio(){
		audio.loop = currentStation.tracks [currentStation.currentTrackIndex].loop;
		audio.clip = currentStation.tracks [currentStation.currentTrackIndex].song;
		audio.Play ();
	}
}

[System.Serializable]
public class RadioStation{
	public string stationName;
	public List<StationTrack> tracks = new List<StationTrack>();
	public bool loopAtEnd = true;
	public int currentTrackIndex;

	[System.Serializable]
	public class StationTrack{
		public AudioClip song;
		public bool loop;
	}

}