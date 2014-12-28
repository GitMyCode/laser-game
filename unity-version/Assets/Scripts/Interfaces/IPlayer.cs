using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayer {



	int Life{get;set;}
	int Energy{get;set;}

    string Name { get; set; }

    GameObject[] Goals { get;set;}
    GameObject[] Zones { get; set; }

    bool tryAddEnergy(int quantite);
    bool tryRemoveEnergy(int quantite);
    bool tryRemoveLife(int quantite);

}
