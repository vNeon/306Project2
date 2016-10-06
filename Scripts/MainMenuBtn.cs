using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour {
	
    public void GoToMainMenu() {
        //Load main menu scene
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
