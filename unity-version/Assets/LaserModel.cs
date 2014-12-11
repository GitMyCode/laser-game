/*
 */

using UnityEngine;
using System.Collections;

public class LaserModel {
	public int hauteur;
	public int largeur;
	public bool estRouge;
	public GameObject head;
	public LaserTrail trail;
	public static int idCounter=0;
	public int id;

	public string name;
	public string lineNameBase = "line";



	public LaserModel(Action action,Vector3 birthPlace){
		float distance = Vector3.Distance(action.startPos,action.endPos);
		float speed    = getSpeedOfLine(distance,action.timeInterval);

		idCounter++;
		this.id  = idCounter; 
		head = (GameObject) GameObject.Instantiate(GameArbiter.linePref, birthPlace,(Quaternion.identity));
		float trailTimer =  getConvertedLengthToTime(head,speed,distance);

		float angleX = action.endPos.x - action.startPos.x;
		float angleY = action.endPos.y - action.startPos.y;
		head.rigidbody2D.velocity = new Vector2(angleX,angleY).normalized *speed;

		head.name = lineNameBase+id;
		trail = head.gameObject.GetComponent<LaserTrail>();
		trail.nameWithId = head.name;
		head.gameObject.GetComponent<rotatingAim>().aim.name = head.name;
		head.gameObject.GetComponent<rotatingAim>().aim.gameObject.name = head.name;
		head.gameObject.name = head.name;
		name = head.name;


	}


	/*
	public LaserModel (GameObject head, LaserTrail trail) {
		idCounter++;
		id = idCounter;
		this.head = head;
		this.trail = trail;



	}
*/


	float getSpeedOfLine(float distance, float interval){
		float speed = 1;
		
		if (interval != 0) {
			speed =  distance/ interval;
			speed = speed / 2;
		}
		return speed;
	}
	
	
	public float getConvertedLengthToTime(GameObject laser, float speed,float distance){
		
		
		
		float time = ((laser.transform.rigidbody2D.velocity.normalized/(laser.rigidbody2D.velocity.magnitude)).magnitude);
		time = time* distance;
		
		return time;
	}





}


