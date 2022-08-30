using System.Collections.Generic;
using System;
using CometEngine;


class PlayerSpriteData
{
	public String IdName="";
	public Sprite SpriteName=null;
}


[AssetMenu("PlayerSpriteManager", "PlayerSpriteManager")]
class PlayerSprite : CometObject
{
	public PlayerSpriteData[] PlayerSprites = null;

	public Sprite GetSpriteByName(String aName)
	{
		Sprite ReturnValue = null;
		for (int i=0; i<PlayerSprites.Length; i++)
		{
			if (PlayerSprites[i].IdName == aName)
			{
				ReturnValue = PlayerSprites[i].SpriteName;
			}
		}
		return ReturnValue;
	}
}