using UnityEngine;
using System.Collections;

public struct CollidableEvent {

	public ICollidable coll1;
	public ICollidable coll2;

	public CollidableEvent(ICollidable c1, ICollidable c2){
		coll1 = c1;
		coll2 = c2;
	}


	public ICollidable getCollidableOfType(ECollidable type){
		if(coll1.CollisionType == type){
			return coll1;
		}
		if(coll2.CollisionType == type){
			return coll2;
		}
		return null;
	}

}
