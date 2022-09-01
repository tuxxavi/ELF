using System;
using CometEngine;
using CometEngine.UI;
using System.Collections.Generic;

class Player1 : CometBehaviour
{    
    public float speed = 1000;
	public bool move_player = true;


	public Text RefTextUp = null;
	public Text RefTextDown = null;
	public Text RefTextPosi = null;

	private RigidBody mPlayer = null;
	private SpriteRenderer mRender = null;
	private Timer mTimer = null;
	private float mTimeToUpdateSeconds = 0;
	private bool mStopLastMove = true;

	private List<Vector2> mRoutePlayer = new List<Vector2>();

	// Called before first frame
	public void Start()
	{
		mPlayer = GetComponent<RigidBody>();
		mRender = GetComponent<SpriteRenderer>();
		//@todo mierda hasta tener la version y el bugazooooo, PAGAA
		RefTextDown = gameObject.GetChild("info").GetChild("down").GetComponent<Text>();
		RefTextUp = gameObject.GetChild("info").GetChild("up").GetComponent<Text>();
		RefTextPosi = gameObject.GetChild("info").GetChild("posi").GetComponent<Text>();
	}

	public void initialize(Player player, Vector3 PosiInit, int[] PosiRoute, Sprite Sprite, float aTimeUpdate, bool aStopLastMove)
	{
		gameObject.name = player.Name+"_"+player.Position;
		move_player = player.Name == "Urih 2";
		mRender.sprite = Sprite;
		transform.position = PosiInit;
		RefTextDown.text = player.Number+"";
		RefTextUp.text = player.Number+"";
		RefTextPosi.text = player.Position;
		mTimeToUpdateSeconds = aTimeUpdate;
		mStopLastMove = aStopLastMove;

		for (int i=0; i<PosiRoute.Length; i++)
		{
			mRoutePlayer.Add(new Vector2(PosiRoute[i], PosiRoute[i+1]));
			i++;
		}
		
		if (!move_player)
		{
			StartTimer();
		}
	}

	public void StartTimer()
	{
		mTimer = gameObject.AddComponent<Timer>();
		mTimer.wait_time_in_seconds = mTimeToUpdateSeconds;
		mTimer.SendEventScript += update_move;
		mTimer.loop = true;
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
		else if (mRoutePlayer.Count > 0 && !move_player)
		{
			mPlayer.position = Vector2.Lerp(mPlayer.position, 
											mPlayer.position + mRoutePlayer[0],
											Time.deltaTime);
		}
		else if (!mStopLastMove)
		{
			mPlayer.position = Vector2.Lerp(mPlayer.position, 
											mPlayer.position + new Vector2(50, 0),
											Time.deltaTime);
		}
	}

	public void update_move()
	{
		if (mRoutePlayer.Count > 0 && !move_player)
		{
			mRoutePlayer.RemoveAt(0);
		}
		else
		{
			mTimer.enabled= false;
		}
	}
}