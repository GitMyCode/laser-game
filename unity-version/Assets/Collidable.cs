using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour , ICollidable {

	// Use this for initialization
	public GameObject explosion;

	void start(){

	}


	void awake() { 

	}

	void Update() { 

	}





	void OnCollisionEnter2D(Collision2D coll) {


		if(coll is ICollidable){
			Debug.Log("dsfsdf");
		}

		if(coll.gameObject.tag == "lineHead" && this.tag == "lineHead"){ 
			GameArbiter.DestroyLine(this.gameObject);
			GameArbiter.DestroyLine(coll.gameObject);
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
