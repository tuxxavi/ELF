using System.Numerics;
using System;
using System.Collections.Generic;
using CometEngine;
using CometEngine.Json;

using PLayerFormation = System.Collections.Generic.Dictionary<System.String, CometEngine.Vector2>;

class Player
{
	public int Id;
	public String Name;
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

	int SUAU_TIENE_DIC = 0;

	// Called before first frame
	public void Start()
	{
		Debug.Log("Empezamos");
		JsonObject data = JsonObject.FileToJson(App.dataAssetsPath + "Game.json");
		if (data != null)
		{
			//formaciones NO VA UNAMIERDA
			//JsonObject FormJSON = data.GetObject("formaciones");
			//JsonObject trips = FormJSON.GetObject("trips");

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
		Vector2 MAGIADELCINE = new Vector2(200, 500);
		List<Player> Players =  mTeams[IdTeam].Players;
		for (int j =0; j<Players.Count; j++)
		{
			GameObject PlayerScene = CometEngine.Object.Instantiate(RuntimeAssets.LoadGameObject("scenes/Player0"));
			PlayerScene.name = Players[j].Name;
			PlayerScene.GetComponent<Player1>().move_player = PlayerScene.name == "Xavi";
			PlayerScene.GetComponent<SpriteRenderer>().sprite = PlayerSprite.GetSpriteByName(mTeams[IdTeam].Name);
			PlayerScene.transform.position = new Vector3(MAGIADELCINE.x, (MAGIADELCINE.y + SUAU_TIENE_DIC) * -1, PlayerScene.transform.position.z);
			SUAU_TIENE_DIC += 100;
		}
	}
}