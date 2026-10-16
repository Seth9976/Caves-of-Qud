using System;
using Genkit.SimplexNoise;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders;

namespace XRL.World.Biomes
{
	// Token: 0x02000A29 RID: 2601
	[HasGameBasedStaticCache]
	public class SlimyBiome : IBiome
	{
		// Token: 0x06006D76 RID: 28022 RVA: 0x0024BB50 File Offset: 0x00249D50
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
			if (SlimyBiome.BiomeLevels == null)
			{
				SlimyBiome.BiomeLevels = new byte[num6, num7, num8];
				LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("SlimyNoise").ToString());
				float num12 = 0.66f;
				float num13 = 0f;
				float num14 = 1f;
				float[,,] array2 = layeredNoise.Generate3D(num6, num7, num8);
				for (int i = 0; i < num8; i++)
				{
					for (int j = 0; j < num7; j++)
					{
						for (int k = 0; k < num6; k++)
						{
							float num15 = array2[k, j, i];
							if (num15 < num12)
							{
								num15 = 0f;
							}
							else
							{
								num15 = (num15 - num12) * (1f / (1f - num12));
							}
							array2[k, j, i] = num15;
							if (num15 > num13)
							{
								num13 = num15;
							}
							if (num15 < num14)
							{
								num14 = num15;
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
							int num16 = 4;
							int num17 = (int)((array2[n, m, l] - num14) / ((num13 - num14) / (float)num16));
							if (num17 < 0)
							{
								num17 = 0;
							}
							if (num17 > num16 - 1)
							{
								num17 = num16 - 1;
							}
							SlimyBiome.BiomeLevels[n, m, l] = (byte)num17;
						}
					}
				}
			}
			return (int)SlimyBiome.BiomeLevels[num9, num10, num11];
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x0024BDCC File Offset: 0x00249FCC
		public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			return IBiome.MutateZoneNameWith(Name, this.GetBiomeValue(ZoneID), "slime patch", "slimy", "slime field", "slime-drenched", "slime bog", null);
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x0024BDF8 File Offset: 0x00249FF8
		public override void MutateZone(Zone Z)
		{
			int biomeValue = this.GetBiomeValue(Z.ZoneID);
			if (Z.GetZoneProperty("relaxedbiomes", "") != "true")
			{
				if (biomeValue == 1)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Slimy1", "minimum", false);
				}
				if (biomeValue == 2)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Slimy2", "minimum", false);
				}
				if (biomeValue == 3)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Slimy3", "minimum", false);
				}
			}
			if (biomeValue >= 1)
			{
				Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_slimy");
			}
			new LiquidPools().BuildZone(Z, "SlimePuddle", 3 * this.GetBiomeValue(Z.ZoneID) - 4, "0", "Slimy Plants");
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x0024BEC0 File Offset: 0x0024A0C0
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
			if (biomeValue == 2 && Stat.SeededRandom(num.ToString(), 1, 100) <= 50)
			{
				SlimewalkerTemplate.Apply(GO);
			}
			if (biomeValue == 3 && Stat.SeededRandom(num.ToString(), 1, 100) <= 50)
			{
				SlimespitterTemplate.Apply(GO);
			}
			return GO;
		}

		// Token: 0x04003201 RID: 12801
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static byte[,,] BiomeLevels;
	}
}
