using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ns : MonoBehaviour {

	//public InputField waitTime;
	//public Button save;

	//public string vrijeme = "3";


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//timer += Time.deltaTime;
		//Debug.Log (timer);
	}

	public void nScena(){
		if (gameObject.name == "Start") {
			StartCoroutine ("gameConfig");
		}
		
		if (gameObject.name == "2Pairs") {
			StartCoroutine ("dvaPara");

		}

		if (gameObject.name == "3Pairs") {
			StartCoroutine ("triPara");
		}
	
		if (gameObject.name == "4Pairs") {
			StartCoroutine ("cetiriPara");
		}

		if (gameObject.name == "Exit") {
			StartCoroutine ("izlaz");
		}
	}




	IEnumerator gameConfig()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("GameConfiguration");
	}

	IEnumerator izlaz()
	{
		yield return new WaitForSeconds(3.0f);
		Application.Quit ();
	}

	IEnumerator dvaPara()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Game2");
	}

	IEnumerator triPara()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Game3");
	}

	IEnumerator cetiriPara()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Game4");
	}

}
