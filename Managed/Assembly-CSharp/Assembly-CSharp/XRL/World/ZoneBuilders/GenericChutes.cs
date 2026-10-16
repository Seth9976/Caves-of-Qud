using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A98 RID: 2712
	public class GenericChutes
	{
		// Token: 0x06006EF6 RID: 28406 RVA: 0x002635C4 File Offset: 0x002617C4
		public static void GenTiles()
		{
			if (GenericChutes.Tiles != null)
			{
				return;
			}
			GenericChutes.Tiles = new Dictionary<string, string[]>();
			GenericChutes.Tiles.Add("", new string[] { "....", "....", "....", "...." });
			GenericChutes.Tiles.Add("NSTART", new string[] { "NNN.", "NNN.", "CCC." });
			GenericChutes.Tiles.Add("NEND", new string[] { "SSS.", ">>>.", "...." });
			GenericChutes.Tiles.Add("SSTART", new string[] { "CCC.", "SSS.", "SSS." });
			GenericChutes.Tiles.Add("SEND", new string[] { "....", ">>>.", "NNN." });
			GenericChutes.Tiles.Add("ESTART", new string[] { "CEEE", "CEEE", "CEEE" });
			GenericChutes.Tiles.Add("EEND", new string[] { ".>WW", ".>WW", ".>WW" });
			GenericChutes.Tiles.Add("WSTART", new string[] { "WWWC", "WWWC", "WWWC" });
			GenericChutes.Tiles.Add("WEND", new string[] { "EE>.", "EE>.", "EE>." });
			GenericChutes.Tiles.Add("NS", new string[] { "SSS.", "SSS.", "SSS." });
			GenericChutes.Tiles.Add("SN", new string[] { "NNN.", "NNN.", "NNN." });
			GenericChutes.Tiles.Add("EW", new string[] { "WWWW", "WWWW", "WWWW" });
			GenericChutes.Tiles.Add("WE", new string[] { "EEEE", "EEEE", "EEEE" });
			GenericChutes.Tiles.Add("NW", new string[] { "WSS.", "WWS.", "WWW." });
			GenericChutes.Tiles.Add("WN", new string[] { "NNN.", "ENN.", "EEN." });
			GenericChutes.Tiles.Add("SW", new string[] { "WWW.", "WWN.", "WNN." });
			GenericChutes.Tiles.Add("WS", new string[] { "EES.", "ESS.", "SSS." });
			GenericChutes.Tiles.Add("NE", new string[] { "SSEE", "SEEE", "EEEE" });
			GenericChutes.Tiles.Add("EN", new string[] { "NNNW", "NNWW", "NWWW" });
			GenericChutes.Tiles.Add("SE", new string[] { "EEEE", "NEEE", "NNEE" });
			GenericChutes.Tiles.Add("ES", new string[] { "SWWW", "SSWW", "SSSW" });
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x0026399C File Offset: 0x00261B9C
		public static GenericChuteTemplate GenerateGolgothaTemplate()
		{
			GenericChuteTemplate genericChuteTemplate = new GenericChuteTemplate();
			genericChuteTemplate.MainBuilding = new Box(29, 1, 47, 6);
			genericChuteTemplate.Chutes = Tools.GenerateBoxes(genericChuteTemplate.MainBuilding, BoxGenerateOverlap.NeverOverlap, new Range(4, 4), new Range(10, 76), new Range(8, 20), new Range(200, 2000));
			return genericChuteTemplate;
		}

		// Token: 0x06006EF8 RID: 28408 RVA: 0x002639F8 File Offset: 0x00261BF8
		public bool BuildZone(Zone Z, int chutes, bool chuteLevel)
		{
			this.nChutes = chutes;
			this.chuteLevel = chuteLevel;
			return this.BuildZone(Z);
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x00263A10 File Offset: 0x00261C10
		public bool BuildZone(Zone Z)
		{
			GenericChutes.GenTiles();
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			GenericChuteTemplate genericChuteTemplate;
			if (zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.GenericChuteTemplate.ChuteTemplate", null) != null)
			{
				genericChuteTemplate = (GenericChuteTemplate)zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.GenericChuteTemplate.ChuteTemplate", null);
			}
			else
			{
				genericChuteTemplate = GenericChutes.GenerateGolgothaTemplate();
				zoneManager.SetZoneColumnProperty(Z.ZoneID, "Builder.GenericChuteTemplate.ChuteTemplate", genericChuteTemplate);
			}
			int num = 0;
			foreach (Box box in genericChuteTemplate.Chutes)
			{
				string text = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("U", 1), "Chute" + num.ToString() + "EndY", false, null);
				string text2 = (string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(Z.GetZoneIDFromDirection("D", 1), "Chute" + num.ToString() + "StartY", false, null);
				int num2 = (box.Width - 2) / 4;
				int num3 = (box.Height - 2) / 3;
				if (num2 < 1)
				{
					num2 = 2;
				}
				if (num3 < 1)
				{
					num3 = 2;
				}
				if (text == null)
				{
					text = Stat.Random(0, num3 - 1).ToString();
				}
				if (text2 == null)
				{
					text2 = Stat.Random(0, num3 - 1).ToString();
				}
				Convert.ToInt32(text);
				Convert.ToInt32(text2);
				The.ZoneManager.SetZoneProperty(Z.ZoneID, "Chute" + num.ToString() + "StartY", text);
				The.ZoneManager.SetZoneProperty(Z.ZoneID, "Chute" + num.ToString() + "EndY", text2);
				TunnelMaker tunnelMaker;
				if (Z.Z % 2 == 0)
				{
					tunnelMaker = new TunnelMaker(num2, num3, text, text2, "ENS");
				}
				else
				{
					tunnelMaker = new TunnelMaker(num2, num3, text, text2, "WNS");
				}
				bool flag = Z.Z % 2 == 0;
				if (this.chuteLevel)
				{
					for (int i = 0; i < tunnelMaker.Height; i++)
					{
						for (int j = 0; j < tunnelMaker.Width; j++)
						{
							int num4 = j * 4 + box.x1 + 1;
							int num5 = i * 3 + box.y1 + 1;
							string text3 = tunnelMaker.Map[j, i];
							if (text3 == "N" || text3 == "S" || text3 == "E" || text3 == "W")
							{
								if ((flag && j == 0) || (!flag && j == tunnelMaker.Width - 1))
								{
									text3 += "START";
								}
								else
								{
									text3 += "END";
								}
							}
							string[] array = GenericChutes.Tiles[text3];
							for (int k = 0; k < 4; k++)
							{
								for (int l = 0; l < 3; l++)
								{
									char c = array[l][k];
									if (c != '.')
									{
										if (c == '>')
										{
											Z.GetCell(num4 + k, num5 + l).Clear(null, false, false, null);
											Z.GetCell(num4 + k, num5 + l).AddObject("SlimyShaft", null, null, null, null);
										}
										else if (c == 'C')
										{
											if (Z.Z > 10)
											{
												Z.GetCell(num4 + k, num5 + l).Clear(null, false, false, null);
												Z.GetCell(num4 + k, num5 + l).AddObject("ConveyorDrive", null, null, null, null);
											}
										}
										else if (Z.Z > 10)
										{
											Z.GetCell(num4 + k, num5 + l).Clear(null, false, false, null);
											GameObject gameObject = GameObjectFactory.Factory.CreateObject("ConveyorPad");
											ConveyorPad part = gameObject.GetPart<ConveyorPad>();
											part.Direction = c.ToString();
											part.Connections = c.ToString();
											Z.GetCell(num4 + k, num5 + l).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
										}
									}
								}
							}
						}
					}
					if (Z.Z > 10)
					{
						int m = 0;
						int num6 = 0;
						int num7 = Stat.Random(6, 10);
						while (m < 100)
						{
							if (num6 >= num7)
							{
								break;
							}
							int num8 = Stat.Random(box.x1, box.x2);
							int num9 = Stat.Random(box.y1, box.y2);
							if (Z.GetCell(num8, num9).HasWall())
							{
								bool flag2 = false;
								using (List<Cell>.Enumerator enumerator2 = Z.GetCell(num8, num9).GetCardinalAdjacentCells(true, true, false).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										if (enumerator2.Current.HasObjectWithPart("ConveyorPad"))
										{
											flag2 = true;
											break;
										}
									}
								}
								if (flag2)
								{
									num6++;
									Z.GetCell(num8, num9).Clear(null, false, false, null);
									GameObject gameObject2 = null;
									if (num == 0)
									{
										gameObject2 = GameObjectFactory.Factory.CreateObject("WalltrapFire");
										Walltrap part2 = gameObject2.GetPart<Walltrap>();
										part2.TurnInterval = Stat.Random(4, 8);
										part2.CurrentTurn = Stat.Random(0, part2.TurnInterval - 2);
									}
									if (num == 1)
									{
										gameObject2 = GameObjectFactory.Factory.CreateObject("WalltrapAcid");
										Walltrap part3 = gameObject2.GetPart<Walltrap>();
										part3.TurnInterval = Stat.Random(12, 16);
										part3.CurrentTurn = Stat.Random(0, part3.TurnInterval - 2);
									}
									if (num == 2)
									{
										gameObject2 = GameObjectFactory.Factory.CreateObject("WalltrapShock");
										Walltrap part4 = gameObject2.GetPart<Walltrap>();
										part4.TurnInterval = Stat.Random(4, 8);
										part4.CurrentTurn = Stat.Random(0, part4.TurnInterval - 2);
									}
									if (num == 3)
									{
										gameObject2 = GameObjectFactory.Factory.CreateObject("WalltrapCrabs");
										Walltrap part5 = gameObject2.GetPart<Walltrap>();
										part5.TurnInterval = Stat.Random(4, 16);
										part5.CurrentTurn = Stat.Random(0, part5.TurnInterval - 2);
									}
									Z.GetCell(num8, num9).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
							m++;
						}
					}
				}
				else
				{
					for (int n = 0; n < tunnelMaker.Height; n++)
					{
						for (int num10 = 0; num10 < tunnelMaker.Width; num10++)
						{
							int num11 = num10 * 4 + box.x1 + 1;
							int num12 = n * 3 + box.y1 + 1;
							string text4 = tunnelMaker.Map[num10, n];
							if (text4 == "N" || text4 == "S" || text4 == "E" || text4 == "W")
							{
								if ((flag && num10 == 0) || (!flag && num10 == tunnelMaker.Width - 1))
								{
									text4 += "START";
								}
								else
								{
									text4 += "END";
								}
							}
							string[] array2 = GenericChutes.Tiles[text4];
							for (int num13 = 0; num13 < 4; num13++)
							{
								int num14 = 0;
								while (num14 < 3)
								{
									char c2 = array2[num14][num13];
									if (c2 == '>')
									{
										using (List<Cell>.Enumerator enumerator2 = Z.GetCell(num11 + num13, num12 + num14).GetLocalAdjacentCells().GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												Cell cell = enumerator2.Current;
												cell.Clear(null, false, false, null);
											}
											goto IL_07D1;
										}
										goto IL_0783;
									}
									goto IL_0783;
									IL_07D1:
									num14++;
									continue;
									IL_0783:
									if (c2 == 'C')
									{
										foreach (Cell cell2 in Z.GetCell(num11 + num13, num12 + num14).GetLocalAdjacentCells())
										{
											cell2.Clear(null, false, false, null);
										}
										goto IL_07D1;
									}
									goto IL_07D1;
								}
							}
						}
					}
				}
				num++;
				if (num >= this.nChutes)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x040032C4 RID: 12996
		public static Dictionary<string, string[]> Tiles;

		// Token: 0x040032C5 RID: 12997
		public int nChutes = 4;

		// Token: 0x040032C6 RID: 12998
		public bool chuteLevel = true;
	}
}
