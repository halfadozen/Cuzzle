using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelBuilder : MonoBehaviour {

	public int width = 10;
	public int height = 10;

	public TextAsset textAsset;
	public TextAsset extrasAsset;

	public string[,] extras;

	public string resourcePath = "MapData";

	void Start() {

		string levelName = "Level1";
		string extrasName = "Extra1";

		if ( textAsset == null )
		{
			textAsset = Resources.Load(resourcePath + "/" + levelName) as TextAsset;
		}

		if ( extrasAsset == null )
		{
			extrasAsset = Resources.Load(resourcePath + "/" + extrasName) as TextAsset;
		}

	}

	public List<string> GetMapFromTextFile( TextAsset tAsset )
	{
		List<string> lines = new List<string>();

		if ( tAsset != null )
		{
			string textData = tAsset.text;
			string[] delimiters = { "\r\n", "\n" };
			lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
			lines.Reverse();
		}

		return lines;
	}

	public List<string> GetMapFromTextFile()
	{
		return GetMapFromTextFile(textAsset);
	}

	public void SetDimensions( List<string> textLines )
	{
		height = textLines.Count;
		foreach ( string line in textLines )
		{
			if ( line.Length > width )
			{
				width = line.Length;
			}
		}
	}

	public string[,] BuildMap()
	{
		List<string> lines = new List<string>();

		lines = GetMapFromTextFile();

		SetDimensions(lines);

		string[,] map = new string[width, height];
		for ( int y = 0; y < height; y++ )
		{
			for ( int x = 0; x < width; x++ )
			{
				if ( lines[y].Length > x )
				{
					map[x, y] = ( lines[y][x] ).ToString();
				}
			}
		}

		return map;
	}


	public string[,] GetExtrasFromTextFile( TextAsset tAsset )
	{


		if ( tAsset != null )
		{
			string textData = tAsset.text;


			textData = textData.Replace("\n", ""); // remove newlines
			textData = textData.Replace("\r", ""); // remove returns
			textData = textData.Replace(",", ""); // remove commas

			// Type = 4 chars, X = 2 chars, z = 2 chars, extraX = 2 chars, extraZ = 2 chars, so 12 characters per datum

			extras = new string[textData.Length / 12, 5];

			int index = 0;

			while ( index < ( textData.Length / 12 ) )
			{
				extras[index, 0] = textData.Substring(index * 12, 4);
				extras[index, 1] = textData.Substring(( index * 12 ) + 4, 2);
				extras[index, 2] = textData.Substring(( index * 12 ) + 6, 2);
				extras[index, 3] = textData.Substring(( index * 12 ) + 8, 2);
				extras[index, 4] = textData.Substring(( index * 12 ) + 10, 2);
				index++;
			}

		}

		return extras;
	}

	public string[,] GetExtrasFromTextFile()
	{
		return GetExtrasFromTextFile(extrasAsset);
	}

	public string[,] GenerateExtras()
	{

		extras = GetExtrasFromTextFile();

		return extras;
	}

	void Update () {
		
	}


} // end of class
