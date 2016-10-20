using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Start at the beginning of the level
 **/ 
public class RestartLevel : MonoBehaviour {
	
    //Restart scene after game over	

    public void RestartScene() {
		
		Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
