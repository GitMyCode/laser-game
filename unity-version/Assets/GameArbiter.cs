using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameArbiter : MonoBehaviour {

	public GameObject circleReference;
	public GameObject absorbeReference;
	public GameObject lineReference;

	public static GameObject circlePref ;
	public static GameObject absorbePref;
	public static GameObject linePref;


	public static Queue collidableQueue = new Queue();
	public static Queue<Action> actionQueue = new Queue<Action>();

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();




	// Use this for initialization
	void Start () {
		circlePref = circleReference;
		absorbePref = absorbeReference;
		linePref = lineReference;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(actionQueue.Count >0){

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
	}




	public void createLine(Action a){


//		GameObject p = a.owner.getGoal();
//		Vector3 line = new Vector3();
//		Debug.DrawLine(line,Color.red,2,false);

		Vector3 birthPosition = a.endPos;
		if(a.owner.getGoal().tag == "goalP2"){
			Transform goalTran = a.owner.getGoal().transform;
			birthPosition.y = (goalTran.position.y - goalTran.localScale.y / 2);
		}

		LaserModel lm = new LaserModel(a,birthPosition);
		lineModelDictionary.Add(lm.name,lm);

	}

	float getSpeedOfLine(float distance, float interval){
		float speed = 1;
		
		if (interval != 0) {
			speed =  distance/ interval;
			speed = speed / 2;
		}
		return speed;
	}


	public float getConvertedLengthToTime(Transform laser, float speed,float distance){
		
		
		
		float time = ((laser.rigidbody2D.velocity.normalized/(laser.rigidbody2D.velocity.magnitude)).magnitude);
		time = time* distance;
		
		return time;
	}


	public void defensiveAction(Action a ){
		List<GameObject> trappedLines = findZoneContainingForCircle(a.endPos);
		if(trappedLines.Count > 0){
			Debug.Log("absorbe!");
			foreach (GameObject gm in trappedLines){
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
		if(lineModelDictionary.ContainsKey(line.name)){
			lineModelDictionary[line.name].Destroy();
			lineModelDictionary.Remove(line.name);
		}
	}


	/*
	//This function returns a point which is a projection from a point to a line.
		//The line is regarded infinite. If the line is finite, use ProjectPointOnLineSegment() instead.
	public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point){		
		
		//get vector from point on line to point in space
		Vector3 linePointToPoint = point - linePoint;
		
		float t = Vector3.Dot(linePointToPoint, lineVec);
		
		return linePoint + lineVec * t;
	}



	//This function returns a point which is a projection from a point to a line.
	//The line is regarded infinite. If the line is finite, use ProjectPointOnLineSegment() instead.
	public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point){		
		
		//get vector from point on line to point in space
		Vector3 linePointToPoint = point - linePoint;
		
		float t = Vector3.Dot(linePointToPoint, lineVec);
		
		return linePoint + lineVec * t;
	}


*/
	


}
