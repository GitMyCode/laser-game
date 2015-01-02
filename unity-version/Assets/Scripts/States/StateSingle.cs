using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StateSingle : StateBase {


    public override string Name { get { return "Game"; } }
    GameObject popUp;
    public override void Awake()
    {

        /*
         * TOTO
         Tout l'initiation necessaire au jeu doit etre fait ici
         */

        base.Awake();
        State = ESubState.Default;
        SceneRoot.Instance.AllPlayers = new Players();
        SceneRoot.Instance.AllPlayers.Awake();
        popUp = GameObject.Find("EndGamePopUp");
        popUp.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (SceneRoot.Instance.SceneState.State == ESubState.EndGame)
        {
            string winnerName = "";
            foreach (PlayerBase p in SceneRoot.Instance.AllPlayers)
            {
                if (p.State != PlayerBase.PlayerState.Dead)
                {
                    winnerName = p.Name;
                }
            }
            popUp.transform.Find("Text").GetComponent<Text>().text = winnerName;
            popUp.SetActive(true);
        }
    }


    public override void OnUIInteract(GameObject reference)
    {
        if (reference.name == "replay")
        {
            SceneRoot.Instance.LoadLevel("LineWar");
            popUp.SetActive(false);
            SceneRoot.Instance.SceneState.State = ESubState.Default;
        }

    }


}
