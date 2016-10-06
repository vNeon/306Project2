using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadNextLevel(){
		Scene currentScene=SceneManager.GetActiveScene ();
		if (currentScene.buildIndex == 1) {
			Time.timeScale = 1;
			SceneManager.LoadScene (2);
		}
	}
}
