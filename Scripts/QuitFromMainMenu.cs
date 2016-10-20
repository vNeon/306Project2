using UnityEngine;
using System.Collections;

/**
 * Quit from the main menu
 **/ 
public class QuitFromMainMenu : MonoBehaviour {
    
	public void Quit() {
        // Quit if the game is running through the Unity Editor
        if (Application.isEditor)
        {
            //This bit is needed to prevent some errors
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit();
        }
    }
}
