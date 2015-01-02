using UnityEngine;
using System.Collections;

public class PlayerAction {

	public enum ActionType{
		DEFENSIVE,
		ATTACK
	};


	public Vector3 startPos;
	public Vector3 endPos;
	public float timeInterval;

	public ActionType action;

	public GameObject owner;

	public PlayerAction(Vector3 startPos, Vector3 endPos, float time, ActionType type,GameObject _owner){
		action = type;
		this.startPos = startPos;
		this.endPos = endPos;
		this.timeInterval = time;
		this.owner = _owner;
	}

}
