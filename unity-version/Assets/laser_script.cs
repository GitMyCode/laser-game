using UnityEngine;
using System.Collections;

public class laser_script : MonoBehaviour
{

	public Transform laser_pref;
	public Touch touch;
	public AudioClip laserSound;

	public float fireRate = 0.1f;
	public float nextShot = 0.0f;

	public Vector3 objFirstpos;
	public Vector3 firstFingerPos;

	private Transform  laser;
	private Rect shootingZone = new Rect(0, 0, Screen.width, Screen.height / 3);

	public float startTime;
	public float speedOfLaser;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{        	
		Touch t = Input.GetTouch (0);

		if (shootingZone.Contains(t.position)) {

			if (t.phase == TouchPhase.Began) {
				startTime = Time.time;
				if (startTime >= nextShot) {
					firstFingerPos = t.position;
					objFirstpos = Camera.main.ScreenToWorldPoint (firstFingerPos);
					objFirstpos.z = 0.0f;// Sinon Z est a -10.

				}
			}
				
	         if(t.phase == TouchPhase.Ended){
				float endTime = Time.time;
				if (endTime >= nextShot) {

					Vector3 endFingerPos = t.position;
					Vector3 endObjPosInPix = Camera.main.ScreenToWorldPoint (endFingerPos);
					endObjPosInPix.z = 0.0f;// Sinon Z est a -10.

					//SCALING
					float scalingy = objFirstpos.y - endObjPosInPix.y;
					Debug.Log (scalingy);

					Transform local_laser_pref = laser_pref;
					local_laser_pref.localScale = new Vector3(0.15f, scalingy, 1);
					//*******

					//Calcul de l'angle
					Vector3 vectorToTarget = endFingerPos - firstFingerPos;
					float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
					Quaternion rotation = new Quaternion ();
					rotation.eulerAngles = new Vector3 (0, 0,angle-90);
					//**********


					//CalculVitesse
					float distance = Vector3.Distance (objFirstpos, endObjPosInPix);
					float diffTime = endTime - startTime;
					speedOfLaser = 1;

					if (diffTime != 0) {
						speedOfLaser = distance / diffTime;
						speedOfLaser = speedOfLaser / 2;
					}
					//***********


					audio.PlayOneShot (laserSound, 0.5f);
					nextShot = Time.time;

					laser = (Transform )Instantiate (local_laser_pref, endObjPosInPix, rotation);

					laser_comportement laserToSpeedUp = laser.GetComponent<laser_comportement> ();
					laserToSpeedUp.speed = speedOfLaser;

				}
	          }
		}
	}

     	

}
