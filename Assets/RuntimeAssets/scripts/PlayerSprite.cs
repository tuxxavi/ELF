using System.Collections.Generic;
using System;
using CometEngine;


class PlayerSpriteData
{
	public String IdName;
	public Sprite SpriteName;
}


[AssetMenu("PlayerSpriteManager", "PlayerSpriteManager")]
class PlayerSprite : CometObject
{
	public PlayerSpriteData[] PlayerSprites;
}