using UnityEngine;
using System.Collections;

/**
 * Collision with the beer bottle trigger the glitch effect in level 4.
 * 
 **/
public class BeerGlitch : MonoBehaviour {
    private GameObject player;
	private GameObject mainCamera;
    public GameController mainController;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera.GetComponent<GlitchEffect> ().enabled = false;
		mainCamera.GetComponent<GlitchEffect> ().intensity = 0;
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            mainController.AddScore(25); //Add score for each bottle collected

            //Intensify glitch effect on player
			mainCamera.GetComponent<GlitchEffect> ().enabled = true;
            if (mainCamera.GetComponent<GlitchEffect>().intensity < 1) {
                mainCamera.GetComponent<GlitchEffect>().intensity += 0.1f;
            }
			Invoke ("WearOff", 20);
        }

    }

    // Have the glitch effect wear off after 20 seconds
	void WearOff(){
		
		mainCamera.GetComponent<GlitchEffect>().intensity -= 0.1f;
        if (mainCamera.GetComponent<GlitchEffect>().intensity == 0)
        {
            mainCamera.GetComponent<GlitchEffect>().enabled = false;
        }
	}
}
