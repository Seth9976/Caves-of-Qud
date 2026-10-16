using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Messages;
using XRL.UI;
using XRL.Wish;

namespace XRL.World.Biomes
{
	// Token: 0x02000A1F RID: 2591
	[HasWishCommand]
	[Serializable]
	public static class BiomeManager
	{
		// Token: 0x06006D5A RID: 27994 RVA: 0x0024A4E4 File Offset: 0x002486E4
		public static List<IBiome> GetTopBiomes(string ZoneID, int HowMany = 2)
		{
			BiomeManager.TopBiomes.Clear();
			foreach (KeyValuePair<string, IBiome> keyValuePair in BiomeManager.Biomes)
			{
				if (keyValuePair.Value.GetBiomeValue(ZoneID) > 0)
				{
					BiomeManager.TopBiomes.Add(keyValuePair.Value);
				}
			}
			if (BiomeManager.TopBiomes.Count > 1)
			{
				BiomeManager.TopBiomes.Sort((IBiome a, IBiome b) => b.GetBiomeValue(ZoneID).CompareTo(a.GetBiomeValue(ZoneID)));
				for (int i = BiomeManager.TopBiomes.Count; i > HowMany; i--)
				{
					BiomeManager.TopBiomes.RemoveAt(i - 1);
				}
			}
			return BiomeManager.TopBiomes;
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x0024A5B8 File Offset: 0x002487B8
		public static int BiomeValue(string Biome, string ZoneID)
		{
			if (!BiomeManager.Biomes.ContainsKey(Biome))
			{
				return 0;
			}
			if (!ZoneID.StartsWith("JoppaWorld"))
			{
				return 0;
			}
			return BiomeManager.Biomes[Biome].GetBiomeValue(ZoneID);
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x0024A5EC File Offset: 0x002487EC
		public static string MutateZoneName(string Input, string ZoneID)
		{
			if (!ZoneID.StartsWith("JoppaWorld"))
			{
				return Input;
			}
			List<IBiome> topBiomes = BiomeManager.GetTopBiomes(ZoneID, 2);
			if (topBiomes.Count == 0)
			{
				return Input;
			}
			if (!The.ZoneManager.CheckBiomesAllowed(ZoneID))
			{
				return Input;
			}
			for (int i = topBiomes.Count - 1; i >= 0; i--)
			{
				Input = topBiomes[i].MutateZoneName(Input, ZoneID, i);
			}
			return Input;
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x0024A650 File Offset: 0x00248850
		public static void MutateZone(Zone Z)
		{
			if (!Z.ZoneID.StartsWith("JoppaWorld"))
			{
				return;
			}
			if (!The.ZoneManager.CheckBiomesAllowed(Z))
			{
				return;
			}
			List<IBiome> topBiomes = BiomeManager.GetTopBiomes(Z.ZoneID, 2);
			if (topBiomes.Count == 0)
			{
				return;
			}
			foreach (GameObject gameObject in Z.YieldObjects())
			{
				if (gameObject.Physics != null)
				{
					for (int i = 0; i < topBiomes.Count; i++)
					{
						topBiomes[i].MutateGameObject(gameObject, Z.ZoneID);
					}
				}
			}
			for (int j = 0; j < topBiomes.Count; j++)
			{
				topBiomes[j].MutateZone(Z);
			}
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x0024A720 File Offset: 0x00248920
		[WishCommand("surfacebiomes", null)]
		public static void DisplaySurfaceDistribution(string Value)
		{
			IBiome biome;
			if (!BiomeManager.Biomes.TryGetValue(Value, out biome))
			{
				Popup.Show("No biome by name '" + Value + "' found.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return;
			}
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			int[,] array = new int[zone.Width, zone.Height];
			int num = 1;
			for (int i = 0; i < zone.Height * Definitions.Height; i++)
			{
				for (int j = 0; j < zone.Width * Definitions.Width; j++)
				{
					int num2 = j / Definitions.Width;
					int num3 = i / Definitions.Height;
					if (biome.GetBiomeValue(ZoneID.Assemble(zone.ZoneWorld, num2, num3, j - num2 * Definitions.Width, i - num3 * Definitions.Height, 10)) != 0)
					{
						array[num2, num3]++;
						num++;
					}
				}
			}
			int num4 = zone.Width * Definitions.Width * zone.Height * Definitions.Height;
			float num5 = (float)num * 1f / (float)num4 * 100f;
			MessageQueue.AddPlayerMessage(string.Format("{0} biome: {1}/{2}, {3}%.", new object[]
			{
				Value,
				num,
				num4,
				(int)num5
			}), "W", true);
			Keys keys = Keys.None;
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			for (;;)
			{
				scrapBuffer.Clear();
				for (int k = 0; k < zone.Height; k++)
				{
					for (int l = 0; l < zone.Width; l++)
					{
						if (array[l, k] != 0)
						{
							scrapBuffer.WriteAt(l, k, (array[l, k] > 9) ? "+" : array[l, k].ToString(), true);
						}
					}
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick")
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					int x = Keyboard.CurrentMouseEvent.x;
					int y = Keyboard.CurrentMouseEvent.y;
					for (int m = 0; m < Definitions.Height; m++)
					{
						for (int n = 0; n < Definitions.Width; n++)
						{
							string zoneDisplayName = The.ZoneManager.GetZoneDisplayName(ZoneID.Assemble(zone.ZoneWorld, x, y, n, m, 10), false, false, true, true);
							int num6;
							dictionary.TryGetValue(zoneDisplayName, out num6);
							dictionary[zoneDisplayName] = num6 + 1;
						}
					}
					int num7 = 0;
					using (IEnumerator<KeyValuePair<string, int>> enumerator = dictionary.OrderBy<KeyValuePair<string, int>, int>((KeyValuePair<string, int> p) => p.Key.Length).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, int> keyValuePair = enumerator.Current;
							scrapBuffer.WriteAt(0, num7++, string.Concat(new string[]
							{
								"{{W|",
								keyValuePair.Key,
								": ",
								keyValuePair.Value.ToString(),
								"}}"
							}), true);
						}
						goto IL_0321;
					}
					goto IL_0315;
				}
				goto IL_0315;
				IL_0321:
				scrapBuffer.Draw();
				if ((keys = Keyboard.getvk(false, false, true)) == Keys.Escape)
				{
					break;
				}
				continue;
				IL_0315:
				if (keys != Keys.Escape && keys != Keys.Enter)
				{
					goto IL_0321;
				}
				break;
			}
		}

		// Token: 0x040031FD RID: 12797
		public static Dictionary<string, IBiome> Biomes = new Dictionary<string, IBiome>
		{
			{
				"Slimy",
				new SlimyBiome()
			},
			{
				"Tarry",
				new TarryBiome()
			},
			{
				"Rusty",
				new RustyBiome()
			},
			{
				"Fungal",
				new FungalBiome()
			},
			{
				"Psychic",
				new PsychicBiome()
			}
		};

		// Token: 0x040031FE RID: 12798
		private static List<IBiome> TopBiomes = new List<IBiome>(2);
	}
}
