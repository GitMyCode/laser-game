using UnityEngine;
using System.Collections;

public interface IPlayer {

	int getLifeRemaining();
	void setLife(int life);

	int getEnergyRemaining();
	void setEnergy(int energy);


	int Life{get;set;}
	int Energy{get;set;}

	GameObject getZone();
	GameObject getGoal();
}
