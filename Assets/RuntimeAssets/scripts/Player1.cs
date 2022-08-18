using System.Diagnostics;
using System;
using CometEngine;

class Player1 : CometBehaviour
{    
    public float speed = 1000;
	public bool move_player = true;
	private RigidBody mPlayer;

	// Called before first frame
	public void Start()
	{
		mPlayer = GetComponent<RigidBody>();
	}

	// Called Every Frame
	public void Update()
	{
		if (move_player)
		{
			Vector2 move = mPlayer.position;
			
			if (Input.GetKey(KeyCode.UP))
			{
			move.y += speed * Time.deltaTime;
			}
			else if (Input.GetKey(KeyCode.DOWN))
			{
			move.y -= speed * Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.LEFT))
			{
			move.x -= speed * Time.deltaTime; 
			}
			else if (Input.GetKey(KeyCode.RIGHT))
			{
			move.x += speed * Time.deltaTime;
			}

			mPlayer.position = move; 
		}
	}
}