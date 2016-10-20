using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Creates supply boxes which have random items present in them.
 * Priyankit Singh
 * */
public class SupplyBoxItems : MonoBehaviour {
	private bool inTrigger;
	private bool isOpened;
	private GameObject player;
	public GameObject gamecontroller;
	private bool printFlag;
	private string printString = "";

	public GameObject inventoryPanel;
	public GameObject[] inventoryIcons;
    public AudioClip sound;

	/**
	 * Defines items that reside in the supply box. 
	 **/
	[System.Serializable]
	public class SupplyDropCurrency{
		public string name;
		public GameObject item;
		public int rarity;
	}

	public List<SupplyDropCurrency> itemsList = new List<SupplyDropCurrency> ();
	public int notEmptyChance = 80;
	private List<SupplyDropCurrency> itemsInBox = new List<SupplyDropCurrency> ();

	void Start(){
		isOpened = false;
		printFlag = false;
		player = GameObject.FindWithTag("Player");
		calculateLoot ();
	}

	// Check if the player enters the keys collider.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			inTrigger = true;
		}
	}

	// Check if the player enters the keys collider.
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			inTrigger = false;
			printFlag = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (inTrigger && !GameController.getPausedStatus())
		{
			if (Input.GetKeyDown(KeyCode.E) && !isOpened)
			{
                AudioSource.PlayClipAtPoint(sound, transform.position, 0.9f);
				getLoot ();
			}
		}
	}

	/**
	 * Decides what items are going to be present in the supply box
	 **/
	void calculateLoot(){
        //Gives a chance to not get anything
		int calculatedChance = Random.Range (0, 101);
		if (calculatedChance > notEmptyChance) {
			Debug.Log ("no loot");
			return;
		} else {
            //Grants a random selection of items
			int totalWeight = 0;
			for (int i = 0; i < itemsList.Count; i++) {
				totalWeight += itemsList [i].rarity;
			}
			int randomVal = Random.Range (0, totalWeight);

			for (int i = 0; i < itemsList.Count; i++) {
				if (randomVal <= itemsList [i].rarity) {
					//Instantiate (itemsList [i].item, transform.position, Quaternion.identity);
					itemsInBox.Add(itemsList[i]);
				}
				randomVal -= itemsList [i].rarity;
			}
			for (int i = 0; i < itemsInBox.Count; i++) {
				Debug.Log (itemsInBox [i].name);
			}
		}
	}

	/**
	 * Adds the loot to player's inventory when they open the box.
	 * */
	void getLoot(){
		isOpened = true;
		if (itemsInBox.Count == 0) {
			printString += "No loot found";
			printFlag = true;
			return;
		}
		printString = "Items Found: ";
		for (int i = 0; i < itemsInBox.Count; i++) {
			printFlag = true;
			if (itemsInBox [i].name.ToLower() == "axe") {
				gamecontroller.GetComponent<GameController>().setAxeTrue ();
				printString += " |Axe|";
				GameObject icon = Instantiate (inventoryIcons[0]);
				// i.transform.SetParent (inventoryPanel.transform);
				icon.transform.parent = inventoryPanel.transform;
				Debug.Log ("Got Axe");
			} else if (itemsInBox [i].name.ToLower() == "ammo") {
				printString += " |Ammo|";
				player.GetComponent<ThrowPortal> ().addAmmo (2);
				Debug.Log ("Got Ammo");
			} else {
				printString += " |Points|";
                gamecontroller.GetComponent<GameController>().AddScore(100);
			}
		}
	}

	// Displays text to player telling them how to pick up the key.
	void OnGUI()
	{
		if (inTrigger && !GameController.getPausedStatus() && !GameController.getGameOver() && printFlag)
		{
			GUI.Box(new Rect(0, 60, 250, 25), printString);
			return;
		}
		if (inTrigger && !GameController.getPausedStatus() && !GameController.getGameOver() && !isOpened)
		{
			GUI.Box(new Rect(0, 60, 250, 25), "Press E to open box");
			return;
		}

		if (inTrigger && !GameController.getPausedStatus() && !GameController.getGameOver() && isOpened)
		{
			GUI.Box(new Rect(0, 60, 200, 25), "Supply box empty");
			return;
		}
	}

}
