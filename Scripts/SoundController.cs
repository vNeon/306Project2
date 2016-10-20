using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Control for sound and audio
 **/ 
public class SoundController : MonoBehaviour {

    public Slider soundSlider;
    public Slider musicSlider;
    public Button confirmButton;
    public GameObject BackgroundMusic;
    public Camera mainCamera;
    private BgmController bgmController;
    private SoundEffectVolume sfxController;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        bgmController = BackgroundMusic.GetComponent<BgmController>();
        sfxController = mainCamera.GetComponent<SoundEffectVolume>();

        //Sets the initial volume of the level from player prefs
        confirmButton.onClick.AddListener(ConfirmVolume);
        if (PlayerPrefs.HasKey("SoundEffectsVolume") && PlayerPrefs.HasKey("MusicVolume"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        } else
        {
            soundSlider.value = 0.75f;
            musicSlider.value = 0.5f;
            ConfirmVolume();
        }

    }

    //Called when user changes volume in the pause menu
    public void ConfirmVolume()
    {
        Debug.Log("update vol");
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        bgmController.UpdateVolume();
        sfxController.UpdateVolume();
    }


}
