using UnityEngine;
using System.Collections;

public class absorbLaser : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			
			
			if (this.GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos))
			{
				Instantiate (explosion, this.transform.position, this.transform.rotation);
				Destroy (this.transform.parent.gameObject);
				Destroy (this);
				
			}
			
			
		}
	}
}
