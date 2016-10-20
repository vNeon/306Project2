using System.Collections;
using System.Collections.Generic;  

/**
 * Calculation of the final score
 **/

public class ScoreController{

    public static ScoreController instance = null;
    public int totalPlayerScore; // Keeps track of total score
    public List<int> levelScores; // Keeps track of score for each level

    //Only works for the first 4 levels. If we ever have more than 4 levels more will need to be added here.
    private ScoreController() {
        totalPlayerScore = 0;
        levelScores = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            levelScores.Add(0);
        }
    }

    //Get the scorecontroller instance, create a new one if none exists
    public static ScoreController getInstance() {
        if (instance == null) {
            instance = new ScoreController();          
        }
        return instance;
    }
}
