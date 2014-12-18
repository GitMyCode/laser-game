﻿using UnityEngine;
using System.Collections;

public class StateBase {

    public enum ESubState { Entering, Pause, Default, Exiting };
    public virtual string Name { get { return "Default"; } }


    protected ESubState mCurrentState;


    private void Pause() { }
    private void UnPause() { }


    public override string ToString()
    {
        return string.Format("[StateBase: Name={0}, State={1}]", Name, State);
    }

    public virtual void Awake()
    {

    }
    public virtual void Start()
    {

       // mFadeInOutSprite = GameObject.Find("FadeInOutPlane").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
       // mFadeInOutSprite.enabled = true;
        Debug.Log("Start " + this);
    }
    public virtual void Update()
    {

    }




    #region ESubState properties

    public ESubState State
    {
        get
        {
            return mCurrentState;
        }
        set
        {
            if (value == ESubState.Pause)
            {
                Pause();
            }
            else if (mCurrentState == ESubState.Pause && value != ESubState.Pause)
            {
                UnPause();
            }
            else if (value == ESubState.Exiting)
            {
              /*  mFadeInOutSprite.gameObject.SetActive(true);

                SetCursor(false);

                mAudioFadeoutSpeed = 0.35f / (1f - mFadeInOutSprite.color.a) / AudioListener.volume;*/
            }

            mCurrentState = value;

            Debug.Log(this);
        }
    }
    #endregion

}