using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour , ICollidable {

	// Use this for initialization
	public GameObject explosion;

	void awake() { 

	}

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
			if (c != null && 
			    c.tag == "lineTarget" &&
			    PlayerController.lineModelDictionary.ContainsKey(c.name))
				{
				Debug.Log("blocked");
				LaserModel laserModel = PlayerController.lineModelDictionary[c.name];
				Destroy(laserModel.head);
				Destroy(laserModel.trail.reference);
				Destroy(laserModel.trail);
				Instantiate (explosion, laserModel.head.transform.position,Quaternion.identity );

			}
			
			
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {


		if(coll is ICollidable){
			Debug.Log("dsfsdf");
		}


		if(coll.gameObject.tag =="lineTrail" && this.tag == "lineTrail" ){
		//	Debug.Log("tien tien Collision :"+coll.gameObject.tag+ "   " +this.tag);
			
		}


		
	}

	//On trigger peut etre la zone ou le trail d'un laser
	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log("Trigger :"+coll.name + " this :"+this.name);
		if(this.tag == "zoneP1" && coll.tag == "lineHead") {
			coll.gameObject.GetComponent<rotatingAim>().reference.gameObject.renderer.enabled = true;
			
		}





		if( (this.tag =="lineHead" || this.tag == "lineTrail") && (coll.tag == "lineTrail" || coll.tag == "lineHead")){
			LaserModel thisModel =  GameArbiter.lineModelDictionary[this.name];  	
			LaserModel otherModel = GameArbiter.lineModelDictionary[coll.name];

			GameObject head = (this.tag =="lineHead")? this.gameObject : coll.gameObject;
			if((thisModel.id != otherModel.id)){
				
				Debug.Log("Detruire!");
				Instantiate (explosion, head.transform.position, Quaternion.identity);
				GameArbiter.DestroyLine(this.gameObject);
				GameArbiter.DestroyLine(coll.gameObject);


			}
		}





	}
	void OnTriggerExit2D(Collider2D coll){
		if(this.tag == "zoneP1" && coll.tag =="lineHead") {
			//coll.renderer.enabled = false;
			coll.gameObject.GetComponent<rotatingAim>().reference.gameObject.renderer.enabled = false;
			//coll.GetComponent<TargetBehaviorScript>().gameObject.renderer.enabled = false;
		}
	}


}
