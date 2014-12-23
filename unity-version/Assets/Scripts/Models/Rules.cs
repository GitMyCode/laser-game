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
        if (GameArbiter.lineModelDictionary.ContainsKey(e.coll1.Owner.name) && 
            GameArbiter.lineModelDictionary.ContainsKey(e.coll2.Owner.name))
        {
            LaserModel thisModel = GameArbiter.lineModelDictionary[e.coll1.Owner.name];
            LaserModel otherModel = GameArbiter.lineModelDictionary[e.coll2.Owner.name];

            thisModel.showDieEffect();
            otherModel.showDieEffect();

            GameArbiter.DestroyLine(e.coll1.Owner);
            GameArbiter.DestroyLine(e.coll2.Owner);
        }
    }

    public static void LineWithGoal(CollidableEvent e)
    {

        Debug.Log("event line avec goal");
    }


   
}


