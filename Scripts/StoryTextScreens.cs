using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.IO;

/**
 * Author: Dinal Wanniarachchi
 * Script to handle the story sequence in the StoryScreen scene.
 * Dialogue text is created by reading from .txt files containing the text for each "page" 
 * of the story sequence. The first seqeunce is for a screen representing a Journal or
 * Diary with lines written by the protagonist.
 * The second sequence shows the protagonist's thoughts before beginning the next level
 * then the level will load.
 **/

public class StoryTextScreens : MonoBehaviour
{
    private Text storyText;
    private Text clickToCont;
    private RectTransform rect;
    private Image background;
    public Sprite journalSprite;
    public Font journalFont;
    
    private int currentPage = 0;
    private int previousLevel;
    private string[] textFiles;
    
    private List<String> journalPages = new List<String>();
    private List<String> storyPages = new List<String>();

    void Start()
    {
        // Find all required Components
        storyText = GetComponent<Text>();
        background = GetComponentInParent<Image>();
        rect = GetComponent<RectTransform>();
        clickToCont = GameObject.Find("ClickContinue").GetComponent<Text>();

        // Get previous level and get the text files for the next level
        previousLevel = PlayerPrefs.GetInt("PreviousLevel");
        textFiles = getTextFiles();

        // Loads the contents of the textfiles into a list
        journalPages = loadText(textFiles[0]);
        storyPages = loadText(textFiles[1]);
        storyPages.Add("Loading Level...");

        // Skips default page
        nextPage();
    }

    /**
     * Method which transitions to the next page, and changes settings depending on whether the 
     * script is for a journal or story screen.
     * Activated when the user clicks on the text.
     **/
    public void nextPage()
    {
        // If the current page is the final screen, load the next level
        if (currentPage == (journalPages.Count + storyPages.Count-1))
        {
            // If final level complete, go to main Menu screen, else load next level.
            if (previousLevel == 5)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(previousLevel + 1);
            }
        }

        if (currentPage < journalPages.Count)
        {
            // Apply Journal settings
            setBackgroundToJournal();
            // Loads the next page of text
            storyText.text = journalPages[currentPage];
            currentPage += 1;
        }
        else if (currentPage < (journalPages.Count + storyPages.Count))
        {
            // Apply Story settings
            setBackgroundToStory();
            // Updates page text
            storyText.text = storyPages[currentPage - journalPages.Count];
            currentPage += 1;

            // On loading screen, remove "Click to Continue" text
            if (currentPage == (journalPages.Count + storyPages.Count))
            {
                clickToCont.enabled = false;
            }
        }

    }

    /**
     * Sets the background and text space to the Journal settings, changing:
     *  Text colours, Text position, Background sprite and Background colour
     **/
    public void setBackgroundToJournal()
    {
        background.color = new Color32(255, 255, 255, 255);
        background.sprite = journalSprite;
        // Text Style
        storyText.font = journalFont;
        storyText.alignment = TextAnchor.UpperLeft;
        storyText.color = new Color32(40, 30, 150, 255);
        rect.offsetMin = new Vector2(690, 41); // Left-Bottom position of text
        rect.offsetMax = new Vector2(-660, -150); // Right-Top position of text

        clickToCont.color = new Color32(0, 0, 0, 255);
        clickToCont.text = "Click on the page to continue";
    }

    /**
     * Sets the background and text space to the Story settings, changing:
     *  Text colour, Text position and Background colour
     **/
    public void setBackgroundToStory()
    {
        background.color = new Color32(0, 0, 0, 255);
        // Text Style
        storyText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        storyText.alignment = TextAnchor.MiddleCenter;
        storyText.color = new Color32(255, 255, 255, 255);
        rect.offsetMin = new Vector2(0, 0); // Left-Bottom position of text
        rect.offsetMax = new Vector2(0, 0); // Right-Top position of text

        clickToCont.color = new Color32(255, 255, 255, 255);
        clickToCont.text = "Click to continue";
    }

    /**
     * Simple method to get the text files containing the story scripts for the relevant
     * story screens. 
     **/
    public string[] getTextFiles()
    {
        string[] storyFiles = new string[2];
        if (previousLevel == 1)
        {
            // Level 1 story scripts
            storyFiles[0] = "Level1Journal";
            storyFiles[1] = "Level1Story";
        }
        else if (previousLevel == 2)
        {
            // Level 2 story scripts
            storyFiles[0] = "Level2Journal";
            storyFiles[1] = "Level2Story";
        }
        else if (previousLevel == 3)
        {
            // Level 3 story scripts
            storyFiles[0] = "Level3Journal";
            storyFiles[1] = "Level3Story";
        }
        else if (previousLevel == 5)
        {
            // Level 4 Ending story scripts
            storyFiles[0] = "Level4Journal";
            storyFiles[1] = "Level4Story";
        }
        return storyFiles;
    }

    /**
     * This method gets the story script from the specified text file and stores each "page" 
     * of the script in a list of strings, each string represeting one page of text.
     **/
    public List<String> loadText(string fileName)
    {
        List<String> textList = new List<String>();
        string pageOfText = null;
        string textContents;
        
        TextAsset textFile = Resources.Load(fileName) as TextAsset;
        textContents = textFile.text;
        string[] linesInFile = textFile.text.Split("\n"[0]);

        foreach (string line in linesInFile)
        {
            // #ENDPAGE indicates the end of a page in the story text file
            if (line.Contains("#ENDPAGE"))
            {
                textList.Add(pageOfText);
                pageOfText = null;
            }
            else
            {
                pageOfText += line + "\n";
            }
        }
        return textList;
    }
}