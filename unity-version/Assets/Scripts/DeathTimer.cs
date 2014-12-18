using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {

	public float timeOut = 1.0f;
	public bool detachChildren = false;

	// Use this for initialization
	void Awake () {
	
		Invoke ("DestroyNow", timeOut);
	}

	private void DestroyNow ()
	{
		if (detachChildren) {
			transform.DetachChildren ();
		}
		DestroyObject (gameObject);
	}
}
