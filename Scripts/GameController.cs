using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public enum Difficulty { 
        Easy, Normal, Hard
}

/*
 * This is the game controller which is in charge of dealing with
 * scoring, health(sleep), achievements, and scene transitions 
 */
public class GameController : MonoBehaviour {

    
    public UnityEngine.UI.Slider sleepMeter;
    public Text timerText;
    public GameObject gameOverMenu;
    public bool hasTriggeredAchievement = false;
    public string achievementMessage;
	public Transform pauseMenuCanvas;
	public Transform levelCompleteCanvas;
	public Text objectiveText;
    public Text finalTime;
	public Text finalScore;


    private Difficulty difficulty{get; set;}
    private int difMultiplyer;
    private float startTime;
    private bool shouldCursorShow = false;
    private ScoreController scoreController;
    private int currentLevelScore;
    private int levelID;
	private static bool isPaused;
	private static bool gameOver;


	// Use this for initialization
	void Start () {

        //For testing, remove for build
        //PlayerPrefs.DeleteAll();

        //Set difficulty beased on user selection, set levelID and multiplyers
        difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty");
        Debug.Log(difficulty);
        scoreController = ScoreController.getInstance();
        if (SceneManager.GetActiveScene().name == "interior scene") {
            levelID = 0;
        }
        else if (SceneManager.GetActiveScene().name == "level 2") {
            levelID = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Level 3") {
            levelID = 2;
        }//TODO: level 4 id mapping

        
        if (difficulty.Equals(Difficulty.Easy)) {
            difMultiplyer = 1;
        }
        else if (difficulty.Equals(Difficulty.Hard))
        {
            difMultiplyer = 4;
        }
        else {
            difMultiplyer = 2;
        }
        currentLevelScore = 0;
        //Start sleep meter tick down
        StartCoroutine(sleepTickDown());
        startTime = Time.time;
		gameOver = false;

       
        sleepMeter.maxValue = (int)(sleepMeter.maxValue / difMultiplyer);
        
	}

    //Useful variables for other controllers to manage GUI
	public static bool getPausedStatus(){
		return isPaused;
	}

	public static bool getGameOver(){
		return gameOver;
	}
	public static void setGameOver(bool g){
		gameOver = g;
	}


	// Update is called once per frame
	void Update () {
		// following checks if ESC key is pressed. If pressed, then PAUSE. 
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (pauseMenuCanvas.gameObject.activeInHierarchy == false && gameOver == false) {
				Time.timeScale = 0;
				isPaused = true;
                shouldCursorShow = true;
				pauseMenuCanvas.gameObject.SetActive (true);
			} else if(gameOver == false){
				Time.timeScale = 1;
				isPaused = false;
                shouldCursorShow = false;
				pauseMenuCanvas.gameObject.SetActive (false);
			}
		}

		if (Interactable.getLevelStatus () == true) {
			endScoreCalculate ();
			shouldCursorShow = true;
			Time.timeScale = 0;
            gameOver = true;

			levelCompleteCanvas.gameObject.SetActive (true);
			Debug.Log (scoreController.levelScores [levelID] + "This is working!" );
			finalScore.text = scoreController.levelScores[levelID] + "";
            finalTime.text = timerText.text;
			Debug.Log (finalScore.text);
			Interactable.setLevelStatus (false);
		}
        //Update the clock GUI text on the HUD
		float t = Time.time - startTime;

		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f0");

		if (seconds.Length == 1) {
			seconds = "0" + seconds;
		}
		if (minutes.Length == 1) {
			minutes = "0" + minutes;
		}

		timerText.text = minutes + ":" + seconds;

        //Check if cursor should be enabled
		if (shouldCursorShow) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	    else {
	        Cursor.lockState = CursorLockMode.Locked;
	        Cursor.visible = false;
	    }
       
	}

    //Coroutine for sleep meter
    IEnumerator sleepTickDown() {
        //Tick down once per second if not paused
        while(sleepMeter.value > 0)
        {
            yield return new WaitForSecondsRealtime(1);
			if (!isPaused && !gameOver){
                sleepMeter.value = sleepMeter.value - 1;
                //print("Stuff " + sleepMeter.value);
			}
        }

        //This executes when player runs out of sleep
        shouldCursorShow = true;
        gameOverMenu.SetActive(true);
		gameOver = true;
		Time.timeScale = 0;
        //endScoreCalculate();
        //Unlocks achievement on first death
        if (!PlayerPrefs.HasKey("FirstDeath")) {
            UnlockAchievement("FirstDeath", "Early Bird");
        }

    }

    //This unlocks an achievement by storing it in PlayerPref
    public void UnlockAchievement(string key, string name) {
        PlayerPrefs.SetString(key, name);
        PlayerPrefs.Save();
        hasTriggeredAchievement = true;
        achievementMessage = "Achievement Unlocked: " + PlayerPrefs.GetString(key);
    }

    //Called when an objective or a collectable is acquired
    public void AddScore(int score) {
        currentLevelScore += score;
		Debug.Log ("Added score: " + score);
    }

    //Called on level completion, calculates the score
    public void endScoreCalculate() { 
        int completionTime = (int)(Time.time - startTime);
        currentLevelScore -= completionTime;
        currentLevelScore *= difMultiplyer;
        //Prevent negative scores
        if (currentLevelScore < 0) {
            currentLevelScore = 0;
        }
        scoreController.levelScores[levelID] = currentLevelScore;

        Debug.Log("Current level score is: " + scoreController.levelScores[levelID]);
    }

    //A 5 second dialogue box for achievements
    void OnGUI() {
        if (hasTriggeredAchievement) {
            GUI.Box(new Rect(Screen.width - 300, Screen.height - 40, 300, 40), achievementMessage);
            StartCoroutine(achievementTickDown());
        }
    }
    //Used to remove the achievement dialogue after 5 seconds
    IEnumerator achievementTickDown() {
        yield return new WaitForSecondsRealtime(5);
        hasTriggeredAchievement = false;
    }

    //Closes the pause menu
	public void ClosePauseCanvas(){
		pauseMenuCanvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		isPaused = false;
        shouldCursorShow = false;
	}	


}
