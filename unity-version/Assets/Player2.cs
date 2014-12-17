using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2 : MonoBehaviour{

	public Transform line_pref;
	private Transform  line;
	//static int laserIDCounter =0;
	//int laserID=1;

	bool CanShoot = true;

	GameObject zone;
	GameObject goal;
	public int life;
	public int energy;
	public EPlayer playerType;
    private IPlayer player;

	GUIText textOutput;
	void Start () {
		zone = GameObject.Find("ZonePlayer2");
		goal = GameObject.FindGameObjectWithTag("goalP2");
		

        goal = GameObject.FindGameObjectWithTag("goalP2");
        zone = GameObject.FindGameObjectWithTag("zoneP2");
        player  = new AIPlayer(5, 5, zone, goal);


		textOutput = GetComponent<GUIText>();
		float scalex = (float) (Screen.width) / 320.0f; //your scale x
		float scaley = (float) (Screen.height) / 480.0f; //your scale y
		Vector2 pixOff = textOutput.pixelOffset; //your pixel offset on screen
		int origSizeText = textOutput.fontSize;
		textOutput.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		textOutput.fontSize = (int) (origSizeText * scalex);
		GameArbiter.players[(int)playerType] = player;
	}

	public int blockCheckRate = 20;
	public int blockCounter = 1;
	void FixedUpdate() {
        player.playerTurn();
/*
		if (blockCounter % blockCheckRate ==0){ 

			foreach(LaserModel lm in GameArbiter.lineModelDictionary.Values){
				if(lm.owner != (IPlayer) this){
					if(zone.transform.collider2D.bounds.Contains(lm.head.transform.position)){
						GameArbiter.actionQueue.Enqueue(new Action(lm.head.transform.position,
						                                           lm.head.transform.position,
						                                           0.1f,Action.ActionType.DEFENSIVE,
						                                           this));
					}

				}
			}
			blockCounter=0;
		} 
			blockCounter++;
        */
	}

	// Update is called once per frame
	void Update () {
        textOutput.text = player.ToString();
	}

	

	
}
