using UnityEngine;
using System.Collections;

public class TargetBehaviorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 convertedPosition = Camera.main.ScreenToWorldPoint (this.transform.position);
		//convertedPosition.z = 0f;
	/*	Vector2 tmp = this.transform.position;
		tmp.y = Screen.height - tmp.y;
		tmp.x = Screen.width - tmp.x;

		if(PlayerController.shootingZone.Contains(tmp)){
			Debug.Log("show target");
			this.renderer.enabled = true;
		}else{
			this.renderer.enabled = false;
		}*/
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log("enter");
		if(coll.tag == "zoneP1") {
			Debug.Log("enter");
			this.renderer.enabled = true;
			
		}


	}
	void OnTriggerExit2D(Collider2D coll){
		Debug.Log("exit");
		//this.renderer.enabled = false;
		if(coll.tag == "zoneP1") {
			Debug.Log("exit");
			this.renderer.enabled = false;
		}
		
		
	}
}
