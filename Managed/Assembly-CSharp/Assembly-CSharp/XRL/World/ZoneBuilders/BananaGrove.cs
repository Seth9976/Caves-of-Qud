using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A3F RID: 2623
	public class BananaGrove : ZoneBuilderSandbox
	{
		// Token: 0x06006DC2 RID: 28098 RVA: 0x0024F600 File Offset: 0x0024D800
		public static void Save(SerializationWriter Writer)
		{
			if (BananaGrove.BananaGroveNoise == null)
			{
				Writer.Write(0);
				return;
			}
			Writer.Write(1);
			for (int i = 0; i < 1200; i++)
			{
				for (int j = 0; j < 375; j++)
				{
					Writer.Write(BananaGrove.BananaGroveNoise[i, j]);
				}
			}
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x0024F658 File Offset: 0x0024D858
		public static void Load(SerializationReader Reader)
		{
			if (Reader.ReadInt32() == 0)
			{
				BananaGrove.BananaGroveNoise = null;
				return;
			}
			BananaGrove.BananaGroveNoise = new double[1200, 375];
			for (int i = 0; i < 1200; i++)
			{
				for (int j = 0; j < 375; j++)
				{
					BananaGrove.BananaGroveNoise[i, j] = Reader.ReadDouble();
				}
			}
		}

		// Token: 0x06006DC4 RID: 28100 RVA: 0x0024F6BC File Offset: 0x0024D8BC
		public bool BuildZone(Zone Z)
		{
			if (BananaGrove.BananaGroveNoise == null)
			{
				BananaGrove.NoiseFunctions = new List<PerlinNoise2D>();
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(1, 3f, Stat.Rand));
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(4, 2f, Stat.Rand));
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(8, 1f, Stat.Rand));
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(16, 2f, Stat.Rand));
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(32, 3f, Stat.Rand));
				BananaGrove.NoiseFunctions.Add(new PerlinNoise2D(128, 4f, Stat.Rand));
				BananaGrove.BananaGroveNoise = PerlinNoise2D.sumNoiseFunctions(1200, 375, 0, 0, BananaGrove.NoiseFunctions);
			}
			int num = Z.wX * 240 + Z.X * 80;
			int num2 = Z.wY * 75 + Z.Y * 25;
			num %= 1200;
			num2 %= 375;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (Z.GetCell(j, i).IsPassable(null, true))
					{
						double num3 = BananaGrove.BananaGroveNoise[j + num, i + num2];
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						if (num3 >= 0.8)
						{
							num4 = 20;
							num5 = 80;
							num6 = 5;
						}
						else if (num3 >= 0.7)
						{
							num4 = 3;
							num5 = 60;
							num6 = 3;
						}
						else if (num3 >= 0.5)
						{
							num4 = 1;
							num5 = 1;
							num6 = 30;
						}
						if (num4.in100())
						{
							Z.GetCell(j, i).AddObject("Starapple Tree", null, null, null, null);
						}
						else if (num5.in100())
						{
							if (5.in100())
							{
								Z.GetCell(j, i).AddObject("Red Death Dacca", null, null, null, null);
							}
							else
							{
								Z.GetCell(j, i).AddObject("Banana Tree", null, null, null, null);
							}
						}
						else if (num6.in100())
						{
							Z.GetCell(j, i).AddObject("Dicalyptus Tree", null, null, null, null);
						}
					}
				}
			}
			Z.GetCell(0, 0).AddObject("Grassy", null, null, null, null);
			if (Z.GetTerrainNameFromDirection(".") == "TerrainTheSpindle" && Z.Z >= 12 && Z.Z <= 15)
			{
				int num7 = 10;
				int num8 = 10;
				Box box = new Box(20 - num7 / 2, 12 - num8 / 2, 20 - num7 / 2 + num7, 12 - num8 / 2 + num8);
				Box box2 = new Box(60 - num7 / 2, 12 - num8 / 2, 60 - num7 / 2 + num7, 12 - num8 / 2 + num8);
				Z.ClearBox(box, null);
				Z.FillBox(box, "EbonFulcrete", false);
				Z.ClearBox(box2, null);
				Z.FillBox(box2, "EbonFulcrete", false);
				Z.ProcessHollowBox(new Box(0, 0, 79, 24), delegate(Cell c)
				{
					if (c.HasWall())
					{
						c.Clear(null, false, false, null);
						c.AddObject("EbonFulcrete", null, null, null, null);
					}
				});
				ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
			}
			List<Location2D> ruinWalkerStarts = new List<Location2D>();
			int CHANCE_RUIN_WALKER = 1;
			int MAX_RUIN_WALKER = 100;
			Action<string, Cell> action = delegate(string o, Cell c)
			{
				if (Stat.Random(1, MAX_RUIN_WALKER) <= CHANCE_RUIN_WALKER && !c.HasObject("EbonFulcrete"))
				{
					ruinWalkerStarts.Add(c.Location);
				}
			};
			if (Z.Y == 2 && Z.GetTerrainNameFromDirection("S") == "TerrainTheSpindle")
			{
				if (Z.X == 0)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NNW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y < 23)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.X == 1)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_N.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y < 23)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.X == 2)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NNE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y < 23)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
			}
			if (Z.Y == 0 && Z.GetTerrainNameFromDirection("N") == "TerrainTheSpindle")
			{
				if (Z.X == 0)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SSW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, null);
				}
				if (Z.X == 1)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_S.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, null);
				}
				if (Z.X == 2)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SSE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.Y > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, null);
				}
				if (Z.X == 2 && Z.Z == 10)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombRuinedMuralCistern.rpm", Z.GetCell(31, 7), 0, null, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
					}, null, null, null, null, null);
					Z.GetCell(0, 0).AddObject("SultanMuralControllerRandom2AlwaysRuined", null, null, null, null);
				}
				if (Z.Z == 11)
				{
					List<Location2D> list = new List<Location2D>();
					List<GameObject> list2 = new List<GameObject>();
					if (Z.X == 1)
					{
						int num9 = 0;
						int num10 = 17;
						list.AddRange(new Box(num9, 2, num10, 2).contents());
					}
					else if (Z.X == 0)
					{
						int num9 = 44;
						int num10 = 79;
						list.AddRange(new Box(num9, 2, num10, 2).contents());
						if (Z.Z == 11)
						{
							list.AddRange(new Box(num9, 0, num9, 2).contents());
							list.AddRange(new Box(num9 - 1, 0, num9 - 1, 1).contents());
						}
						if (Z.Z == 11)
						{
							Z.GetCell(num9 - 1, 1).AddObject("MediumBoulder", null, null, null, null);
						}
					}
					Z.Clear(list, null);
					foreach (Location2D location2D in list)
					{
						Cell cell = Z.GetCell(location2D);
						foreach (Cell cell2 in cell.GetAdjacentCells(true))
						{
							foreach (GameObject gameObject in cell2.GetWalls())
							{
								if (!gameObject.HasPropertyOrTag("HasGraffiti") && !list2.Contains(gameObject))
								{
									list2.Add(gameObject);
								}
							}
						}
						foreach (PopulationResult populationResult in PopulationManager.Generate("RobbersCutContents", null, null, null, null, null))
						{
							for (int k = 0; k < populationResult.Number; k++)
							{
								cell.AddObject(populationResult.Blueprint, null, null, null, null);
							}
						}
					}
					int num11 = 30;
					foreach (GameObject gameObject2 in list2)
					{
						if (Stat.Random(1, 100) <= num11 && !gameObject2.HasPropertyOrTag("HasGraffiti") && !gameObject2.HasPart<Graffitied>())
						{
							Graffitied graffitied = new Graffitied();
							gameObject2.AddPart<Graffitied>(graffitied, true, false);
							graffitied.Graffiti(gameObject2);
						}
					}
				}
			}
			if (Z.X == 0 && Z.GetTerrainNameFromDirection("W") == "TerrainTheSpindle")
			{
				if (Z.Y == 0)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_ENE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.Y == 1)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_E.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.Y == 2)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_ESE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X > 1)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
			}
			if (Z.X == 2 && Z.GetTerrainNameFromDirection("E") == "TerrainTheSpindle")
			{
				if (Z.Y == 0)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_WNW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X < 78)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.Y == 1)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_W.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X < 78)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
				if (Z.Y == 2)
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_WSW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, null, null, null, delegate(string o, Cell c)
					{
						if (Z.Z < 14)
						{
							return o;
						}
						if (c.X < 78)
						{
							return null;
						}
						return "EbonFulcrete";
					}, delegate(string o, Cell c)
					{
						c.Clear(null, false, false, null);
					}, action);
				}
			}
			if (Z.X == 0 && Z.Y == 0 && Z.GetTerrainNameFromDirection("NW") == "TerrainTheSpindle")
			{
				ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.NW, null, null, null, null, delegate(string o, Cell c)
				{
					if (Z.Z < 14)
					{
						return o;
					}
					if (c.X > 1 && c.Y > 1 && c.X < 78 && c.Y < 23)
					{
						return null;
					}
					return "EbonFulcrete";
				}, delegate(string o, Cell c)
				{
					c.Clear(null, false, false, null);
				}, action);
			}
			if (Z.X == 2 && Z.Y == 0 && Z.GetTerrainNameFromDirection("NE") == "TerrainTheSpindle")
			{
				ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.NE, null, null, null, null, delegate(string o, Cell c)
				{
					if (Z.Z < 14)
					{
						return o;
					}
					if (c.X > 1 && c.Y > 1 && c.X < 78 && c.Y < 23)
					{
						return null;
					}
					return "EbonFulcrete";
				}, delegate(string o, Cell c)
				{
					c.Clear(null, false, false, null);
				}, action);
			}
			if (Z.X == 0 && Z.Y == 2 && Z.GetTerrainNameFromDirection("SW") == "TerrainTheSpindle")
			{
				ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.SW, null, null, null, null, delegate(string o, Cell c)
				{
					if (Z.Z < 14)
					{
						return o;
					}
					if (c.X > 1 && c.Y > 1 && c.X < 78 && c.Y < 23)
					{
						return null;
					}
					return "EbonFulcrete";
				}, delegate(string o, Cell c)
				{
					c.Clear(null, false, false, null);
				}, action);
			}
			if (Z.X == 2 && Z.Y == 2 && Z.GetTerrainNameFromDirection("SE") == "TerrainTheSpindle")
			{
				ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.SE, null, null, null, null, delegate(string o, Cell c)
				{
					if (Z.Z < 14)
					{
						return o;
					}
					if (c.X > 1 && c.Y > 1 && c.X < 78 && c.Y < 23)
					{
						return null;
					}
					return "EbonFulcrete";
				}, delegate(string o, Cell c)
				{
					c.Clear(null, false, false, null);
				}, action);
			}
			using (List<Location2D>.Enumerator enumerator = ruinWalkerStarts.GetEnumerator())
			{
				IL_0F63:
				while (enumerator.MoveNext())
				{
					Location2D location2D2 = enumerator.Current;
					int num12 = 100;
					while ((location2D2.X < 6 || location2D2.X > 73 || location2D2.Y < 6 || location2D2.Y > 19) && location2D2.X >= 0 && location2D2.X <= 79 && location2D2.Y >= 0 && location2D2.Y <= 24 && location2D2 != null)
					{
						Location2D location2D3;
						Location2D location2D4;
						for (;;)
						{
							num12--;
							if (num12 < 1)
							{
								goto IL_0F63;
							}
							int num13 = 60;
							if (Stat.Random(1, 100) <= num13)
							{
								location2D3 = location2D2.FromDirection(location2D2.CardinalDirectionToCenter());
								if (!(location2D3 == null) && Z.GetCell(location2D3) != null && !Z.GetCell(location2D3).HasObject("EbonFulcrete"))
								{
									goto Block_104;
								}
							}
							else
							{
								location2D4 = location2D2.FromDirection(Directions.GetRandomCardinalDirection());
								if (!(location2D4 == null) && Z.GetCell(location2D4) != null && !Z.GetCell(location2D4).HasObject("EbonFulcrete"))
								{
									goto Block_107;
								}
							}
						}
						IL_0E33:
						Cell cell3 = Z.GetCell(location2D2);
						if (cell3 == null || cell3.HasObject("EbonFulcrete"))
						{
							break;
						}
						if (location2D2 != null)
						{
							foreach (Cell cell4 in Z.GetCell(location2D2).GetCardinalAdjacentCells(true, true, true))
							{
								int num14 = 50;
								if (cell4.HasWall() && !cell4.HasObject("EbonFulcrete") && Stat.Random(1, 100) <= num14)
								{
									cell4.ClearWalls();
									int num15 = 35;
									if (Stat.Random(1, 100) <= num15)
									{
										cell4.AddObject(PopulationManager.RollOneFrom("TombWallRuinRemains", null, null).Blueprint, null, null, null, null);
									}
								}
							}
							continue;
						}
						continue;
						Block_104:
						location2D2 = location2D3;
						goto IL_0E33;
						Block_107:
						location2D2 = location2D4;
						goto IL_0E33;
					}
				}
			}
			if (!this.Underground)
			{
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Z.ClearReachableMap();
				Z.BuildReachableMap(Z.Width / 2, Z.Height / 2);
			}
			return true;
		}

		// Token: 0x0400323A RID: 12858
		private static List<PerlinNoise2D> NoiseFunctions;

		// Token: 0x0400323B RID: 12859
		private static double[,] BananaGroveNoise;

		// Token: 0x0400323C RID: 12860
		private const int MaxWidth = 1200;

		// Token: 0x0400323D RID: 12861
		private const int MaxHeight = 375;

		// Token: 0x0400323E RID: 12862
		private const int DACCA_CHANCE = 5;

		// Token: 0x0400323F RID: 12863
		public bool Underground;
	}
}
