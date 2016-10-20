using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


/**
 * Display the pre level's texts
 **/ 
public class DisplayNextText : MonoBehaviour {
    private Text storyText;
    private int currentPage = 0;

    // TODO: Create text for each story segment/level
    private string[] pageTexts = new string[]
    {
        "You feel tired, and disoriented",
        "This is my old home...\n\nI wonder why I'm back here",
        "Maybe I should have a look around."
    };
    
    void Start ()
    {
        storyText = GetComponent<Text>();
        nextPage();
    }

    
    public void nextPage()
    {
        if (currentPage < 3)
        {
            // Loads the next page of text
            storyText.text = pageTexts[currentPage];
            currentPage += 1;
        }
        else
        {
            // Loads first game level ("Interior Scene")
            SceneManager.LoadScene("interior scene");
            // TODO: Load appropriate level according the previously played level 
        }
    }
}