using System.Numerics;
using System;
using System.Collections.Generic;
using CometEngine;
using CometEngine.Json;


using Route = System.Collections.Generic.Dictionary<string, int[]>;
using PLayerFormation = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, int[]>>;

class Player
{
	public int Id;
	public String Name;
	public String Position;
	public int Number;
}

class Team
{
	public String Name;
	public List<Player> Players = new List<Player>();
}



class main : CometBehaviour
{
	private List<Team> mTeams = new List<Team>();
	private Dictionary<String, PLayerFormation> mFormaciones = new Dictionary<String, PLayerFormation>();
	public PlayerSprite PlayerSprite = null;

	// Called before first frame
	public void Start()
	{
		Debug.Log("Empezamos");
		JsonObject data = JsonObject.FileToJson(App.dataAssetsPath + "Game.json");
		if (data != null)
		{
			//formaciones NO VA UNAMIERDA
			JsonObject FormJSON = data.GetObject("formaciones");
			JsonKey[] keys = FormJSON.GetKeys();
			for (int i=0; i<keys.Length; i++)
			{
				JsonKey[] value = FormJSON.GetObject(keys[i].key).GetKeys();
				PLayerFormation Data = new PLayerFormation();
				for (int j=0; j<value.Length; j++)
				{
					Route Route = new Route();
					int[] position_init = FormJSON.GetObject(keys[i].key).GetObject(value[j].key).GetIntArray("init");
					Route.Add("init", position_init);
					Data.Add(value[j].key, Route);
				}
				mFormaciones.Add(keys[i].key, Data);
			}

			JsonArray PlayersJSON = data.GetArray("players");
			JsonArray TeamsJSON = data.GetArray("equipos");
			for (uint i=0; i<TeamsJSON.GetSize(); i++)
			{
				JsonObject team = TeamsJSON.GetObject(i);
				if (team != null)
				{
					Team Data = new Team();
					Data.Name = team.GetString("name");
					int[] Players = team.GetIntArray("players");
					for (uint j=0; j<Players.Length; j++)
					{
						JsonObject info = PlayersJSON.GetObject((uint)Players[j]);
						Player player = new Player();
						player.Id = info.GetInt("id");
						player.Name = info.GetString("name");
						player.Position = info.GetString("position");
						player.Number = info.GetInt("number");
						Data.Players.Add(player);
					}
					mTeams.Add(Data);
				}
			}
		}
		data.Free();

		
		for (int i =0; i<mTeams.Count; i++)
		{
			AddPlayersInScene(i);
		}
	}

	// Called Every Frame
	public void Update()
	{
	}

	public void AddPlayersInScene(int IdTeam)
	{
		List<Player> Players =  mTeams[IdTeam].Players;
		for (int j =0; j<Players.Count; j++)
		{
			GameObject PlayerScene = CometEngine.Object.Instantiate(RuntimeAssets.LoadGameObject("scenes/Player0"));
			int[] position = GetPositionFormation("trips", Players[j].Position);
			PlayerScene.GetComponent<Player1>().initialize(Players[j],
				new Vector3(position[0], position[1] * -1, PlayerScene.transform.position.z),
				PlayerSprite.GetSpriteByName(mTeams[IdTeam].Name),
				1);
		}
	}

	private int[] GetPositionFormation(string jugada, string position, string tag = "init")
	{
		return mFormaciones[jugada][position][tag];
	}
}