using UnityEngine;
using System.Collections;

public class SceneRoot : MonoBehaviour {


    private static SceneRoot instance;
    private static StateBase mCurrentState;

    private Players mPlayers;


    public static SceneRoot Instance
    {
        get { return instance; }
    }
	

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        instance = this;
        if (mCurrentState == null)
        {
             mCurrentState = new StateMainMenu();
        }
		mCurrentState.Awake();
        
    }

	// Update is called once per frame
	void Update () {
        mCurrentState.Update();
	}


    public void OnUIInteract(GameObject reference)
    {
        if (mCurrentState == null)
        {
            LoadLevel("main");
        }
        else
        {
            mCurrentState.OnUIInteract(reference);
        }

    }

    public void EndGame()
    {

    }

    public void LoadLevel(string levelName)
    {
        //LevelToLoad = levelName;
        Application.LoadLevel(levelName);
    }
	


    public StateBase StateBase
    {
        get { return mCurrentState; }
    }

    #region SceneRoot properties
    public StateBase SceneState
    {
        get
        {
            return mCurrentState;
        }
        set
        {
            mCurrentState = value;
        }
    }

    public Players AllPlayers
    {
        get
        {
            return mPlayers ;
        }
        set
        {
            mPlayers = value;
        }
    }




    #endregion

}
