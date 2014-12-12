using UnityEngine;
using System.Collections;

public interface IPlayer {

	int getLifeRemaining();
	void setLife(int life);

	int getEnergyRemaining();
	void setEnergy(int energy);



	GameObject getZone();
	GameObject getGoal();
}
