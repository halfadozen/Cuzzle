using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {


	public float rotationSpeed = 10f;

	void Start()
	{

	}


	void Update()
	{
		if ( PlayerController.Instance.movable == true )
		{
			transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

		}

	}


} // end of class
