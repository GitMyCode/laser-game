using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleModel {




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<GameObject> findZoneContainingForCircle(Vector3 touchposition)
    {

        List<GameObject> allLines = new List<GameObject>();

        DebugDraw.DrawSphere(touchposition, (GameArbiter.Instance.transform.lossyScale.x), Color.red);
        RaycastHit2D[] hit = Physics2D.CircleCastAll(
            touchposition,
            GameArbiter.Instance.transform.lossyScale.x / 2
            , Vector2.zero);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag.Equals("lineHead"))
            {
                allLines.Add(hit[i].collider.gameObject);
            }
        }
        return allLines;
    }

}
