using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public Transform laserPrefabTransform;
	public Touch touch;
	public AudioClip laserSound;
	
	public float fireRate = 0.1f;
	public float nextShot = 0.0f;

	private Transform  line;
	private Rect shootingZone = new Rect(0, 0, Screen.width, Screen.height / 3);
	
	public float startTime;
	public float speedOfLaser;

	public static Dictionary<string, LaserTrail> lineTrailDictionary = new Dictionary<string, LaserTrail>();
	public static string lineNameBase = "player1Line";

	public static Dictionary<string, LaserModel> lineModelDictionary = new Dictionary<string,LaserModel >();
	static int lineIDCounter =0;
	// Use this for initialization

	int lineID=0;

	Vector3 startRawPosition;
	Vector3 endRawPosition;


	Vector3 swipeStartPosition;
	Vector3 swipeEndPosition;

	void Start () {
	
	}
	 
	// Update is called once per frame
	void Update () {

		if(Input.touchCount == 0){
			return;
		}

		Touch t = Input.GetTouch (0);
		  
		if (shootingZone.Contains(t.position)) {


			if (t.phase == TouchPhase.Began) {
				startTime = Time.time;
				if (startTime >= nextShot) {
					startRawPosition = t.position;
					swipeStartPosition = Camera.main.ScreenToWorldPoint (t.position);
					swipeStartPosition.z = 0.0f;

				}
			}
			
			if(t.phase == TouchPhase.Ended){
				float endTime = Time.time;
				if (endTime >= nextShot) {
					endRawPosition = t.position;
					swipeEndPosition = Camera.main.ScreenToWorldPoint (t.position);
					swipeEndPosition.z = 0.0f;// Sinon Z est a -10.


					float distance = Vector3.Distance(swipeStartPosition,swipeEndPosition);
					if(distance< 1){
						return;
					}


					float speed    = getSpeedOfLine(distance,startTime,endTime);

					shootLaserSound ();

					lineIDCounter++;

					line = (Transform)Instantiate(laserPrefabTransform, swipeEndPosition,(Quaternion.identity)); // Create Laser on Scence


					float angleX = swipeEndPosition.x - swipeStartPosition.x;
					float angleY = swipeEndPosition.y - swipeStartPosition.y;
					line.rigidbody2D.velocity = new Vector2(angleX,angleY).normalized *speed;

					float time = getConvertedLengthToTime(line, speed,distance);

					line.gameObject.GetComponent<LaserTrail>().lifeTime = time;
					line.gameObject.GetComponent<LaserTrail>().distance = distance; 

						

					lineID = lineIDCounter;
					line.name = lineNameBase+lineID;
					line.gameObject.GetComponent<LaserTrail>().nameWithId = line.name;
					line.gameObject.GetComponent<rotatingAim>().aim.name = lineNameBase+ lineID;
					line.gameObject.GetComponent<rotatingAim>().aim.gameObject.name = lineNameBase+lineID;
					line.gameObject.name = lineNameBase+lineID;

					LaserModel lm = new LaserModel(line.gameObject,line.gameObject.GetComponent<LaserTrail>());
					lineModelDictionary.Add(line.name,lm);
				}
			}
		}
	
	}

	public float getConvertedLengthToTime(Transform laser, float speed,float distance){



		float time = ((laser.rigidbody2D.velocity.normalized/(laser.rigidbody2D.velocity.magnitude)).magnitude);
		time = time* distance;

		return time;
	}

	float getSpeedOfLine(float distance, float startTime , float endTime){
		float diffTime = endTime - startTime;
		float speed = 1;
		
		if (diffTime != 0) {
			speed =  distance/ diffTime;
			speed = speed / 2;
		}
		return speed;
	}

	void shootLaserSound(){
		audio.PlayOneShot (laserSound, 0.5f);
		nextShot = Time.time;
	} 


}
