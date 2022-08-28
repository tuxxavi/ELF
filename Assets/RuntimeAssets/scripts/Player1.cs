using System;
using CometEngine;

class Player1 : CometBehaviour
{    
    public float speed = 1000;
	public bool move_player = true;
	public int[][] automove;
	public int time_to_update_seconds;
	private RigidBody mPlayer;
	private Timer mTimer;

	// Called before first frame
	public void Start()
	{
		mPlayer = GetComponent<RigidBody>();
	}

	public void initialize(String name_param, Vector3 position, Sprite Sprite)
	{
		GetComponentInParent<SpriteRenderer>().name = name_param;
		move_player = name == "Xavi";
		GetComponentInParent<SpriteRenderer>().sprite = Sprite;
		this.transform.position = position;
	}

	public void StartTimer()
	{
		mTimer = gameObject.AddComponent<Timer>();
		mTimer.wait_time_in_seconds = time_to_update_seconds;
		mTimer.SendEventScript += novanada;
		mTimer.loop = move_player;
		mTimer.StartTimer();
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

	public void novanada()
	{
		print("dsdsd");
	}
}