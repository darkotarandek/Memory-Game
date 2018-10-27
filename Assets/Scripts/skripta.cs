using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class skripta : MonoBehaviour{

	public GameObject enemy;
	public gameController controller;
	//public float timer = 0;

	public void Start(){
		controller = enemy.GetComponent<gameController> ();
	}

	void Update () {
		//timer += Time.deltaTime;
		//Debug.Log (timer);
	}


	public void enter(){
		controller.pickPuzzle ("");
	}
}
