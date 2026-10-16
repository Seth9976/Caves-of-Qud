using System;
using Genkit.SimplexNoise;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;

namespace XRL.World.Biomes
{
	// Token: 0x02000A22 RID: 2594
	[HasGameBasedStaticCache]
	public class FungalBiome : IBiome
	{
		// Token: 0x06006D63 RID: 28003 RVA: 0x0024ACC8 File Offset: 0x00248EC8
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
			if (((string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(ZoneID, "NoBiomes", false, "")).EqualsNoCase("Yes"))
			{
				return 0;
			}
			int num6 = 240;
			int num7 = 75;
			int num8 = 10;
			int num9 = num * 3 + num3;
			int num10 = num2 * 3 + num4;
			int num11 = num5 % num8;
			if (FungalBiome.BiomeLevels == null)
			{
				FungalBiome.BiomeLevels = new byte[num6, num7, num8];
				LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("FungalNoise").ToString());
				float num12 = 0.72f;
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
							FungalBiome.BiomeLevels[n, m, l] = (byte)num17;
						}
					}
				}
			}
			return (int)FungalBiome.BiomeLevels[num9, num10, num11];
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x0024AF44 File Offset: 0x00249144
		public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			return IBiome.MutateZoneNameWith(Name, this.GetBiomeValue(ZoneID), "fungus patch", "fungus grove", "fungus forest");
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x0024AF64 File Offset: 0x00249164
		public override void MutateZone(Zone Z)
		{
			int biomeValue = this.GetBiomeValue(Z.ZoneID);
			FungalJungle.FungalUpAZone(Z, biomeValue, true);
			if (biomeValue >= 1)
			{
				Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_mushroom");
			}
			if (biomeValue == 1)
			{
				new PopTableZoneBuilder().BuildZone(Z, "FungalBiome1", "minimum", true);
			}
			if (biomeValue == 2)
			{
				new PopTableZoneBuilder().BuildZone(Z, "FungalBiome2", "minimum", true);
			}
			if (biomeValue == 3)
			{
				new PopTableZoneBuilder().BuildZone(Z, "FungalBiome3", "minimum", true);
			}
		}

		// Token: 0x06006D66 RID: 28006 RVA: 0x0024AFEC File Offset: 0x002491EC
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
			if (biomeValue == 1 && Stat.SeededRandom(num.ToString(), 1, 100) <= 25)
			{
				FungusFriendTemplate.Apply(GO, SporePuffer.InfectionObjectList.GetRandomElement<string>(null), XRLCore.Core.Game.ZoneManager.GetZone(ZoneID));
			}
			if (biomeValue == 2)
			{
				if (Stat.SeededRandom(num.ToString(), 1, 100) <= 25)
				{
					FungusFriendTemplate.Apply(GO, SporePuffer.InfectionObjectList.GetRandomElement<string>(null), XRLCore.Core.Game.ZoneManager.GetZone(ZoneID));
				}
				else if (Stat.SeededRandom(num.ToString(), 1, 100) <= 50)
				{
					FungusColonizedTemplate.Apply(GO, SporePuffer.InfectionObjectList.GetRandomElement<string>(null), XRLCore.Core.Game.ZoneManager.GetZone(ZoneID));
				}
			}
			if (biomeValue == 3 && Stat.SeededRandom(num.ToString(), 1, 100) <= 70)
			{
				FungusColonizedTemplate.Apply(GO, SporePuffer.InfectionObjectList.GetRandomElement<string>(null), XRLCore.Core.Game.ZoneManager.GetZone(ZoneID));
			}
			return GO;
		}

		// Token: 0x040031FF RID: 12799
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static byte[,,] BiomeLevels;
	}
}
