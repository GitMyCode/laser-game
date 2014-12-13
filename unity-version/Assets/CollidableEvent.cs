using UnityEngine;
using System.Collections;

public struct CollidableEvent {

	public Collidable coll1;
	public Collidable coll2;

	public CollidableEvent(Collidable c1, Collidable c2){
		coll1 = c1;
		coll2 = c2;
	}


	public Collidable getCollidableOfType(ECollidable type){
		if(coll1.collisionType == type){
			return coll1;
		}
		if(coll2.collisionType == type){
			return coll2;
		}
		return null;
	}

}
