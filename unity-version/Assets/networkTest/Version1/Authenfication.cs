using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class Authenfication : MonoBehaviour {

    private System.Action<bool> mAuthCallback;
    private bool mAuthOnStart = true;
    private bool mSigningIn = false;
    public GameObject networkMenu;
    public GameObject signInBut;

    void Start()
    {
        mAuthCallback = (bool success) =>
        {

            Debug.Log("In Auth callback, success = " + success);

            mSigningIn = false;
            if (success)
            {
                initiliazeNetworkMenu();
            }
            else
            {
                Debug.Log("Auth failed!!");
            }
        };

        var config = new PlayGamesClientConfiguration.Builder()
            .WithInvitationDelegate(InvitationManager.Instance.OnInvitationReceived)
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;

        if (mAuthOnStart)  // try silent authentication
        {
            Authorize(true);
        }

    }

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

    void initiliazeNetworkMenu()
    {
        Debug.Log("Initialize Network Menu");
        Selectable[] items = networkMenu.GetComponentsInChildren<Selectable>(true);

        foreach (Selectable s in items)
        {
            s.gameObject.SetActive(true);
        }

        signInBut.SetActive(false);

    }
}
