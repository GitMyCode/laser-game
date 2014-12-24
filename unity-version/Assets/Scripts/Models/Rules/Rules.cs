using UnityEngine;
using System.Collections;
using System;

using System.Collections.Generic;

public class Rules : IRules
{

    private  Dictionary<ECollidable, Delegate> rules;


    public Rules()
    {
        rules = new Dictionary<ECollidable, Delegate>();

        rules[(ECollidable.LINE | ECollidable.LINE)] = new Action<CollidableEvent>(LineWithLine);
        rules[(ECollidable.LINE | ECollidable.GOAL)] = new Action<CollidableEvent>(LineWithGoal);
    }


    public bool hasRules(CollidableEvent e)
    {
        ECollidable type1 = e.coll1.collisionType;
        ECollidable type2 = e.coll2.collisionType;
        return rules.ContainsKey((type1 | type2));
    }

    public void applyRules(CollidableEvent e)
    {
        ECollidable key = e.coll1.collisionType | e.coll2.collisionType;
        if (rules.ContainsKey(key))
        {
            rules[key].DynamicInvoke(e);
        }
    }
    
    static Rules()
    {
        
    }


    public  void LineWithLine(CollidableEvent e)
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

    public void LineWithGoal(CollidableEvent e)
    {

        Collidable lineCol = e.getCollidableOfType(ECollidable.LINE);
        Collidable goalCol = e.getCollidableOfType(ECollidable.GOAL);


        if (GameArbiter.lineModelDictionary.ContainsKey(lineCol.name))
        {
            Player hurtPlayer = goalCol.owner.GetComponent<Player>();
            if (hurtPlayer.tryRemoveLife(1))
            {
                LaserModel lineAtGoal = GameArbiter.lineModelDictionary[lineCol.name];
                hurtPlayer.damageEffect(lineAtGoal.head.transform.position);


                foreach (LaserModel lm in GameArbiter.lineModelDictionary.Values)
                {
                    lm.showDieEffect();
                    lm.Destroy();
                }
                GameArbiter.lineModelDictionary.Clear();
                GameArbiter.Instance.gameRest();
                

            }
            else //EndGame!
            {

            }

        }


    }


   
}


