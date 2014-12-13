using UnityEngine;
using System.Collections;

public class EndGamePopUp : MonoBehaviour {

	public Rect windowRect = new Rect(20, 20, 120, 50);
	void OnGUI() {

		//windowRect = GUI.Window(0 , centerRectangle(windowRect), DoMyWindow, "Finish!");
	}
	public static void DoMyWindow(int windowID) {
		if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
			Debug.Log("button");


	}



	Rect centerRectangle ( Rect someRect  ) {
		someRect.x = ( Screen.width - someRect.width ) / 2; 
		someRect.y = ( Screen.height - someRect.height ) / 2;
		
		return someRect;
	}
	
}