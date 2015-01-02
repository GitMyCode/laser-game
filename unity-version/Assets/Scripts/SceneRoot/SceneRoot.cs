using UnityEngine;
using System.Collections;

public class SceneRoot : MonoBehaviour {


    private static SceneRoot instance;
    private StateBase mCurrentState;
	private bool connectionOk = false;

    private Players mPlayers;
	private GameObject gm;
	private NetworkManager nt;

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
            if (UIManagerScript.state.GetType() == typeof(StateSingle))
            {
				mCurrentState = UIManagerScript.state; // SINGLE PLAYER
            }
            else {
				networkInitialisation();
            }
                
        }
		mCurrentState.Awake();
        
    }

	private void networkInitialisation(){
		gm = GameObject.Find("Main Camera");
		nt = gm.AddComponent("NetworkManager") as NetworkManager;
		StartCoroutine(ConnectionApproved());
		mCurrentState = UIManagerScript.state;
	}

	IEnumerator ConnectionApproved()
	{
		while (!NetworkManager.connected)
		{
			Debug.Log("NOT CONNECTED");
			yield return 0;
		}
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
