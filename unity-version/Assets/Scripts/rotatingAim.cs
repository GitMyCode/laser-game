﻿using UnityEngine;
using System.Collections;

public class rotatingAim : MonoBehaviour {
	public GameObject aim;

	public GameObject reference;
	// Use this for initialization
	void Start () {
		GameObject aimeClone = (GameObject) Instantiate (aim, this.transform.position, this.transform.rotation);
		aimeClone.name = aim.name;
		aimeClone.transform.parent = transform;
		aimeClone.transform.localPosition = new Vector3 (0, 0, 0);
		reference = aimeClone;
	}
	
	// Update is called once per frame
	void Update () {




	}
}
