using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameArbiter : GameBehaviours {


    private static GameArbiter instance;


	public GameObject circleReference;
	public GameObject absorbeReference;
	public GameObject lineReference;
	public IPlayer[] playersReference;

	public static GameObject circlePref ;
	public static GameObject absorbePref;
	public static GameObject linePref;
	public static IPlayer[]  players = new IPlayer[2];


	public Queue<CollidableEvent> collidableQueue = new Queue<CollidableEvent>();
	public Queue<Action> actionQueue = new Queue<Action>();

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();


	private GameObject explosion;

	private VectorGrid m_vectorGrid;
	private VectorGrid m_vectorGrid2;

	public int energyRegeneration;
	private int regenerateCounter = 1;

    public static List<Player> mPlayers;


	float _oldWidth;
	float _oldHeight;
	float _fontSize = 96;
	float Ratio= 20; // public


    public static GameArbiter Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        collidableQueue = new Queue<CollidableEvent>();
        actionQueue = new Queue<Action>();

        //players = initPlayers();

        circlePref = circleReference;
        absorbePref = absorbeReference;
        linePref = lineReference;

        m_vectorGrid = GameObject.Find("VectorGrid").GetComponent<VectorGrid>();
        m_vectorGrid2 = GameObject.Find("VectorGrid2").GetComponent<VectorGrid>();
        explosion = (GameObject)Resources.Load("explosion3") as GameObject;
    }

	// Use this for initialization
    void Start()
    {
        

    }
    

	private bool showEndGamePopUp = false;
	private string winner= "";
	void OnGUI(){



		
		if(showEndGamePopUp){
			float scalex = (float) (Screen.width) / 320.0f; //your scale x
			float scaley = (float) (Screen.height) / 480.0f; //your scale y
			// substitute matrix - only scale is altered from standard
			GUI.skin.textField.fontSize = (int)_fontSize;
			GUI.skin.label.fontSize = (int)_fontSize;
			GUIStyle g = GUI.skin.GetStyle("label");
			g.fontSize = (int)(20.0f + 10.0f * Mathf.Sin(Time.time));


			Rect  w =	GUI.Window(0 , centerRectangle(new Rect(20,20,120*scalex,50*scaley)), DoMyWindow, "Finish!");
			_fontSize = Mathf.Min(Screen.width, Screen.height) / Ratio;

		
		}
	}
	public  void DoMyWindow(int windowID) {
				if (GUI.Button(new Rect(10, 20,  100, 20), winner)){
			showEndGamePopUp = false;
			resetGame();
		}

	}
	Rect centerRectangle ( Rect someRect  ) {
		someRect.x = ( Screen.width - someRect.width ) / 2; 
		someRect.y = ( Screen.height - someRect.height ) / 2;
		
		return someRect;
	}


    public void ProcessTurnEvents()
    {
        while (actionQueue.Count > 0)
        {
            Action a = actionQueue.Dequeue();
            actionHandling(a);
        }
        while (collidableQueue.Count > 0)
        {
            CollidableEvent e = collidableQueue.Dequeue();
            eventHandling(e);
        }


       
    }


    public void actionHandling(Action a)
    {
        switch (a.action)
        {
            case Action.ActionType.DEFENSIVE:
                {
                    defensiveAction(a);
                } break;
            case Action.ActionType.ATTACK:
                {
                    createLine(a);
                } break;
        }

    }
	


	public void eventHandling(CollidableEvent e){
        ECollidable key = e.coll1.collisionType | e.coll2.collisionType;
        if (Rules.rules.ContainsKey(key))
        {
            Rules.rules[key].DynamicInvoke(e);
        }
       // e.coll1.Accept(e.coll2);
       // e.coll2.Accept(e.coll1);

        /*
		ECollidable type1 = e.coll1.collisionType;
		ECollidable type2 = e.coll2.collisionType;

		if(type1 == type2){//Line collision
			if(lineModelDictionary.ContainsKey(e.coll1.name) && lineModelDictionary.ContainsKey(e.coll2.name)){
				LaserModel thisModel =  GameArbiter.lineModelDictionary[e.coll1.name];  	
				LaserModel otherModel = GameArbiter.lineModelDictionary[e.coll2.name];
				
				GameObject head = (this.tag =="lineHead")? e.coll1.gameObject : e.coll2.gameObject;
				if((thisModel.id != otherModel.id)){
					Instantiate (explosion, head.transform.position, Quaternion.identity);
					GameArbiter.DestroyLine(e.coll1.gameObject);
					GameArbiter.DestroyLine(e.coll2.gameObject);

				}
			}

            //Line and Goal collision
		}else if(type1 == ECollidable.GOAL || type2 == ECollidable.GOAL){
            Player hurtPlayer = (type1 == ECollidable.GOAL) ? e.coll1.playerOwner: e.coll2.playerOwner;
            Player attackPlayer = (type1 != ECollidable.GOAL) ? e.coll1.playerOwner: e.coll2.playerOwner;

			GameObject line = (type1 == ECollidable.GOAL)? e.coll2.gameObject : e.coll1.gameObject ;

			if(lineModelDictionary.ContainsKey(line.name)){
				if(tryRemoveLife(hurtPlayer,1)){
					Instantiate (explosion, line.transform.position, Quaternion.identity);
					
                    hurtPlayer.damageEffect(line.transform.position);

					foreach(LaserModel lm in lineModelDictionary.Values){
						Instantiate (explosion, lm.head.transform.position, Quaternion.identity);
					}
					DestroyAllLines();		
				}else{
                    winner = attackPlayer.Name;
					showEndGamePopUp= true;

				}
			}

		}
        */
	}
	




	public void createLine(Action a){

            
        IPlayer p = a.owner.GetComponent<Player>();
		if(tryRemoveEnergy(p,1)){
			Vector3 birthPosition = a.endPos;
			if(p.Goals[0].tag == "goalP2"){
				Transform goalTran = p.Goals[0].transform;
				birthPosition.y = (goalTran.position.y - goalTran.localScale.y / 2);
			}
			
			LaserModel lm = new LaserModel(a,birthPosition);
			lineModelDictionary.Add(lm.name,lm);

		}


	}


	public void defensiveAction(Action a ){
		List<GameObject> trappedLines = findZoneContainingForCircle(a.endPos);
		if(trappedLines.Count > 0){
			foreach (GameObject gm in trappedLines){
				tryAddEnergy(a.owner.GetComponent<Player>(),2);
				LaserModel lm = lineModelDictionary[gm.name];
				lm.head.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
			StartCoroutine(absorbeCircle(a.endPos,trappedLines));
		}else{
			StartCoroutine(defensiveCircle(a.endPos));
		}

	}
	public IEnumerator absorbeCircle(Vector3 pos,List<GameObject> lines){
		GameObject circle = (GameObject) Instantiate(absorbePref, pos,Quaternion.identity);
		circle.GetComponent<ParticleSystem>().Emit(10);
		float durationTime = circle.GetComponent<ParticleSystem>().duration;
		yield return new WaitForSeconds (durationTime);
		foreach (GameObject gm in lines){
			DestroyLine(gm);
		}
		Destroy (circle);
	}

	public IEnumerator defensiveCircle(Vector3 pos){
		GameObject circle = (GameObject) Instantiate(circlePref, pos,Quaternion.identity);
		circle.GetComponent<ParticleSystem>().Emit(10);
		float durationTime = circle.GetComponent<ParticleSystem>().duration;
		yield return new WaitForSeconds (durationTime);

		Destroy (circle);
	}

	
	public List<GameObject> findZoneContainingForCircle(Vector3 touchposition){

		List<GameObject> allLines = new List<GameObject>();
            
		DebugDraw.DrawSphere(touchposition, (circlePref.transform.lossyScale.x), Color.red);
		RaycastHit2D[] hit = Physics2D.CircleCastAll (
			touchposition,
			circlePref.transform.lossyScale.x/2
			,Vector2.zero);

		for (int i =0; i < hit.Length; i++) {
			if(hit[i].collider.tag.Equals("lineHead")){
				allLines.Add(hit[i].collider.gameObject);
			}		
		}
		return allLines;
	}

	public void resetGame(){
		DestroyAllLines();
		foreach(IPlayer player in players){
			player.Life = 5;
			player.Energy = 5;
		}

	}

	/*
		Methode qui aurait du etre dans l'objet Grid
		Temporairement ici
	 */
	public static bool DestroyLine(GameObject line){
		if(line != null && lineModelDictionary.ContainsKey(line.name)){
			lineModelDictionary[line.name].Destroy();
			lineModelDictionary.Remove(line.name);
            return true;
		}
        return false;
	}
	public static void DestroyAllLines(){
		foreach(LaserModel lm in lineModelDictionary.Values){
			lm.Destroy();
		}
		lineModelDictionary.Clear();

	}


	public bool tryRemoveLife(IPlayer player, int quantite){
		if(player.Life - quantite >0){
			player.Life -= quantite;
			return true;
		}
		player.Life = 0;
		return false;
	}


	public bool tryAddEnergy(IPlayer player,int quantite){
		if( player.Energy < 5 ){
			player.Energy+= quantite;
			if(player.Energy >5){
				player.Energy = 5;
			}
			return true;
		}
		return false;
	}
	public bool tryRemoveEnergy(IPlayer player,int quantite){
		if(quantite >=0 && player.Energy >= quantite ){
			player.Energy -= quantite;
			return true;
		}
		return false;
	}

    /*
    private IPlayer[] initPlayers()
    {
        IPlayer[] allPlayers = new IPlayer[2];

        GameObject goal = GameObject.FindGameObjectWithTag("goalP1");
        GameObject zone = GameObject.FindGameObjectWithTag("zoneP1");
        IPlayer player1 = new HumanPlayer(5, 5, zone, goal);
        goal = GameObject.FindGameObjectWithTag("goalP2");
        zone = GameObject.FindGameObjectWithTag("zoneP2");
        IPlayer player2 = new AIPlayer(5, 5, zone, goal);

        allPlayers[0] = player1;
        allPlayers[1] = player2;

        return allPlayers;
        
    }
    */


}
