using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class RaceManager : RealTimeMultiplayerListener
{
    const int QuickGameOpponents = 1;
    const int GameVariant = 0;
    const int MinOpponents = 1;
    const int MaxOpponents = 1;

    static RaceManager sInstance = null;

    public enum RaceState { SettingUp, Playing, Finished, SetupFailed, Aborted };
    private RaceState mRaceState = RaceState.SettingUp;

    // my participant ID
    private string mMyParticipantId = "";

    // room setup progress
    private float mRoomSetupProgress = 0.0f;

    // speed of the "fake progress" (to keep the player happy)
    // during room setup
    const float FakeProgressSpeed = 1.0f;
    const float MaxFakeProgress = 30.0f;
    float mRoomSetupStartTime = 0.0f;

    private RaceManager()
    {
        mRoomSetupStartTime = Time.time;
    }

    public static void CreateWithInvitationScreen()
    {
        sInstance = new RaceManager();
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents,
                GameVariant, sInstance);
    }


    public RaceState State
    {
        get
        {
            return mRaceState;
        }
    }

    public static RaceManager Instance
    {
        get
        {
            return sInstance;
        }
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
            mRaceState = RaceState.Playing;
            mMyParticipantId = GetSelf().ParticipantId;
        }
        else
        {
            mRaceState = RaceState.SetupFailed;
        }
    }

    public void OnLeftRoom()
    {
        if (mRaceState != RaceState.Finished)
        {
            mRaceState = RaceState.Aborted;
        }
    }

    public void OnPeersConnected(string[] peers)
    {
    }

    public void OnPeersDisconnected(string[] peers)
    {
       
    }

    private void RemoveCarFor(string participantId)
    {
       
    }

    public void OnRoomSetupProgress(float percent)
    {
        mRoomSetupProgress = percent;
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
       
    }

    private Participant GetSelf()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf();
    }

    private List<Participant> GetRacers()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    private Participant GetParticipant(string participantId)
    {
        return PlayGamesPlatform.Instance.RealTime.GetParticipant(participantId);
    }



    internal static void AcceptInvitation(string p)
    {
        throw new NotImplementedException();
    }
}