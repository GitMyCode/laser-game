using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameArbiter : GameBehaviours {


    private static GameArbiter instance;


	public GameObject circleReference;
	public GameObject absorbeReference;
	public GameObject lineReference;

	public static GameObject circlePref ;
	public static GameObject absorbePref;


	public Queue<CollidableEvent> collidableQueue = new Queue<CollidableEvent>();
	public Queue<Action> actionQueue = new Queue<Action>();

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();


    public static List<Player> mPlayers;

    public IRules rules;

    public GameObject pop;


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
        rules = new Rules();

        circlePref = circleReference;
        absorbePref = absorbeReference;

        Debug.Log("Awake gamearbiter");
        pop.gameObject.SetActive(false);
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

        if (rules.hasRules(e))
        {
            rules.applyRules(e);
        }
        
     
	}

    public void restGame()
    {
        StartCoroutine(pauseGame());
    }

    public void endGame()
    {
        pop.transform.Find("Text").GetComponent<Text>().text = "TEST";
        pop.SetActive(true);
    }
    

	public void createLine(Action a){

            
        IPlayer p = a.owner.GetComponent<Player>();
		if(p.tryRemoveEnergy(1)){
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
				a.owner.GetComponent<Player>().tryAddEnergy(2);
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

    IEnumerator pauseGame()
    {
        SceneRoot.Instance.SceneState.State = StateBase.ESubState.Pause;
        yield return new WaitForSeconds(1);

        SceneRoot.Instance.SceneState.State = StateBase.ESubState.Default;
        yield return new WaitForSeconds(1);
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
    /*
	public void resetGame(){
		DestroyAllLines();
		foreach(IPlayer player in players){
			player.Life = 5;
			player.Energy = 5;
		}

	}
    */
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
	public void clearArena(){
		foreach(LaserModel lm in lineModelDictionary.Values){
			lm.Destroy();

		}
		lineModelDictionary.Clear();
        collidableQueue.Clear();
        actionQueue.Clear();

	}


}
