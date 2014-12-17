using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPlayer : IPlayer {


    private int life;
    private int energy;
    private GameObject zone;
    private GameObject goal;


    public HumanPlayer(int life, int energy, GameObject zone,GameObject goal)
    {
        this.life = life;
        this.energy = energy;
        this.zone = zone;
        this.goal = goal;
    }

    public virtual void Update()
    {

    }

    public void playerTurn()
    {
        if (Input.touchCount == 0)
        {
            return ;
        }

        Touch t = Input.GetTouch(0);

        Vector2 tmp = t.position;
        tmp.y = Screen.height - tmp.y;

        if (isInZone(tmp))
        {


            float startTime = Time.time;
            Vector3 swipeStartPosition = tmp;
            Vector3 swipeEndPosition = tmp;


            if (t.phase == TouchPhase.Began)
            {

                startTime = Time.time;
                //startRawPosition = t.position;
                swipeStartPosition = Camera.main.ScreenToWorldPoint(t.position);
                swipeStartPosition.z = 0.0f;

            }

            if (t.phase == TouchPhase.Ended)
            {
                float endTime = Time.time;
                //endRawPosition = t.position;
                swipeEndPosition = Camera.main.ScreenToWorldPoint(t.position);
                swipeEndPosition.z = 0.0f;// Sinon Z est a -10.


                float distance = Vector3.Distance(swipeStartPosition, swipeEndPosition);
                float intervalTime = endTime - startTime;


                if (distance < 10)
                {
                   GameArbiter.Instance.actionQueue.Enqueue(new Action(swipeStartPosition, swipeEndPosition, intervalTime, Action.ActionType.DEFENSIVE, this));
                   return;
                }

                GameArbiter.Instance.actionQueue.Enqueue(new Action(swipeStartPosition, swipeEndPosition, intervalTime, Action.ActionType.ATTACK, this));
            }
        }
    }

    public System.Collections.Generic.HashSet<Action> getPlayerActions()
    {
        if (Input.touchCount == 0)
        {
            return null;
        }

        Touch t = Input.GetTouch(0);

        Vector2 tmp = t.position;
        tmp.y = Screen.height - tmp.y;

        if (isInZone(tmp))
        {


            float startTime = Time.time;
            Vector3 swipeStartPosition = tmp;
            Vector3 swipeEndPosition = tmp;


            if (t.phase == TouchPhase.Began)
            {

                startTime = Time.time;
                //startRawPosition = t.position;
                swipeStartPosition = Camera.main.ScreenToWorldPoint(t.position);
                swipeStartPosition.z = 0.0f;

            }

            if (t.phase == TouchPhase.Ended)
            {
                float endTime = Time.time;
                //endRawPosition = t.position;
                swipeEndPosition = Camera.main.ScreenToWorldPoint(t.position);
                swipeEndPosition.z = 0.0f;// Sinon Z est a -10.


                float distance = Vector3.Distance(swipeStartPosition, swipeEndPosition);
                float intervalTime = endTime - startTime;

                HashSet<Action> actionSet = new HashSet<Action>();

                if (distance < 10)
                {
                    actionSet.Add(new Action(swipeStartPosition, swipeEndPosition, intervalTime, Action.ActionType.DEFENSIVE, this));
                    return actionSet;
                }


                Action attackAction = new Action(swipeStartPosition, swipeEndPosition, intervalTime, Action.ActionType.ATTACK, this);
                actionSet.Add(attackAction);
                return actionSet;
            }
        }
        return null;
    }

    public bool isInZone(Vector3 point)
    {
        return zone.transform.collider2D.bounds.Contains(point);
    }

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }

    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
        }
    }

    public EPlayer Player
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }




    public GameObject Zone
    {
        get
        {
            return zone;
        }
        set
        {
            zone = value;
        }
    }

    public override string ToString()
    {
        return string.Format("[P1: life={0},\n energy={1}]", life, energy);
    }



    public GameObject Goal
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

   
}
