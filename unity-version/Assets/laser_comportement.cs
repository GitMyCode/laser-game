using UnityEngine;
using System.Collections;

public class laser_comportement : MonoBehaviour {

	public int speed = 5;
	public static Vector3 startSwipe;
	public static Vector3 end;
	public static Vector3 endnotadapted;
	public float amttomove;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



		/*
		amttomove = speed * Time.deltaTime;
		Vector3 test = Vector3.left * amttomove;
		test = test + Vector3.up * amttomove;

		transform.Translate(test,Space.World);
		//transform.Translate(new Vector3(0,speed * Time.deltaTime,0),Space.World);
		//transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);
		//transform.Translate( Vector3.up * speed* Time.deltaTime, Space.Self );
		//transform.TransformDirection (0, 10, 0);
		*/
	}

	void FixedUpdate (){
		amttomove = speed * Time.deltaTime;
		Vector3 start = Vector3.up;
		//Vector3 start = startSwipe
		//start.x = startSwipe.x;

		float dot = start.x*-end.y + start.y*end.x;

		if (dot > 0) {
			//RIGHT
			/*
			Vector3 test = (new Vector3(endnotadapted.x,0,0)) * amttomove/250;
			test = test + (new Vector3(0,endnotadapted.y,0)) * amttomove/250;
			transform.Translate(test,Space.World);
			*/

			Vector3 test = Vector3.right * amttomove;
			test = test + Vector3.up * amttomove;
			transform.Translate(test,Space.World);


		} else if (dot < 0) {
			//LEFT
			Vector3 test = Vector3.left * amttomove;
			test = test + Vector3.up * amttomove;

			transform.Translate(test,Space.World);
		} else {
			transform.Translate(new Vector3(0,speed * Time.deltaTime,0),Space.World);
		}
	}  
}
