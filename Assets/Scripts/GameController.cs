using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameController : MonoBehaviour {

	private static GameController _instance; 

	public LevelBuilder mapData;
	public DrawLevel currentLevel;
	public PlayerController playerController;
	public GameObject myPlayer;


	public string[,] levelInstance;
	public string [,] extrasList;
	public List<GameObject> listOfCurrentObjects;

	private Canvas timeLeftCanvas;
	private TextMeshProUGUI countdownText;
	private float timer;
	private int timerInt;
	private int timerIntCompare;
	private bool canCount = true;
	private bool doOnce = false;
	private float mainTimer;
	public float timeRemaining;
	public int currentScore;


	public static GameController Instance
	{
		get
		{
			if (_instance == null )
			{
				GameObject go = new GameObject("GameManager");
				go.AddComponent<GameController>();
			}

			return _instance;
		}
	}



	private void Awake () {

		_instance = this;
		
		if (mapData != null )
		{
			levelInstance = mapData.BuildMap();
			extrasList = mapData.GenerateExtras();

			currentLevel.Draw(levelInstance);
			currentLevel.DrawExtras(extrasList);
			
//			currentLevel.listOfGameObjects.Clear();
		}


		Debug.Log("Finito");

		

	}

	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		myPlayer = PlayerController.Instance.gameObject;
		listOfCurrentObjects = currentLevel.listOfGameObjects;
		timeRemaining = currentLevel.timeAllowed;
		countdownText = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();
		countdownText.text = timeRemaining.ToString();


	}


	private void Update ()
	{
		Countdown();
	}

	private void Countdown()
	{
		timeRemaining -= Time.deltaTime;
		countdownText.text = timeRemaining.ToString("0");
		if (timeRemaining <= 0)
		{
			countdownText.text = "Time is up!";
		}
	}


} // end of class
