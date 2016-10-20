using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Class contians all possible achievements
 * */
public class AchievementsController : MonoBehaviour {

    // Each image represents the achievements in the main menu - used to change the colour of the background to indicate unlocked achievements
    public Image earlyBird;
	public Image firstDoor;
    public Image badStudent;
    public Image goodStudent;
    public Image lumberJack;

	// Use this for initialization
	void Start () {

        //Check whether each of the achievements have been unlocked (in player prefs)
		if (PlayerPrefs.HasKey("FirstDeath")) {
			earlyBird.color = Color.green;
		}

		if (PlayerPrefs.HasKey("FirstDoor")) {
			firstDoor.color = Color.green;
		}

        if (PlayerPrefs.HasKey("BadStudent"))
        {
            badStudent.color = Color.green;
        }

        if (PlayerPrefs.HasKey("GoodStudent"))
        {
            goodStudent.color = Color.green;
        }

        if (PlayerPrefs.HasKey("LumberJack"))
        {
            lumberJack.color = Color.green;
        }
    }

}
