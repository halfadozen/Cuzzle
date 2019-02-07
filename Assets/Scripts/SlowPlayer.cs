using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class SlowPlayer : BaseBehaviour {


	void Start () {



	}
	

	void Update ()
	{

				
	}

	private void OnTriggerEnter( Collider other )
	{
		if ( other.tag == "Player" )
		{
			print("ENTER SLOWED");
			PlayerController.Instance.isSlowed = true;
		}
	}

	private void OnTriggerStay( Collider other )
	{
		if (other.tag == "Player" )
		{
			print("STAY SLOWED");
			PlayerController.Instance.isSlowed = true;
		}
	}

	private void OnTriggerExit( Collider other )
	{
		if(other.tag == "Player" )
		{
			print("EXIT SLOWED");
			// PlayerController.Instance.isSlowed = false;
		}
	}
}
