using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter2D(Collision2D coll) {
		
		LaserTrail thisLaser = this.gameObject.GetComponent<LaserTrail>();
		
		
		if(coll.gameObject.tag == "trail"){
			if(thisLaser.nameWithId == coll.gameObject.name){
			}else{
				//Destroy(this.GetComponent<TrailRendererWith2DCollider>());
				//Destroy(coll.gameObject.GetComponent("Laser2"));
				//Destroy(coll.gameObject.GetComponent<TrailRendererWith2DCollider>());
				LaserTrail otherLaserTrail = coll.gameObject.GetComponent<LaserTrail>();
				
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
		
	}
}
