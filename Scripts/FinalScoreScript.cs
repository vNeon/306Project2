using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FinalScoreScript : MonoBehaviour {

	public Text score;
	public ScoreController scoreController;

	void Start(){
	}

	public void setScore(int LevelID){
		scoreController = ScoreController.getInstance ();
		Debug.Log(scoreController.levelScores [LevelID]);
		score.text = scoreController.levelScores[LevelID]+"";
	}



}
