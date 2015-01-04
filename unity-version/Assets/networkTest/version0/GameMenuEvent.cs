using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GameMenuEvent : MonoBehaviour {


    private System.Action<bool> mAuthCallback;
    private bool mAuthOnStart = true;
    private bool mSigningIn = false;

    // Use this for initialization
    void Start()
    {
        mAuthCallback = (bool success) =>
        {

            Debug.Log("In Auth callback, success = " + success);

            mSigningIn = false;
            if (success)
            {
                NavigationUtil.ShowMainMenu();
            }
            else
            {
                Debug.Log("Auth failed!!");
            }
        };

        // enable debug logs (note: we do this because this is a sample;
        // on your production app, you probably don't want this turned 
        // on by default, as it will fill the user's logs with debug info).
        var config = new PlayGamesClientConfiguration.Builder()
            .WithInvitationDelegate(InvitationManager.Instance.OnInvitationReceived)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // try silent authentication
        if (mAuthOnStart)
        {
            Authorize(true);
        }
        
    }

    // Link this to the play button on click event list.
    // This starts a "non-silent" login process.
    public void OnPlayClicked()
    {
        Authorize(false);
    }

    //Starts the signin process.
    void Authorize(bool silent)
    {
        if (!mSigningIn)
        {
            Debug.Log("Starting sign-in...");
            PlayGamesPlatform.Instance.Authenticate(mAuthCallback, silent);
        }
        else
        {
            Debug.Log("Already started signing in");
        }

    }
}
