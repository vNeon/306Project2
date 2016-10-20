using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Go back to the main menu 
**/
public class MainMenuBtn : MonoBehaviour {
	
    public void GoToMainMenu() {
        //Load main menu scene
        Time.timeScale = 1;
        PlayerPrefs.SetInt("PreviousLevel", 1);
        SceneManager.LoadScene("Main Menu");
    }
}
