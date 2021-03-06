﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPlayer : Player
{




    public int blockCheckRate = 12;
    public int blockCounter = 1;

    bool CanShoot = true;

    void Awake()
    {
        base.Awake();
        // swipeText = (GUIText)GetComponent(typeof(GUIText));
        fingerTrackArray = new Vector3[5];
        startTimeArray = new float[5];
        swipeCompleteArray = new bool[5];
    }

    protected override void Start()
    {
        base.Start();
        Name = "Player2";
        Life = 5;
        Energy = 5;

    }

    //public member vars
    public int swipeLength;
    public int swipeVariance;
    //private member vars
    private GUIText swipeText;
    private Vector3[] fingerTrackArray;
    private float[] startTimeArray;

    private bool[] swipeCompleteArray;
    private int activeTouch = -1;

    public int tapLength = 3;
    //methods

    HashSet<string> alreadyBlocked = new HashSet<string>();

    protected override void GameFixedUpdate()
    {
        base.GameFixedUpdate();
       if (blockCounter % blockCheckRate == 0)
        {

            foreach (LaserModel lm in GameArbiter.lineModelDictionary.Values )
            {
                if (lm.owner != this.gameObject && lm.head != null && (!alreadyBlocked.Contains(lm.head.name)))
                {
                    if (isInZone(lm.head.transform.position))
                    {

                        GameArbiter.Instance.actionQueue.Enqueue(new Action(lm.head.transform.position,
                                                                   lm.head.transform.position,
                                                                   0.1f, Action.ActionType.DEFENSIVE,
                                                                   gameObject
                                                                   ));
                        alreadyBlocked.Add(lm.head.name);
                    }

                }
            }
            blockCounter = 0;
        }
        blockCounter++;
        
       // textOutput.text = ToString();
    }


    protected override void GameUpdate()
    {
        base.GameUpdate();

        


        if (CanShoot)
        {
            launchLine();
            CanShoot = false;
            StartCoroutine(reload());
        }
    }

    void launchLine()
    {
        
        GameObject zone = zones[0];
        float randomX = Random.Range(zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);
        float randomY = Random.Range((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y));
        Vector3 start = new Vector3(randomX, randomY, 0);
        randomX = Random.Range(zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);

        randomY = Random.Range((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y + zone.transform.localScale.y / 2) - (randomY + 11));
        Vector3 end = new Vector3(randomX, randomY, 0);

        Debug.DrawRay(start, end, Color.red, 2, false);
        GameArbiter.Instance.actionQueue.Enqueue(new Action(start, end, Random.Range(0.1f, 0.3f), Action.ActionType.ATTACK, gameObject));
        
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1);
        CanShoot = true;
    }

    public override string ToString()
    {
        return string.Format("AI \n life : {0},\n energy: {1} ", Life, Energy);
    }



}
