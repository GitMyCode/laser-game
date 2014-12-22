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
    }

    public static void LineWithGoal(CollidableEvent e)
    {

        Debug.Log("event line avec goal");
    }


   
}


