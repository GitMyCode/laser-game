using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, IPlayer {

	public GameObject circleAbsorb;

	public Transform laserPrefabTransform;

	public Touch touch;
	public AudioClip laserSound;
	
	public float fireRate = 0.1f;
	public float nextShot = 0.0f;

	private Transform  line;
	public static Rect shootingZone = new Rect(0, Screen.height - Screen.height/3, Screen.width, Screen.height / 3);

	
	public float startTime;
	public float speedOfLaser;

	public static Dictionary<string, LaserTrail> lineTrailDictionary = new Dictionary<string, LaserTrail>();
	public static string lineNameBase = "player1Line";

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();
	public static int lineIDCounter =0;
	// Use this for initialization

	//int lineID=0;

	//Vector3 startRawPosition;
	//Vector3 endRawPosition;


	Vector3 swipeStartPosition;
	Vector3 swipeEndPosition;


	public GameObject zone;
	public GameObject goal;
	public int life;
	public int energy;	
	private EPlayer player;


	GUIText textOutput;

	void OnGUI() { 
		/*
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,Color.blue);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(shootingZone, GUIContent.none);

		GameObject zone = GameObject.Find("ZonePlayer1");
*/
		/*
		Vector3 v=Camera.main.WorldToScreenPoint(zone.transform.localPosition);
		Vector3 s=zone.transform.localScale;
		//Debug.Log(zone.transform.localScale.x + " : "+zone.transform.localScale.y);
		
		float height=s.y;//*zone.transform.localScale.y;
		float width=s.x;//*zone.transform.localScale.x;
		Rect rc=new Rect(v.x,Screen.height-v.y,zone.transform.localScale.x,zone.transform.localScale.y);
		//RectTransform t = zone.gameObject.GetComponent<RectTransform>();
		texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,Color.red);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(rc, GUIContent.none);
*/
	}

	void Start () {

		zone = GameObject.Find("ZonePlayer1");
		goal = GameObject.FindGameObjectWithTag("goalP1");
		textOutput = GetComponent<GUIText>();

		setLife(5);
		setEnergy(5);


		float scalex = (float) (Screen.width) / 320.0f; //your scale x
		float scaley = (float) (Screen.height) / 480.0f; //your scale y
		Vector2 pixOff = textOutput.pixelOffset; //your pixel offset on screen
		int origSizeText = textOutput.fontSize;
		textOutput.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		textOutput.fontSize = (int) (origSizeText * scalex);

		GameArbiter.players[0] = this;
		player = EPlayer.Player1;
	}
	 
	// Update is called once per frame
	void Update () {
		textOutput.text = ToString();

		if(Input.touchCount == 0){
			return;
		}

		Touch t = Input.GetTouch (0);

		Vector2 tmp = t.position;
		tmp.y = Screen.height - tmp.y;

		if (shootingZone.Contains(tmp)) {


			if (t.phase == TouchPhase.Began) {



				startTime = Time.time;
				if (startTime >= nextShot) {
					//startRawPosition = t.position;
					swipeStartPosition = Camera.main.ScreenToWorldPoint (t.position);
					swipeStartPosition.z = 0.0f;

				}
			}
			
			if(t.phase == TouchPhase.Ended){
				float endTime = Time.time;
				if (endTime >= nextShot) {
					//endRawPosition = t.position;
					swipeEndPosition = Camera.main.ScreenToWorldPoint (t.position);
					swipeEndPosition.z = 0.0f;// Sinon Z est a -10.


					float distance = Vector3.Distance(swipeStartPosition,swipeEndPosition);
					float intervalTime = endTime - startTime;

					if(distance< 10){
						GameArbiter.actionQueue.Enqueue(new Action(swipeStartPosition,swipeEndPosition,intervalTime,Action.ActionType.DEFENSIVE,this));
						return;
					}


					Action attackAction = new Action(swipeStartPosition,swipeEndPosition,intervalTime,Action.ActionType.ATTACK,this);
					GameArbiter.actionQueue.Enqueue(attackAction);

				
				}
			}
		}
	
	}

	void shootLaserSound(){
		audio.PlayOneShot (laserSound, 0.5f);
		nextShot = Time.time;
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
		return energy;
	}

	public void setEnergy (int energy)
	{
		this.energy = energy;
	}


	public override string ToString ()
	{
		return string.Format ("[P1: life={0},\n energy={1}]", life, energy);
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

	public EPlayer Player {
		get {
			return player;
		}
		set {
			this.player = value;
		}
	}


	
}
