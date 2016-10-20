using UnityEngine;
using System.Collections;

/**
 * Implementation of RGP, randomly spawn object at predetermine spots
 **/ 
public class SpawnItems : MonoBehaviour {

	public Transform[] SpawnPointsRandom;
    public Transform[] SpawnPointsFixed;
    public float spawnTime = 2.0f;

	// Make this an array if want to spawn multiple objects
	public GameObject[] SpawnedObjects;

    private int objIndex = 0;
    private int spwnFixIndex = 0; 

	// Use this for initialization
	void Start () {
		// Remove this line for calling a single time
		//InvokeRepeating("SpawnNewItem", spawnTime, spawnTime);
		//SpawnNewItem();
	}

    //Spawns an item at any of the listed spawn points.
	public void SpawnNewItemRandom(){
		int spawnPointIndex = Random.Range (0, SpawnPointsRandom.Length);
		//Debug.Log ("Spawned a key at index " + spawnPointIndex);
		Instantiate (SpawnedObjects[objIndex], SpawnPointsRandom[spawnPointIndex].position, SpawnPointsRandom[spawnPointIndex].rotation);
        Debug.Log("Spawned: " + SpawnedObjects[objIndex].name + " at " + spawnPointIndex);
        objIndex++;
	}

    //Spawns an item at a fixed point
    public void SpawnNewItemFixed()
    {
        //Debug.Log("Spawned a key at index " + spawnPointIndex);
        Instantiate(SpawnedObjects[spwnFixIndex], SpawnPointsFixed[spwnFixIndex].position, SpawnPointsFixed[spwnFixIndex].rotation);
        spwnFixIndex++;
        objIndex++;
    }
}
