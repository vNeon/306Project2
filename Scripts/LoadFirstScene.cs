using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadFirstScene : MonoBehaviour {

    // Indicates the difficulty, set in unity
	public int difficulty; 


	public void LoadByIndex(int sceneIndex)
    {
		
        // Resume game 
		Time.timeScale = 1;

        // Save selected difficulty setting into playerprefs
		PlayerPrefs.SetInt ("Difficulty", difficulty);

        // Load the Narrative Scene ("StoryScreen" scene)
        SceneManager.LoadScene(3);


    }

}
