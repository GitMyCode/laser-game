using UnityEngine;
using System.Collections;

public class laser_comportement : MonoBehaviour {

	public int speed = 5;


	// Use this for initialization
	void Start () {
		//rigidbody2D.velocity = Vector2.one.normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(new Vector3(0,speed * Time.deltaTime,0),Space.World);

		//transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);
		//transform.Translate( Vector3.up * speed* Time.deltaTime, Space.Self );
		//transform.TransformDirection (0, 10, 0);

	}
}
