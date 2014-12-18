using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayer {


    void playerTurn();





	int Life{get;set;}
	int Energy{get;set;}

    GameObject[] Goals { get;set;}
    GameObject[] Zones { get; set; }

	EPlayer PlayerEnumType{get;set;}

}
