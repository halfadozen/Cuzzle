			
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DrawLevel : MonoBehaviour {

	public string[,] currentLevel;

	public GameObject floorCube;
	public GameObject lowWallCube;
	public GameObject moveableWallCube;
	public GameObject breakableWallCube;
	public GameObject PlayerObject;
	public GameObject finish;
	public GameObject slowPlayer;
	public GameObject ghostPlayer;
	public GameObject teleportIn;
	public GameObject teleportOut;

	public List<GameObject> listOfGameObjects;


	public int levelWidth;
	public int levelHeight;
	public int timeAllowed;

	public int Width
	{
		get
		{
			return levelWidth;
		}
	}

	public int Height
	{
		get
		{
			return levelHeight;
		}
	}

	void Start () {

		// List<GameObject> listOfGameObjects = new List<GameObject>();
		listOfGameObjects.Clear();
	}
	

	void Update () {
		
	}

	public void Draw( string[,]level )
	{

		currentLevel = level;
		levelWidth = currentLevel.GetLength(0);
		levelHeight = currentLevel.GetLength(1);

		GameObject t;


		for (int z = 0; z < levelHeight; z++ )
		{
			for (int x = 0; x < levelWidth; x++ )
			{


				//if ( currentLevel[x, z] != "" )
				//{
				//	t = Instantiate(floorCube, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
				//	t.name = "floorCube" + x.ToString("D2") + "," + z.ToString("D2");
				//	listOfGameObjects.Add(t);
				//}
				if (currentLevel[x,z] == "1" )
				{
					t = Instantiate(lowWallCube, new Vector3(x, 0.0f, z), Quaternion.identity) ;
					t.name = "lowWallCube" + x.ToString("D2") + z.ToString("D2");
					listOfGameObjects.Add(t);
				}
				else if (currentLevel[x,z] == "M")
				{
					t = Instantiate(moveableWallCube, new Vector3(x, 0.0f, z), Quaternion.identity);
					t.name = "moveableWallCube" + x.ToString("D2") + z.ToString("D2");
					listOfGameObjects.Add(t);
				}
				else if (currentLevel[x,z] == "D" )
				{
					t = Instantiate(breakableWallCube, new Vector3(x, 0.0f, z), Quaternion.identity);
					t.name = "breakableWallCube" + x.ToString("D2") + z.ToString("D2");
					listOfGameObjects.Add(t);
				}
				else if (currentLevel[x,z] == "T" )
				{
					t = Instantiate(teleportIn, new Vector3(x, -0.1f, z), Quaternion.identity);
					listOfGameObjects.Add(t);
					t.name = "teleportIn" + x.ToString("D2") + z.ToString("D2");
					Debug.Log("Initial = " + t.name);
				}
				else if (currentLevel[x,z] == "F" )
				{
					t = Instantiate(finish, new Vector3(x, -0.1f, z), Quaternion.identity);
					t.name = "finish" + x.ToString("D2") + z.ToString("D2");
					listOfGameObjects.Add(t);

				}
			}
		}

		

	}

	public void DrawExtras( string [,] extrasList)
	{
		string[,] extras = extrasList;
		int lengthExtras = ( extrasList.Length ) / 5;
		int i = 0, j = 0;
		int currentExtraX, targetExtraX;
		int currentExtraZ, targetExtraZ;
		int multiplier = 1;
		GameObject t;

		for (i = 0; i< lengthExtras; i++ )
		{

			currentExtraX = (Convert.ToInt32(extras[i, 1])) * multiplier;
			currentExtraZ = (Convert.ToInt32(extras[i, 2])) * multiplier;
			targetExtraX = (Convert.ToInt32(extras[i, 3])) * multiplier;
			targetExtraZ = (Convert.ToInt32(extras[i, 4])) * multiplier;
			if ( extras[i, j] == "Slow" )
			{
				t = Instantiate(slowPlayer, new Vector3(currentExtraX, -0.1f, currentExtraZ), Quaternion.identity) as GameObject;
				listOfGameObjects.Add(t);
				t.name = "slowPlayer" + extras[i, 1] + extras[i, 2] + extras[i, 3];
			}
			else if ( extras[i, j] == "Ghst" )
			{
				t = Instantiate(ghostPlayer, new Vector3(currentExtraX, -0.1f, currentExtraZ), Quaternion.identity) as GameObject;
				listOfGameObjects.Add(t);
				t.name = "ghostPlayer" + extras[i, 1] + extras[i, 2] + extras[i, 3];
			}
			else if ( extras[i, j] == "Play" )
			{
				
				t = Instantiate(PlayerObject, new Vector3(currentExtraX, 0.1f, currentExtraZ), Quaternion.identity) as GameObject;
				listOfGameObjects.Add(t);
				
			}
			else if ( extras[i, j] == "TelI" )
			{
				t = Instantiate(teleportOut, new Vector3(targetExtraX, -0.1f, targetExtraZ), Quaternion.identity) as GameObject;
				listOfGameObjects.Add(t);
				t.name = "teleportOut" + extras[i, 3] + extras[i, 4];
				Debug.Log("teleportIn" + ( currentExtraX / multiplier ).ToString("D2") + "," + ( currentExtraZ / multiplier ).ToString("D2"));
				t = GameObject.Find("teleportIn" + ( currentExtraX / multiplier ).ToString("D2")  + ( currentExtraZ / multiplier ).ToString("D2"));
				t.name = "teleportIn" + ( currentExtraX / multiplier).ToString("D2") +  ( currentExtraZ / multiplier ).ToString("D2") + ( targetExtraX / multiplier ).ToString("D2") + ( targetExtraZ / multiplier ).ToString("D2");
			}
			else if ( extras[i, j] == "Time" )
			{
				timeAllowed = Convert.ToInt32(extras[i, 1]);
			}
		}

	}

} // end of class
