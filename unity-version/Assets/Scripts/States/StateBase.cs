using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateBase {

    public enum ESubState { Entering, Pause, Default, Exiting };
    public virtual string Name { get { return "Default"; } }


    protected ESubState mCurrentState;


    private void Pause() {
        mCurrentState = ESubState.Pause;
    }
    private void UnPause() {
        mCurrentState = ESubState.Default;
    }


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

    public void OnUIInteract(GameObject reference)
    {

    }
    public void OnUIAction()
    {
        Debug.Log("click sur replay");
    }

    public virtual void OnUIAction(GameObject sender, Object data)
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
