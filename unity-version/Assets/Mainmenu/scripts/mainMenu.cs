//Attached to Main Camera
using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {
	public Texture backgroundTexture;
	public GUIStyle playbuttonTexture;
	public string currentMenu;
	//public Transform laser_pref;

	void OnGUI(){

		if(currentMenu == "Main"){
			main_menu();
		}
		/*
        if (Input.GetKeyDown("space")) {
        	print("test event triggering");
        	Instantiate(laser_pref, new Vector3(transform.position.x,transform.position.y + 1, 2), Quaternion.identity);
        }
        */
	}

	private void main_menu(){
		//Display our backgroud texture 
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);
		GUI.skin.label.fontSize = 40;
		/*
		//Display Start Button with GUI outline
		if (GUI.Button(new Rect(Screen.width * .37f, Screen.height * .5f, Screen.width * .25f, Screen.height * .15f),"Play Game")){
			print("clicked LaserPlay");
			Application.LoadLevel("LaserPlay");
		}
		*/

		//Display Start Button without GUI outline
		if (GUI.Button(new Rect(Screen.width * .13f, Screen.height * .6f, Screen.width * .75f, Screen.height * .20f),"",playbuttonTexture)){
			print("clicked Play");
			Application.LoadLevel("LaserPlay");
		}
	}

	void update(){
		/*
		// Check to see if the screen is being touched
        if (Input.touchCount > 0) {
            // Get the touch info
            Touch t = Input.GetTouch(0);
            // Did the touch action just begin?
            if (t.phase == TouchPhase.Began){
            	Instantiate(laser_pref, new Vector3(transform.position.x,transform.position.y + 1, 2), Quaternion.identity); 
            }
        }
        */
  
	}

}
