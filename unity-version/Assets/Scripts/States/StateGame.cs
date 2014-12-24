using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StateGame : StateBase {


    public override string Name { get { return "Game"; } }
    GameObject popUp;
    public override void Awake()
    {
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
            foreach (Player p in SceneRoot.Instance.AllPlayers)
            {
                if (p.State == Player.PlayerState.Winner)
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
            SceneRoot.Instance.LoadLevel("LaserPlay");
            popUp.SetActive(false);
            SceneRoot.Instance.SceneState.State = ESubState.Default;
        }

    }


}
