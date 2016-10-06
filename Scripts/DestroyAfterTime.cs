using UnityEngine;
using System.Collections;
// Destroys the gameObject after a set amount of time.
public class DestroyAfterTime : MonoBehaviour {

	public float destroyTime = 3.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, destroyTime);
	}

}
