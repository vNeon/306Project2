using UnityEngine;
using System.Collections;

/**
 * Play the glitch effect audio
 **/ 
public class GlitchSoundEffect : MonoBehaviour {

    public GlitchEffect glitch;
    public AudioSource sound;
    public float initialVolume;
	// Use this for initialization
	void Start () {
        sound.volume = initialVolume;
	}
	
	// This updates the glitch effect vloume based on how glitchy the game is (1 should be maximum)
	void Update () {
        if (glitch.enabled) { 
            sound.volume = glitch.intensity/3;
        }
	}
}
