using System;
using CometEngine;

class reff : CometBehaviour
{
	// Called before first frame
	public void Start()
	{

	}

	// Called Every Frame
	public void Update()
	{
		if (Input.GetKey(KeyCode.R))
		{
			
		Debug.Log("REFF");
		}
	}
}