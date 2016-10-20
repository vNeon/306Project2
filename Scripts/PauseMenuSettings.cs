using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Implementation of the setting, contain music and sound control
 **/ 
public class PauseMenuSettings : MonoBehaviour {
    
    public Slider soundSlider;
    public Slider musicSlider;
    public Button confirmButton;
    public GameObject BackgroundMusic;
    private BgmController bgmController;
    
	// Use this for initialization

    void Start()
    {
        //Set volume based on player prefs. if it doesn exist set a default value.
        bgmController = BackgroundMusic.GetComponent<BgmController>();
        confirmButton.onClick.AddListener(ConfirmVolume);
        if (PlayerPrefs.HasKey("SoundEffectsVolume") && PlayerPrefs.HasKey("MusicVolume"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            soundSlider.value = 0.75f;
            musicSlider.value = 0.5f;
            ConfirmVolume();
        }
        
	}
	
	// Called by the confirm button for the volume selection menu. This updates player prefs.
    public void ConfirmVolume()
    {
        Debug.Log("update vol");
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        bgmController.UpdateVolume();
    }

}
