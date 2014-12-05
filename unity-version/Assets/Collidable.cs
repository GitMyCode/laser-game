using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour {

	// Use this for initialization
	public GameObject explosion;


	void Update() { 
		if (Input.touchCount > 0 )
		{
			Touch tap = Input.GetTouch(0);
			if(tap.phase == TouchPhase.Ended ){ 
				return;
			}

			Vector3 wp = Camera.main.ScreenToWorldPoint(tap.position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);


			Collider2D c = Physics2D.OverlapPoint(touchPos); 	
			if (c != null && PlayerController.laserModelDictionary.ContainsKey(c.name)){
				Debug.Log("blocked");
				LaserModel laserModel = PlayerController.laserModelDictionary[c.name];
				Destroy(laserModel.head);
				Destroy(laserModel.trail.reference);
				Destroy(laserModel.trail);
				Instantiate (explosion, laserModel.head.transform.position,Quaternion.identity );

			}
			
			
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		/*
		
		//LaserTrail thisLaser = this.gameObject.GetComponent<LaserTrail>();

		if(coll.gameObject.tag == "laserTrail"){
			Debug.Log("coll: Trail   " + "this: "+this.gameObject.tag);
		}

		if(coll.gameObject.tag == "laserHead"){
			Debug.Log("coll: Head   " + "this: "+this.gameObject.tag);
		}
		
		if(coll.gameObject.tag == ""){
			if(thisLaser.nameWithId == coll.gameObject.name){
			}else{
				//Destroy(this.GetComponent<TrailRendererWith2DCollider>());
				//Destroy(coll.gameObject.GetComponent("Laser2"));
				//Destroy(coll.gameObject.GetComponent<TrailRendererWith2DCollider>());
				//LaserTrail otherLaserTrail = coll.gameObject.GetComponent<LaserTrail>();
				
				Destroy(coll.gameObject);
				if(otherLaserTrail!= null){
					Destroy(GameObject.Find(otherLaserTrail.nameWithId));
				}else{
					Destroy(coll.gameObject.GetComponent("Laser2(Clone)"));
				}
				Destroy(GameObject.Find(thisLaser.nameWithId));
				Destroy(this.gameObject);
			}
		}
		if(coll.gameObject.tag == "goal"){
			Debug.Log("Detruit le laser il a atteint un but");
			Destroy(GameObject.Find(thisLaser.nameWithId));
			Destroy(this.gameObject);
			
			
			
		}
		*/
		
	}

	//On trigger peut etre la zone ou le trail d'un laser
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Trigger :"+coll.name + " this :"+this.name);

	

		if(this.tag =="laserHead" && coll.tag == "laserTrail" ){
			LaserTrail thisLaserTrail =  PlayerController.laserTrailDictionary[this.name];  	
			LaserTrail otherLaserTrail = PlayerController.laserTrailDictionary[coll.name];
			
			if((thisLaserTrail.laserID != otherLaserTrail.laserID)){
				
				Debug.Log("Detruire!");
				Instantiate (explosion, this.transform.position, this.transform.rotation);
				
				LaserModel thisModel = PlayerController.laserModelDictionary[this.name];
				LaserModel otherModel = PlayerController.laserModelDictionary[coll.name];
				
				Destroy(thisModel.head);
				Destroy(thisModel.trail.reference);
				Destroy(thisModel.trail);
				
				Destroy(otherModel.head);
				Destroy(otherModel.trail.reference);
				Destroy(otherModel.trail);
				
				PlayerController.laserModelDictionary.Remove(coll.name);
				PlayerController.laserModelDictionary.Remove(this.name);

			}
		}





	}


}
