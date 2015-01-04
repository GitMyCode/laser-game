using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class NetworkListener : RealTimeMultiplayerListener {

    const int QuickGameOpponents = 1;
    const int GameVariant = 0;
    const int MinOpponents = 1;
    const int MaxOpponents = 1;

    static NetworkListener sInstance = null;

    public enum NetworkState { SettingUp, Playing, Finished, SetupFailed, Aborted };
    private NetworkState mRaceState = NetworkState.SettingUp;

    // my participant ID
    private string mMyParticipantId = "";

    // room setup progress
    private float mRoomSetupProgress = 0.0f;

    // speed of the "fake progress" (to keep the player happy)
    // during room setup
    const float FakeProgressSpeed = 1.0f;
    const float MaxFakeProgress = 30.0f;
    float mRoomSetupStartTime = 0.0f;

    public NetworkState State
    {
        get
        {
            return mRaceState;
        }
    }

    public static NetworkListener Instance
    {
        get
        {
            return sInstance;
        }
    }


	// Use this for initialization
	void Start () {
	
	}
	
    public static void CreateQuickGame()
    {
        sInstance = new NetworkListener();
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(QuickGameOpponents, QuickGameOpponents,
                GameVariant, sInstance);
    }

    public static void CreateWithInvitationScreen()
    {
        sInstance = new NetworkListener();
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
                GameVariant, sInstance);
    }

    public static void AcceptFromInbox()
    {
        sInstance = new NetworkListener();
        PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(sInstance);
    }

    public static void AcceptInvitation(string invitationId)
    {
        sInstance = new NetworkListener();
        PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, sInstance);
    }

    private Participant GetSelf()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf();
    }

    private List<Participant> GetPlayers()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    private Participant GetParticipant(string participantId)
    {
        return PlayGamesPlatform.Instance.RealTime.GetParticipant(participantId);
    }


    // Implementation RealTimeMultiplayerListener

    public void OnRoomSetupProgress(float percent)
    {
        mRoomSetupProgress = percent;
    }

    public float RoomSetupProgress
    {
        get
        {
            float fakeProgress = (Time.time - mRoomSetupStartTime) * FakeProgressSpeed;
            if (fakeProgress > MaxFakeProgress)
            {
                fakeProgress = MaxFakeProgress;
            }
            float progress = mRoomSetupProgress + fakeProgress;
            return progress < 99.0f ? progress : 99.0f;
        }
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            mRaceState = NetworkState.Playing;
            mMyParticipantId = GetSelf().ParticipantId;
        }
        else
        {
            mRaceState = NetworkState.SetupFailed;
        }
    }

    public void OnLeftRoom()
    {
        if (mRaceState != NetworkState.Finished)
        {
            mRaceState = NetworkState.Aborted;
        }
    }

    public void OnPeersConnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        throw new NotImplementedException();
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        throw new NotImplementedException();
    }
}
