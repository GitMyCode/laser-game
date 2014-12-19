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
        if (GameArbiter.DestroyLine(this.gameObject))
        {
            lm.showDieEffect();
        }
       
    }

    public void VisitCollision(CollisionLine cl)
    {
        if (cl.name != this.name)
        {

            if (GameArbiter.DestroyLine(this.gameObject))
            {
                lm.showDieEffect();
            }
        }
      
    }


    public void Accept(ICollidable visitor)
    {
        visitor.VisitCollision(this);
    }
}
