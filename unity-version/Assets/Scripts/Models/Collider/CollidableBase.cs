using UnityEngine;
using System.Collections;

public abstract class CollidableBase : GameBehaviours, ICollidable{

	// Use this for initialization
	private GameObject explosion;


	public ECollidable collisionType;
	public EPlayer ePlayerOwner;
    public Player playerOwner;



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
        
		if(this is CollidableBase && ((GameObject)other).GetComponent(typeof(ICollidable)) !=null ){
            ICollidable a = ((GameObject)other).GetComponent(typeof(ICollidable)) as ICollidable;
			GameArbiter.Instance.collidableQueue.Enqueue(new CollidableEvent(this ,a));
			return true;
		}
		return false;
    }
    #endregion


    public void VisitCollision(CollisionGoal cg)
    {
        throw new System.NotImplementedException();
    }

    public void VisitCollision(CollisionLine cl)
    {
        throw new System.NotImplementedException();
    }

    public void Accept(ICollidable visitor)
    {
        throw new System.NotImplementedException();
    }
}
