using UnityEngine;
using System.Collections;

public class laser_script : MonoBehaviour
{

	public Transform laser_pref;
	public Touch touch;
	public AudioClip laserSound;

	public float fireRate = 0.5f;
	public float nextShot = 0.0f;

	public Vector3 objFirstpos;
	public Vector3 firstFingerPos;


	private GameObject lineRenderer;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{        	
		Touch t = Input.GetTouch (0);

		if (t.phase == TouchPhase.Began) {
			if (Time.time >= nextShot) {
				firstFingerPos = t.position;
				objFirstpos = Camera.main.ScreenToWorldPoint (firstFingerPos);
				objFirstpos.z = 0.0f;// Sinon Z est a -10.
			}
		}
			
         if(t.phase == TouchPhase.Ended){
			if (Time.time >= nextShot) {

				Vector3 endFingerPos = t.position;
				Vector3 objPos = Camera.main.ScreenToWorldPoint (endFingerPos);
				objPos.z = 0.0f;// Sinon Z est a -10.

				//SCALING
				float scalingy = objFirstpos.y - objPos.y;
				Transform local_laser_pref = laser_pref;
				local_laser_pref.localScale = new Vector3(0.15f, scalingy, 1);
				//*******

				Vector3 vectorToTarget = endFingerPos - firstFingerPos;

				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				Quaternion rotation = new Quaternion ();
				rotation.eulerAngles = new Vector3 (0, 0, angle-90);

				//laser_comportement.start = firstFingerPos;
				laser_comportement.startSwipe = firstFingerPos;
				laser_comportement.end = objPos;
				laser_comportement.endnotadapted = endFingerPos;

				Rigidbody laser = (Rigidbody) Instantiate (local_laser_pref, objPos, rotation);
				audio.PlayOneShot (laserSound, 0.5f);
				nextShot = Time.time + fireRate;
			}
          }

	}

     	

}
