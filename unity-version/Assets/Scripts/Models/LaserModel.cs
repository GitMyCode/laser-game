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
    public static GameObject explosion = (GameObject)Resources.Load("explosion2") as GameObject;
    public static GameObject linePrefab = (GameObject)Resources.Load("Prefabs/LaserWithTrailPref2") as GameObject;

	public int id;

	public string name;
	public string lineNameBase = "line";

	public GameObject owner; 

    static LaserModel(){
        
    }

	public LaserModel(Action action,Vector3 birthPlace){
        
		float distance = Vector3.Distance(action.startPos,action.endPos);
		float speed    = getSpeedOfLine(distance,action.timeInterval);

		this.owner = action.owner;
        
		idCounter++;
		this.id  = idCounter; 
		head = (GameObject) GameObject.Instantiate(linePrefab, birthPlace,(Quaternion.identity));
        Collidable collidable = head.GetComponent<Collidable>();
        collidable.Owner = owner;
        
		float angleX = action.endPos.x - action.startPos.x;
		float angleY = action.endPos.y - action.startPos.y;
		head.rigidbody2D.velocity = new Vector2(angleX,angleY).normalized *speed;
       // head.gameObject.GetComponent<CollidableBase>().playerOwner = (Player)owner;

		head.name = lineNameBase+id;
		head.gameObject.name = head.name;
		name = head.name;


	}


	float getSpeedOfLine(float distance, float interval){
		float speed = 1;
		
		if (interval != 0) {
			speed =  distance/ interval;
			speed = speed / 2;
		}

		if(speed > 80){
			speed = 80;
		}
		return speed;
	}
	
	
	public float getConvertedLengthToTime(GameObject laser, float speed,float distance){
		
		
		
		float time = ((laser.transform.rigidbody2D.velocity.normalized/(laser.rigidbody2D.velocity.magnitude)).magnitude);
		time = time* distance;
		
		return time;
	}

	public void Destroy(){
		GameObject.Destroy(head);
         

	//	GameObject.Destroy(trail.reference);
    //		GameObject.Destroy(trail);
	}
    public void showDieEffect()
    {
        GameObject.Instantiate (explosion, head.transform.position, Quaternion.identity);
    }





}


