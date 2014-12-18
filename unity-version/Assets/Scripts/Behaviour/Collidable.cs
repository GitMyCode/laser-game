using UnityEngine;
using System.Collections;

public class Collidable : MonoBehaviour, ICollidable {

	// Use this for initialization
	private GameObject explosion;


	public ECollidable collisionType;
	public EPlayer ePlayerOwner;
    public Player playerOwner;


	void Start(){
		explosion = (GameObject) Resources.Load("explosion") as GameObject;
        playerOwner = transform.parent.GetComponent(typeof(Player)) as Player;
	}



	void awake() {
        playerOwner = gameObject.transform.GetComponent<Player>();
		//explosion = (GameObject) Resources.Load("Assets\\Prefabs\\explosion.prefab") as GameObject;
	}

	void Update() { 

	}





	void OnCollisionEnter2D(Collision2D coll) {
		trySendCollidableEvent(coll.gameObject);
		/*
		if(coll is ICollidable){
			Debug.Log("dsfsdf");
		}

		if(coll.gameObject.tag == "lineHead" && this.tag == "lineHead"){ 
			GameArbiter.DestroyLine(this.gameObject);
			GameArbiter.DestroyLine(coll.gameObject);
		}
*/


		
	}

	//On trigger peut etre la zone ou le trail d'un laser
	void OnTriggerEnter2D(Collider2D coll){

		//Debug.Log("Trigger :"+coll.name + " this :"+this.name);
		//if(this.tag == "zoneP1" && coll.tag == "lineHead") {
		//	coll.gameObject.GetComponent<rotatingAim>().reference.gameObject.renderer.enabled = true;
			
	//	}



		trySendCollidableEvent(coll.gameObject);






	}
	void OnTriggerExit2D(Collider2D coll){
		if(this.tag == "zoneP1" && coll.tag =="lineHead") {
			//coll.renderer.enabled = false;
			//coll.gameObject.GetComponent<rotatingAim>().reference.gameObject.renderer.enabled = false;
			//coll.GetComponent<TargetBehaviorScript>().gameObject.renderer.enabled = false;
		}
	}



	bool trySendCollidableEvent(GameObject other){

		if(this is Collidable && ((GameObject)other).GetComponent<Collidable>() !=null ){
			GameArbiter.Instance.collidableQueue.Enqueue(new CollidableEvent(this,other.GetComponent<Collidable>() ));
			return true;
		}
		return false;
	}


}
