using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : Monobehaviour
{
	public Transform Player, Destination;
	public GameObject playerg;
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			playerg.SetActivate(false);
			player.position = destination.position;
			playerg.SetActivate(true);
		}
	}
}