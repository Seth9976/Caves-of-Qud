using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;
using UnityEngine.UI;
using XRL.World;
using XRL.World.Parts;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C5 RID: 1477
	public class MapFileCellRender
	{
		// Token: 0x06003EF5 RID: 16117 RVA: 0x00135B50 File Offset: 0x00133D50
		public void Reset()
		{
			this.Tile = null;
			this.Char = '\0';
			this.Detail = (this.Background = (this.Foreground = The.Color.DarkBlack));
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x00135B90 File Offset: 0x00133D90
		public void RenderBlueprint(GameObjectBlueprint bp, MapFileCellReference cref)
		{
			if (bp.HasPart("Render"))
			{
				Render.ProcessRenderString(bp.GetPartParameter<string>("Render", "RenderString", null));
				string partParameter = bp.GetPartParameter<string>("Render", "DetailColor", "k");
				try
				{
					if (partParameter.Length == 0)
					{
						Debug.Log("null detail color");
					}
					else if (!ConsoleLib.Console.ColorUtility.ColorMap.ContainsKey(partParameter[0]))
					{
						Debug.LogWarning(string.Format("Unknown detail color {0}", partParameter[0]));
					}
					else
					{
						this.Detail = ConsoleLib.Console.ColorUtility.ColorMap[partParameter[0]];
					}
				}
				catch (Exception)
				{
				}
				string text;
				if (!bp.Tags.TryGetValue("PaintWith", out text))
				{
					text = "";
				}
				string partParameter2 = bp.GetPartParameter<string>("Render", "RenderString", null);
				if (partParameter2.Length > 0)
				{
					this.Char = partParameter2[0];
				}
				this.Tile = bp.GetPartParameter<string>("Render", "Tile", null);
				if (this.Tile.IsNullOrEmpty())
				{
					this.Tile = null;
				}
				else
				{
					this.Char = '\0';
				}
				string text2 = bp.GetPartParameter<string>("Render", "TileColor", null);
				if (text2.IsNullOrEmpty() || this.Tile.IsNullOrEmpty())
				{
					text2 = bp.GetPartParameter<string>("Render", "ColorString", null);
				}
				char? c = new char?('y');
				char? c2 = new char?('k');
				ConsoleLib.Console.ColorUtility.FindLastForegroundAndBackground(text2, ref c, ref c2);
				this.Foreground = ConsoleLib.Console.ColorUtility.ColorMap[c.Value];
				this.Background = ConsoleLib.Console.ColorUtility.ColorMap[c2.Value];
				string text3;
				if (bp.Tags.TryGetValue("PaintedFence", out text3))
				{
					string text4;
					if (bp.Tags.TryGetValue("PaintedFenceAtlas", out text4))
					{
						this.Tile = text4;
					}
					else
					{
						this.Tile = "Tiles/";
					}
					this.Tile = this.Tile + text3.GetRandomSubstring(',', false, new global::System.Random(cref.x ^ cref.y)) + "_";
					if (cref.region.width == 1 && cref.region.height == 1)
					{
						this.Tile += "ew";
					}
					else
					{
						this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y - 1, bp.Name, text) ? "n" : "");
						this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y + 1, bp.Name, text) ? "s" : "");
						this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y, bp.Name, text) ? "e" : "");
						this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y, bp.Name, text) ? "w" : "");
					}
					if (bp.Tags.TryGetValue("PaintedFenceExtension", out text3))
					{
						this.Tile += text3;
						return;
					}
					this.Tile += ".bmp";
					return;
				}
				else if (bp.Tags.TryGetValue("PaintedWall", out text3))
				{
					string text4;
					if (bp.Tags.TryGetValue("PaintedWallAtlas", out text4))
					{
						this.Tile = text4;
					}
					else
					{
						this.Tile = "Tiles/";
					}
					this.Tile += text3.GetRandomSubstring(',', false, new global::System.Random(cref.x ^ cref.y));
					if (bp.Tags.ContainsKey("PaintedCheckerboard"))
					{
						this.Tile += (((cref.x + cref.y) % 2 == 0) ? "1" : "2");
					}
					this.Tile += "-";
					this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y - 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y - 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y - 1, bp.Name, text) ? "1" : "0");
					if (bp.Tags.TryGetValue("PaintedWallExtension", out text3))
					{
						this.Tile += text3;
						return;
					}
					this.Tile += ".bmp";
					return;
				}
				else if (bp.Tags.TryGetValue("PaintedLiquid", out text3))
				{
					string text4;
					if (bp.Tags.TryGetValue("PaintedLiquidAtlas", out text4))
					{
						this.Tile = text4;
					}
					else
					{
						this.Tile = "Assets_Content_Textures_Water_";
					}
					this.Tile = this.Tile + text3 + "-";
					this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y - 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y - 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x + 1, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y + 1, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y, bp.Name, text) ? "1" : "0");
					this.Tile += (cref.region.HasBlueprintInCell(cref.x - 1, cref.y - 1, bp.Name, text) ? "1" : "0");
					if (bp.Tags.TryGetValue("PaintedWallExtension", out text3))
					{
						this.Tile += text3;
						return;
					}
					this.Tile += ".bmp";
				}
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x00136514 File Offset: 0x00134714
		public void To3C(UIThreeColorProperties img)
		{
			if (this.Char == '\0')
			{
				img.image.sprite = SpriteManager.GetUnitySprite(this.Tile);
				img.SetColors(this.Foreground, this.Detail, this.Background);
				return;
			}
			Image image = img.image;
			string text = "Text/";
			int @char = (int)this.Char;
			image.sprite = SpriteManager.GetUnitySprite(text + @char.ToString() + ".bmp");
			img.SetColors(this.Background, this.Foreground, this.Foreground);
		}

		// Token: 0x04001F16 RID: 7958
		public string Tile;

		// Token: 0x04001F17 RID: 7959
		public char Char;

		// Token: 0x04001F18 RID: 7960
		public Color Foreground = Color.grey;

		// Token: 0x04001F19 RID: 7961
		public Color Background = Color.black;

		// Token: 0x04001F1A RID: 7962
		public Color Detail = Color.black;
	}
}
