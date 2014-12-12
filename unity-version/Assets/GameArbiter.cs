using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameArbiter : MonoBehaviour {

	public GameObject circleReference;
	public GameObject absorbeReference;
	public GameObject lineReference;
	public IPlayer[] playersReference;

	public static GameObject circlePref ;
	public static GameObject absorbePref;
	public static GameObject linePref;
	public static IPlayer[]  players = new IPlayer[2];


	public static Queue<CollidableEvent> collidableQueue = new Queue<CollidableEvent>();
	public static Queue<Action> actionQueue = new Queue<Action>();

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();


	private GameObject explosion;

	
	public int energyRegeneration;
	private int regenerateCounter = 1;
	// Use this for initialization
	void Start () {
		circlePref = circleReference;
		absorbePref = absorbeReference;
		linePref = lineReference;
		energyRegeneration = 120;

		explosion = (GameObject) Resources.Load("explosion2") as GameObject;

	}

	void FixedUpdate(){

		if(regenerateCounter % energyRegeneration == 0){
			foreach(IPlayer p in players){
				tryAddEnergy(p,1);
			}
			regenerateCounter=1;
		}
		regenerateCounter++;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {


			while(collidableQueue.Count > 0){
				CollidableEvent e = collidableQueue.Dequeue();	
				eventHandling(e);
			}

			while(actionQueue.Count>0){
				Action a = actionQueue.Dequeue();
				switch(a.action){
					case Action.ActionType.DEFENSIVE:
						{
							defensiveAction(a);
						}break;
					case Action.ActionType.ATTACK:
						{
							createLine(a);
						}break;
				}
			}
	}


	public void eventHandling(CollidableEvent e){
		ECollidable type1 = e.coll1.collisionType;
		ECollidable type2 = e.coll2.collisionType;

		if(type1 == type2){
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

		}else if(type1 == ECollidable.GOAL || type2 == ECollidable.GOAL){
			IPlayer hurtPlayer = (type1 == ECollidable.GOAL)? players[(int)e.coll1.playerOwner] : players[(int)e.coll2.playerOwner] ;
			GameObject line = (type1 == ECollidable.GOAL)? e.coll2.gameObject : e.coll1.gameObject ;
			if(lineModelDictionary.ContainsKey(line.name) && tryRemoveLife(hurtPlayer,1)){
				DestroyLine(line);		
			}
		}
	}
	




	public void createLine(Action a){


//		GameObject p = a.owner.getGoal();
//		Vector3 line = new Vector3();
//		Debug.DrawLine(line,Color.red,2,false);

		if(tryRemoveEnergy(a.owner,1)){
			Vector3 birthPosition = a.endPos;
			
			if(a.owner.getGoal().tag == "goalP2"){
				Transform goalTran = a.owner.getGoal().transform;
				birthPosition.y = (goalTran.position.y - goalTran.localScale.y / 2);
			}
			
			LaserModel lm = new LaserModel(a,birthPosition);
			lineModelDictionary.Add(lm.name,lm);

		}


	}


	public void defensiveAction(Action a ){
		List<GameObject> trappedLines = findZoneContainingForCircle(a.endPos);
		if(trappedLines.Count > 0){
			Debug.Log("absorbe!");
			foreach (GameObject gm in trappedLines){
				tryAddEnergy(a.owner,2);
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

		Debug.DrawLine(touchposition, (touchposition * circlePref.transform.lossyScale.x), Color.red);
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
		Methode qui aurait du etre dans l'objet Grid
		Temporairement ici
	 */
	public static void DestroyLine(GameObject line){
		if(line != null && lineModelDictionary.ContainsKey(line.name)){
			lineModelDictionary[line.name].Destroy();
			lineModelDictionary.Remove(line.name);
		}
	}


	private bool tryRemoveLife(IPlayer player, int quantite){
		if(player.Life - quantite >0){
			player.Life -= quantite;
			return true;
		}
		player.Life = 0;
		return false;
	}


	private bool tryAddEnergy(IPlayer player,int quantite){
		if( player.Energy < 5 ){
			player.Energy+= quantite;
			if(player.Energy >5){
				player.Energy = 5;
			}
			return true;
		}
		return false;
	}
	private bool tryRemoveEnergy(IPlayer player,int quantite){
		if(quantite >0 && player.Energy >= quantite ){
			player.Energy -= quantite;
			return true;
		}
		return false;
	}


}
