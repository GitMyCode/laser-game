using UnityEngine;
using System.Collections;

public class collwall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D coll) {
		//Destroy (coll.gameObject);
		Destroy (gameObject);
		Debug.Log ("Collision: " + collider.name);
		coll.transform.rotation = Quaternion.Inverse(coll.transform.rotation);
	}


}
