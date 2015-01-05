using UnityEngine;
using System.Collections;

public class myAction {

	public enum ActionType{
		DEFENSIVE,
		ATTACK
	};


	public Vector3 startPos;
	public Vector3 endPos;
	public float timeInterval;

	public ActionType action;

	public GameObject owner;


	public myAction(Vector3 startPos, Vector3 endPos, float time, ActionType type,GameObject _owner){

		action = type;
		this.startPos = startPos;
		this.endPos = endPos;
		this.timeInterval = time;
		this.owner = _owner; 
	}

    public string serializeAction(myAction action)
    {
        string serialize = getVector(action.startPos) + ";";
        serialize += getVector(action.endPos) + ";";
        serialize += action.timeInterval + ";";
        serialize += action.action + ";";

        return serialize;
    }

    public static myAction DeserializeAction(string actionTxt)
    {
        Vector3 startPos;
        Vector3 endPos;
        float timeInterval;
        myAction.ActionType action;

        string[] actionTab = getActionFromString(actionTxt);
        startPos = stringToVector(actionTab[1]);
        endPos = stringToVector(actionTab[0]);
        timeInterval = stringToFloat(actionTab[2]);
        action = (myAction.ActionType)stringToAction(actionTab[3]);
        GameObject gm = GameObject.Find("Player2");


        if (action == ActionType.DEFENSIVE) { // to circle to appear on top of screen 
            startPos.y = startPos.y * -1.0f;
            endPos.y = endPos.y * -1.0f;
        }

        return new myAction(startPos, endPos, timeInterval, action, gm);
    }

    private string getVector(Vector3 vec)
    {

        string vecString = vec.ToString();
        vecString = vecString.Substring(1, vecString.Length - 2);
        return vecString;
    }

    public static string[] getActionFromString(string actionTxt)
    {
        string[] actionTab = new string[4];

        int endVector1 = actionTxt.IndexOf(";");
        string vector1 = actionTxt.Substring(0, endVector1);
        actionTab[0] = vector1;
        actionTxt = actionTxt.Substring(endVector1 + 1);

        int endVector2 = actionTxt.IndexOf(";");
        string vector2 = actionTxt.Substring(0, endVector2);
        actionTab[1] = vector2;
        actionTxt = actionTxt.Substring(endVector2 + 1);

        int endTimeLapse = actionTxt.IndexOf(";");
        string timeIntervalTxt = actionTxt.Substring(0, endTimeLapse);
        actionTab[2] = timeIntervalTxt;
        actionTxt = actionTxt.Substring(endTimeLapse + 1);

        int endActionType = actionTxt.IndexOf(";");
        string actionTypeTxt = actionTxt.Substring(0, endActionType);
        actionTab[3] = actionTypeTxt;

        return actionTab;
    }

    private static Vector3 stringToVector(string vectorTxt)
    {
        float[] vectorValues = new float[3];
        int separation;
        for (int i = 0; i < 2; i++)
        {
            separation = vectorTxt.IndexOf(",");
            string vectorValue = vectorTxt.Substring(0, separation);
            vectorValues[i] = float.Parse(vectorValue);
            vectorTxt = vectorTxt.Substring(separation + 1);
        }
        vectorValues[2] = float.Parse(vectorTxt);

        return new Vector3(vectorValues[0], vectorValues[1] , vectorValues[2]);
    }

    private static float stringToFloat(string timeIntervalTxt)
    {
        return float.Parse(timeIntervalTxt);
    }

    private static myAction.ActionType stringToAction(string actionTypeTxt)
    {
        myAction.ActionType type;

        if (actionTypeTxt[0] == 'A')
        {
            type = myAction.ActionType.ATTACK;
        }
        else
        {
            type = myAction.ActionType.DEFENSIVE;
        }
        return type;
    }

}
