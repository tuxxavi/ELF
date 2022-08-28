using System;
using CometEngine;

class Ball : CometBehaviour
{

	// Called before first frame
	public void Start()
	{

	}

	// Called Every Frame
	public void Update()
	{
	}
	
	private void OnCollisionEnter(Collision aColl)
	{
		if (aColl.gameObject.tag == "Player")
		{
			gameObject.parent = aColl.gameObject;
		}
	}

}