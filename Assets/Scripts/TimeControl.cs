using UnityEngine;
using Chronos;

public class TimeControl : MonoBehaviour {


	void Update () {

		Clock clock = Timekeeper.instance.Clock("Player");
		

		if ( Input.GetKeyDown(KeyCode.Alpha1) )
		{
			clock.localTimeScale = -1; // Rewind
		}
		else if ( Input.GetKeyDown(KeyCode.Alpha2) )
		{
			clock.localTimeScale = 0; // Pause
		}
		else if ( Input.GetKeyDown(KeyCode.Alpha3) )
		{
			clock.localTimeScale = 0.5f; // Slow
		}
		else if ( Input.GetKeyDown(KeyCode.Alpha4) )
		{
			clock.localTimeScale = 1; // Normal
		}
		else if ( Input.GetKeyDown(KeyCode.Alpha5) )
		{
			clock.localTimeScale = 2; // Accelerate
		}
	}
}
