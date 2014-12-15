using UnityEngine;
using System.Collections;

public class EndGamePopUp : MonoBehaviour {

	public Rect windowRect = new Rect(20, 20, 120, 50);
	void OnGUI() {
		/*
		Vector3 scale = new Vector3();
		scale.x = Screen.width/320; // calculate hor scale
		scale.y = Screen.height/480; // calculate vert scale
		scale.z = 1;
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		
		var groupWidth = 120;
		var groupHeight = 150;
		
		var screenWidth = Screen.width;
		var screenHeight = Screen.height;
		
		var groupX = ( screenWidth - groupWidth ) / 2;
		var groupY = ( screenHeight - groupHeight ) / 2;
		
		GUI.BeginGroup( new Rect( groupX, groupY, groupWidth, groupHeight ) );
		GUI.Box( new Rect( 0, 0, groupWidth, groupHeight ), "Level Select" );
		
		if ( GUI.Button(new Rect( 10, 30, 100, 30 ), "Level 1" ) )
		{
			Application.LoadLevel(1);
		}
		if ( GUI.Button( new Rect( 10, 70, 100, 30 ), "Level 2" ) )
		{
			Application.LoadLevel(2);
		}
		if ( GUI.Button( new Rect( 10, 110, 100, 30 ), "Level 3" ) )
		{
			Application.LoadLevel(3);
		}
		
		GUI.EndGroup();
		//GUI.matrix = svMat;
*/
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