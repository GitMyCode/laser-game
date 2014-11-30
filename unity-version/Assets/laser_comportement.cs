using UnityEngine;
using System.Collections;

public class laser_comportement : MonoBehaviour {

	public float speed;

	void FixedUpdate (){
		Vector3 test = Vector3.up * speed * Time.deltaTime;
		transform.Translate(test);
	}  


}
