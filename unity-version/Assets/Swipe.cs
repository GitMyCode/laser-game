/*
 
Swipe gesture for iPhone. Add this script and a GUIText component to an empty GO
With the GO selected in the inspector set:
swipeLength // this is how long you want the swipe to be. 25 pixels seems ok
swipeVariance // this is how far the drag can go 'off line'. 5 pixels either way seems ok
 
You can swipe as many fingers left or right and it will only pick up one of them
It will then allow further swipes when that finger has been lifed from the screen.
Typically its swipe > lift finger > swipe .......
 
Be aware that it sometimes does not pick up the iPhoneTouchPhase.Ended
This is either a bug in the logic (plz test) or as the TouchPhases are notoriously
inaccurate its could well be this or it could be iPhone 1.6 given that it is quirky with touches.
Anyhow it does not affect the working of the class
other than a dead swipe once in a while which then rectifies itself on the next swipe so
no big deal.
 
No need for orientation as it will respect whatever you set.
*/

using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {

	//public member vars
	public int swipeLength;
	public int swipeVariance;
	//private member vars
	private GUIText swipeText;
	private Vector2[] fingerTrackArray;
	private bool[] swipeCompleteArray;
	private int activeTouch = -1;

	public int tapLength=3;
	//methods
	void Start()
	{
		//get a reference to the GUIText component
		swipeText = (GUIText) GetComponent(typeof(GUIText));
		fingerTrackArray = new Vector2[5];
		swipeCompleteArray = new bool[5];
	}  
	
	void Update()
	{
		//touch count is a mess at the moment so add the extra check to see if there are no more than 5 touches
		if(Input.touchCount > 0 &&  Input.touchCount < 6)
		{
			foreach(Touch touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began)
				{
					fingerTrackArray[touch.fingerId] = touch.position; 
				}
				//check if withing swipe variance      
				if(touch.position.y > (fingerTrackArray[touch.fingerId].y + swipeVariance))
					fingerTrackArray[touch.fingerId] = touch.position;
				if(touch.position.y < (fingerTrackArray[touch.fingerId].y - swipeVariance))
					fingerTrackArray[touch.fingerId] = touch.position;
				//swipe right
				if((touch.position.x > fingerTrackArray[touch.fingerId].x + swipeLength) &&  !swipeCompleteArray[touch.fingerId] &&
				   activeTouch == -1)
				{
					activeTouch = touch.fingerId;
					Debug.Log(touch.fingerId + " " + fingerTrackArray[touch.fingerId] + " " +  touch.position);
					swipeCompleteArray[touch.fingerId] = true;
					SwipeComplete("swipe right  ",  touch);
				}
				//swipe left
				if((touch.position.x < fingerTrackArray[touch.fingerId].x - swipeLength) && !swipeCompleteArray[touch.fingerId] &&
				   activeTouch == -1)
				{
					activeTouch = touch.fingerId;
					Debug.Log(touch.fingerId + " " + fingerTrackArray[touch.fingerId] + " " +  touch.position);
					swipeCompleteArray[touch.fingerId] = true;
					SwipeComplete("swipe left  ", touch);
				}
				float dist = Vector2.Distance(touch.position,fingerTrackArray[touch.fingerId]);
				if( dist <tapLength && !swipeCompleteArray[touch.fingerId] &&
				   activeTouch == -1 && touch.phase == TouchPhase.Ended)
				{

				Debug.Log("distance :"+dist);
					Debug.Log("tracker.tapCount="+touch.tapCount);
					activeTouch = touch.fingerId;
					Debug.Log(touch.fingerId + " " + fingerTrackArray[touch.fingerId] + " " +  touch.position);
					swipeCompleteArray[touch.fingerId] = true;
					SwipeComplete("Tap ", touch);
				}


				//when the touch has ended we can start accepting swipes again
				if(touch.fingerId == activeTouch &&  touch.phase == TouchPhase.Ended)
				{
					Debug.Log("Ending " + touch.fingerId);
					//if more than one finger has swiped then reset the other fingers so
					//you do not get a double/triple etc. swipe
					foreach(Touch touchReset in Input.touches)
					{
						fingerTrackArray[touchReset.fingerId] = touchReset.position;   
					}
					swipeCompleteArray[touch.fingerId] = false;
					activeTouch = -1;
				}



				/*if(touch.phase == TouchPhase.Ended && touch.tapCount>0)
				{
					if(Input.touchCount == 1)
					{
						Debug.Log("tracker.tapCount="+touch.tapCount);
						swipeCompleteArray[touch.fingerId] = false;
						SwipeComplete("Tap", touch);
						return;
					}
				}*/




			}          
		}  
	}
	
	void SwipeComplete(string messageToShow, Touch touch)
	{
		
		swipeText.text = messageToShow;
		//Debug.Log("doing something");
		//Do something here
	}
}


