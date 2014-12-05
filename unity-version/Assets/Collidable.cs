using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour {

	// Use this for initialization
	public GameObject explosion;

	void Start () {
	
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
			//Destroy(otherLaserTrail.trans.gameObject);
			//Destroy(coll.gameObject);

			//Destroy(thisLaserTrail.reference);
			//Destroy(thisLaserTrail.gameObject);
			//Destroy(this.gameObject);
			/*
			Destroy(thisLaserTrail.trans.gameObject);
			Destroy(thisLaserTrail.GetComponent<Collider2D>());
			Destroy(this.GetComponent<Collider2D>());
			Destroy(thisLaserTrail.GetComponent<PolygonCollider2D>());
			Destroy(this.GetComponent<PolygonCollider2D>());

			Destroy(thisLaserTrail.gameObject);
			Destroy(GameObject.Find(thisLaserTrail.name));
			Destroy(this.collider);
			Destroy(this.gameObject);
			Destroy(this);
			Destroy(GameObject.Find(this.name));
			*/
//Destroy(this.gameObject);


		}



	}


}
