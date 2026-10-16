using System;
using ConsoleLib.Console;
using Kobold;
using QupKit;
using UnityEngine;
using XRL.UI;
using XRL.World;

// Token: 0x0200006D RID: 109
public class QudItemListElement : ObjectPool<QudItemListElement>
{
	// Token: 0x060003BC RID: 956 RVA: 0x00017ECE File Offset: 0x000160CE
	public void PoolReset()
	{
		this.weight = 0;
		this.go = null;
		this.rightText = null;
		this.spriteInfo = null;
		this.tile = null;
		this.displayName = null;
		this.category = null;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00017F04 File Offset: 0x00016104
	public void InitFrom(XRL.World.GameObject go)
	{
		if (go == null)
		{
			this.tile = "Items/bit1.bmp";
			this.displayName = Sidebar.FormatToRTF("&knothing", "FF");
			this.rightText = "";
			this.foregroundColor = new Color(0f, 0f, 0f, 0f);
			this.detailColor = new Color(0f, 0f, 0f, 0f);
			this.backgroundColor = new Color(0f, 0f, 0f, 0f);
			this.weight = 0;
			return;
		}
		this.go = go;
		this.displayName = go.DisplayName;
		if (this.displayName.Contains("&") || this.displayName.Contains("^") || this.displayName.Contains("{{"))
		{
			this.displayName = Sidebar.FormatToRTF(this.displayName, "FF");
		}
		if (go.Physics != null)
		{
			this.weight = go.Physics.Weight;
			this.rightText = this.weight.ToString() + " lbs";
		}
		if (go.Render != null && go.Render.Tile != null)
		{
			this.tile = go.Render.Tile;
			this.foregroundColor = Color.white;
			this.detailColor = Color.black;
			this.backgroundColor = new Color(0f, 0f, 0f, 0f);
			if (!string.IsNullOrEmpty(go.Render.DetailColor))
			{
				this.detailColor = ConsoleLib.Console.ColorUtility.ColorMap[go.Render.DetailColor[0]];
			}
			if (!string.IsNullOrEmpty(go.Render.TileColor))
			{
				for (int i = 0; i < go.Render.TileColor.Length; i++)
				{
					if (go.Render.TileColor[i] == '&' && i < go.Render.TileColor.Length - 1)
					{
						if (go.Render.TileColor[i + 1] == '&')
						{
							i++;
						}
						else
						{
							this.foregroundColor = ConsoleLib.Console.ColorUtility.ColorMap[go.Render.TileColor[i + 1]];
						}
					}
					if (go.Render.TileColor[i] == '^' && i < go.Render.TileColor.Length - 1)
					{
						if (go.Render.TileColor[i + 1] == '^')
						{
							i++;
						}
						else
						{
							this.backgroundColor = ConsoleLib.Console.ColorUtility.ColorMap[go.Render.TileColor[i + 1]];
						}
					}
				}
				return;
			}
			if (!string.IsNullOrEmpty(go.Render.ColorString))
			{
				for (int j = 0; j < go.Render.ColorString.Length; j++)
				{
					if (go.Render.ColorString[j] == '&' && j < go.Render.ColorString.Length - 1)
					{
						if (go.Render.ColorString[j + 1] == '&')
						{
							j++;
						}
						else
						{
							this.foregroundColor = ConsoleLib.Console.ColorUtility.ColorMap[go.Render.ColorString[j + 1]];
						}
					}
					if (go.Render.ColorString[j] == '^' && j < go.Render.ColorString.Length - 1)
					{
						if (go.Render.ColorString[j + 1] == '^')
						{
							j++;
						}
						else
						{
							this.backgroundColor = ConsoleLib.Console.ColorUtility.ColorMap[go.Render.ColorString[j + 1]];
						}
					}
				}
			}
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x000182E3 File Offset: 0x000164E3
	public Sprite GenerateSprite()
	{
		if (this.tile == null)
		{
			return null;
		}
		if (this.spriteInfo == null)
		{
			this.spriteInfo = SpriteManager.GetTextureInfo(this.tile, true);
		}
		return SpriteManager.GetUnitySprite(this.spriteInfo);
	}

	// Token: 0x040002D2 RID: 722
	public string displayName = "<unknown>";

	// Token: 0x040002D3 RID: 723
	public string rightText = "";

	// Token: 0x040002D4 RID: 724
	public XRL.World.GameObject go;

	// Token: 0x040002D5 RID: 725
	public Color foregroundColor;

	// Token: 0x040002D6 RID: 726
	public Color backgroundColor;

	// Token: 0x040002D7 RID: 727
	public Color detailColor;

	// Token: 0x040002D8 RID: 728
	public string tile;

	// Token: 0x040002D9 RID: 729
	public string category;

	// Token: 0x040002DA RID: 730
	public exTextureInfo spriteInfo;

	// Token: 0x040002DB RID: 731
	public int weight;
}
