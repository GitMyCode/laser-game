using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;

// Utility class to navigate between the various panels in the main scene.
public static class NavigationUtil
{

    private static PanelMgr theMgr;

    public static PanelMgr PanelMgr
    {
        get
        {
            if (theMgr == null)
            {
                theMgr = EventSystem.current.GetComponent<PanelMgr>();
            }
            return theMgr;
        }
    }

    public static void ShowMainMenu()
    {
        PanelMgr mgr = NavigationUtil.PanelMgr;
        if (mgr != null)
        {
            Debug.Log("Showing MainMenu!");
            mgr.OpenMainMenu();
        }
        else
        {
            Debug.LogWarning("PanelMgr script missing!");
        }
    }

    public static void ShowPlayingPanel()
    {
        PanelMgr mgr = NavigationUtil.PanelMgr;
        if (mgr != null)
        {
            Debug.Log("Showing Playing Panel!");
            mgr.OpenPlayingPanel();
        }
        else
        {
            Debug.Log("PanelMgr script missing!");
        }
    }

    public static void ShowGameMenu()
    {
        PanelMgr mgr = NavigationUtil.PanelMgr;
        if (mgr != null)
        {
            Debug.Log("Showing Game Menu!");
            mgr.OpenGameMenu();
        }
        else
        {
            Debug.Log("PanelMgr script missing!");
        }
    }

    public static void ShowInvitationPanel()
    {
        PanelMgr mgr = NavigationUtil.PanelMgr;
        if (mgr != null)
        {
            Debug.Log("Showing Invitation Panel!");
            mgr.OpenInvitationPanel();
        }
        else
        {
            Debug.Log("PanelMgr script Missing");
        }
    }
}