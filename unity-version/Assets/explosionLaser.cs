using UnityEngine;
using System.Collections;

public class explosionLaser : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "laser") {
			Instantiate (explosion, coll.transform.position, coll.transform.rotation);
			Destroy (this.gameObject);
			Destroy (coll.gameObject);
		}

	}
}
