using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.RPCnetwork
{
    class NetworkUtil
    {

        public string serializeAction(Action action)
        {
            string serialize = getVector(action.startPos) + ";";
            serialize += getVector(action.endPos) + ";";
            serialize += action.timeInterval + ";";
            serialize += action.action + ";";

            return serialize;
        }

        public static Action DeserializeAction(string actionTxt, IPlayer _owner)
        {
            Vector3 startPos;
            Vector3 endPos;
            float timeInterval;
            Action.ActionType action;

            string[] actionTab = getActionFromString(actionTxt);
            startPos = stringToVector(actionTab[0]);
            endPos = stringToVector(actionTab[1]);
            timeInterval = stringToFloat(actionTab[2]);
            action = (Action.ActionType) stringToAction(actionTab[3]);
            GameObject gm = null;/// EN ATTENDANT

            return new Action(startPos, endPos, timeInterval, action, gm);
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
            //actionTxt = actionTxt.Substring(endActionType + 1);
            // string ownerTxt = actionTxt.Substring(0);
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
            return new Vector3(vectorValues[0], vectorValues[1], vectorValues[2]);
        }

        private static float stringToFloat(string timeIntervalTxt)
        {
            return float.Parse(timeIntervalTxt);
        }

        private static Action.ActionType stringToAction(string actionTypeTxt)
        {
            Action.ActionType type;
            if (actionTypeTxt[0] == 'A')
            {
                type = Action.ActionType.ATTACK;
            }
            else
            {
                type = Action.ActionType.DEFENSIVE;
            }
            return type;
        }

        /*HUMANPLAYER
            
        public enum HumanType{
        NETWORK,
        LOCAL
         };
         
        HumanPlayer.HumanType type;
        void localOrNetworkQueue(Action action)
        {
            if (type == HumanPlayer.HumanType.NETWORK)
            {
                //if (networkView.networkView.isMine)
                // {
                string actionTxt = action.serializeAction(action);               //serialize
                AddActionToQueue(actionTxt);
                // }
                // else
                // {
                //     GameArbiter.Instance.actionQueue.Enqueue(action);
                //  }
            }
            else
            {
                GameArbiter.Instance.actionQueue.Enqueue(action);
            }

        }

        
        [RPC]
        void AddActionToQueue(string actionTxt)
        {
            Action actionFromPlayer = Action.DeserializeAction(actionTxt, this);         //Deserialize
            GameArbiter.Instance.actionQueue.Enqueue(actionFromPlayer);
            if (networkView.isMine)
                networkView.RPC("AddActionToQueue", RPCMode.All, actionTxt);
        }
        */

    }
}
