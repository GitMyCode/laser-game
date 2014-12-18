using UnityEngine;
using System.Collections;

public class Action {

	public enum ActionType{
		DEFENSIVE,
		ATTACK
	};


	public Vector3 startPos;
	public Vector3 endPos;
	public float timeInterval;

	public ActionType action;

	public IPlayer owner;

	public Action(Vector3 startPos, Vector3 endPos, float time, ActionType type,IPlayer _owner){
		action = type;
		this.startPos = startPos;
		this.endPos = endPos;
		this.timeInterval = time;
		this.owner = _owner;
	}

}
