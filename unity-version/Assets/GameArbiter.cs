using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameArbiter : MonoBehaviour {

	public GameObject circleReference;
	public GameObject lineReference;

	public static GameObject circlePref ;
	public static GameObject linePref;


	public static Queue collidableQueue = new Queue();
	public static Queue<Action> actionQueue = new Queue<Action>();

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();




	// Use this for initialization
	void Start () {
		circlePref = circleReference;
		linePref = lineReference;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(actionQueue.Count >0){

			while(actionQueue.Count>0){
				Action a = actionQueue.Dequeue();
				switch(a.action){
					case Action.ActionType.DEFENSIVE:
						{StartCoroutine(emptyAbsorb(a.endPos));}break;
					case Action.ActionType.ATTACK:
						{createLine(a);}break;
				}
			}
		}
	}


	public IEnumerator emptyAbsorb(Vector3 pos){
		GameObject circle = (GameObject) Instantiate(circlePref, pos,Quaternion.identity);
		circle.GetComponent<ParticleSystem>().Emit(10);
		//Animator anim = circle.GetComponent<Animator> ();
		//float length = anim.animation.clip.length;
		float durationTime = circle.GetComponent<ParticleSystem>().duration;
		yield return new WaitForSeconds (durationTime);
		Destroy (circle);
	}

	public void createLine(Action a){
		LaserModel lm = new LaserModel(a,a.endPos);
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

	
	
	public bool findZoneContainingForCircle(Vector3 touchposition){
		
		RaycastHit2D[] hit = Physics2D.CircleCastAll (Camera.main.ScreenToWorldPoint (touchposition),2.0f,Vector2.zero);
		Collider2D collOfHead = null;
		
		for (int i =0; i < hit.Length; i++) {
			if(hit[i].collider.gameObject.tag.Equals("laserHead")){
				collOfHead = hit[i].collider;
				i=hit.Length;
			}		
		}
		
		//Collider2D = hit.collider.tag
		if (collOfHead != null) {
			
			GameObject line;
			/*
			foreach (KeyValuePair<string, LaserModel> entry in laserModelDictionary) {
					line = entry.Value.head;
					string lasername = line.name;
				if (line.name.Equals(collOfHead.gameObject.name)) {
					line.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					return true;
				}
			}
			*/
			LaserModel lm = lineModelDictionary[collOfHead.gameObject.name];
			line = lm.head;
			line.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			return true;
		}
		
		return false;
	}
	


}
