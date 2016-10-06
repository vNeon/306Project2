using UnityEngine;
using System.Collections;

public class QuitFromMainMenu : MonoBehaviour {
    
	public void Quit() {
        // Quit if the game is running through the Unity Editor
        if (Application.isEditor)
        {
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
