using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayer {


    void playerTurn();





	int Life{get;set;}
	int Energy{get;set;}

    GameObject Zone { get; set; }
    GameObject Goal { get; set; }

	EPlayer Player{get;set;}

}
