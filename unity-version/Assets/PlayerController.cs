using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

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

	int lineID=0;

	Vector3 startRawPosition;
	Vector3 endRawPosition;


	Vector3 swipeStartPosition;
	Vector3 swipeEndPosition;
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
		//GUI.Label(shootingZone,Color.blue.ToString());


	}
	 
	// Update is called once per frame
	void Update () {

		if(Input.touchCount == 0){
			return;
		}

		Touch t = Input.GetTouch (0);

		Vector2 tmp = t.position;
		tmp.y = Screen.height - tmp.y;
		//swipeStartPosition = Camera.main.ScreenToWorldPoint (t.position);
		//swipeStartPosition.z= 0f;
		//Debug.Log("Touch :" + t.position+ " converted:"+swipeStartPosition);
		if (shootingZone.Contains(tmp)) {


			if (t.phase == TouchPhase.Began) {

				StartCoroutine(emptyAbsorb(t));

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

	public IEnumerator emptyAbsorb(Touch t){
		Vector3 positionAbosrb = Camera.main.ScreenToWorldPoint (t.position);
		positionAbosrb.z = 0.0f;
		GameObject circle = (GameObject) Instantiate(circleAbsorb, positionAbosrb,Quaternion.identity);
		Animator anim = circle.GetComponent<Animator> ();
		float length = anim.animation.clip.length;
		
		yield return new WaitForSeconds (length);
		Destroy (circle);
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
