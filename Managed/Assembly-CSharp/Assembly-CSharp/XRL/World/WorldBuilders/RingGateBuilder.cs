using System;
using System.Collections.Generic;
using Qud.API;
using UnityEngine;
using XRL.Names;
using XRL.Rules;

namespace XRL.World.WorldBuilders
{
	// Token: 0x0200099E RID: 2462
	public class RingGateBuilder : WorldBuilder
	{
		// Token: 0x06006A7D RID: 27261 RVA: 0x002222AC File Offset: 0x002204AC
		public override bool BuildWorld(string World)
		{
			this.World = World;
			try
			{
				MetricsManager.rngCheckpoint("teleportgates");
				MetricsManager.LogInfo("Placing teleport gates");
				this.BuildTeleportGates();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("RingGateBuilder.BuildWorld", ex, "game_exception");
			}
			return true;
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x00222300 File Offset: 0x00220500
		public string ZoneIDFromXYz(string World, int xp, int yp, int zp)
		{
			int num = (int)Math.Floor((double)((float)xp / 3f));
			int num2 = (int)Math.Floor((double)((float)yp / 3f));
			return ZoneID.Assemble(World, num, num2, xp % 3, yp % 3, zp);
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x00222340 File Offset: 0x00220540
		public string ZoneIDFromXY(string World, int xp, int yp)
		{
			int num = (int)Math.Floor((double)((float)xp / 3f));
			int num2 = (int)Math.Floor((double)((float)yp / 3f));
			return ZoneID.Assemble(World, num, num2, xp % 3, yp % 3, 10);
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x00222380 File Offset: 0x00220580
		public void BuildTeleportGates()
		{
			List<string> list = this.GenerateTeleportGateZones();
			base.game.SetObjectGameState(this.World + "TeleportGateZones", list);
			this.PlaceTeleportGates(list);
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x002223B8 File Offset: 0x002205B8
		public List<string> GenerateTeleportGateZones()
		{
			Zone zone = The.ZoneManager.GetZone(this.World);
			List<string> list = new List<string>();
			if (!this.StaticGateZones.IsNullOrEmpty())
			{
				List<string> list2 = this.StaticGateZones.CachedCommaExpansion();
				list.AddRange(list2);
				if (RingGateBuilder.TELEPORT_GATE_DEBUG)
				{
					foreach (string text in list2)
					{
						Debug.LogError("STATIC GATE FOR " + text + " " + The.ZoneManager.GetZoneDisplayName(text, false, false, true, true));
					}
				}
			}
			for (int i = 0; i < 25; i++)
			{
				for (int j = 0; j < 80; j++)
				{
					GameObject firstObjectWithPart = zone.GetCell(j, i).GetFirstObjectWithPart("TerrainTravel");
					if (firstObjectWithPart != null)
					{
						if (firstObjectWithPart.Blueprint.Contains("TerrainRuins"))
						{
							for (int k = 0; k <= 2; k++)
							{
								for (int l = 0; l <= 2; l++)
								{
									if (RingGateBuilder.TELEPORT_GATE_RUINS_SURFACE_PERMILLAGE_CHANCE.in1000())
									{
										string text2 = Zone.XYToID(this.World, j * 3 + l, i * 3 + k, 10);
										if (!list.Contains(text2))
										{
											list.Add(text2);
											if (RingGateBuilder.TELEPORT_GATE_DEBUG)
											{
												Debug.LogError(string.Concat(new string[]
												{
													"GATE FOR ",
													text2,
													" ",
													The.ZoneManager.GetZoneDisplayName(text2, false, false, true, true),
													" BASED ON RUINS SURFACE"
												}));
											}
										}
									}
									for (int m = 1; m <= RingGateBuilder.TELEPORT_GATE_RUINS_DEPTH; m++)
									{
										if (RingGateBuilder.TELEPORT_GATE_RUINS_DEEP_PERMILLAGE_CHANCE.in1000())
										{
											string text3 = Zone.XYToID(this.World, j * 3 + l, i * 3 + k, 10 + m);
											if (!list.Contains(text3))
											{
												list.Add(text3);
												if (RingGateBuilder.TELEPORT_GATE_DEBUG)
												{
													Debug.LogError(string.Concat(new string[]
													{
														"GATE FOR ",
														text3,
														" ",
														The.ZoneManager.GetZoneDisplayName(text3, false, false, true, true),
														" BASED ON RUINS DEEP"
													}));
												}
											}
										}
									}
								}
							}
						}
						else if (firstObjectWithPart.Blueprint.Contains("TerrainBaroqueRuins"))
						{
							for (int n = 0; n <= 2; n++)
							{
								for (int num = 0; num <= 2; num++)
								{
									if (RingGateBuilder.TELEPORT_GATE_BAROQUE_RUINS_SURFACE_PERMILLAGE_CHANCE.in1000())
									{
										string text4 = Zone.XYToID(this.World, j * 3 + num, i * 3 + n, 10);
										if (!list.Contains(text4))
										{
											list.Add(text4);
											if (RingGateBuilder.TELEPORT_GATE_DEBUG)
											{
												Debug.LogError(string.Concat(new string[]
												{
													"GATE FOR ",
													text4,
													" ",
													The.ZoneManager.GetZoneDisplayName(text4, false, false, true, true),
													" BASED ON BAROQUE RUINS SURFACE"
												}));
											}
										}
									}
									for (int num2 = 1; num2 <= RingGateBuilder.TELEPORT_GATE_BAROQUE_RUINS_DEPTH; num2++)
									{
										if (RingGateBuilder.TELEPORT_GATE_BAROQUE_RUINS_DEEP_PERMILLAGE_CHANCE.in1000())
										{
											string text5 = Zone.XYToID(this.World, j * 3 + num, i * 3 + n, 10 + num2);
											if (!list.Contains(text5))
											{
												list.Add(text5);
												if (RingGateBuilder.TELEPORT_GATE_DEBUG)
												{
													Debug.LogError(string.Concat(new string[]
													{
														"GATE FOR ",
														text5,
														" ",
														The.ZoneManager.GetZoneDisplayName(text5, false, false, true, true),
														" BASED ON BAROQUE RUINS DEEP"
													}));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.World == "JoppaWorld")
			{
				foreach (JournalMapNote journalMapNote in JournalAPI.GetMapNotes((JournalMapNote note) => note.Has("ruins") || note.Has("historic")))
				{
					if (journalMapNote.Has("historic"))
					{
						string text6 = Zone.XYToID(this.World, journalMapNote.ResolvedX, journalMapNote.ResolvedY, 10);
						if (RingGateBuilder.TELEPORT_GATE_HISTORIC_SITE_SURFACE_PERMILLAGE_CHANCE.in1000() && !list.Contains(text6))
						{
							list.Add(text6);
							if (RingGateBuilder.TELEPORT_GATE_DEBUG)
							{
								Debug.LogError(string.Concat(new string[]
								{
									"GATE FOR ",
									text6,
									" ",
									The.ZoneManager.GetZoneDisplayName(text6, false, false, true, true),
									" BASED ON HISTORIC RUIN SURFACE"
								}));
							}
						}
						string text7 = The.ZoneManager.GetZoneProperty(text6, "HistoricSite", false, null) as string;
						if (!text7.IsNullOrEmpty())
						{
							for (int num3 = 1; num3 <= RingGateBuilder.TELEPORT_GATE_HISTORIC_SITE_CHECK_DEPTH; num3++)
							{
								string text8 = Zone.XYToID(this.World, journalMapNote.ResolvedX, journalMapNote.ResolvedY, 10 + num3);
								try
								{
									The.ZoneManager.GetZoneDisplayName(text8, false, false, true, true);
								}
								catch (Exception)
								{
								}
								if (!((string)The.ZoneManager.GetZoneProperty(text8, "HistoricSite", false, null) == text7))
								{
									break;
								}
								if (RingGateBuilder.TELEPORT_GATE_HISTORIC_SITE_DEEP_PERMILLAGE_CHANCE.in1000() && !list.Contains(text8))
								{
									list.Add(text8);
									if (RingGateBuilder.TELEPORT_GATE_DEBUG)
									{
										Debug.LogError(string.Concat(new string[]
										{
											"GATE FOR ",
											text8,
											" ",
											The.ZoneManager.GetZoneDisplayName(text8, false, false, true, true),
											" BASED ON HISTORIC RUIN DEEP"
										}));
									}
								}
							}
						}
					}
					else if (RingGateBuilder.TELEPORT_GATE_SECRET_RUIN_PERMILLAGE_CHANCE.in1000())
					{
						string text9 = Zone.XYToID(this.World, journalMapNote.ResolvedX, journalMapNote.ResolvedY, 10);
						if (!list.Contains(text9))
						{
							list.Add(text9);
							if (RingGateBuilder.TELEPORT_GATE_DEBUG)
							{
								Debug.LogError(string.Concat(new string[]
								{
									"GATE FOR ",
									text9,
									" ",
									The.ZoneManager.GetZoneDisplayName(text9, false, false, true, true),
									" BASED ON SECRET RUIN"
								}));
							}
						}
					}
				}
			}
			int num4 = list.Count * RingGateBuilder.TELEPORT_GATE_RANDOM_PROPORTION / 100;
			int num5 = 0;
			int num6 = 0;
			while (num5 < num4)
			{
				int num7 = (RingGateBuilder.TELEPORT_GATE_RANDOM_SURFACE_TARGET_PERCENTAGE_CHANCE.in100() ? 10 : Stat.Random(11, RingGateBuilder.TELEPORT_GATE_RANDOM_DEEP_TARGET_DEPTH));
				int num8 = Stat.Random(0, 79);
				int num9 = Stat.Random(0, 24);
				int num10 = Stat.Random(0, 2);
				int num11 = Stat.Random(0, 2);
				string text10 = Zone.XYToID(this.World, num8 * 3 + num10, num9 * 3 + num11, num7);
				if (list.Contains(text10))
				{
					if (++num6 < 100)
					{
						num5--;
					}
				}
				else
				{
					list.Add(text10);
					if (RingGateBuilder.TELEPORT_GATE_DEBUG)
					{
						Debug.LogError(string.Concat(new string[]
						{
							"GATE FOR ",
							text10,
							" ",
							The.ZoneManager.GetZoneDisplayName(text10, false, false, true, true),
							" BASED ON RANDOM ",
							(num5 + 1).ToString(),
							" OF ",
							num4.ToString()
						}));
					}
				}
				num5++;
			}
			return list;
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x00222B58 File Offset: 0x00220D58
		public void PlaceTeleportGates(List<string> zones = null)
		{
			if (zones == null)
			{
				zones = this.GenerateTeleportGateZones();
			}
			List<string> list = null;
			if (!this.StaticGateZones.IsNullOrEmpty())
			{
				list = this.StaticGateZones.CachedCommaExpansion();
			}
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			List<string> list4 = new List<string>();
			List<string> list5 = new List<string>();
			foreach (string text in zones)
			{
				switch (Stat.Random(0, 2))
				{
				case 0:
					list3.Add(text);
					break;
				case 1:
					list4.Add(text);
					break;
				case 2:
					list5.Add(text);
					break;
				}
			}
			list3.ShuffleInPlace<string>(null);
			list4.ShuffleInPlace<string>(null);
			list5.ShuffleInPlace<string>(null);
			int num = 0;
			while (list3.Count % 2 != 0 && num < 100)
			{
				string randomElement = list3.GetRandomElement<string>(null);
				if (++num > 100 || list == null || !list.Contains(randomElement))
				{
					list2.Add(randomElement);
					list3.Remove(randomElement);
				}
			}
			num = 0;
			while (list4.Count % 3 != 0 && num < 100)
			{
				string randomElement2 = list4.GetRandomElement<string>(null);
				if (++num > 100 || list == null || !list.Contains(randomElement2))
				{
					list2.Add(randomElement2);
					list4.Remove(randomElement2);
				}
			}
			num = 0;
			while (list5.Count % 4 != 0 && num < 100)
			{
				string randomElement3 = list5.GetRandomElement<string>(null);
				if (++num > 100 || list == null || !list.Contains(randomElement3))
				{
					list2.Add(randomElement3);
					list5.Remove(randomElement3);
				}
			}
			List<List<string>> list6 = new List<List<string>>();
			List<List<string>> list7 = new List<List<string>>();
			List<List<string>> list8 = new List<List<string>>();
			Dictionary<string, List<List<string>>> dictionary = new Dictionary<string, List<List<string>>>(64);
			int num2 = 0;
			int count = list3.Count;
			while (num2 + 1 < count)
			{
				string text2 = list3[num2];
				string text3 = list3[num2 + 1];
				The.ZoneManager.SetZoneProperty(text2, "TeleportGateDestinationZone", text3);
				The.ZoneManager.SetZoneProperty(text3, "TeleportGateDestinationZone", text2);
				The.ZoneManager.SetZoneProperty(text2, "TeleportGateRingSize", 2);
				The.ZoneManager.SetZoneProperty(text3, "TeleportGateRingSize", 2);
				List<string> list9 = new List<string> { text2, text3 };
				list6.Add(list9);
				this.ConfigureRingName(list9, dictionary);
				if (RingGateBuilder.TELEPORT_GATE_DEBUG)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"DEFINED 2-RING FROM ",
						text2,
						" ",
						The.ZoneManager.GetZoneDisplayName(text2, false, false, true, true),
						" TO ",
						text3,
						" ",
						The.ZoneManager.GetZoneDisplayName(text3, false, false, true, true)
					}));
				}
				num2 += 2;
			}
			int num3 = 0;
			int count2 = list4.Count;
			while (num3 + 2 < count2)
			{
				string text4 = list4[num3];
				string text5 = list4[num3 + 1];
				string text6 = list4[num3 + 2];
				The.ZoneManager.SetZoneProperty(text4, "TeleportGateDestinationZone", text5);
				The.ZoneManager.SetZoneProperty(text5, "TeleportGateDestinationZone", text6);
				The.ZoneManager.SetZoneProperty(text6, "TeleportGateDestinationZone", text4);
				The.ZoneManager.SetZoneProperty(text4, "TeleportGateRingSize", 3);
				The.ZoneManager.SetZoneProperty(text5, "TeleportGateRingSize", 3);
				The.ZoneManager.SetZoneProperty(text6, "TeleportGateRingSize", 3);
				List<string> list10 = new List<string> { text4, text5, text6 };
				list7.Add(list10);
				this.ConfigureRingName(list10, dictionary);
				if (RingGateBuilder.TELEPORT_GATE_DEBUG)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"DEFINED 3-RING FROM ",
						text4,
						" ",
						The.ZoneManager.GetZoneDisplayName(text4, false, false, true, true),
						" TO ",
						text5,
						" ",
						The.ZoneManager.GetZoneDisplayName(text5, false, false, true, true),
						" TO ",
						text6,
						" ",
						The.ZoneManager.GetZoneDisplayName(text6, false, false, true, true)
					}));
				}
				num3 += 3;
			}
			int num4 = 0;
			int count3 = list5.Count;
			while (num4 + 3 < count3)
			{
				string text7 = list5[num4];
				string text8 = list5[num4 + 1];
				string text9 = list5[num4 + 2];
				string text10 = list5[num4 + 3];
				The.ZoneManager.SetZoneProperty(text7, "TeleportGateDestinationZone", text8);
				The.ZoneManager.SetZoneProperty(text8, "TeleportGateDestinationZone", text9);
				The.ZoneManager.SetZoneProperty(text9, "TeleportGateDestinationZone", text10);
				The.ZoneManager.SetZoneProperty(text10, "TeleportGateDestinationZone", text7);
				The.ZoneManager.SetZoneProperty(text7, "TeleportGateRingSize", 4);
				The.ZoneManager.SetZoneProperty(text8, "TeleportGateRingSize", 4);
				The.ZoneManager.SetZoneProperty(text9, "TeleportGateRingSize", 4);
				The.ZoneManager.SetZoneProperty(text10, "TeleportGateRingSize", 4);
				List<string> list11 = new List<string> { text7, text8, text9, text10 };
				list8.Add(list11);
				this.ConfigureRingName(list11, dictionary);
				if (RingGateBuilder.TELEPORT_GATE_DEBUG)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"DEFINED 4-RING FROM ",
						text7,
						" ",
						The.ZoneManager.GetZoneDisplayName(text7, false, false, true, true),
						" TO ",
						text8,
						" ",
						The.ZoneManager.GetZoneDisplayName(text8, false, false, true, true),
						" TO ",
						text9,
						" ",
						The.ZoneManager.GetZoneDisplayName(text9, false, false, true, true),
						" TO ",
						text10,
						" ",
						The.ZoneManager.GetZoneDisplayName(text10, false, false, true, true)
					}));
				}
				num4 += 4;
			}
			foreach (string text11 in list2)
			{
				num = 0;
				string randomElement4;
				do
				{
					randomElement4 = zones.GetRandomElement<string>(null);
				}
				while (++num < 10 && randomElement4 == text11);
				The.ZoneManager.SetZoneProperty(text11, "TeleportGateDestinationZone", randomElement4);
				if (RingGateBuilder.TELEPORT_GATE_DEBUG)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"DEFINED SECANT FROM ",
						text11,
						" ",
						The.ZoneManager.GetZoneDisplayName(text11, false, false, true, true),
						" TO ",
						randomElement4,
						" ",
						The.ZoneManager.GetZoneDisplayName(randomElement4, false, false, true, true)
					}));
				}
			}
			The.Game.SetObjectGameState(this.World + "TeleportGate2Rings", list6);
			The.Game.SetObjectGameState(this.World + "TeleportGate3Rings", list7);
			The.Game.SetObjectGameState(this.World + "TeleportGate4Rings", list8);
			The.Game.SetObjectGameState(this.World + "TeleportGateSecants", list2);
			foreach (string text12 in zones)
			{
				if (list == null || !list.Contains(text12))
				{
					The.ZoneManager.AddZonePostBuilderAfterTerrain(text12, "ZoneTemplate:RingGate");
				}
			}
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x00223368 File Offset: 0x00221568
		private string GetNameRoot(List<string> ring)
		{
			string text = null;
			List<string> list = null;
			foreach (string text2 in ring)
			{
				string text3 = The.ZoneManager.GetZoneProperty(text2, "TeleportGateCandidateNameRoot", false, null) as string;
				if (!text3.IsNullOrEmpty())
				{
					if (list != null)
					{
						list.Add(text3);
					}
					if (text == null)
					{
						text = text3;
					}
					else
					{
						list = new List<string> { text, text3 };
					}
				}
			}
			if (list != null)
			{
				return list.GetRandomElement<string>(null);
			}
			if (!text.IsNullOrEmpty())
			{
				return text;
			}
			return NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x00223444 File Offset: 0x00221644
		private void ConfigureRingName(List<string> ring, Dictionary<string, List<List<string>>> nameRootMap)
		{
			string text = this.GetNameRoot(ring);
			List<List<string>> list;
			bool flag = nameRootMap.TryGetValue(text, out list);
			if (flag)
			{
				switch (list.Count)
				{
				case 0:
					break;
				case 1:
				{
					string text2 = "Aleph-" + text;
					switch (list[0].Count)
					{
					case 0:
						goto IL_019D;
					case 1:
						break;
					case 2:
						goto IL_00BC;
					case 3:
						goto IL_0107;
					default:
						The.ZoneManager.SetZoneProperty(list[0][3], "TeleportGateName", text2 + " milk gate");
						The.ZoneManager.SetZoneProperty(list[0][3], "TeleportGateNameRoot", text2);
						goto IL_0107;
					}
					IL_0071:
					The.ZoneManager.SetZoneProperty(list[0][0], "TeleportGateName", text2 + " moon gate");
					The.ZoneManager.SetZoneProperty(list[0][0], "TeleportGateNameRoot", text2);
					goto IL_019D;
					IL_00BC:
					The.ZoneManager.SetZoneProperty(list[0][1], "TeleportGateName", text2 + " sun gate");
					The.ZoneManager.SetZoneProperty(list[0][1], "TeleportGateNameRoot", text2);
					goto IL_0071;
					IL_0107:
					The.ZoneManager.SetZoneProperty(list[0][2], "TeleportGateName", text2 + " fool gate");
					The.ZoneManager.SetZoneProperty(list[0][2], "TeleportGateNameRoot", text2);
					goto IL_00BC;
					IL_019D:
					text = "Bet-" + text;
					break;
				}
				case 2:
					text = "Gimel-" + text;
					break;
				case 3:
					text = "Daled-" + text;
					break;
				default:
					text = "He-" + text;
					break;
				}
			}
			switch (ring.Count)
			{
			case 0:
				goto IL_02E0;
			case 1:
				break;
			case 2:
				goto IL_0232;
			case 3:
				goto IL_026C;
			default:
				The.ZoneManager.SetZoneProperty(ring[3], "TeleportGateName", text + " milk gate");
				The.ZoneManager.SetZoneProperty(ring[3], "TeleportGateNameRoot", text);
				goto IL_026C;
			}
			IL_01F5:
			The.ZoneManager.SetZoneProperty(ring[0], "TeleportGateName", text + " moon gate");
			The.ZoneManager.SetZoneProperty(ring[0], "TeleportGateNameRoot", text);
			goto IL_02E0;
			IL_0232:
			The.ZoneManager.SetZoneProperty(ring[1], "TeleportGateName", text + " sun gate");
			The.ZoneManager.SetZoneProperty(ring[1], "TeleportGateNameRoot", text);
			goto IL_01F5;
			IL_026C:
			The.ZoneManager.SetZoneProperty(ring[2], "TeleportGateName", text + " fool gate");
			The.ZoneManager.SetZoneProperty(ring[2], "TeleportGateNameRoot", text);
			goto IL_0232;
			IL_02E0:
			if (!flag)
			{
				nameRootMap[text] = new List<List<string>> { ring };
			}
		}

		// Token: 0x040030AA RID: 12458
		public string World;

		// Token: 0x040030AB RID: 12459
		public string StaticGateZones;

		// Token: 0x040030AC RID: 12460
		public static readonly int TELEPORT_GATE_RUINS_SURFACE_PERMILLAGE_CHANCE = 10;

		// Token: 0x040030AD RID: 12461
		public static readonly int TELEPORT_GATE_RUINS_DEEP_PERMILLAGE_CHANCE = 1;

		// Token: 0x040030AE RID: 12462
		public static readonly int TELEPORT_GATE_RUINS_DEPTH = 30;

		// Token: 0x040030AF RID: 12463
		public static readonly int TELEPORT_GATE_BAROQUE_RUINS_SURFACE_PERMILLAGE_CHANCE = 10;

		// Token: 0x040030B0 RID: 12464
		public static readonly int TELEPORT_GATE_BAROQUE_RUINS_DEEP_PERMILLAGE_CHANCE = 1;

		// Token: 0x040030B1 RID: 12465
		public static readonly int TELEPORT_GATE_BAROQUE_RUINS_DEPTH = 30;

		// Token: 0x040030B2 RID: 12466
		public static readonly int TELEPORT_GATE_SECRET_RUIN_PERMILLAGE_CHANCE = 10;

		// Token: 0x040030B3 RID: 12467
		public static readonly int TELEPORT_GATE_HISTORIC_SITE_SURFACE_PERMILLAGE_CHANCE = 20;

		// Token: 0x040030B4 RID: 12468
		public static readonly int TELEPORT_GATE_HISTORIC_SITE_DEEP_PERMILLAGE_CHANCE = 20;

		// Token: 0x040030B5 RID: 12469
		public static readonly int TELEPORT_GATE_HISTORIC_SITE_CHECK_DEPTH = 30;

		// Token: 0x040030B6 RID: 12470
		public static readonly int TELEPORT_GATE_RANDOM_PROPORTION = 10;

		// Token: 0x040030B7 RID: 12471
		public static readonly int TELEPORT_GATE_RANDOM_SURFACE_TARGET_PERCENTAGE_CHANCE = 40;

		// Token: 0x040030B8 RID: 12472
		public static readonly int TELEPORT_GATE_RANDOM_DEEP_TARGET_DEPTH = 40;

		// Token: 0x040030B9 RID: 12473
		public static readonly bool TELEPORT_GATE_DEBUG = false;
	}
}
