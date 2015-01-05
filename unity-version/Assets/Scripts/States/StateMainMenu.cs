using UnityEngine;
using System.Collections;

public class StateMainMenu : StateBase {


    public override string Name { get{return "MainMenu";} }

    public override void Awake()
    {
        base.Awake();
        State = ESubState.Default;
       // SceneRoot.Instance.LoadLevel("main");
       // SceneRoot.Instance.SceneState.State = ESubState.Default;
        //Create room
    }

    public override void Update()
    {
        base.Update();
    }


    public override void OnUIInteract(GameObject reference)
    {
        if (reference.name == "StartSingle")
        {
            SceneRoot.Instance.SceneState = new StateSingle();
            SceneRoot.Instance.SceneState.State = ESubState.Default;
            SceneRoot.Instance.LoadLevel("LineWar");
            
        }
        else if (reference.name == "Multiplayer")
        {
            SceneRoot.Instance.SceneState = new StateMultiplayer();
            SceneRoot.Instance.SceneState.State = ESubState.Default;
            SceneRoot.Instance.LoadLevel("LineWar");
           
        }
    }

	
}
