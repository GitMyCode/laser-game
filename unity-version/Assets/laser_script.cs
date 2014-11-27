﻿using UnityEngine;
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

	public Vector3 endObjPosInPix;
	private Transform  laser;
	private Rect shootingZone = new Rect(0, 0, Screen.width, Screen.height / 3);

	public float startTime;
	public float speedOfLaser;

	void Start ()
	{

	}

	void Update ()
	{        	
		Touch t = Input.GetTouch (0);

		if (shootingZone.Contains(t.position)) {

			if (t.phase == TouchPhase.Began) {
				startTime = Time.time;
				if (startTime >= nextShot) {
					startTouchAndConvertion (t); // On convertit la position du touch en pixel pour ne pas avoir a se soucier des differents ecrans.
				}
			}
				
	         if(t.phase == TouchPhase.Ended){
				float endTime = Time.time;
				if (endTime >= nextShot) {
					Vector3 endFingerPos = endTouchAndConvertion (t);
					Transform local_laser_pref = scalingY();
					Quaternion rotation = calculAngle(endFingerPos);			
					calculSpeed (endTime);
					shootLaserSound ();
					laser = (Transform )Instantiate (local_laser_pref, endObjPosInPix, rotation); // Create Laser on Scence
					giveSpeedToLaser ();
				}
	          }
		}
	}

	void startTouchAndConvertion(Touch t){
		firstFingerPos = t.position;
		objFirstpos = Camera.main.ScreenToWorldPoint (firstFingerPos);
		objFirstpos.z = 0.0f;// Sinon Z est a -10.
	}

	Vector3 endTouchAndConvertion(Touch t){
		Vector3 endFingerPos = t.position;
		endObjPosInPix = Camera.main.ScreenToWorldPoint (endFingerPos);
		endObjPosInPix.z = 0.0f;// Sinon Z est a -10.
		return endFingerPos;
	}

	Transform scalingY(){
		float scalingy = objFirstpos.y - endObjPosInPix.y;
		Transform local_laser_pref = laser_pref;
		local_laser_pref.localScale = new Vector3(0.15f, scalingy, 1);
		return local_laser_pref;
	}
		
	Quaternion calculAngle(Vector3 endFingerPos){
		Vector3 vectorToTarget = endFingerPos - firstFingerPos;
		float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion rotation = new Quaternion (); 
		rotation.eulerAngles = new Vector3 (0, 0,angle-90);
		return rotation;
	}

	void calculSpeed(float endTime){
		float distance = Vector3.Distance (objFirstpos, endObjPosInPix);
		float diffTime = endTime - startTime;
		speedOfLaser = 1;

		if (diffTime != 0) {
			speedOfLaser = distance / diffTime;
			speedOfLaser = speedOfLaser / 2;
		}
	}

	void giveSpeedToLaser(){
		laser_comportement laserToSpeedUp = laser.GetComponent<laser_comportement> ();
		laserToSpeedUp.speed = speedOfLaser;
	}

	void shootLaserSound(){
		audio.PlayOneShot (laserSound, 0.5f);
		nextShot = Time.time;
	} 
}
