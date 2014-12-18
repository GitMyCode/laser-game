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


}
