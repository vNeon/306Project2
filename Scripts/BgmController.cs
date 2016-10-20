using UnityEngine;
using System.Collections;

/**
 * Controll the background music 
 **/
public class BgmController : MonoBehaviour {
    AudioSource source;
	// Use this for initialization

    //Set starting volume by reading from playerprefs
	void Start () {
        source = GetComponent<AudioSource>();
        source.ignoreListenerVolume = true;
        source.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
	
    //This is called whenever user changes the volume in a menu.
	public void UpdateVolume () {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
            source.ignoreListenerVolume = true;
        }
        source.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
}
