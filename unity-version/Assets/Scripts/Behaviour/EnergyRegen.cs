using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;

public class EnergyRegen : GameBehaviours,ISubject {

    PlayerBase player;
    public int energyRegeneration = 120;
    private int regenerateCounter = 1;
    public ArrayList observers = new ArrayList();
    GameObject barIndicator;

	// Use this for initialization
	void Start () {
        player = transform.GetComponent<PlayerBase>();

        barIndicator = GameObject.Find("BarIndicator");
        this.attach(barIndicator.GetComponent<BarIndicatorObserver>());
	}

    public void attach(Observer observer)
    {
        if (observers.Contains(observer) == false)
        {
            observers.Add(observer);
        }
    }
	
	// Update is called once per frame
	
    protected override void GameFixedUpdate()
    {
        if (regenerateCounter % energyRegeneration == 0)
        {
            tryAddEnergy(player, 1);
            regenerateCounter = 1;
        }
        regenerateCounter++;
    }
    private bool tryAddEnergy(IPlayer player, int quantite)
    {
        if (player.Energy < 5)
        {
            player.Energy += quantite;
             notifyObservers();
           
            if (player.Energy > 5)
            {
                player.Energy = 5;

            }
            return true;
        }
        return false;
    }

    public  void notifyObservers()
    {
        foreach (Observer item in observers)
        {
            item.updateBar(player);
        }
    }

    public void detach(Observer observer)
    {
        throw new System.NotImplementedException();
    }
}
