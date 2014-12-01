using UnityEngine;
using System.Collections;

public class collwall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2d(Collider2D collider) {
		Destroy (gameObject);
		Debug.Log ("Collision: " + collider.name);
	}
}
