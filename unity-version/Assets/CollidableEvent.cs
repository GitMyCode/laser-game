using UnityEngine;
using System.Collections;

public struct CollidableEvent {

	public Collidable coll1;
	public Collidable coll2;

	public CollidableEvent(Collidable c1, Collidable c2){
		coll1 = c1;
		coll2 = c2;
	}

}
