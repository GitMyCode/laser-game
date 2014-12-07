using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class botLaser : MonoBehaviour {

	public Transform line_pref;
	private Transform  line;
	static int laserIDCounter =0;
	int laserID=1;

	bool CanShoot = true;
	void Start () {

		 
	}
	
	// Update is called once per frame
	void Update () {

		if (CanShoot) {
			launchLine();
			CanShoot = false;
			StartCoroutine(reload());
		}

	}

	void launchLine(){
		Transform local_laser_pref = line_pref;
		local_laser_pref.localScale = new Vector3(0.15f, 2.0f, 1);
		
		
		PlayerController.lineIDCounter++;
		
		local_laser_pref.name = "laser"+laserID;
		
		line = (Transform) Instantiate (local_laser_pref, this.transform.position, this.transform.rotation);
		
		float speed = -1.0f;
		
		
		line.rigidbody2D.velocity = Vector2.up *speed;
		
		laserID = PlayerController.lineIDCounter;
		line.name = "laser"+laserID;
		
		line.gameObject.name = "laser"+laserID;
		
		LaserModel lm = new LaserModel(line.gameObject,line.gameObject.GetComponent<LaserTrail>());
		PlayerController.lineModelDictionary.Add(line.name,lm);
		
		float length = line.rigidbody2D.velocity.magnitude;
		float time = 1/length;
		line.gameObject.GetComponent<LaserTrail>().lifeTime = time;

	}

	IEnumerator reload(){
		yield return new WaitForSeconds(12);
		CanShoot = true;
	}

}
