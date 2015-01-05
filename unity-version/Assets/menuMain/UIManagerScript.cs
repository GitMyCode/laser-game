using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

    public static bool multiplayer = false;

	public void StartGameSingle()
	{
        multiplayer = false;
        Application.LoadLevel("LineWar");
	}

    public void StartGameMultiplayer()
    {
        multiplayer = true;
        Application.LoadLevel("LineWar");
    }

}
