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


	private GameObject lineRenderer;
	private Rect shootingZone = new Rect(0, 0, Screen.width, Screen.height / 3);

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{        	
		Touch t = Input.GetTouch (0);

		if (shootingZone.Contains(t.position)) {

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
					Debug.Log (scalingy);

					Transform local_laser_pref = laser_pref;
					local_laser_pref.localScale = new Vector3(0.15f, scalingy, 1);
					//*******


					Vector3 vectorToTarget = endFingerPos - firstFingerPos;
					float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
					Quaternion rotation = new Quaternion ();
					rotation.eulerAngles = new Vector3 (0, 0,angle-90);


					audio.PlayOneShot (laserSound, 0.5f);
					nextShot = Time.time;
					Rigidbody laser = (Rigidbody) Instantiate (local_laser_pref, objPos, rotation);
				}
	          }
		}
	}

     	

}
