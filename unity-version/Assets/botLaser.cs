using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class botLaser : MonoBehaviour, IPlayer {

	public Transform line_pref;
	private Transform  line;
	//static int laserIDCounter =0;
	//int laserID=1;

	bool CanShoot = true;

	GameObject zone;
	GameObject goal;
	public int life;
	public int energy;

	GUIText textOutput;
	void Start () {
		zone = GameObject.Find("ZonePlayer2");
		goal = GameObject.FindGameObjectWithTag("goalP2");
		setLife(5);
		setEnergy(5);

		textOutput = GetComponent<GUIText>();
		float scalex = (float) (Screen.width) / 320.0f; //your scale x
		float scaley = (float) (Screen.height) / 480.0f; //your scale y
		Vector2 pixOff = textOutput.pixelOffset; //your pixel offset on screen
		int origSizeText = textOutput.fontSize;
		textOutput.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		textOutput.fontSize = (int) (origSizeText * scalex);
		GameArbiter.players[1] = this;
	}

	public int blockCheckRate = 20;
	public int blockCounter = 1;
	void FixedUpdate() {

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

	}

	// Update is called once per frame
	void Update () {

		if (CanShoot) {
			launchLine();
			CanShoot = false;
			StartCoroutine(reload());
		}
		textOutput.text = ToString();


	}

	void launchLine(){
		float randomX = Random.Range (zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);
		float randomY = Random.Range ((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y));
		Vector3 start = new Vector3(randomX,randomY,0);

		randomX = Random.Range (zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);

		randomY = Random.Range ((zone.transform.position.y - zone.transform.localScale.y / 2) , (zone.transform.position.y + zone.transform.localScale.y / 2) - (randomY+11));
		Vector3 end   = new Vector3(randomX,randomY,0);

		Debug.DrawRay(start,end,Color.red,2,false);
		GameArbiter.actionQueue.Enqueue(new Action(start,end,Random.Range(0.1f,0.3f),Action.ActionType.ATTACK,this));
	
	}

	IEnumerator reload(){
		yield return new WaitForSeconds(1);
		CanShoot = true;
	}

	public GameObject getZone ()
	{
		return zone;
	}

	public GameObject getGoal ()
	{
		return goal;
	}
	public int getLifeRemaining ()
	{
		return life;
	}

	public void setLife (int life)
	{
		this.life = life;
	}

	public int getEnergyRemaining ()
	{
		return this.energy;
	}

	public void setEnergy (int energy)
	{
		this.energy = energy;
	}

	public override string ToString ()
	{
		return string.Format ("[P2: life={0},\n energy={1}]", life, energy);
	}

	public int Life {
		get {
			return this.life;
		}
		set {
			this.life = value;
		}
	}
	
	public int Energy {
		get {
			return energy;
		}
		set {
			this.energy = value;
		}
	}


}
