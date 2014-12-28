using UnityEngine;
using System.Collections;

public class Collidable : GameBehaviours, ICollidable{

	// Use this for initialization
	private GameObject explosion;


	public ECollidable collisionType;
    public GameObject owner;

    protected virtual void Start()
    {

    }
    
    #region Collision send Event
    void OnCollisionEnter2D(Collision2D coll) {
		trySendCollidableEvent(coll.gameObject);
		
		
	}

	//On trigger peut etre la zone ou le trail d'un laser
	void OnTriggerEnter2D(Collider2D coll){

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

        Collidable otherCollidable = other.GetComponent<Collidable>();
		if(otherCollidable != null){
			GameArbiter.Instance.collidableQueue.Enqueue(new CollidableEvent(this ,otherCollidable));
			return true;
		}
		return false;
    }
    #endregion

    ECollidable ICollidable.CollisionType
    {
        get
        {
            return collisionType;
        }
        set
        {
            collisionType = value;
        }
    }

    public GameObject Owner
    {
        get
        {
            return owner;

        }
        set
        {
            owner = value;
        }
    }
}
