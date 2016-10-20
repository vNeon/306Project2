using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Load the first scene
 **/
public class LoadFirstScene : MonoBehaviour {

    // Indicates the difficulty, set in unity
	public int difficulty; 


	public void LoadByIndex(int sceneIndex)
    {
		
        // Resume game 
		Time.timeScale = 1;

        // Save selected difficulty setting into playerprefs
		PlayerPrefs.SetInt ("Difficulty", difficulty);

        // Sets last played level to the main menu
        PlayerPrefs.SetInt("PreviousLevel", 1);

        // Load the Narrative Scene ("StoryScreen" scene)
        SceneManager.LoadScene("StoryScreen");


    }

}
