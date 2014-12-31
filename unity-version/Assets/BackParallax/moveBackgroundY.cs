using UnityEngine;
using System.Collections;

public class moveBackgroundY : MonoBehaviour {

    public float parallax = 2f;
	
	// Update is called once per frame
	void Update () {
        Material mat = GetComponent<MeshRenderer>().material;
        Vector2 offset = mat.mainTextureOffset;
        offset.y += Time.deltaTime / 10f / parallax;
        mat.mainTextureOffset = offset;
	}
}
