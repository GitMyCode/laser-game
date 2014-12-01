using UnityEngine;
using System.Collections;

public class impact : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter2D(Collision2D coll) {
		Instantiate (explosion, coll.transform.position, coll.transform.rotation);
		Destroy (coll.gameObject);
	}
}
