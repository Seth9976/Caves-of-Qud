using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A51 RID: 2641
	[Serializable]
	public class BuildingZoneTemplate : IComposite
	{
		// Token: 0x06006DFA RID: 28154 RVA: 0x00255D60 File Offset: 0x00253F60
		public Point2D GetRandomWall()
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Template.Map[j, i] == BuildingTemplateTile.Wall)
					{
						num++;
					}
				}
			}
			if (num > 0)
			{
				int num2 = Stat.Random(1, num);
				int num3 = 0;
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (this.Template.Map[l, k] == BuildingTemplateTile.Wall && ++num3 == num2)
						{
							return new Point2D(l, k);
						}
					}
				}
			}
			return new Point2D(0, 0);
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x00255E18 File Offset: 0x00254018
		public List<Point2D> GetWalls()
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Template.Map[j, i] == BuildingTemplateTile.Wall)
					{
						num++;
					}
				}
			}
			List<Point2D> list = new List<Point2D>(num);
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					if (this.Template.Map[l, k] == BuildingTemplateTile.Wall)
					{
						list.Add(new Point2D(l, k));
					}
				}
			}
			return list;
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x00255EBC File Offset: 0x002540BC
		public BuildingTemplate FromWFC(string wfcTemplateTable, string segmentation, int width, int height, int hmirrorchance = 20, int vmirrorchance = 20)
		{
			BuildingTemplate buildingTemplate = new BuildingTemplate(width, height, BuildingTemplateTile.Outside);
			Grid<Color4> grid = new Grid<Color4>(width, height);
			grid.fromWFCTemplate(PopulationManager.RollOneFrom(wfcTemplateTable, null, null).Blueprint.Split(',', StringSplitOptions.None)[0], 3, int.MaxValue);
			Grid<Color4> grid2 = new Grid<Color4>(width, height);
			if (segmentation == "None")
			{
				grid2.fill(Color4.white);
			}
			else
			{
				grid2.fill(Color4.red);
				if (segmentation == "Block" || segmentation == "Blocks" || segmentation == "BlocksAndCircles")
				{
					int num = 1;
					if (segmentation == "Blocks" || segmentation == "BlocksAndCircles")
					{
						num += Stat.Random(0, 2);
					}
					for (int i = 0; i < num; i++)
					{
						int num2 = Stat.Random(Math.Min(3, width), Math.Max(3, width));
						int num3 = Stat.Random(Math.Min(3, height), Math.Max(3, height));
						int num4 = Stat.Random(0, width - num2);
						int num5 = Stat.Random(0, height - num3);
						for (int j = num4; j < num4 + num2; j++)
						{
							for (int k = num5; k < num5 + num3; k++)
							{
								if (j > 0 && k > 0 && j < width && k < height)
								{
									if (j == num4 || k == num5 || j == num4 + num2 - 1 || k == num5 + num3 - 1)
									{
										if (grid2.get(j, k) == Color4.red)
										{
											grid2.set(j, k, Color4.black);
										}
									}
									else
									{
										grid2.set(j, k, Color4.white);
									}
								}
							}
						}
					}
				}
				if (segmentation == "Circle" || segmentation == "Circles" || segmentation == "BlocksAndCircles")
				{
					int num6 = 1;
					if (segmentation == "Circles" || segmentation == "BlocksAndCircles")
					{
						num6 += Stat.Random(0, 2);
					}
					for (int l = 0; l < num6; l++)
					{
						int num7 = Stat.Random(2, Math.Max(3, (int)((float)Math.Min(width, height) / 2f)));
						int num8 = Stat.Random((int)((double)num7 / 0.6), Math.Max((int)((double)num7 / 0.6), width - (int)((double)num7 / 0.6)));
						int num9 = Stat.Random(num7, Math.Max(num7, height - num7));
						for (int m = 0; m <= height; m++)
						{
							for (int n = 0; n <= width; n++)
							{
								if (n > 0 && m > 0 && n < width && m < height && BuildingZoneTemplate.<FromWFC>g__CosmeticDistanceTo|8_0(n, m, num8, num9) <= num7)
								{
									if (BuildingZoneTemplate.<FromWFC>g__CosmeticDistanceTo|8_0(n, m, num8, num9) == num7 || (BuildingZoneTemplate.<FromWFC>g__CosmeticDistanceTo|8_0(n, m, num8, num9) <= num7 && n == 0) || m == 0 || n == width - 1 || m == height - 1)
									{
										if (grid2.get(n, m) == Color4.red)
										{
											grid2.set(n, m, Color4.black);
										}
									}
									else
									{
										grid2.set(n, m, Color4.white);
									}
								}
							}
						}
					}
				}
			}
			if (hmirrorchance.in100())
			{
				grid2 = grid2.mirrorHorizontal();
			}
			if (vmirrorchance.in100())
			{
				grid2 = grid2.mirrorVertical();
			}
			if (hmirrorchance.in100())
			{
				grid = grid.mirrorHorizontal();
			}
			if (vmirrorchance.in100())
			{
				grid = grid.mirrorVertical();
			}
			for (int num10 = 0; num10 < height; num10++)
			{
				for (int num11 = 0; num11 < width; num11++)
				{
					if (grid2.get(num11, num10) == Color4.red)
					{
						buildingTemplate.Map[num11, num10] = BuildingTemplateTile.Outside;
					}
					else if (grid.get(num11, num10) == Color4.black || grid2.get(num11, num10) == Color4.black)
					{
						buildingTemplate.Map[num11, num10] = BuildingTemplateTile.Wall;
					}
					else if (grid.get(num11, num10) == Color4.white)
					{
						buildingTemplate.Map[num11, num10] = BuildingTemplateTile.Outside;
					}
					else
					{
						buildingTemplate.Map[num11, num10] = BuildingTemplateTile.Inside;
					}
				}
			}
			return buildingTemplate;
		}

		// Token: 0x06006DFD RID: 28157 RVA: 0x00256328 File Offset: 0x00254528
		public void New(int nWidth, int nHeight, int ZonesWide, int ZonesHigh, string wfcTemplateTable = "Ruins_PrimaryTemplate_*Default")
		{
			this.Template = new BuildingTemplate(nWidth, nHeight, BuildingTemplateTile.Outside);
			this.Width = nWidth;
			this.Height = nHeight;
			string text = PopulationManager.RollOneFrom("Ruins_Style", null, null).Blueprint;
			int num = 0;
			int num2 = 50;
			text = "WFC Common Segmentation";
			if (text == "Classic")
			{
				num = 0;
			}
			if (text == "WFC Chance Per Building")
			{
				num = 20;
			}
			if (text == "WFC Common Segmentation")
			{
				num = 100;
			}
			if (text == "WFC Same Template")
			{
				num = 100;
			}
			string text2 = null;
			bool flag = false;
			BuildingTemplate buildingTemplate = null;
			for (int i = 0; i < ZonesWide; i++)
			{
				for (int j = 0; j < ZonesHigh; j++)
				{
					int num3 = 80 / ZonesWide;
					int num4 = 25 / ZonesHigh;
					int num5 = num3 * i;
					int num6 = num3 * (i + 1);
					int num7 = num4 * j;
					if (num6 - num5 > 35 && Stat.Random(1, 100) <= 35)
					{
						num6 -= (num6 - num5) / 2;
					}
					if (flag && text == "WFC Same Template")
					{
						this.Template.AddMap(num5, num7, buildingTemplate);
					}
					else if (!flag || Stat.Random(1, 100) <= num2)
					{
						flag = true;
						if (Stat.Random(1, 100) <= num)
						{
							if (text != "WFC Common Segmentation" || text2 == null)
							{
								text2 = PopulationManager.RollOneFrom("Ruins_Segmentation", null, null).Blueprint;
							}
							buildingTemplate = this.FromWFC(wfcTemplateTable, text2, num3, num4, 20, 20);
						}
						else
						{
							buildingTemplate = new BuildingTemplate(num3, num4, 8 / (ZonesWide * ZonesHigh), false);
						}
						this.Template.AddMap(num5, num7, buildingTemplate);
					}
				}
			}
			for (int k = 0; k < this.Width; k++)
			{
				int num8 = 0;
				while (num8 < this.Height && (this.Template.Map[k, num8] == BuildingTemplateTile.Inside || this.Template.Map[k, num8] == BuildingTemplateTile.Outside))
				{
					this.Template.Map[k, num8] = BuildingTemplateTile.Outside;
					num8++;
				}
				int num9 = this.Height - 1;
				while (num9 >= 0 && (this.Template.Map[k, num9] == BuildingTemplateTile.Inside || this.Template.Map[k, num9] == BuildingTemplateTile.Outside))
				{
					this.Template.Map[k, num9] = BuildingTemplateTile.Outside;
					num9--;
				}
			}
			for (int l = 0; l < this.Height; l++)
			{
				int num10 = 0;
				while (num10 < this.Width && (this.Template.Map[num10, l] == BuildingTemplateTile.Inside || this.Template.Map[num10, l] == BuildingTemplateTile.Outside))
				{
					this.Template.Map[num10, l] = BuildingTemplateTile.Outside;
					num10++;
				}
				int num11 = this.Width - 1;
				while (num11 >= 0 && (this.Template.Map[num11, l] == BuildingTemplateTile.Inside || this.Template.Map[num11, l] == BuildingTemplateTile.Outside))
				{
					this.Template.Map[num11, l] = BuildingTemplateTile.Outside;
					num11--;
				}
			}
		}

		// Token: 0x06006DFE RID: 28158 RVA: 0x0025665C File Offset: 0x0025485C
		public void BuildZone(Zone Z, bool bUnderground)
		{
			GameObject terrainObject = Z.GetTerrainObject();
			string text;
			if (terrainObject.HasTag("RuinWalls"))
			{
				if (!The.ZoneManager.TryGetWorldCellProperty<string>(Z.ZoneID, "RuinWallType", out text))
				{
					text = PopulationManager.RollOneFrom(terrainObject.GetTag("RuinWalls", null), null, null).Blueprint;
					The.ZoneManager.SetWorldCellProperty(Z.ZoneID, "RuinWallType", text);
				}
			}
			else
			{
				text = "Fulcrete";
			}
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					Cell cell = Z.GetCell(j, i);
					if (j >= this.Template.Width || i >= this.Template.Height)
					{
						cell.Clear(null, false, false, (GameObject go) => go.HasTag("NoRuinsRemove"));
					}
					else if (this.Template.Map[j, i] == BuildingTemplateTile.OutsideWall || this.Template.Map[j, i] == BuildingTemplateTile.Wall)
					{
						cell.Clear(null, false, false, (GameObject go) => go.HasTag("NoRuinsRemove"));
						cell.AddObject(text, null, null, null, null);
					}
					else if (this.Template.Map[j, i] == BuildingTemplateTile.Door)
					{
						cell.Clear(null, false, false, (GameObject go) => go.HasTag("NoRuinsRemove"));
						cell.AddObject("Door", null, null, null, null);
					}
					else if (this.Template.Map[j, i] == BuildingTemplateTile.StairsDown)
					{
						cell.Clear(null, false, false, null);
						if (Z.GetZoneZ() != 10)
						{
							if (Z.GetZoneZ() % 2 == 0)
							{
								cell.AddObject("StairsUp", null, null, null, null);
							}
							else
							{
								cell.AddObject("StairsDown", null, null, null, null);
							}
						}
					}
					else if (this.Template.Map[j, i] == BuildingTemplateTile.StairsUp)
					{
						cell.Clear(null, false, false, null);
						if (Z.GetZoneZ() % 2 == 0)
						{
							cell.AddObject("StairsDown", null, null, null, null);
						}
						else
						{
							cell.AddObject("StairsUp", null, null, null, null);
						}
					}
					else if (!bUnderground || this.Template.Map[j, i] != BuildingTemplateTile.Outside)
					{
						if (!bUnderground && this.Template.Map[j, i] == BuildingTemplateTile.Inside)
						{
							if (!cell.HasObjectWithTag("NoRuinsRemove"))
							{
								cell.ClearWalls();
							}
						}
					}
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection != "-")
				{
					Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).Clear(null, false, false, null);
				}
			}
		}

		// Token: 0x06006DFF RID: 28159 RVA: 0x0025697C File Offset: 0x00254B7C
		[CompilerGenerated]
		internal static int <FromWFC>g__CosmeticDistanceTo|8_0(int x, int y, int X, int Y)
		{
			return (int)Math.Sqrt((double)((float)(x - X) * 0.6666f * ((float)(x - X) * 0.6666f) + (float)((y - Y) * (y - Y))));
		}

		// Token: 0x0400325D RID: 12893
		public BuildingTemplate Template;

		// Token: 0x0400325E RID: 12894
		public int Width;

		// Token: 0x0400325F RID: 12895
		public int Height;

		// Token: 0x04003260 RID: 12896
		public int ZonesWide;

		// Token: 0x04003261 RID: 12897
		public int ZonesHigh;
	}
}
