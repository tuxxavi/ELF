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

	private List<int[]> mRoutePlayer = new List<int[]>();

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

	public void initialize(Player player, Vector3 position, Sprite Sprite, float aTimeUpdate)
	{
		gameObject.name = player.Name+"_"+player.Position;
		move_player = player.Name == "Xavi";
		mRender.sprite = Sprite;
		transform.position = position;
		RefTextDown.text = player.Number+"";
		RefTextUp.text = player.Number+"";
		RefTextPosi.text = player.Position;
		mTimeToUpdateSeconds = aTimeUpdate;
		//@todo
		mRoutePlayer.Add(new int[]{500, 0});
		mRoutePlayer.Add(new int[]{300, 300});
		StartTimer();
	}

	public void StartTimer()
	{
		mTimer = gameObject.AddComponent<Timer>();
		mTimer.wait_time_in_seconds = mTimeToUpdateSeconds;
		mTimer.SendEventScript += update_move;
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

	public void update_move()
	{
		if (mRoutePlayer.Count > 0)
		{
			print("muevo x:" + mRoutePlayer[0][0] + " y:" + mRoutePlayer[0][1]);
			mPlayer.position += new Vector2(mRoutePlayer[0][0], mRoutePlayer[0][1]);
			mRoutePlayer.RemoveAt(0);
		}
	}
}