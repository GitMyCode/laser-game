using UnityEngine;
using System.Collections;

public class EnergyRegen : MonoBehaviour {

    Player player;
    public int energyRegeneration = 120;
    private int regenerateCounter = 1;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnFixedUpdate()
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
            if (player.Energy > 5)
            {
                player.Energy = 5;
            }
            return true;
        }
        return false;
    }
}
