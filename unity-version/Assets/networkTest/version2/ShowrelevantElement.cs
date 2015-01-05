using UnityEngine;
using System.Collections;

public class ShowrelevantElement : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public GameObject wallL;
    public GameObject wallR;
    public GameObject gameArbiter;
    public GameObject sceneRoot;
    public GameObject eventSystem2;
    public GameObject energyIndicator;
    public GameObject barIndicator;
    public GameObject lifeIndicator;
    public GameObject lifeIndicatorP2;
    public GameObject networkMenu;
    public GameObject eventSystem;
    public GameObject authentification;
    public GameObject goalp2;

    // Use this for initialization
    void Start()
    {
        if (!UIManagerScript.multiplayer)
        {
            ShowOrHideGame(true);
            ShowOrHideNetwork(false);
        }
        else {
            shutAi();
            ShowOrHideGame(false);
            ShowOrHideNetwork(true);
        }
    }

    public void ShowOrHideGame(bool value)
    {
        p1.SetActive(value);
        p2.SetActive(value);
        wallL.SetActive(value);
        wallR.SetActive(value);
        gameArbiter.SetActive(value);
        sceneRoot.SetActive(value);
        eventSystem2.SetActive(value);
        energyIndicator.SetActive(value);
        barIndicator.SetActive(value);
        lifeIndicator.SetActive(value);
        lifeIndicatorP2.SetActive(false);
    }

    public void ShowOrHideNetwork(bool value)
    {
        networkMenu.SetActive(value);
        eventSystem.SetActive(value);
        authentification.SetActive(value);
    }

    public void shutAi() {
        Destroy(p2.GetComponent<AIPlayer>());
        //Destroy(p2.GetComponent<EnergyRegen>());
    }

    public void multiConfiguration() {
        lifeIndicatorP2.SetActive(true);
        //p2.GetComponent<HumanPlayer>().Goals = goalp2;
    }
}
