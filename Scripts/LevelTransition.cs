using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * This script is used to transition to the next level once a level has
 * been completed and the "Next Level" button in Exit screen is clicked.
 * It will firstly load the Story Screen level before the next game level.
 **/
public class LevelTransition : MonoBehaviour {

    // Loads the next level
	public void LoadNextLevel(){
		Scene currentScene=SceneManager.GetActiveScene ();
		//if (currentScene.buildIndex == 1) {
		Time.timeScale = 1;
        PlayerPrefs.SetInt("PreviousLevel", currentScene.buildIndex);

        // The third level goes straight to the fourth
        if (currentScene.buildIndex == 4)
        {
            SceneManager.LoadScene(5);
        } else
        {
            // Loads a story scene first
            SceneManager.LoadScene("StoryScreen");
        }
        //}
    }
}
