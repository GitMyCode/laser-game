using UnityEngine;
using System.Collections;

public class centerEneryBar : MonoBehaviour {

     public GameObject ObjectToCenter;
	// Use this for initialization
	void Start () {
        ObjectToCenter = GameObject.Find("LifeBackgroundUIP2");
        ObjectToCenter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
	}
	
}
