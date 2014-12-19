using UnityEngine;
using System.Collections;

public class CollisionGoal : CollidableBase, ICollidable
{

    Player player;

    protected virtual void Start()
    {
        player = transform.parent.GetComponent(typeof(Player)) as Player;
    }


  

    public void VisitCollision(CollisionGoal cg)
    {
    }

    public void VisitCollision(CollisionLine cl)
    {
        GameArbiter.Instance.tryRemoveLife(player, 1);
        player.damageEffect(cl.lm.head.transform.position); ;
    }


    public void Accept(ICollidable visitor)
    {
        visitor.VisitCollision(this);
    }
}
