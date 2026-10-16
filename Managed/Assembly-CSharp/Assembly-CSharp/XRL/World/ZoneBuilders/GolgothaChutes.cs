using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A9A RID: 2714
	public class GolgothaChutes
	{
		// Token: 0x06006EFC RID: 28412 RVA: 0x002642E4 File Offset: 0x002624E4
		public static void GenTiles()
		{
			if (GolgothaChutes.Tiles != null)
			{
				return;
			}
			GolgothaChutes.Tiles = new Dictionary<string, string[]>();
			GolgothaChutes.Tiles.Add("", new string[] { "....", "....", "....", "...." });
			GolgothaChutes.Tiles.Add("NSTART", new string[] { "NNN.", "NNN.", "CCC." });
			GolgothaChutes.Tiles.Add("NEND", new string[] { "SSS.", ">>>.", "...." });
			GolgothaChutes.Tiles.Add("SSTART", new string[] { "CCC.", "SSS.", "SSS." });
			GolgothaChutes.Tiles.Add("SEND", new string[] { "....", ">>>.", "NNN." });
			GolgothaChutes.Tiles.Add("ESTART", new string[] { "CEEE", "CEEE", "CEEE" });
			GolgothaChutes.Tiles.Add("EEND", new string[] { ".>WW", ".>WW", ".>WW" });
			GolgothaChutes.Tiles.Add("WSTART", new string[] { "WWWC", "WWWC", "WWWC" });
			GolgothaChutes.Tiles.Add("WEND", new string[] { "EE>.", "EE>.", "EE>." });
			GolgothaChutes.Tiles.Add("NS", new string[] { "SSS.", "SSS.", "SSS." });
			GolgothaChutes.Tiles.Add("SN", new string[] { "NNN.", "NNN.", "NNN." });
			GolgothaChutes.Tiles.Add("EW", new string[] { "WWWW", "WWWW", "WWWW" });
			GolgothaChutes.Tiles.Add("WE", new string[] { "EEEE", "EEEE", "EEEE" });
			GolgothaChutes.Tiles.Add("NW", new string[] { "WSS.", "WWS.", "WWW." });
			GolgothaChutes.Tiles.Add("WN", new string[] { "NNN.", "ENN.", "EEN." });
			GolgothaChutes.Tiles.Add("SW", new string[] { "WWW.", "WWN.", "WNN." });
			GolgothaChutes.Tiles.Add("WS", new string[] { "EES.", "ESS.", "SSS." });
			GolgothaChutes.Tiles.Add("NE", new string[] { "SSEE", "SEEE", "EEEE" });
			GolgothaChutes.Tiles.Add("EN", new string[] { "NNNW", "NNWW", "NWWW" });
			GolgothaChutes.Tiles.Add("SE", new string[] { "EEEE", "NEEE", "NNEE" });
			GolgothaChutes.Tiles.Add("ES", new string[] { "SWWW", "SSWW", "SSSW" });
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x002646BC File Offset: 0x002628BC
		public static GolgothaTemplate GenerateGolgothaTemplate()
		{
			GolgothaTemplate golgothaTemplate = new GolgothaTemplate();
			golgothaTemplate.MainBuilding = new Box(29, 1, 47, 6);
			golgothaTemplate.Chutes = Tools.GenerateBoxes(golgothaTemplate.MainBuilding.Grow(1), BoxGenerateOverlap.NeverOverlap, new Range(4, 4), new Range(10, 76), new Range(8, 20), new Range(200, 2000));
			return golgothaTemplate;
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x00264720 File Offset: 0x00262920
		public bool BuildZone(Zone Z)
		{
			GolgothaChutes.GenTiles();
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			GolgothaTemplate golgothaTemplate = null;
			if (zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Golgotha.GolgothaTemplate", null) != null)
			{
				golgothaTemplate = (GolgothaTemplate)zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Golgotha.GolgothaTemplate", null);
			}
			else
			{
				golgothaTemplate = GolgothaChutes.GenerateGolgothaTemplate();
				zoneManager.SetZoneColumnProperty(Z.ZoneID, "Builder.Golgotha.GolgothaTemplate", golgothaTemplate);
			}
			if (Z.Z == 15)
			{
				List<NoiseMapNode> list = new List<NoiseMapNode>();
				Tools.FillBox(Z, new Box(0, 0, 79, 24), "Verdigris");
				Tools.ClearBox(Z, new Box(1, 1, 78, 23));
				int num = 0;
				foreach (Box box in golgothaTemplate.Chutes)
				{
					Box box2 = box.Grow(-1);
					int num2 = Stat.Random(0, 3);
					if (num2 == 0)
					{
						int num3 = box2.x1;
						while (num3 <= box2.x2 && num3 < 77)
						{
							if (num3 != 1)
							{
								Z.GetCell(num3, box2.y1).AddObject("Verdigris", null, null, null, null);
							}
							num3++;
						}
						int num4 = box2.y1;
						while (num4 <= box2.y2 && num4 < 22)
						{
							if (num4 != 1)
							{
								Z.GetCell(box2.x2, num4).AddObject("Verdigris", null, null, null, null);
							}
							num4++;
						}
					}
					if (num2 == 1)
					{
						int num5 = box2.x1;
						while (num5 <= box2.x2 && num5 < 77)
						{
							if (num5 != 1)
							{
								Z.GetCell(num5, box2.y1).AddObject("Verdigris", null, null, null, null);
							}
							num5++;
						}
						int num6 = box2.y1;
						while (num6 <= box2.y2 && num6 < 22)
						{
							if (num6 != 1)
							{
								Z.GetCell(box2.x2, num6).AddObject("Verdigris", null, null, null, null);
							}
							num6++;
						}
					}
					if (num2 == 2)
					{
						int num7 = box2.x1;
						while (num7 <= box2.x2 && num7 < 77)
						{
							if (num7 != 1)
							{
								Z.GetCell(num7, box2.y2).AddObject("Verdigris", null, null, null, null);
							}
							num7++;
						}
						int num8 = box2.y1;
						while (num8 <= box2.y2 && num8 < 22)
						{
							if (num8 != 1)
							{
								Z.GetCell(box2.x1, num8).AddObject("Verdigris", null, null, null, null);
							}
							num8++;
						}
					}
					if (num2 == 3)
					{
						int num9 = box2.x1;
						while (num9 <= box2.x2 && num9 < 77)
						{
							if (num9 != 1)
							{
								Z.GetCell(num9, box2.y1).AddObject("Verdigris", null, null, null, null);
							}
							num9++;
						}
						int num10 = box2.y1;
						while (num10 <= box2.y2 && num10 < 22)
						{
							if (num10 != 1)
							{
								Z.GetCell(box2.x1, num10).AddObject("Verdigris", null, null, null, null);
							}
							num10++;
						}
					}
					string text = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("U", 1), "Chute" + num.ToString() + "EndY", true, null);
					bool flag = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("D", 1), "Chute" + num.ToString() + "StartY", true, null) != null;
					int num11 = (box2.Width - 2) / 4;
					int num12 = (box2.Height - 2) / 3;
					if (num11 < 1)
					{
						num11 = 2;
					}
					if (num12 < 1)
					{
						num12 = 2;
					}
					if (text == null)
					{
						text = Stat.Random(0, num12 - 1).ToString();
					}
					if (!flag)
					{
						Stat.Random(0, num12 - 1).ToString();
					}
					int num13 = num11 - 4 + box2.x1 + 1;
					int num14 = num12 - 3 + box2.y1 + 1;
					list.Add(new NoiseMapNode(num13, num14));
					num++;
				}
				NoiseMap noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 40, 5, 100, 0, 6, 0, 1, list, 2);
				noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 60, 5, 100, 0, 5, 0, 1, null, 2);
				for (int i = 0; i < Z.Height; i++)
				{
					for (int j = 0; j < Z.Width; j++)
					{
						if ((double)noiseMap.Noise[j, i] > 0.01 && Z.GetCell(j, i).IsEmptyOfSolid(true))
						{
							Z.GetCell(j, i).AddObject("GooPuddle", null, null, null, null);
						}
					}
				}
				noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 60, 5, 100, 0, 5, 0, 1, null, 2);
				for (int k = 0; k < Z.Height; k++)
				{
					for (int l = 0; l < Z.Width; l++)
					{
						if ((double)noiseMap.Noise[l, k] > 0.01 && Z.GetCell(l, k).IsEmptyOfSolid(true))
						{
							Z.GetCell(l, k).AddObject("SludgePuddle", null, null, null, null);
						}
					}
				}
				noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 60, 5, 100, 0, 5, 0, 1, null, 2);
				for (int m = 0; m < Z.Height; m++)
				{
					for (int n = 0; n < Z.Width; n++)
					{
						if ((double)noiseMap.Noise[n, m] > 0.01 && Z.GetCell(n, m).IsEmptyOfSolid(true))
						{
							Z.GetCell(n, m).AddObject("OozePuddle", null, null, null, null);
						}
					}
				}
				noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 25, 5, 100, 0, 5, 0, 1, null, 2);
				for (int num15 = 0; num15 < Z.Height; num15++)
				{
					for (int num16 = 0; num16 < Z.Width; num16++)
					{
						if ((double)noiseMap.Noise[num16, num15] > 0.01 && Z.GetCell(num16, num15).IsEmptyOfSolid(true))
						{
							Z.GetCell(num16, num15).Clear(null, false, false, null);
							Z.GetCell(num16, num15).AddObject("Garbage", null, null, null, null);
						}
					}
				}
				Tools.FillBox(Z, golgothaTemplate.MainBuilding, "Verdigris");
				Tools.ClearBox(Z, golgothaTemplate.MainBuilding.Grow(-1));
				int num17 = golgothaTemplate.MainBuilding.Grow(-1).x1 + 2;
				int num18 = golgothaTemplate.MainBuilding.Grow(-1).y1 + 2;
				Cell cell = Z.GetCell(num17, num18);
				cell.Clear(null, false, false, null);
				cell.AddObject("FlyingWhitelistArea", null, null, null, null);
				cell.AddObject("Platform", null, null, null, null);
				Cell cell2 = Z.GetCell(num17, num18 - 1);
				cell2.Clear(null, false, false, null);
				cell2.AddObject("ElevatorSwitch", null, null, null, null);
				Z.GetCell((golgothaTemplate.MainBuilding.x1 + golgothaTemplate.MainBuilding.x2) / 2, golgothaTemplate.MainBuilding.y2).Clear(null, false, false, null);
				Point point = (from c in Z.GetCells()
					where c.X == 1 || c.X == Z.Width - 2 || c.Y == 1 || c.Y == Z.Height - 2
					select c).GetRandomElement<Cell>(null).Point;
				foreach (Cell cell3 in Z.GetCell(point).GetLocalAdjacentCellsCircular(2, true))
				{
					cell3.Clear(null, false, false, null);
					cell3.AddObject("DarkCyanPit", null, null, null, null);
				}
				using (List<Cell>.Enumerator enumerator2 = Z.GetCells().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Cell cell4 = enumerator2.Current;
						if (!cell4.IsSolid())
						{
							cell4.SetReachable(true);
						}
					}
					goto IL_165B;
				}
			}
			if (Z.Z > 10)
			{
				for (int num19 = 0; num19 < Z.Height; num19++)
				{
					for (int num20 = 0; num20 < Z.Width; num20++)
					{
						Z.GetCell(num20, num19).AddObject("Shale", null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).AddObject("Dirty", null, null, null, null);
			if (Z.Z == 10)
			{
				Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			}
			Tools.FillBox(Z, golgothaTemplate.MainBuilding, "Verdigris");
			Tools.ClearBox(Z, golgothaTemplate.MainBuilding.Grow(-1));
			int num21 = golgothaTemplate.MainBuilding.Grow(-1).x1 + 2;
			int num22 = golgothaTemplate.MainBuilding.Grow(-1).y1 + 2;
			Cell cell5 = Z.GetCell(num21, num22);
			cell5.Clear(null, false, false, null);
			cell5.AddObject("FlyingWhitelistArea", null, null, null, null);
			cell5.AddObject("OpenShaft", null, null, null, null);
			Cell cell6 = Z.GetCell((golgothaTemplate.MainBuilding.x1 + golgothaTemplate.MainBuilding.x2) / 2, golgothaTemplate.MainBuilding.y2);
			cell6.Clear(null, false, false, null);
			cell6.AddObject("Purple Security Door", null, null, null, null);
			Cell cell7 = Z.GetCell((golgothaTemplate.MainBuilding.x1 + golgothaTemplate.MainBuilding.x2) / 2 + 1, golgothaTemplate.MainBuilding.y2 - 1);
			cell7.Clear(null, false, false, null);
			cell7.AddObject("DoorSwitch", null, null, null, null);
			int num23 = 0;
			foreach (Box box3 in golgothaTemplate.Chutes)
			{
				if (Z.Z > 10)
				{
					Tools.FillBox(Z, box3, "Verdigris");
				}
				string text2 = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("U", 1), "Chute" + num23.ToString() + "EndY", false, null);
				string text3 = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("D", 1), "Chute" + num23.ToString() + "StartY", false, null);
				int num24 = (box3.Width - 2) / 4;
				int num25 = (box3.Height - 2) / 3;
				if (num24 < 1)
				{
					num24 = 2;
				}
				if (num25 < 1)
				{
					num25 = 2;
				}
				if (text2 == null)
				{
					text2 = Stat.Random(0, num25 - 1).ToString();
				}
				if (text3 == null)
				{
					text3 = Stat.Random(0, num25 - 1).ToString();
				}
				Convert.ToInt32(text2);
				Convert.ToInt32(text3);
				XRLCore.Core.Game.ZoneManager.SetZoneProperty(Z.ZoneID, "Chute" + num23.ToString() + "StartY", text2);
				XRLCore.Core.Game.ZoneManager.SetZoneProperty(Z.ZoneID, "Chute" + num23.ToString() + "EndY", text3);
				TunnelMaker tunnelMaker;
				if (Z.Z % 2 == 0)
				{
					tunnelMaker = new TunnelMaker(num24, num25, text2, text3, "ENS");
				}
				else
				{
					tunnelMaker = new TunnelMaker(num24, num25, text2, text3, "WNS");
				}
				bool flag2 = Z.Z % 2 == 0;
				for (int num26 = 0; num26 < tunnelMaker.Width; num26++)
				{
					for (int num27 = 0; num27 < tunnelMaker.Height; num27++)
					{
						int num28 = num26 * 4 + box3.x1 + 1;
						int num29 = num27 * 3 + box3.y1 + 1;
						string text4 = tunnelMaker.Map[num26, num27];
						if (text4 == "N" || text4 == "S" || text4 == "E" || text4 == "W")
						{
							if ((flag2 && num26 == 0) || (!flag2 && num26 == tunnelMaker.Width - 1))
							{
								text4 += "START";
							}
							else
							{
								text4 += "END";
							}
						}
						string[] array = GolgothaChutes.Tiles[text4];
						bool flag3 = false;
						for (int num30 = 0; num30 < 4; num30++)
						{
							for (int num31 = 0; num31 < 3; num31++)
							{
								Z.GetCell(num28 + num30, num29 + num31).Clear(null, false, false, null);
								char c2 = array[num31][num30];
								if (c2 == '.')
								{
									if (Z.Z > 10)
									{
										Z.GetCell(num28 + num30, num29 + num31).AddObject("Verdigris", null, null, null, null);
									}
								}
								else if (c2 == '>')
								{
									Z.GetCell(num28 + num30, num29 + num31).AddObject("SlimyShaft", null, null, null, null);
									flag3 = true;
								}
								else if (c2 == 'C')
								{
									if (Z.Z > 10)
									{
										Z.GetCell(num28 + num30, num29 + num31).AddObject("ConveyorDrive", null, null, null, null);
									}
								}
								else if (Z.Z > 10)
								{
									GameObject gameObject = GameObject.Create("ConveyorPad");
									ConveyorPad part = gameObject.GetPart<ConveyorPad>();
									part.Direction = c2.ToString();
									part.Connections = c2.ToString();
									Z.GetCell(num28 + num30, num29 + num31).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
						}
						if (flag3 && Z.Z == 10)
						{
							Tools.Box(Z, new Box(num28 - 1, num29 - 1, Math.Min(num28 + 5, 79), Math.Min(num29 + 4, 24)), "Verdigris", 80);
						}
					}
				}
				if (Z.Z > 10)
				{
					int num32 = 0;
					int num33 = 0;
					int num34 = Stat.Random(6, 10);
					while (num32 < 100 && num33 < num34)
					{
						int num35 = Stat.Random(box3.x1, box3.x2);
						int num36 = Stat.Random(box3.y1, box3.y2);
						if (Z.GetCell(num35, num36).HasWall())
						{
							bool flag4 = false;
							using (List<Cell>.Enumerator enumerator2 = Z.GetCell(num35, num36).GetCardinalAdjacentCells(true, true, false).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.HasObjectWithPart("ConveyorPad"))
									{
										flag4 = true;
										break;
									}
								}
							}
							if (flag4)
							{
								num33++;
								Z.GetCell(num35, num36).Clear(null, false, false, null);
								GameObject gameObject2 = null;
								if (num23 == 0)
								{
									gameObject2 = GameObject.Create("WalltrapFire");
								}
								if (num23 == 1)
								{
									gameObject2 = GameObject.Create("WalltrapAcid");
								}
								if (num23 == 2)
								{
									gameObject2 = GameObject.Create("WalltrapShock");
								}
								if (num23 == 3)
								{
									gameObject2 = GameObject.Create("WalltrapCrabs");
								}
								Walltrap part2 = gameObject2.GetPart<Walltrap>();
								if (num23 == 0)
								{
									part2.TurnInterval = Stat.Random(4, 8);
								}
								if (num23 == 1)
								{
									part2.TurnInterval = Stat.Random(12, 16);
								}
								if (num23 == 2)
								{
									part2.TurnInterval = Stat.Random(4, 8);
								}
								if (num23 == 3)
								{
									part2.TurnInterval = Stat.Random(4, 16);
								}
								part2.CurrentTurn = Stat.Random(0, part2.TurnInterval - 2);
								Z.GetCell(num35, num36).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						num32++;
					}
				}
				num23++;
			}
			if (Z.Z != 10)
			{
				NoiseMap noiseMap2 = new NoiseMap(80, 25, 10, 4, 3, Stat.Random(0, 2), Stat.Random(15, 35), Stat.Random(35, 50), 5, 3, 1, 1, null);
				foreach (List<NoiseMapNode> list2 in noiseMap2.AreaNodes.Values)
				{
					foreach (NoiseMapNode noiseMapNode in list2)
					{
						Z.GetCell(noiseMapNode.x, noiseMapNode.y).ClearWalls();
					}
				}
			}
			if (Z.Z != 10)
			{
				NoiseMap noiseMap2 = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 10, 5, 100, 0, 6, 0, 1, null, 2);
				for (int num37 = 0; num37 < Z.Height; num37++)
				{
					for (int num38 = 0; num38 < Z.Width; num38++)
					{
						if ((double)noiseMap2.Noise[num38, num37] > 0.01 && Z.GetCell(num38, num37).IsEmptyOfSolid(true) && !Z.GetCell(num38, num37).HasSpawnBlocker())
						{
							Z.GetCell(num38, num37).AddObject("Garbage", null, null, null, null);
						}
					}
				}
				noiseMap2 = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 20, 5, 100, 0, 5, 0, 1, null, 2);
				for (int num39 = 0; num39 < Z.Height; num39++)
				{
					for (int num40 = 0; num40 < Z.Width; num40++)
					{
						if ((double)noiseMap2.Noise[num40, num39] > 0.01 && Z.GetCell(num40, num39).IsEmptyOfSolid(true) && Z.GetCell(num40, num39).HasSpawnBlocker())
						{
							Z.GetCell(num40, num39).AddObject("GooPuddle", null, null, null, null);
						}
					}
				}
				noiseMap2 = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 20, 5, 100, 0, 5, 0, 1, null, 2);
				for (int num41 = 0; num41 < Z.Height; num41++)
				{
					for (int num42 = 0; num42 < Z.Width; num42++)
					{
						if ((double)noiseMap2.Noise[num42, num41] > 0.01 && Z.GetCell(num42, num41).IsEmptyOfSolid(true) && Z.GetCell(num42, num41).HasSpawnBlocker())
						{
							Z.GetCell(num42, num41).AddObject("SludgePuddle", null, null, null, null);
						}
					}
				}
				noiseMap2 = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, 20, 5, 100, 0, 5, 0, 1, null, 2);
				for (int num43 = 0; num43 < Z.Height; num43++)
				{
					for (int num44 = 0; num44 < Z.Width; num44++)
					{
						if ((double)noiseMap2.Noise[num44, num43] > 0.01 && Z.GetCell(num44, num43).IsEmptyOfSolid(true) && Z.GetCell(num44, num43).HasSpawnBlocker())
						{
							Z.GetCell(num44, num43).AddObject("OozePuddle", null, null, null, null);
						}
					}
				}
			}
			Z.ClearReachableMap();
			using (List<Box>.Enumerator enumerator = golgothaTemplate.Chutes.GetEnumerator())
			{
				IL_1642:
				while (enumerator.MoveNext())
				{
					Box box4 = enumerator.Current;
					for (int num45 = box4.y1 + 1; num45 < box4.y2; num45++)
					{
						for (int num46 = box4.x1 + 1; num46 < box4.x2; num46++)
						{
							if (Z.GetCell(num46, num45).HasObjectWithPart("StairsDown"))
							{
								Z.BuildReachableMap(num46, num45, false);
								goto IL_1642;
							}
						}
					}
				}
			}
			IL_165B:
			for (int num47 = 0; num47 < Z.Height; num47++)
			{
				for (int num48 = 0; num48 < Z.Width; num48++)
				{
					GameObject openLiquidVolume = Z.GetCell(num48, num47).GetOpenLiquidVolume();
					LiquidVolume liquidVolume = ((openLiquidVolume != null) ? openLiquidVolume.LiquidVolume : null);
					if (liquidVolume != null && liquidVolume.Volume > 500 && 20.in100())
					{
						Z.GetCell(num48, num47).AddObject("EelSpawn", null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).RequireObject("HolyPlaceNephilimWidget", null, null, null, null);
			return true;
		}

		// Token: 0x040032C9 RID: 13001
		public static Dictionary<string, string[]> Tiles;
	}
}
