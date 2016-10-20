using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Implementation of level 2 puzzle.
 **/ 
public class LevelTwoLogic : MonoBehaviour {

    public float payphonePrice;
    public Text ObjectiveText;
    public Text MoneyText;

    public float moneyHeld;
    public bool notEnoughMoney;
    public bool hasBoughtWater;
    public float waterPrice;
    public GameObject inventoryPanel;
    public GameObject[] inventoryIcons;
    public GameController mainController;
    public SpawnItems spawner;

    public AudioClip moneyAudio;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        moneyHeld = 0;
        hasBoughtWater = false;
        MoneyText.text = "$" + moneyHeld;
    }

    //Begin the task to buy something for the teacher
    public void startQuest() {
        moneyHeld = 6;
        ObjectiveText.text = "Teacher asked me to buy something. I can keep the change.";
        MoneyText.text = "$" + moneyHeld;
        AudioSource.PlayClipAtPoint(moneyAudio, Camera.main.transform.position);
    }

    //Called by interacting with the vending machine.
    public bool spendMoney() {
        if (moneyHeld > waterPrice)
        {
            notEnoughMoney = false;
            moneyHeld -= waterPrice;
            hasBoughtWater = true;

            //Set water bottle image in inventory
            GameObject i;
            i = Instantiate(inventoryIcons[0]);
            i.transform.SetParent(inventoryPanel.transform);

            //update objective
            ObjectiveText.text = "I got the water the teacher wanted. I should give it to her...";
            MoneyText.text = "$" + moneyHeld;
            return true;
        }
        else {
            notEnoughMoney = true;
            return false;
        }
    }

    public void pickupMoney(float m) {
        moneyHeld += m;
        MoneyText.text = "$" + moneyHeld;
    }

    //Called by interacting with the payphone
    public void triggerPayphone() {
        if (moneyHeld < payphonePrice)
        {
            notEnoughMoney = true;
        }
        else { 
            //unlock achievement if user did not buy water
            if (!hasBoughtWater) {
                if (!PlayerPrefs.HasKey("BadStudent"))
                {
                    mainController.UnlockAchievement("BadStudent", "Rebel: Did not buy water");
                }
            }
            //update objective and spawn soccer ball
            moneyHeld -= payphonePrice;
            notEnoughMoney = false;
            ObjectiveText.text = "I guess I'll play by myself. Gotta find a spare ball.";
            spawner.SpawnNewItemRandom();
            MoneyText.text = "$" + moneyHeld;
        }
    }
}
