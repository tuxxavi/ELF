using System;
using CometEngine;
using CometEngine.UI;

class Player1 : CometBehaviour
{    
    public float speed = 1000;
	public bool move_player = true;
	public int time_to_update_seconds;

	public Text RefTextUp;
	public Text RefTextDown;
	public Text RefTextPosi;

	private RigidBody mPlayer;
	private SpriteRenderer mRender;
	private Timer mTimer;

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

	public void initialize(Player player, Vector3 position, Sprite Sprite)
	{
		gameObject.name = player.Name+"_"+player.Position;
		move_player = player.Name == "Xavi";
		mRender.sprite = Sprite;
		transform.position = position;
		RefTextDown.text = player.Number+"";
		RefTextUp.text = player.Number+"";
		RefTextPosi.text = player.Position;
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