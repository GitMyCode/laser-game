using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameArbiter : MonoBehaviour {

	public GameObject circlePref ;
	
	public static Queue collidableQueue = new Queue();
	public static Queue<Action> actionQueue = new Queue<Action>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(actionQueue.Count >0){

			while(actionQueue.Count>0){
				StartCoroutine(emptyAbsorb(actionQueue.Dequeue().position));
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


}
