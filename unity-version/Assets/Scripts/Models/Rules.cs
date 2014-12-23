using UnityEngine;
using System.Collections;
using System;

using System.Collections.Generic;

public class Rules : MonoBehaviour
{

    public static Dictionary<ECollidable, Delegate> rules;
    static Rules()
    {
        rules = new Dictionary<ECollidable, Delegate>();

        rules[(ECollidable.LINE | ECollidable.LINE)] = new Action<CollidableEvent>(LineWithLine);
        rules[(ECollidable.LINE | ECollidable.GOAL)] = new Action<CollidableEvent>(LineWithGoal);
    }


    public static void LineWithLine(CollidableEvent e)
    {

        Debug.Log("event line avec line");
        if (GameArbiter.lineModelDictionary.ContainsKey(e.coll1.name) && 
            GameArbiter.lineModelDictionary.ContainsKey(e.coll2.name))
        {
            LaserModel thisModel = GameArbiter.lineModelDictionary[e.coll1.name];
            LaserModel otherModel = GameArbiter.lineModelDictionary[e.coll2.name];

            thisModel.showDieEffect();
            otherModel.showDieEffect();

            GameArbiter.lineModelDictionary.Remove(e.coll1.name);
            GameArbiter.lineModelDictionary.Remove(e.coll2.name);

            thisModel.Destroy();
            otherModel.Destroy();
        }
    }

    public static void LineWithGoal(CollidableEvent e)
    {

        Collidable lineCol = e.getCollidableOfType(ECollidable.LINE);
        Collidable goalCol = e.getCollidableOfType(ECollidable.GOAL);


        if (GameArbiter.lineModelDictionary.ContainsKey(lineCol.name))
        {
            Player hurtPlayer = goalCol.owner.GetComponent<Player>();
            if (hurtPlayer.tryRemoveLife(1))
            {
                LaserModel lm = GameArbiter.lineModelDictionary[lineCol.name];
                lm.showDieEffect();
                hurtPlayer.damageEffect(lm.head.transform.position);

                GameArbiter.lineModelDictionary.Remove(lineCol.name);
                lm.Destroy();

            }

        }


    }


   
}


