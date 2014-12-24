using UnityEngine;
using System.Collections;

public class SceneRoot : MonoBehaviour {


    private static SceneRoot instance;
    private StateBase mCurrentState;


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
            mCurrentState = new StateGame();
        }

        mCurrentState.Awake();
    }

	// Update is called once per frame
	void Update () {
        mCurrentState.Update();
	}


    public void OnUIInteract(GameObject reference)
    {
        mCurrentState.OnUIInteract(reference);
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
