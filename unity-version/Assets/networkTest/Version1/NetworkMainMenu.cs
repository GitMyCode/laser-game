using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class NetworkMainMenu : MonoBehaviour 

{

    private const float ERROR_STATUS_TIMEOUT = 10.0f;
    private const float INFO_STATUS_TIMEOUT = 2.0f;
    private float mStatusCountdown = 0f;

    public GameObject invitationPanel;
    public GameObject networkMenu;
    public GameObject signInBut;

    public GameObject hideOrShowGameObj;
    void Start()
    {
   
    }

    void Update()
    {

        UpdateInvitation();

        if (NetworkListener.Instance == null)
        {
            return;
        }
        switch (NetworkListener.Instance.State)
        {
            case NetworkListener.NetworkState.SettingUp:

                break;
            case NetworkListener.NetworkState.SetupFailed:
                Debug.Log("Room Connected Failed !!");
                break;
            case NetworkListener.NetworkState.Aborted:

                break;
            case NetworkListener.NetworkState.Finished:

                break;
            case NetworkListener.NetworkState.Playing:

                hideOrShowGameObj.GetComponent<ShowrelevantElement>().ShowOrHideGame(true);
                hideOrShowGameObj.GetComponent<ShowrelevantElement>().multiConfiguration();
               // showGame.ShowOrHideNetwork(false);
                break;
            default:
                Debug.Log("NetworkListener.Instance.State = " + NetworkListener.Instance.State);
                break;
        }
    }


    // Handle detecting incoming invitations.
    public void UpdateInvitation()
    {
        if (InvitationManager.Instance == null)
        {
            return;
        }

        // if an invitation arrived, switch to the "invitation incoming" GUI
        // or directly to the game, if the invitation came from the notification
        Invitation inv = InvitationManager.Instance.Invitation;
        if (inv != null)
        {
            if (InvitationManager.Instance.ShouldAutoAccept)
            {
                // jump straight into the game, since the user already indicated
                // they want to accept the invitation!
                InvitationManager.Instance.Clear();
                RaceManager.AcceptInvitation(inv.InvitationId);
            }
            else
            {
                // show the "incoming invitation" screen
                invitationPanel.SetActive(true);
            }
        }
    }

    //Handler for the Quick Match button.
    public void OnQuickMatch()
    {
        Debug.Log("QuickMatch");
        NetworkListener.CreateQuickGame();
    }

    //Handler for the send initation button.
    public void OnInvite()
    {
        Debug.Log("Invite");
        NetworkListener.CreateWithInvitationScreen();
    }

    //Handler for the inbox button.
    public void OnInboxClicked()
    {
        Debug.Log("Inbox");
        NetworkListener.AcceptFromInbox();
    }

    //Handler for the signout button.
    public void OnSignoutClicked()
    {

        if (PlayGamesPlatform.Instance != null)
        {
            Debug.Log("Signout");
            PlayGamesPlatform.Instance.SignOut();
        }
        else
        {
            Debug.Log("PG Instance is null!");
        }
        hideNetworkMenu();
    }

    void hideNetworkMenu()
    {
        Selectable[] items = networkMenu.GetComponentsInChildren<Selectable>(true);

        foreach (Selectable s in items)
        {
            s.gameObject.SetActive(false);
        }

        signInBut.SetActive(true);

    }
    /*
    void playMode()
    {
        Selectable[] items = networkMenu.GetComponentsInChildren<Selectable>(true);

        foreach (Selectable s in items)
        {
            s.gameObject.SetActive(false);
        }

        signInBut.SetActive(false);
        playingText.SetActive(true);
    }
    */
}
