/*
 */

using UnityEngine;
using System.Collections;

public class LaserModel {
	public int hauteur;
	public int largeur;
	public bool estRouge;
	public GameObject head;
	public LaserTrail trail;
	public static int idCounter=0;
	public int id;







	public LaserModel (GameObject head, LaserTrail trail) {
		idCounter++;
		id = idCounter;
		this.head = head;
		this.trail = trail;

	}






}


