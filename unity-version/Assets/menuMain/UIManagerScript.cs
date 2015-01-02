using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

    public static StateBase state;

	public void StartGameSingle()
	{
        state = new StateSingle();
        Application.LoadLevel("LineWar");
	}

    public void StartGameMultiplayer()
    {
        state = new StateMultiplayer();
		Application.LoadLevel("LineWar");
    }

}
