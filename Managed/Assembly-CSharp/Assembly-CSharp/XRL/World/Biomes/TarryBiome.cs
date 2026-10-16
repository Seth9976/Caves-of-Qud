using System;
using System.Collections.Generic;
using System.Linq;
using Genkit.SimplexNoise;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders;

namespace XRL.World.Biomes
{
	// Token: 0x02000A2C RID: 2604
	[HasGameBasedStaticCache]
	public class TarryBiome : IBiome
	{
		// Token: 0x06006D7F RID: 28031 RVA: 0x0024C0DC File Offset: 0x0024A2DC
		public override int GetBiomeValue(string ZoneID)
		{
			if (!ZoneID.Contains("."))
			{
				return 0;
			}
			string[] array = ZoneID.Split('.', StringSplitOptions.None);
			int num = Convert.ToInt32(array[1]);
			int num2 = Convert.ToInt32(array[2]);
			int num3 = Convert.ToInt32(array[3]);
			int num4 = Convert.ToInt32(array[4]);
			int num5 = Convert.ToInt32(array[5]);
			if (num5 < 10)
			{
				return 0;
			}
			string objectTypeForZone = ZoneManager.GetObjectTypeForZone(num, num2, "JoppaWorld");
			if (GameObjectFactory.Factory.Blueprints.ContainsKey(objectTypeForZone) && GameObjectFactory.Factory.Blueprints[objectTypeForZone].Tags.ContainsKey("NoBiomes"))
			{
				return 0;
			}
			if ((string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(ZoneID, "NoBiomes", false, "") == "Yes")
			{
				return 0;
			}
			int num6 = 240;
			int num7 = 75;
			int num8 = 10;
			int num9 = num * 3 + num3;
			int num10 = num2 * 3 + num4;
			int num11 = num5 % num8;
			if (TarryBiome.BiomeLevels == null)
			{
				TarryBiome.BiomeLevels = new byte[num6, num7, num8];
				LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("TarryNoise").ToString());
				float num12 = 0.66f;
				float num13 = 0f;
				float num14 = 1f;
				int num15 = 4;
				float[,,] array2 = layeredNoise.Generate3D(num6, num7, num8);
				for (int i = 0; i < num8; i++)
				{
					for (int j = 0; j < num7; j++)
					{
						for (int k = 0; k < num6; k++)
						{
							float num16 = array2[k, j, i];
							if (num16 < num12)
							{
								num16 = 0f;
							}
							else
							{
								num16 = (num16 - num12) * (1f / (1f - num12));
							}
							array2[k, j, i] = num16;
							if (num16 > num13)
							{
								num13 = num16;
							}
							if (num16 < num14)
							{
								num14 = num16;
							}
						}
					}
				}
				for (int l = 0; l < num8; l++)
				{
					for (int m = 0; m < num7; m++)
					{
						for (int n = 0; n < num6; n++)
						{
							int num17 = (int)((array2[n, m, l] - num14) / ((num13 - num14) / (float)num15));
							if (num17 < 0)
							{
								num17 = 0;
							}
							if (num17 > num15 - 1)
							{
								num17 = num15 - 1;
							}
							TarryBiome.BiomeLevels[n, m, l] = (byte)num17;
						}
					}
				}
			}
			return (int)TarryBiome.BiomeLevels[num9, num10, num11];
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x0024C358 File Offset: 0x0024A558
		public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			return IBiome.MutateZoneNameWith(Name, this.GetBiomeValue(ZoneID), "tar patch", "tarry", "tar pools", null, "flaming tar pits", null);
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x0024C380 File Offset: 0x0024A580
		public override void MutateZone(Zone Z)
		{
			int biomeValue = this.GetBiomeValue(Z.ZoneID);
			if (biomeValue >= 1)
			{
				Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_tarry");
			}
			if (biomeValue == 1)
			{
				new PopTableZoneBuilder().BuildZone(Z, "Tarry1", "minimum", false);
			}
			if (biomeValue == 2)
			{
				new PopTableZoneBuilder().BuildZone(Z, "Tarry2", "minimum", false);
			}
			if (biomeValue == 3)
			{
				if (!Z.GetZoneProperty("relaxedbiomes", "").EqualsNoCase("true"))
				{
					new PopTableZoneBuilder().BuildZone(Z, "Tarry3", "minimum", false);
				}
				else
				{
					new PopTableZoneBuilder().BuildZone(Z, "Tarry2", "minimum", false);
				}
				new LiquidPools().BuildZone(Z, "AsphaltPuddle", 3 * this.GetBiomeValue(Z.ZoneID) - 4, "2-3", null);
				using (IEnumerator<Cell> enumerator = Z.GetCellsWithObject("AsphaltPuddle").Take<Cell>(Stat.Random(3, 18)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Cell cell = enumerator.Current;
						if (cell != null)
						{
							cell.AddObject("Firestarter", null, null, null, null);
						}
					}
					return;
				}
			}
			new LiquidPools().BuildZone(Z, "AsphaltPuddle", 3 * this.GetBiomeValue(Z.ZoneID) - 4, "0", null);
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x0024C4E4 File Offset: 0x0024A6E4
		public override GameObject MutateGameObject(GameObject GO, string ZoneID)
		{
			long num;
			if (GO.HasProperty("Batch"))
			{
				num = GO.GetLongProperty("Batch", 0L);
			}
			else
			{
				num = (long)Stat.Random(0, 2147483646);
			}
			int biomeValue = this.GetBiomeValue(ZoneID);
			if (biomeValue == 2 && Stat.SeededRandom(num.ToString(), 1, 100) <= 25)
			{
				KindlethumbedTemplate.Apply(GO);
			}
			if (biomeValue == 3 && Stat.SeededRandom(num.ToString(), 1, 100) <= 50)
			{
				FirethumbedTemplate.Apply(GO);
			}
			return GO;
		}

		// Token: 0x04003202 RID: 12802
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static byte[,,] BiomeLevels;
	}
}
