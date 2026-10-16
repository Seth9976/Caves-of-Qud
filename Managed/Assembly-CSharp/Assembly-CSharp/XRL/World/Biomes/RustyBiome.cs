using System;
using System.Collections.Generic;
using Genkit.SimplexNoise;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.Biomes
{
	// Token: 0x02000A26 RID: 2598
	[HasGameBasedStaticCache]
	public class RustyBiome : IBiome
	{
		// Token: 0x06006D6D RID: 28013 RVA: 0x0024B2A8 File Offset: 0x002494A8
		public override int GetBiomeValue(string ZoneID)
		{
			int num;
			try
			{
				if (!ZoneID.Contains("."))
				{
					num = 0;
				}
				else
				{
					string[] array = ZoneID.Split('.', StringSplitOptions.None);
					int num2 = Convert.ToInt32(array[1]);
					int num3 = Convert.ToInt32(array[2]);
					int num4 = Convert.ToInt32(array[3]);
					int num5 = Convert.ToInt32(array[4]);
					int num6 = Convert.ToInt32(array[5]);
					if (num6 < 10)
					{
						num = 0;
					}
					else
					{
						string objectTypeForZone = ZoneManager.GetObjectTypeForZone(num2, num3, "JoppaWorld");
						if (GameObjectFactory.Factory.Blueprints.ContainsKey(objectTypeForZone) && GameObjectFactory.Factory.Blueprints[objectTypeForZone].Tags.ContainsKey("NoBiomes"))
						{
							num = 0;
						}
						else if ((string)XRLCore.Core.Game.ZoneManager.GetZoneProperty(ZoneID, "NoBiomes", false, "") == "Yes")
						{
							num = 0;
						}
						else
						{
							int num7 = 240;
							int num8 = 75;
							int num9 = 10;
							int num10 = num2 * 3 + num4;
							int num11 = num3 * 3 + num5;
							int num12 = num6 % num9;
							if (RustyBiome.BiomeLevels == null)
							{
								RustyBiome.BiomeLevels = new byte[num7, num8, num9];
								LayeredNoise layeredNoise = LayeredNoise.CreateLinearOctiveLayers(3, 1.33f, 0.12f, XRLCore.Core.Game.GetWorldSeed("RustyNoise").ToString());
								float num13 = 0.66f;
								float num14 = 0f;
								float num15 = 1f;
								float[,,] array2 = layeredNoise.Generate3D(num7, num8, num9);
								for (int i = 0; i < num9; i++)
								{
									for (int j = 0; j < num8; j++)
									{
										for (int k = 0; k < num7; k++)
										{
											float num16 = array2[k, j, i];
											if (num16 < num13)
											{
												num16 = 0f;
											}
											else
											{
												num16 = (num16 - num13) * (1f / (1f - num13));
											}
											array2[k, j, i] = num16;
											if (num16 > num14)
											{
												num14 = num16;
											}
											if (num16 < num15)
											{
												num15 = num16;
											}
										}
									}
								}
								for (int l = 0; l < num9; l++)
								{
									for (int m = 0; m < num8; m++)
									{
										for (int n = 0; n < num7; n++)
										{
											int num17 = 4;
											int num18 = (int)((array2[n, m, l] - num15) / ((num14 - num15) / (float)num17));
											if (num18 < 0)
											{
												num18 = 0;
											}
											if (num18 > num17 - 1)
											{
												num18 = num17 - 1;
											}
											RustyBiome.BiomeLevels[n, m, l] = (byte)num18;
										}
									}
								}
							}
							num = (int)RustyBiome.BiomeLevels[num10, num11, num12];
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SlimeBiomeValue", ex, "game_exception");
				num = 0;
			}
			return num;
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x0024B578 File Offset: 0x00249778
		public override string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			return IBiome.MutateZoneNameWith(Name, this.GetBiomeValue(ZoneID), "rust patch", "rusty", "rust field", "rust-shrouded", "rust bog", null);
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x0024B5A4 File Offset: 0x002497A4
		public override void MutateZone(Zone Z)
		{
			int biomeValue = this.GetBiomeValue(Z.ZoneID);
			if (Z.GetZoneProperty("relaxedbiomes", "") != "true")
			{
				if (biomeValue == 1)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Rusty1", "minimum", false);
				}
				if (biomeValue == 2)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Rusty2", "minimum", false);
				}
				if (biomeValue == 3)
				{
					new PopTableZoneBuilder().BuildZone(Z, "Rusty3", "minimum", false);
				}
			}
			if (biomeValue >= 1)
			{
				Z.SetZoneProperty("ambient_bed_2", "Sounds/Ambiences/amb_biome_rusty");
			}
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, biomeValue * 3, 20, 20, 4, 3, 0, 1, list);
			int num = -1;
			for (int i = 0; i < noiseMap.nAreas; i++)
			{
				if (noiseMap.AreaNodes[i].Count > num)
				{
					num = noiseMap.AreaNodes[i].Count;
				}
			}
			for (int j = 0; j < Z.Height; j++)
			{
				for (int k = 0; k < Z.Width; k++)
				{
					if ((double)noiseMap.Noise[k, j] >= 0.8 && Z.GetCell(k, j).HasWall())
					{
						if (Z.GetCell(k, j).AnyAdjacentCell((Cell c) => !c.HasWall(), true) && Stat.Random(1, 100) <= biomeValue * 5)
						{
							if (Z.GetZoneProperty("faction", null) != null)
							{
								GameObject gameObject = GameObjectFactory.Factory.CreateObject("Qudzu");
								gameObject.Brain.Factions = "";
								gameObject.Brain.Allegiance.Clear();
								gameObject.Brain.Allegiance.Add(Z.GetZoneProperty("faction", ""), 100);
								gameObject.Render.DisplayName = "domesticated " + gameObject.Render.DisplayName;
								Z.GetCell(k, j).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
							else
							{
								Z.GetCell(k, j).AddObject("Qudzu", null, null, null, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x0024B880 File Offset: 0x00249A80
		public override GameObject MutateGameObject(GameObject GO, string ZoneID)
		{
			Zone zone = XRLCore.Core.Game.ZoneManager.GetZone(ZoneID);
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
			if (biomeValue == 1 && zone.GetZoneProperty("relaxedbiomes", "") != "true")
			{
				if (Stat.SeededRandom(num.ToString(), 1, 100) <= 50)
				{
					RustedTemplate.Apply(GO);
				}
				if (Stat.SeededRandom(num.ToString(), 1, 100) <= 25)
				{
					RustedInventoryTemplate.Apply(GO);
				}
			}
			if (biomeValue == 2)
			{
				if (zone.GetZoneProperty("relaxedbiomes", "") != "true")
				{
					RustedTemplate.Apply(GO);
					if (Stat.SeededRandom(num.ToString(), 1, 100) <= 75)
					{
						RustedInventoryTemplate.Apply(GO);
					}
				}
				if (Stat.SeededRandom(num.ToString(), 1, 100) <= 25)
				{
					QudzuSymbioteTemplate.Apply(GO, zone);
				}
			}
			if (biomeValue == 3)
			{
				if (zone.GetZoneProperty("relaxedbiomes", "") != "true")
				{
					RustedTemplate.Apply(GO);
					if (Stat.SeededRandom(num.ToString(), 1, 100) <= 100)
					{
						RustedInventoryTemplate.Apply(GO);
					}
				}
				if (Stat.SeededRandom(num.ToString(), 1, 100) <= 60)
				{
					QudzuSymbioteTemplate.Apply(GO, zone);
				}
			}
			return GO;
		}

		// Token: 0x04003200 RID: 12800
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static byte[,,] BiomeLevels;
	}
}
