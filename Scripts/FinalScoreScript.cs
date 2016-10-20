using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Set the final score at completion
 **/ 
public class FinalScoreScript : MonoBehaviour {

	public Text score;
	public ScoreController scoreController;

	void Start(){
	}

    //Called by game controller to set the score text at the end of a level
	public void setScore(int LevelID){
		scoreController = ScoreController.getInstance ();
		Debug.Log(scoreController.levelScores [LevelID]);
		score.text = scoreController.levelScores[LevelID]+"";
	}



}
