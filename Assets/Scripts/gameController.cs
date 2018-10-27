using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class gameController : MonoBehaviour{

	[SerializeField]
	private Sprite bgImage;

	public AudioSource sound;

	[SerializeField]
	private AudioClip pljesak;
	public AudioClip zvuk;

	EventTrigger trigger;
	EventTrigger trigger2;

	public Sprite[] puzzles;
	public List<Sprite> gamePuzzles = new List<Sprite> ();
	public List<Button> btns = new List<Button>();

	private bool firstGuess, secondGuess;
	private int countCorrectGuesses;
	private int gameGuesses;
	private int firstGuessIndex, secondGuessIndex;
	private string firstGuessPuzzle, secondGuessPuzzle;

	public int otvorenaPrvaKartica = 0;
	public int otvorenaDrugaKartica = 0;
	public int ulazak = 1;
	string trenutni;
	public float timer = 0;

	public int postavljenoVrijeme = 3;


	public Transform puzzleField;
	public GameObject btn;
	public Button povratak;


	void Update () {
		timer += Time.deltaTime;
		if (timer > postavljenoVrijeme && ulazak == 1) {
			pickPuzzle (trenutni);
		}
		if (ulazak == 0) {
			timer = 0;
		}
		//Debug.Log (trenutni);
	}



	void Awake(){
		puzzles = Resources.LoadAll<Sprite> ("Sprites");
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game2")) {
			for (int i = 0; i < 4; i++) {
				GameObject button = Instantiate (btn);
				button.name = "" + i;
				button.transform.SetParent (puzzleField);
			}
		} else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game3")){
			for (int i = 0; i < 6; i++) {
				GameObject button = Instantiate (btn);
				button.name = "" + i;
				button.transform.SetParent (puzzleField);
			}
		} else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game4")){
			for (int i = 0; i < 8; i++) {
				GameObject button = Instantiate (btn);
				button.name = "" + i;
				button.transform.SetParent (puzzleField);	
			}
		}
	}

	void Start(){
		//ulazak = 1;

		GameObject g = GameObject.Find("Canvas");
		save saveSkripta = g.GetComponent<save>();
		postavljenoVrijeme = System.Convert.ToInt16(save.vrime);
		Debug.Log ("Vrijeme postavljeno na: " + postavljenoVrijeme);

		sound = GetComponent<AudioSource>();
		GetButtons ();
		AddListeners ();
		AddExiters ();
		addGamePuzzles ();
		shuffle (gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;

		Button btnPovratak = povratak.GetComponent<Button>();
		trigger = btnPovratak.gameObject.AddComponent<EventTrigger>();
		var pointerEnterPovratak = new EventTrigger.Entry();
		pointerEnterPovratak.eventID = EventTriggerType.PointerEnter;
		string povratakBtn = btnPovratak.gameObject.name;
		pointerEnterPovratak.callback.AddListener((e) => povratakFunkcija());
		trigger.triggers.Add(pointerEnterPovratak);
	}

	void GetButtons(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
		for (int i = 0; i < objects.Length; i++) {
			btns.Add (objects[i].GetComponent<Button>());
			btns [i].image.sprite = bgImage;

		}
	}

	void addGamePuzzles(){
		int prvi, drugi, treci, cetvrti;
		prvi = Random.Range (0, 7);
		drugi = Random.Range (0, 7);

		if(prvi == drugi){
			drugi = Random.Range (0, 7);
		}

		treci = Random.Range(0, 7);
		if(prvi == treci || drugi == treci){
			treci = Random.Range(0, 7);
		}

		cetvrti = Random.Range(0, 7);
		if (prvi == cetvrti || drugi == cetvrti || treci == cetvrti) {
			cetvrti = Random.Range (0, 7);
		}
			
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game2")) {
			for (int i = 0; i < 2; i++) {
				gamePuzzles.Add (puzzles[prvi]);
				gamePuzzles.Add (puzzles[drugi]);
			}
		} else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game3")){
			for (int i = 0; i < 2; i++) {
				gamePuzzles.Add (puzzles[prvi]);
				gamePuzzles.Add (puzzles[drugi]);
				gamePuzzles.Add (puzzles[treci]);
			}
		} else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game4")){
			for (int i = 0; i < 2; i++) {
				gamePuzzles.Add (puzzles[prvi]);
				gamePuzzles.Add (puzzles[drugi]);
				gamePuzzles.Add (puzzles[treci]);
				gamePuzzles.Add (puzzles[cetvrti]);
			}
		}
	}

	void AddListeners(){
		for (int i=0; i<btns.Count; i++) {
			/*btn.onClick.AddListener(() => pickPuzzle ());*/
			trigger = btns[i].gameObject.AddComponent<EventTrigger>();
			var pointerEnter = new EventTrigger.Entry();
			pointerEnter.eventID = EventTriggerType.PointerEnter;
			string kojiButton = btns [i].gameObject.name;
			pointerEnter.callback.AddListener((e) => pickPuzzle(kojiButton));
			trigger.triggers.Add(pointerEnter);
		}
	}

	void AddExiters(){
		for (int i=0; i<btns.Count; i++) {
			/*btn.onClick.AddListener(() => pickPuzzle ());*/
			trigger2 = btns[i].gameObject.AddComponent<EventTrigger>();
			var pointerExit = new EventTrigger.Entry();
			pointerExit.eventID = EventTriggerType.PointerExit;
			string kojiButton = btns [i].gameObject.name;
			pointerExit.callback.AddListener((e) => dontEnter(kojiButton));
			trigger2.triggers.Add(pointerExit);
		}
	}

	public void dontEnter(string kojiButton){
		trenutni = kojiButton;
		//Debug.Log ("Usao u dont enter");
		ulazak = 0;
	}


	public void pickPuzzle(string kojiButton){
		trenutni = kojiButton;
		ulazak = 1;
		if (timer > 3) {
			//Debug.Log ("Usao u on Enter, ulazak = " + ulazak);
			//if (ulazak == 0) {
				//System.Threading.Thread.Sleep (2000);
				string name = kojiButton;
				Debug.Log (name);
				if (!firstGuess) {
					firstGuess = true;
					otvorenaPrvaKartica = 1;
					//Debug.Log ("Otvorena prva = " + otvorenaPrvaKartica);
					firstGuessIndex = int.Parse (kojiButton);
					firstGuessPuzzle = gamePuzzles [firstGuessIndex].name;
				//Debug.Log (firstGuessPuzzle);
				AudioSource audio = gameObject.AddComponent<AudioSource >();
				AudioClip clip = (AudioClip)Resources.Load (firstGuessPuzzle);
				if (clip != null) {
					audio.PlayOneShot (clip);
				} 
				else {
					Debug.Log ("Cant play audio: " + firstGuessPuzzle);
				}

					btns [firstGuessIndex].image.sprite = gamePuzzles [firstGuessIndex];
					//System.Threading.Thread.Sleep (2000);
					//sound.PlayOneShot (zvuk);
				} else if (!secondGuess) {
					secondGuess = true;
					otvorenaDrugaKartica = 1;
					//Debug.Log ("Otvorena druga = " + otvorenaDrugaKartica);
					secondGuessIndex = int.Parse (kojiButton);
					secondGuessPuzzle = gamePuzzles [secondGuessIndex].name;
				AudioSource audio = gameObject.AddComponent<AudioSource >();
				AudioClip clip = (AudioClip)Resources.Load (secondGuessPuzzle);
				if (clip != null) {
					audio.PlayOneShot (clip);
				} 
				else {
					Debug.Log ("Cant play audio: " + firstGuessPuzzle);
				}
					btns [secondGuessIndex].image.sprite = gamePuzzles [secondGuessIndex];
					//System.Threading.Thread.Sleep (2000);
					//sound.PlayOneShot (zvuk);
					StartCoroutine (checkPuzzles ());
				}
			timer = 0;
			ulazak = 0;
		}
	}

	IEnumerator checkPuzzles(){
		if (firstGuessPuzzle == secondGuessPuzzle) {
			sound.PlayOneShot(pljesak);
			yield return new WaitForSeconds (2.5f);
			btns [firstGuessIndex].interactable = false;
			btns [secondGuessIndex].interactable = false;
			btns [firstGuessIndex].image.color = new Color (0, 0, 0, 0);
			btns [secondGuessIndex].image.color = new Color (0, 0, 0, 0);

			checkIfTheGameIsFinished ();
		} else {
			yield return new WaitForSeconds (2.5f);
			btns [firstGuessIndex].image.sprite = bgImage;
			btns [secondGuessIndex].image.sprite = bgImage;
			sound.PlayOneShot (zvuk);
		}
		yield return new WaitForSeconds (2.5f);
		firstGuess = secondGuess = false;
	}

	void checkIfTheGameIsFinished(){
		countCorrectGuesses++;
		if (countCorrectGuesses == gameGuesses) {
			SceneManager.LoadScene ("GameConfiguration");
		}
	}

	void shuffle(List<Sprite> list){
		for (int i = 0; i < list.Count; i++) {
			Sprite temp = list [i];
			int randomIndex = Random.Range (0, list.Count);
			list [i] = list [randomIndex];
			list [randomIndex] = temp;
		}
	}

	private IEnumerator addDelay(float waitTime)
	{
		yield return new WaitForSeconds(3.0f);
		//Debug.Log ("cekam...");
		//nastavi = true;
	}



	public void povratakFunkcija()
	{
		Invoke("vratiSeNakon3Sekunde", 3.0f);
		//Debug.Log ("Povratak");
		//yield return new WaitForSeconds(3.0f);

	}


	private void vratiSeNakon3Sekunde() {
		SceneManager.LoadScene ("GameConfiguration");
	}


}