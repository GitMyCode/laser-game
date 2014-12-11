using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class botLaser : MonoBehaviour {

	public Transform line_pref;
	private Transform  line;
	//static int laserIDCounter =0;
	//int laserID=1;

	bool CanShoot = true;

	GameObject zone;

	void Start () {
		zone = GameObject.Find("ZonePlayer2");
		 
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
		float randomX = Random.Range (zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);
		float randomY = Random.Range ((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y));
		Vector3 start = new Vector3(randomX,randomY,0);

		randomX = Random.Range (zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);
		randomY = Random.Range ((zone.transform.position.y - zone.transform.localScale.y / 2) , (zone.transform.position.y + zone.transform.localScale.y / 2) - (randomY+11));
		Vector3 end   = new Vector3(randomX,randomY,0);

		GameArbiter.actionQueue.Enqueue(new Action(start,end,Random.Range(0.2f,0.5f),Action.ActionType.ATTACK));
	
	}

	IEnumerator reload(){
		yield return new WaitForSeconds(2);
		CanShoot = true;
	}

}
