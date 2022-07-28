using System;
using CometEngine;

class Enemigo : CometBehaviour
{
    public float speed = 50;
	private RigidBody mPlayer;
	// Called before first frame
	public void Start()
	{
		mPlayer = GetComponent<RigidBody>();
	}

	// Called Every Frame
	public void Update()
	{

	}
}