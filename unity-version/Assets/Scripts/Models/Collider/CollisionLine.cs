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
        if (lm != null)
            lm.Destroy();
    }

    public void VisitCollision(CollisionLine cl)
    {
        if(lm!=null)
            lm.Destroy();
    }


    public void Accept(ICollidable visitor)
    {
        visitor.VisitCollision(this);
    }
}
