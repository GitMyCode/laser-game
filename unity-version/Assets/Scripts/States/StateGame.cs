using UnityEngine;
using System.Collections;

public class StateGame : StateBase {


    public override string Name { get { return "Game"; } }

    public override void Awake()
    {
        base.Awake();

        SceneRoot.Instance.AllPlayers = new Players();
        SceneRoot.Instance.AllPlayers.Awake();
    }

    public override void Update()
    {
        base.Update();
    }


    public override void OnUIInteract(GameObject reference)
    {
        if (reference.name == "replay")
        {
            foreach (Player p in SceneRoot.Instance.AllPlayers)
            {
                p.Life = 5;
                p.Energy = 5;
            }
            GameArbiter.Instance.pop.SetActive(false);
            SceneRoot.Instance.SceneState.State = ESubState.Default;
        }

    }


}
