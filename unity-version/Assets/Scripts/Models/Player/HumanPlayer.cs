using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanPlayer : Player {


    


    void Awake()
    {
        base.Awake();
        // swipeText = (GUIText)GetComponent(typeof(GUIText));
        fingerTrackArray = new Vector3[5];
        startTimeArray = new float[5];
        swipeCompleteArray = new bool[5];
    } 

    protected override void Start()
    {
        base.Start();
        Name = "Player1";
        Life = 5;
        Energy = 5;
       

    }

    //public member vars
    public int swipeLength;
    public int swipeVariance;
    //private member vars
    private GUIText swipeText;
    private Vector3[] fingerTrackArray;
    private float[] startTimeArray;

    private bool[] swipeCompleteArray;
    private int activeTouch = -1;

    public int tapLength = 3;
    //methods
  

    

    protected override void GameUpdate()
    {
        base.GameUpdate();

        if (Input.touchCount > 0 && Input.touchCount < 6)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerTrackArray[touch.fingerId] = pointToWorlCam(touch.position);
                    startTimeArray[touch.fingerId] = Time.time;
                }
                Vector3 touchPos = pointToWorlCam(touch.position);
                float dist = Vector2.Distance(touchPos, fingerTrackArray[touch.fingerId]);
              

                float intervalTime = Time.time - startTimeArray[touch.fingerId];
                if (dist >= tapLength && !swipeCompleteArray[touch.fingerId] &&
                   activeTouch == -1 && touch.phase == TouchPhase.Ended && fingerTrackArray[touch.fingerId] != Vector3.zero)
                {
                    activeTouch = touch.fingerId;
                    swipeCompleteArray[touch.fingerId] = true;

                    GameArbiter.Instance.actionQueue.Enqueue(new Action(fingerTrackArray[touch.fingerId], touchPos, intervalTime, Action.ActionType.ATTACK, gameObject));

                }

                if (dist < tapLength && !swipeCompleteArray[touch.fingerId] &&
                   activeTouch == -1 && touch.phase == TouchPhase.Ended)
                {

                    //				Debug.Log("distance :"+dist);
                    //	Debug.Log("tracker.tapCount="+touch.tapCount);
                    activeTouch = touch.fingerId;
                    //	Debug.Log(touch.fingerId + " " + fingerTrackArray[touch.fingerId] + " " +  touch.position);
                    swipeCompleteArray[touch.fingerId] = true;
                    GameArbiter.Instance.actionQueue.Enqueue(new Action(fingerTrackArray[touch.fingerId], touchPos, intervalTime, Action.ActionType.DEFENSIVE, gameObject));
                    DebugDraw.DrawSphere(touch.position, 10, Color.yellow);

                }


                //when the touch has ended we can start accepting swipes again
                if (touch.fingerId == activeTouch && touch.phase == TouchPhase.Ended)
                {
                    //	Debug.Log("Ending " + touch.fingerId);
                    //if more than one finger has swiped then reset the other fingers so
                    //you do not get a double/triple etc. swipe
                    foreach (Touch touchReset in Input.touches)
                    {
                        fingerTrackArray[touchReset.fingerId] = Vector3.zero;
                    }
                    swipeCompleteArray[touch.fingerId] = false;
                    activeTouch = -1;
                }
            }
        }
    }

    public override string ToString()
    {
        return string.Format("Human \n life : {0},\n energy: {1} ", Life, Energy);
    }
  
}
