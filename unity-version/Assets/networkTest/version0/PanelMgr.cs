using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class PanelMgr : MonoBehaviour
{

    public GameObject firstPanel;
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject playingPanel;
    public GameObject invitationPanel;

    private GameObject currentPanel;
    private GameObject prevSelected;

    void Start()
    {
        OpenPanel(firstPanel);
    }

    public void OpenPlayingPanel()
    {
        OpenPanel(playingPanel);
    }

    public void OpenGameMenu()
    {
        OpenPanel(gameMenu);
    }

    public void OpenMainMenu()
    {
        OpenPanel(mainMenu);
    }

    public void OpenInvitationPanel()
    {
        OpenPanel(invitationPanel);
    }

    void OpenPanel(GameObject panel)
    {

        if (currentPanel == panel)
        {
            return;
        }

        if (currentPanel != null)
        {
            ClosePanel(currentPanel);
        }

        panel.gameObject.SetActive(true);
        prevSelected = EventSystem.current.currentSelectedGameObject;
        currentPanel = panel;

        Selectable[] items = panel.GetComponentsInChildren<Selectable>(true);


        foreach (Selectable s in items)
        {
            if (s.IsActive() && s.IsInteractable())
            {
                EventSystem.current.SetSelectedGameObject(s.gameObject);
                break;
            }
        }
    }

    void Update()
    {

        StandaloneInputModule mod = EventSystem.current.currentInputModule as StandaloneInputModule;

        //handle quirk where the input is mouse mode at the start, so fire the submit event manually
        if (Input.GetKey(KeyCode.JoystickButton0))
        {

            if (mod != null && mod.inputMode == StandaloneInputModule.InputMode.Mouse)
            {
                Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                if (b)
                {
                    ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject,
                                            null, ExecuteEvents.submitHandler);
                }
            }
        }
        /// uncomment to log which element has the focus, and create a private int ct;
        //if(ct++ % 40 == 0) {
        //    Debug.Log("focus: " + EventSystem.current.currentSelectedGameObject);
        //}

    }

    void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(prevSelected);
    }
}