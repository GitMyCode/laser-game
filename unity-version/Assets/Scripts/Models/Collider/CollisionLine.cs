using UnityEngine;
using System.Collections;

public class CollisionLine : CollidableBase, ICollidable {


    public LaserModel lm;

    protected virtual void Start()
    {
        CollisionLine test = new CollisionLine();
    }


    public void VisitCollision(CollisionGoal cg)
    {
        GameArbiter.DestroyLine(this.gameObject);
       
    }

    public void VisitCollision(CollisionLine cl)
    {

        GameArbiter.DestroyLine(this.gameObject);
      
    }


    public void Accept(ICollidable visitor)
    {
        visitor.VisitCollision(this);
    }
}
