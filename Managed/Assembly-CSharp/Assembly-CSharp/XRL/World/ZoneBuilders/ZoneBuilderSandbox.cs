using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.EditorFormats.Map;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B16 RID: 2838
	[Serializable]
	public class ZoneBuilderSandbox
	{
		// Token: 0x060071B6 RID: 29110 RVA: 0x002A8FBC File Offset: 0x002A71BC
		protected static string PopulationOr(string Population, string Default)
		{
			if (!PopulationManager.HasPopulation(Population))
			{
				return Default;
			}
			return Population;
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x002A8FCC File Offset: 0x002A71CC
		public Location2D getMatchedEdgeConnectionLocation(Zone zone, string label, string dir, int margin = 3)
		{
			dir = dir.ToLower();
			string text = "";
			if (dir == "n")
			{
				text = "ns";
			}
			if (dir == "s")
			{
				text = "ns";
			}
			if (dir == "e")
			{
				text = "ew";
			}
			if (dir == "w")
			{
				text = "ew";
			}
			if (dir == "n")
			{
				label = label + (zone.Y - 1).ToString() + zone.X.ToString();
			}
			if (dir == "s")
			{
				label = label + zone.Y.ToString() + zone.X.ToString();
			}
			if (dir == "e")
			{
				label = label + zone.Y.ToString() + zone.X.ToString();
			}
			if (dir == "w")
			{
				label = label + zone.Y.ToString() + (zone.X - 1).ToString();
			}
			int worldSeed = XRLCore.Core.Game.GetWorldSeed(label + text);
			if (dir == "n")
			{
				return Location2D.Get(this.GetSeededRange(worldSeed, margin, 79 - margin), 0);
			}
			if (dir == "s")
			{
				return Location2D.Get(this.GetSeededRange(worldSeed, margin, 79 - margin), 24);
			}
			if (dir == "e")
			{
				return Location2D.Get(79, this.GetSeededRange(worldSeed, margin, 24 - margin));
			}
			if (dir == "w")
			{
				return Location2D.Get(0, this.GetSeededRange(worldSeed, margin, 24 - margin));
			}
			return Location2D.Get(0, 0);
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x002A9194 File Offset: 0x002A7394
		public double sampleSimplexNoise(string seedid, int x, int y, int z, float low, float high, float frequency = 0.1f, int octaves = 5, float lacunarity = 1.1f, float gain = 1f)
		{
			if (this.simplexFastNoise == null)
			{
				this.simplexFastNoise = new FastNoise(1337);
				this.simplexFastNoise.SetSeed(this.GetSeedValue(seedid));
				this.simplexFastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
				this.simplexFastNoise.SetFrequency(frequency);
				this.simplexFastNoise.SetFractalType(FastNoise.FractalType.FBM);
				this.simplexFastNoise.SetFractalOctaves(octaves);
				this.simplexFastNoise.SetFractalLacunarity(lacunarity);
				this.simplexFastNoise.SetFractalGain(gain);
			}
			return (double)((this.simplexFastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f * (high - low) + low);
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x002A9240 File Offset: 0x002A7440
		public void EnsureCellReachable(Zone zone, Cell cell)
		{
			List<Cell> list = (from c in zone.GetCells()
				where c.IsReachable()
				select c).ToList<Cell>();
			list.Sort((Cell a, Cell b) => a.PathDistanceTo(cell).CompareTo(b.PathDistanceTo(cell)));
			Grid<int> grid = new Grid<int>(zone.Width, zone.Height);
			grid.regionalize(delegate(int x, int y, int c)
			{
				if (zone.GetCell(x, y).HasWall())
				{
					return 80;
				}
				if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
				{
					return 80;
				}
				return 0;
			}, true);
			using (Pathfinder pathfinder = new Pathfinder(240, 75))
			{
				pathfinder.setWeightsFromGrid<int>(grid, delegate(int x, int y, int c)
				{
					if (zone.GetCell(x, y).HasWall())
					{
						return 80;
					}
					if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
					{
						return 80;
					}
					return 0;
				});
				if (pathfinder.FindPath(cell.Location, list[0].Location, false, true, 24300, false))
				{
					foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
					{
						Cell cell2 = zone.GetCell(pathfinderNode.X, pathfinderNode.Y);
						if (cell2 != null)
						{
							cell2.ClearWalls();
						}
					}
				}
			}
		}

		// Token: 0x060071BA RID: 29114 RVA: 0x002A93A0 File Offset: 0x002A75A0
		public static void TunnelTo(Zone zone, Location2D start, Location2D end, bool pathWithNoise = true, float pathWindyness = 0.2f, int emptyBase = 0, Action<Cell> action = null, Func<int, int, int, int> weightFunc = null)
		{
			using (Pathfinder pathfinder = zone.getPathfinder(null))
			{
				if (pathWithNoise)
				{
					ZoneBuilderSandbox.pathNoise.SetSeed(Stat.Random(int.MinValue, int.MaxValue));
					ZoneBuilderSandbox.pathNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
					ZoneBuilderSandbox.pathNoise.SetFractalOctaves(2);
					ZoneBuilderSandbox.pathNoise.SetFrequency(pathWindyness);
				}
				Grid<int> grid = new Grid<int>(zone.Width, zone.Height);
				zone.GetCells().ForEach(delegate(Cell c)
				{
					if (c.HasWall())
					{
						grid.set(c.X, c.Y, 80);
					}
				});
				if (weightFunc == null)
				{
					weightFunc = delegate(int x, int y, int c)
					{
						int num = 0;
						if (x == 0)
						{
							return 2000;
						}
						if (y == 0)
						{
							return 2000;
						}
						if (x == zone.Width - 1)
						{
							return 2000;
						}
						if (y == zone.Height - 1)
						{
							return 2000;
						}
						if (pathWithNoise)
						{
							num = (int)(Math.Abs(ZoneBuilderSandbox.pathNoise.GetNoise((float)((x + zone.wX * 80) / 3), (float)(y + zone.wY * 25))) * 160f);
						}
						if (zone.GetCell(x, y).HasWall())
						{
							return 80 + num;
						}
						if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
						{
							return 80 + num;
						}
						return num + emptyBase;
					};
				}
				pathfinder.setWeightsFromGrid<int>(grid, weightFunc);
				if (pathfinder.FindPath(start, end, false, true, 24300, true))
				{
					foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
					{
						Cell cell = zone.GetCell(pathfinderNode.X, pathfinderNode.Y);
						if (cell != null)
						{
							if (action == null)
							{
								cell.ClearWalls();
							}
							else
							{
								action(cell);
							}
						}
					}
				}
			}
		}

		// Token: 0x060071BB RID: 29115 RVA: 0x002A9534 File Offset: 0x002A7734
		public static void EnsureAllVoidsConnected(Zone zone, bool pathWithNoise = false, float pathWindyness = 0.2f, Action<GameObject> ClearAction = null)
		{
			Grid<int> grid = new Grid<int>(zone.Width, zone.Height);
			InfluenceMap regions = grid.regionalize(delegate(int x, int y, int c)
			{
				if (zone.GetCell(x, y).HasWall())
				{
					return 80;
				}
				if (zone.GetCell(x, y).HasObjectWithTag("EnsureVoidBlocker"))
				{
					return 80;
				}
				if (zone.GetCell(x, y).HasObjectWithTag("InfluenceMapBlocker"))
				{
					return 80;
				}
				if (zone.GetCell(x, y).HasObject((GameObject o) => o.Physics != null && o.Physics.Solid && !o.HasPart<Door>()))
				{
					return 80;
				}
				return 0;
			}, true);
			List<InfluenceMapRegion> list = new List<InfluenceMapRegion>();
			using (Pathfinder pathfinder = zone.getPathfinder(null))
			{
				regions.Regions.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => a.Center.SquareDistance(regions.Regions.First<InfluenceMapRegion>().Center).CompareTo(b.Center.SquareDistance(regions.Regions.First<InfluenceMapRegion>().Center)));
				if (pathWithNoise)
				{
					ZoneBuilderSandbox.pathNoise.SetSeed(Stat.Random(int.MinValue, int.MaxValue));
					ZoneBuilderSandbox.pathNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
					ZoneBuilderSandbox.pathNoise.SetFractalOctaves(2);
					ZoneBuilderSandbox.pathNoise.SetFrequency(pathWindyness);
				}
				using (List<InfluenceMapRegion>.Enumerator enumerator = regions.Regions.GetEnumerator())
				{
					Func<int, int, int, int> <>9__4;
					while (enumerator.MoveNext())
					{
						InfluenceMapRegion region = enumerator.Current;
						if (list.Count > 0)
						{
							list.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => a.Center.SquareDistance(region.Center).CompareTo(b.Center.SquareDistance(region.Center)));
							InfluenceMapRegion influenceMapRegion = list.First<InfluenceMapRegion>();
							Location2D randomElement = region.Cells.GetRandomElement<Location2D>(null);
							Location2D randomElement2 = influenceMapRegion.Cells.GetRandomElement<Location2D>(null);
							Pathfinder pathfinder2 = pathfinder;
							Grid<int> grid2 = grid;
							Func<int, int, int, int> func;
							if ((func = <>9__4) == null)
							{
								func = (<>9__4 = delegate(int x, int y, int c)
								{
									int num = 0;
									if (x == 0)
									{
										return 2000;
									}
									if (y == 0)
									{
										return 2000;
									}
									if (x == zone.Width - 1)
									{
										return 2000;
									}
									if (y == zone.Height - 1)
									{
										return 2000;
									}
									if (pathWithNoise)
									{
										num = (int)(Math.Abs(ZoneBuilderSandbox.pathNoise.GetNoise((float)((x + zone.wX * 80) / 3), (float)(y + zone.wY * 25))) * 160f);
									}
									if (zone.GetCell(x, y).HasWall())
									{
										return 80 + num;
									}
									if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
									{
										return 1000 + num;
									}
									if (zone.GetCell(x, y).HasObject("EnsureVoidBlocker"))
									{
										return 1000 + num;
									}
									if (zone.GetCell(x, y).HasObject((GameObject o) => o.Physics != null && o.Physics.Solid))
									{
										return 1000;
									}
									return num;
								});
							}
							pathfinder2.setWeightsFromGrid<int>(grid2, func);
							if (pathfinder.FindPath(randomElement, randomElement2, false, true, 24300, true))
							{
								foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
								{
									Cell cell = zone.GetCell(pathfinderNode.X, pathfinderNode.Y);
									if (cell != null)
									{
										cell.ClearWalls();
										if (cell.HasObject((GameObject o) => o.Physics != null && o.Physics.Solid))
										{
											GameObject firstObject = cell.GetFirstObject((GameObject o) => o.Physics != null && o.Physics.Solid);
											if (!firstObject.IsImportant())
											{
												if (ClearAction != null)
												{
													ClearAction(firstObject);
												}
												else
												{
													firstObject.Obliterate(null, false, null);
												}
											}
										}
									}
								}
								list.Add(region);
							}
						}
						else
						{
							list.Add(region);
						}
					}
				}
				regions.RecalculateCostOnly(false);
			}
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x002A9818 File Offset: 0x002A7A18
		public List<Location2D> CreateConveyor(Location2D start, Location2D end, Zone zone, Func<int, int, int> getExtraWeight = null, string forceFinalDirection = null, string padObject = "ConveyorPad")
		{
			List<Location2D> list;
			using (Pathfinder pathfinder = zone.getPathfinder(delegate(int x, int y, Cell cell)
			{
				int num2 = ((getExtraWeight == null) ? 0 : getExtraWeight(x, y));
				if (cell.HasObject("ConveyorPad") || cell.HasObject(padObject) || cell.HasObject("ConveyorDrive"))
				{
					return int.MaxValue;
				}
				if (cell.HasWall())
				{
					return 40 + num2;
				}
				return num2;
			}))
			{
				int num = 10;
				for (;;)
				{
					pathfinder.FindPath(start, end, false, true, 9999, true);
					if (!pathfinder.Steps.Any<PathfinderNode>((PathfinderNode s) => pathfinder.Steps.Where<PathfinderNode>((PathfinderNode t) => t == s).Count<PathfinderNode>() > 1) || num <= 0)
					{
						break;
					}
					num--;
					MetricsManager.LogWarning("overlapping conveyor path detected");
				}
				for (int i = 0; i < pathfinder.Steps.Count; i++)
				{
					Cell cell2 = zone.GetCell(pathfinder.Steps[i].pos);
					cell2.ClearWalls();
					GameObject gameObject = GameObject.Create(padObject);
					ConveyorPad part = gameObject.GetPart<ConveyorPad>();
					if (part != null)
					{
						string text;
						if (i < pathfinder.Steps.Count - 1)
						{
							text = pathfinder.Directions[i];
						}
						else if (forceFinalDirection != null)
						{
							text = forceFinalDirection;
						}
						else
						{
							text = pathfinder.Directions[i - 1];
						}
						part.Direction = text;
						part.Connections = text;
						if (i > 0)
						{
							ConveyorPad conveyorPad = part;
							conveyorPad.Connections += Directions.GetOppositeDirection(pathfinder.Directions[i - 1]);
						}
					}
					else
					{
						MetricsManager.LogError("built " + gameObject.DebugName + " for conveyer belt but it has no ConveyorPad");
					}
					cell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				list = new List<Location2D>(pathfinder.Steps.Select<PathfinderNode, Location2D>((PathfinderNode s) => s.pos));
			}
			return list;
		}

		// Token: 0x060071BD RID: 29117 RVA: 0x002A9A2C File Offset: 0x002A7C2C
		public List<ColorOutputMap> getWfcBuildingTemplate(string template)
		{
			if (this.templates == null)
			{
				this.templates = new Dictionary<string, List<ColorOutputMap>>();
			}
			if (!this.templates.ContainsKey(template))
			{
				ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(template, 3, 78, 23, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				colorOutputMap.Paste(colorOutputMap2, 1, 1);
				List<ColorOutputMap> list = colorOutputMap.CarveSubmaps(new List<Color32>
				{
					ColorOutputMap.BLACK,
					ColorOutputMap.RED
				});
				list.RemoveAll((ColorOutputMap o) => o.extrawidth < 4 || o.extraheight < 4 || o.extrawidth * o.extraheight <= 16 || o.CountColor(ColorOutputMap.BLACK) < 7);
				this.templates.Add(template, list);
				MemoryHelper.GCCollect();
			}
			return this.templates[template];
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x002A9B28 File Offset: 0x002A7D28
		public string GetDefaultWall(Zone Z)
		{
			return Z.GetDefaultWall();
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x002A9B30 File Offset: 0x002A7D30
		public GameObject GetTerrainObject(Zone Z)
		{
			return Z.GetTerrainObject();
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x002A9B38 File Offset: 0x002A7D38
		public string GetTerrain(Zone Z)
		{
			string[] array = Z.ZoneID.Split('.', StringSplitOptions.None);
			int num = Convert.ToInt32(array[1]);
			int num2 = Convert.ToInt32(array[2]);
			return ZoneManager.GetObjectTypeForZone(num, num2, "JoppaWorld");
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x002A9B70 File Offset: 0x002A7D70
		public int GetSeedValue(string id)
		{
			return XRLCore.Core.Game.GetWorldSeed(id);
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x002A9B82 File Offset: 0x002A7D82
		public static int GetOracleIntFromString(string s, int Low, int High)
		{
			return new global::System.Random(Hash.String(s)).Next(Low, High);
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x002A9B96 File Offset: 0x002A7D96
		public T GetOracleListEntryFromInt<T>(int i, List<T> list)
		{
			return list[ZoneBuilderSandbox.GetOracleIntFromString(i.ToString(), 0, list.Count - 1)];
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x002A9BB3 File Offset: 0x002A7DB3
		public int GetOracleIntZone(Zone Z, int Low, int High)
		{
			return new global::System.Random(Hash.String(Z.ZoneID)).Next(Low, High);
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x002A9BCC File Offset: 0x002A7DCC
		public static List<T> GetOracleNFromList<T>(List<T> list, int n, string key)
		{
			global::System.Random random = new global::System.Random(Hash.String(key + XRLCore.Core.Game.GetWorldSeed(key).ToString()));
			List<T> list2 = new List<T>();
			for (int i = 0; i < n; i++)
			{
				list2.Add(list.RemoveRandomElement<T>(random));
			}
			return list2;
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x002A9C24 File Offset: 0x002A7E24
		public static Location2D GetOracleLocationForZone(Zone Z, string seed, int ZOffset = 0, Predicate<Location2D> filter = null)
		{
			return ZoneBuilderSandbox.GetOracleLocation(string.Concat(new string[]
			{
				Z.wX.ToString(),
				".",
				Z.wY.ToString(),
				".",
				Z.X.ToString(),
				".",
				Z.Y.ToString(),
				".",
				(Z.Z + ZOffset).ToString(),
				seed
			}), filter);
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x002A9CB4 File Offset: 0x002A7EB4
		public static Location2D GetOracleLocation(string seed, Predicate<Location2D> filter = null)
		{
			int num = 0;
			Location2D location2D;
			do
			{
				location2D = Location2D.Get(ZoneBuilderSandbox.GetOracleIntFromString(seed + num.ToString(), 0, 80), ZoneBuilderSandbox.GetOracleIntFromString(seed + num.ToString(), 0, 24));
				num++;
			}
			while (filter != null && !filter(location2D));
			return location2D;
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x002A9D04 File Offset: 0x002A7F04
		public bool HasZoneColumnValue(Zone Z, string key)
		{
			return The.Game.ZoneManager.HasZoneColumnProperty(Z.ZoneID, key);
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x002A9D1C File Offset: 0x002A7F1C
		public string GetZoneColumnValue(Zone Z, string key, string defaultValue = null)
		{
			return (string)The.Game.ZoneManager.GetZoneColumnProperty(Z.ZoneID, key, defaultValue);
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x002A9D3A File Offset: 0x002A7F3A
		public void SetZoneColumnValue(Zone Z, string key, object value)
		{
			The.Game.ZoneManager.SetZoneColumnProperty(Z.ZoneID, key, value);
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x002A9D53 File Offset: 0x002A7F53
		public static void BridgeOver(Zone Z, Location2D start, ZoneBuilderSandbox.BridgeDirection dir = ZoneBuilderSandbox.BridgeDirection.Random, string OverObject = "Pit")
		{
			if (dir == ZoneBuilderSandbox.BridgeDirection.Random)
			{
				dir = ((Stat.Random(0, 1) == 0) ? ZoneBuilderSandbox.BridgeDirection.Horizontal : ZoneBuilderSandbox.BridgeDirection.Vertical);
			}
			if (dir == ZoneBuilderSandbox.BridgeDirection.Horizontal)
			{
				ZoneBuilderSandbox.HBridgeOver(Z, start, OverObject);
			}
			if (dir == ZoneBuilderSandbox.BridgeDirection.Vertical)
			{
				ZoneBuilderSandbox.VBridgeOver(Z, start, OverObject);
			}
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x002A9D80 File Offset: 0x002A7F80
		public static void VBridgeOver(Zone Z, Location2D start, string OverObject = "Pit")
		{
			int num = start.Y;
			while (num > 0 && Z.GetCell(start.X, num).HasObject(OverObject))
			{
				Z.GetCell(start.X, num).RequireObject("Bridge", null, null, null, null);
				num--;
			}
			int num2 = start.Y;
			while (num2 < Z.Height && Z.GetCell(start.X, num2).HasObject(OverObject))
			{
				Z.GetCell(start.X, num2).RequireObject("Bridge", null, null, null, null);
				num2++;
			}
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x002A9E18 File Offset: 0x002A8018
		public static void HBridgeOver(Zone Z, Location2D start, string OverObject = "Pit")
		{
			int num = start.X;
			while (num > 0 && Z.GetCell(num, start.Y).HasObject(OverObject))
			{
				Z.GetCell(num, start.Y).RequireObject("Bridge", null, null, null, null);
				num--;
			}
			int num2 = start.X;
			while (num2 < Z.Width && Z.GetCell(num2, start.Y).HasObject(OverObject))
			{
				Z.GetCell(num2, start.Y).RequireObject("Bridge", null, null, null, null);
				num2++;
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x002A9EAE File Offset: 0x002A80AE
		public static int GetOracleIntColumn(Zone Z, string seed, int Low, int High)
		{
			return ZoneBuilderSandbox.GetOracleIntColumn(Z.wX, Z.wY, Z.X, Z.Y, seed, Low, High);
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x002A9ED0 File Offset: 0x002A80D0
		public static int GetOracleIntColumn(Zone Z, int Low, int High)
		{
			return ZoneBuilderSandbox.GetOracleIntColumn(Z.wX, Z.wY, Z.X, Z.Y, null, Low, High);
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x002A9EF2 File Offset: 0x002A80F2
		public static int GetOracleIntColumn(int ParasangX, int ParasangY, int ZoneX, int ZoneY, int Low, int High)
		{
			return ZoneBuilderSandbox.GetOracleIntColumn(ParasangX, ParasangY, ZoneX, ZoneY, null, Low, High);
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x002A9F04 File Offset: 0x002A8104
		public unsafe static int GetOracleIntColumn(int ParasangX, int ParasangY, int ZoneX, int ZoneY, string Seed, int Low, int High)
		{
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)16], 4);
			*span[0] = ParasangX;
			*span[1] = ParasangY;
			*span[2] = ZoneX;
			*span[3] = ZoneY;
			uint num = Hash.FNV1A32(span, 2166136261U);
			if (Seed != null)
			{
				num = Hash.FNV1A32(Seed, num);
			}
			XRLGame game = The.Game;
			return new global::System.Random((int)Hash.FNV1A32((game != null) ? game.GetWorldSeed(null) : 0, num)).Next(Low, High);
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x002A9F8C File Offset: 0x002A818C
		public global::System.Random GetSeededRand(string val)
		{
			return new global::System.Random(Hash.String(val + XRLCore.Core.Game.GetWorldSeed(val).ToString()));
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x002A9FC4 File Offset: 0x002A81C4
		public int GetSeededRange(string val, int Low = -2147483648, int High = 2147483647)
		{
			return this.GetSeededRange(Hash.String(val + XRLCore.Core.Game.GetWorldSeed(val).ToString()), Low, High);
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x002A9FFC File Offset: 0x002A81FC
		public int GetSeededRange(int Seed, int Low = -2147483648, int High = 2147483647)
		{
			return new global::System.Random(Seed).Next(Low, High);
		}

		// Token: 0x060071D5 RID: 29141 RVA: 0x002AA00B File Offset: 0x002A820B
		public IEnumerable<Location2D> YieldGaussianCellCluster(float XP, float YP, float StdDeviationX = 3f, float StdDeviationY = 5f)
		{
			for (;;)
			{
				int num = (int)Stat.GaussianRandom(XP, StdDeviationX);
				int num2 = (int)Stat.GaussianRandom(YP, StdDeviationY);
				if (num < 0)
				{
					num = 0;
				}
				if (num > 79)
				{
					num = 79;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > 24)
				{
					num2 = 24;
				}
				yield return Location2D.Get(num, num2);
			}
			yield break;
		}

		// Token: 0x060071D6 RID: 29142 RVA: 0x002AA031 File Offset: 0x002A8231
		public Cell GetCell(Zone Z, Location2D P)
		{
			return Z.GetCell(P.X, P.Y);
		}

		// Token: 0x060071D7 RID: 29143 RVA: 0x002AA045 File Offset: 0x002A8245
		public Cell GetCell(Zone Z, Point2D P)
		{
			return Z.GetCell(P.x, P.y);
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x002AA05C File Offset: 0x002A825C
		public void PlaceHut(Zone Z, Rect2D R, string Floor = "DirtPath", string Wall = "Shale", string Table = null, bool Round = false)
		{
			Rect2D rect2D = R.ReduceBy(1, 1);
			this.ClearRect(Z, rect2D);
			for (int i = rect2D.y1; i <= rect2D.y2; i++)
			{
				for (int j = rect2D.x1; j <= rect2D.x2; j++)
				{
					Z.GetCell(j, i).AddObject(Floor, null, null, null, null);
				}
			}
			if (Round)
			{
				for (int k = R.x1 + 1; k <= R.x2 - 1; k++)
				{
					Z.GetCell(k, R.y1).ClearAndAddObject(Wall);
					Z.GetCell(k, R.y2).ClearAndAddObject(Wall);
				}
				for (int l = R.y1 + 1; l <= R.y2 - 1; l++)
				{
					Z.GetCell(R.x1, l).ClearAndAddObject(Wall);
					Z.GetCell(R.x2, l).ClearAndAddObject(Wall);
				}
				Z.GetCell(R.x1 + 1, R.y1 + 1).ClearAndAddObject(Wall);
				Z.GetCell(R.x2 - 1, R.y1 + 1).ClearAndAddObject(Wall);
				Z.GetCell(R.x1 + 1, R.y2 - 1).ClearAndAddObject(Wall);
				Z.GetCell(R.x2 - 1, R.y2 - 1).ClearAndAddObject(Wall);
			}
			else
			{
				for (int m = R.x1; m <= R.x2; m++)
				{
					Z.GetCell(m, R.y1).ClearAndAddObject(Wall);
					Z.GetCell(m, R.y2).ClearAndAddObject(Wall);
				}
				for (int n = R.y1; n <= R.y2; n++)
				{
					Z.GetCell(R.x1, n).ClearAndAddObject(Wall);
					Z.GetCell(R.x2, n).ClearAndAddObject(Wall);
				}
			}
			Cell cell = Z.GetCell(R.Door);
			if (cell != null)
			{
				cell.ClearWalls();
			}
			foreach (PopulationResult populationResult in PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null))
			{
				for (int num = 0; num < populationResult.Number; num++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, rect2D, populationResult.Blueprint, populationResult.Hint, 0, null);
				}
			}
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x002AA2F8 File Offset: 0x002A84F8
		public void PaintZone(Zone Z, string PaintTile, string PaintRender, string PaintColor, string PaintTileColor, string PaintDetail, bool bForce = false)
		{
			foreach (Cell cell in Z.GetCells())
			{
				if (bForce || string.IsNullOrEmpty(cell.PaintTile))
				{
					cell.PaintTile = PaintTile;
					cell.PaintColorString = PaintColor;
					cell.PaintTileColor = PaintTileColor;
					cell.PaintDetailColor = PaintDetail;
					cell.PaintRenderString = PaintRender;
				}
			}
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x002AA37C File Offset: 0x002A857C
		public List<Location2D> BuildPathWithObject(Zone zone, List<Location2D> Segments, string PaintObject, int Width, bool Noise, Func<int, int, int, int> weightFunc = null)
		{
			List<Location2D> list = new List<Location2D>();
			HashSet<Cell> hashSet = new HashSet<Cell>();
			List<Location2D> list2;
			using (Pathfinder pathfinder = new Pathfinder(240, 75))
			{
				Grid<int> grid = new Grid<int>(zone.Width, zone.Height);
				grid.regionalize(delegate(int x, int y, int c)
				{
					if (weightFunc != null)
					{
						return weightFunc(x, y, c);
					}
					if (zone.GetCell(x, y).HasWall())
					{
						return 80;
					}
					if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
					{
						return 80;
					}
					return 0;
				}, true);
				pathfinder.setWeightsFromGrid<int>(grid, delegate(int x, int y, int c)
				{
					if (weightFunc != null)
					{
						return weightFunc(x, y, c);
					}
					if (zone.GetCell(x, y).HasWall())
					{
						return 80;
					}
					if (zone.GetCell(x, y).HasObject("InfluenceMapBlocker"))
					{
						return 80;
					}
					return 0;
				});
				for (int i = 0; i < Segments.Count - 1; i++)
				{
					pathfinder.FindPath(Segments[i], Segments[i + 1], false, true, 9999, false);
					if (pathfinder.Found)
					{
						foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
						{
							if (zone.GetCell(pathfinderNode.pos) != null)
							{
								foreach (Cell cell in zone.GetCell(pathfinderNode.pos).GetLocalAdjacentCells(Width, true))
								{
									if (!hashSet.Contains(cell))
									{
										hashSet.Add(cell);
										cell.AddObject(PaintObject, null, null, null, null);
										Location2D location = cell.Location;
										list.Add(location);
									}
								}
							}
						}
					}
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x002AA564 File Offset: 0x002A8764
		public List<Location2D> BuildSimplePathWithObject(Zone Z, List<Location2D> Segments, string Blueprint, int Width, bool Noise)
		{
			List<Location2D> list = new List<Location2D>();
			GameObject gameObject = null;
			for (int i = 0; i < Segments.Count; i += 2)
			{
				FindPath findPath = new FindPath(Z, Segments[i].X, Segments[i].Y, Z, Segments[i + 1].X, Segments[i + 1].Y, false, true, gameObject, Noise);
				if (findPath.Usable)
				{
					foreach (Cell cell in findPath.Steps)
					{
						foreach (Cell cell2 in cell.GetLocalAdjacentCells())
						{
							if (!cell2.HasObject(Blueprint))
							{
								cell2.AddObject(Blueprint, null, null, null, null);
								Location2D location = cell2.Location;
								if (!list.Contains(location))
								{
									list.Add(location);
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x002AA68C File Offset: 0x002A888C
		public List<Location2D> BuildPath(Zone Z, List<Location2D> Segments, string PaintTile, string PaintRender, string PaintColor, string PaintDetail, int Width, bool Noise)
		{
			List<Location2D> list = new List<Location2D>();
			GameObject gameObject = null;
			for (int i = 0; i < Segments.Count; i += 2)
			{
				FindPath findPath = new FindPath(Z, Segments[i].X, Segments[i].Y, Z, Segments[i + 1].X, Segments[i + 1].Y, false, true, gameObject, Noise);
				if (findPath.Usable)
				{
					foreach (Cell cell in findPath.Steps)
					{
						foreach (Cell cell2 in cell.GetLocalAdjacentCells())
						{
							cell2.PaintTile = PaintTile;
							cell2.PaintColorString = PaintColor;
							cell2.PaintDetailColor = PaintDetail;
							cell2.PaintRenderString = PaintRender;
							Location2D location = cell2.Location;
							if (!list.Contains(location))
							{
								list.Add(location);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x002AA7B8 File Offset: 0x002A89B8
		public static void PlacePopulationInCells(Zone Z, List<Location2D> Cells, string Table)
		{
			List<Location2D> list = new List<Location2D>(Cells);
			Algorithms.RandomShuffleInPlace<Location2D>(list, Stat.Rand);
			List<PopulationResult> list2 = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list2.Count; i++)
			{
				for (int j = 0; j < list2[i].Number; j++)
				{
					if (list.Count == 0)
					{
						return;
					}
					Cell cell = Z.GetCell(list[list.Count - 1]);
					if (!cell.HasObject(list2[i].Blueprint))
					{
						cell.AddObject(list2[i].Blueprint, null, null, null, null);
					}
					list.RemoveAt(list.Count - 1);
				}
			}
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x002AA87D File Offset: 0x002A8A7D
		public static void PlacePopulationInRegion(Zone Z, IEnumerable<Location2D> R, string Table, string DefaultHint = null, Func<string, GameObject> customFabricator = null)
		{
			ZoneBuilderSandbox.PlacePopulationInRegion(Z, new LocationList(R), Table, DefaultHint, customFabricator);
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x002AA88F File Offset: 0x002A8A8F
		public static void PlacePopulationInRegion(Zone Z, IEnumerable<Cell> R, string Table, string DefaultHint = null, Func<string, GameObject> customFabricator = null)
		{
			ZoneBuilderSandbox.PlacePopulationInRegion(Z, new LocationList(R.Select<Cell, Location2D>((Cell c) => c.Location)), Table, DefaultHint, customFabricator);
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x002AA8C8 File Offset: 0x002A8AC8
		public static void PlacePopulationInRegion(Zone Z, ILocationArea R, string Table, string DefaultHint = null, Func<string, GameObject> customFabricator = null)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			ZoneBuilderSandbox.ObjectPlacementCache.Clear();
			ZoneBuilderSandbox.currentpop++;
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					if (list[i].Blueprint == "*Fracti")
					{
						string text;
						if (Stat.Random(0, 2) == 0)
						{
							text = "Fracti2";
						}
						else if (Stat.Random(0, 2) == 1)
						{
							text = "Fracti3";
						}
						else
						{
							text = "Fracti";
						}
						CellularGrid cellularGrid = new CellularGrid(Stat.Rand, 1, 80, 25);
						cellularGrid.BornList = new int[] { 2, 4, 5, 6 };
						cellularGrid.SurviveList = new int[] { 2, 3, 4, 5, 7, 8 };
						cellularGrid.Passes = 8;
						cellularGrid.SeedChance = 3;
						cellularGrid.SeedBorders = false;
						cellularGrid.BorderDepth = 0;
						cellularGrid.Generate(Stat.Rand, Z.Width, Z.Height);
						for (int k = 1; k < Z.Width - 1; k++)
						{
							for (int l = 1; l < Z.Height - 1; l++)
							{
								if (cellularGrid.cells[k, l] == 1 && R.EnumerateNonBorderLocations().Contains(Location2D.Get(k, l)) && !Z.GetCell(Location2D.Get(k, l)).HasObjectWithTag("NoFractus"))
								{
									Z.GetCell(Location2D.Get(k, l)).AddObject(text, null, null, null, null);
								}
							}
						}
					}
					else
					{
						ZoneBuilderSandbox.PlaceObjectInArea(Z, R, ZoneBuilderSandbox.ApplyHintsToObject((customFabricator == null) ? GameObject.Create(list[i].Blueprint) : customFabricator(list[i].Blueprint), list[i].Hint, i), i, j, string.IsNullOrEmpty(list[i].Hint) ? DefaultHint : list[i].Hint, list[i].Builder, true);
					}
				}
			}
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x002AAB04 File Offset: 0x002A8D04
		public static void encloseRectWithWall(Zone zone, Rect2D rect, string wallObject)
		{
			for (int i = rect.x1; i <= rect.x2; i++)
			{
				if (zone.GetCell(i, rect.y1) != null)
				{
					zone.GetCell(i, rect.y1).ClearWalls();
				}
				if (zone.GetCell(i, rect.y1) != null)
				{
					zone.GetCell(i, rect.y1).AddObject(wallObject, null, null, null, null);
				}
				if (zone.GetCell(i, rect.y2) != null)
				{
					zone.GetCell(i, rect.y2).ClearWalls();
				}
				if (zone.GetCell(i, rect.y2) != null)
				{
					zone.GetCell(i, rect.y2).AddObject(wallObject, null, null, null, null);
				}
			}
			for (int j = rect.y1 + 1; j <= rect.y2 - 1; j++)
			{
				if (zone.GetCell(rect.x1, j) != null)
				{
					zone.GetCell(rect.x1, j).ClearWalls();
				}
				if (zone.GetCell(rect.x1, j) != null)
				{
					zone.GetCell(rect.x1, j).AddObject(wallObject, null, null, null, null);
				}
				if (zone.GetCell(rect.x2, j) != null)
				{
					zone.GetCell(rect.x2, j).ClearWalls();
				}
				if (zone.GetCell(rect.x2, j) != null)
				{
					zone.GetCell(rect.x2, j).AddObject(wallObject, null, null, null, null);
				}
			}
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x002AAC70 File Offset: 0x002A8E70
		public static GameObject PlaceObjectOnFloor(Zone Z, string Object, string OkFloor)
		{
			List<Cell> cellsWithFloor = Z.GetCellsWithFloor(OkFloor);
			Algorithms.RandomShuffleInPlace<Cell>(cellsWithFloor, Stat.Rand);
			foreach (Cell cell in cellsWithFloor)
			{
				if (cell.IsEmpty() && !cell.HasObject(Object))
				{
					return cell.AddObject(Object, null, null, null, null);
				}
			}
			return null;
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x002AACEC File Offset: 0x002A8EEC
		public static void PlacePopulationInRect(Zone Z, Rect2D R, string Table)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, R, list[i].Blueprint, list[i].Hint, i, null);
				}
			}
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x002AAD60 File Offset: 0x002A8F60
		public static void PlacePopulationInRect(Zone Z, Rect2D R, string Table, Action<GameObject> BeforeObjectCreated)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, R, list[i].Blueprint, list[i].Hint, BeforeObjectCreated, null);
				}
			}
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x002AADD4 File Offset: 0x002A8FD4
		public static void PlacePopulationInRect(Zone Z, Rect2D R, Rect2D Exclude, string Table)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, R, Exclude, list[i].Blueprint, list[i].Hint, 0, null);
				}
			}
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x002AAE4C File Offset: 0x002A904C
		public static void PlacePopulationInRect(Zone Z, Rect2D R, Rect2D Exclude, string Table, Action<GameObject> BeforeObjectCreated)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, R, Exclude, list[i].Blueprint, list[i].Hint, BeforeObjectCreated, null);
				}
			}
		}

		// Token: 0x060071E7 RID: 29159 RVA: 0x002AAEC4 File Offset: 0x002A90C4
		public static void PlacePopulationInRegionRect(Zone Z, ILocationArea Region, Rect2D Rect, string FrontSide, string Table, string Owner = null, List<Location2D> AvoidPoints = null, Func<string, GameObject> customFabricator = null)
		{
			List<PopulationResult> list = PopulationManager.Generate(Table, "zonetier", Z.NewTier.ToString(), null, null, null);
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list[i].Number; j++)
				{
					ZoneBuilderSandbox.PlaceObjectInRegionRect(Z, Region, Rect, FrontSide, list[i].Blueprint, list[i].Hint, Owner, AvoidPoints, null, customFabricator);
				}
			}
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x002AAF40 File Offset: 0x002A9140
		public static void AddPointWithHint(List<Location2D> L, Location2D P, Rect2D R, string Hint, List<Location2D> AvoidPoints = null)
		{
			if (R.IsPointAdjacentToDoor(P))
			{
				return;
			}
			if (AvoidPoints != null && R.IsPointAdjacentToAvoid(P, AvoidPoints))
			{
				return;
			}
			L.Add(P);
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x002AAF65 File Offset: 0x002A9165
		public static void AddPointWithHint(List<Location2D> L, Location2D P, List<Location2D> Avoid, Rect2D R, string Hint)
		{
			if (!R.IsPointAdjacentToAvoid(P, Avoid))
			{
				L.Add(P);
			}
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x002AAF7C File Offset: 0x002A917C
		public static bool PlaceObjectInRegionRect(Zone Z, ILocationArea Region, Rect2D R, string FrontSide, string Object, string Hint = null, string Owner = null, List<Location2D> AvoidPoints = null, GameObject precreated = null, Func<string, GameObject> customFabricator = null)
		{
			ZoneBuilderSandbox.Points.Clear();
			if (string.IsNullOrEmpty(Hint))
			{
				if (precreated != null)
				{
					Hint = precreated.GetPropertyOrTag("PlacementHint", "");
				}
				else
				{
					GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(Object);
					Hint = ((blueprint != null) ? blueprint.GetTag("PlacementHint", "") : "");
				}
			}
			if (Hint.StartsWith("OutsideDoor:"))
			{
				int num = Convert.ToInt32(Hint.Split(':', StringSplitOptions.None)[1]);
				Rect2D rect2D = R.ReduceBy(-2, -2);
				if (FrontSide == "N")
				{
					ZoneBuilderSandbox.Points.Add(Location2D.Get(R.Door.x - num, R.Door.y - 1));
					ZoneBuilderSandbox.Points.Add(Location2D.Get(R.Door.x + num, R.Door.y - 1));
				}
				if (FrontSide == "S")
				{
					ZoneBuilderSandbox.Points.Add(Location2D.Get(R.Door.x - num, R.Door.y + 1));
					ZoneBuilderSandbox.Points.Add(Location2D.Get(R.Door.x + num, R.Door.y + 1));
				}
				if (FrontSide == "E")
				{
					ZoneBuilderSandbox.Points.Add(Location2D.Get(rect2D.Door.x + 1, rect2D.Door.y - num));
					ZoneBuilderSandbox.Points.Add(Location2D.Get(rect2D.Door.x + 1, rect2D.Door.y + num));
				}
				if (FrontSide == "W")
				{
					ZoneBuilderSandbox.Points.Add(Location2D.Get(rect2D.Door.x - 1, rect2D.Door.y - num));
					ZoneBuilderSandbox.Points.Add(Location2D.Get(rect2D.Door.x - 1, rect2D.Door.y + num));
				}
			}
			else
			{
				if (Hint == "AlongWall")
				{
					using (IEnumerator<Location2D> enumerator = Region.EnumerateLocations().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Location2D location2D = enumerator.Current;
							if (Z.GetCell(location2D).GetLocalAdjacentCells().Any<Cell>((Cell c) => c.HasWall()))
							{
								ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, location2D, R, Hint, AvoidPoints);
							}
						}
						goto IL_0767;
					}
				}
				if (Hint == "InsideCorner")
				{
					if (FrontSide != "N" || FrontSide != "W")
					{
						ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x1, R.y1), R, Hint, AvoidPoints);
					}
					if (FrontSide != "S" || FrontSide != "W")
					{
						ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x1, R.y2), R, Hint, AvoidPoints);
					}
					if (FrontSide != "N" || FrontSide != "E")
					{
						ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x2, R.y1), R, Hint, AvoidPoints);
					}
					if (FrontSide != "S" || FrontSide != "E")
					{
						ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x2, R.y2), R, Hint, AvoidPoints);
					}
				}
				else if (Hint == "AlongOutsideFrontWall")
				{
					Rect2D rect2D2 = R.ReduceBy(-2, -2);
					for (int i = rect2D2.x1 + 1; i <= rect2D2.x2 - 1; i++)
					{
						if (FrontSide == "N")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(i, rect2D2.y1), rect2D2, Hint, AvoidPoints);
						}
						if (FrontSide == "S")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(i, rect2D2.y2), rect2D2, Hint, AvoidPoints);
						}
					}
					for (int j = R.y1 + 1; j <= R.y2 - 1; j++)
					{
						if (FrontSide == "W")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(rect2D2.x1, j), rect2D2, Hint, AvoidPoints);
						}
						if (FrontSide == "E")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(rect2D2.x2, j), rect2D2, Hint, AvoidPoints);
						}
					}
				}
				else if (Hint == "AlongOutsideWall")
				{
					Rect2D rect2D3 = R.ReduceBy(-2, -2);
					for (int k = rect2D3.x1 + 1; k <= rect2D3.x2 - 1; k++)
					{
						if (FrontSide != "N")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(k, rect2D3.y1), rect2D3, Hint, AvoidPoints);
						}
						if (FrontSide != "S")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(k, rect2D3.y2), rect2D3, Hint, AvoidPoints);
						}
					}
					for (int l = rect2D3.y1 + 1; l <= rect2D3.y2 - 1; l++)
					{
						if (FrontSide != "W")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(rect2D3.x1, l), rect2D3, Hint, AvoidPoints);
						}
						if (FrontSide != "E")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(rect2D3.x2, l), rect2D3, Hint, AvoidPoints);
						}
					}
				}
				else if (Hint == "AlongInsideWall")
				{
					for (int m = R.x1; m <= R.x2; m++)
					{
						if (FrontSide != "N")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(m, R.y1), R, Hint, AvoidPoints);
						}
						if (FrontSide != "S")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(m, R.y2), R, Hint, AvoidPoints);
						}
					}
					for (int n = R.y1; n <= R.y2; n++)
					{
						if (FrontSide != "W")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x1, n), R, Hint, AvoidPoints);
						}
						if (FrontSide != "E")
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(R.x2, n), R, Hint, AvoidPoints);
						}
					}
				}
				else if (Hint == "Interior" || Hint == "Inside")
				{
					for (int num2 = R.y1 + 1; num2 <= R.y2 - 1; num2++)
					{
						for (int num3 = R.x1 + 1; num3 <= R.x2 - 1; num3++)
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(num3, num2), R, Hint, AvoidPoints);
						}
					}
				}
				else
				{
					for (int num4 = R.y1; num4 <= R.y2; num4++)
					{
						for (int num5 = R.x1; num5 <= R.x2; num5++)
						{
							ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(num5, num4), R, Hint, AvoidPoints);
						}
					}
				}
			}
			IL_0767:
			ZoneBuilderSandbox.Points.RemoveAll((Location2D p) => Z.GetCell(p).HasSpawnBlocker());
			if (ZoneBuilderSandbox.Points.Count == 0)
			{
				for (int num6 = R.y1; num6 <= R.y2; num6++)
				{
					for (int num7 = R.x1; num7 <= R.x2; num7++)
					{
						ZoneBuilderSandbox.AddPointWithHint(ZoneBuilderSandbox.Points, Location2D.Get(num7, num6), R, Hint, AvoidPoints);
					}
				}
			}
			if (ZoneBuilderSandbox.Points.Count == 0)
			{
				return ZoneBuilderSandbox.PlaceObjectInArea(Z, Region, (precreated != null) ? precreated : ((customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object)), 0, 0, Hint, null, false);
			}
			Algorithms.RandomShuffleInPlace<Location2D>(ZoneBuilderSandbox.Points, Stat.Rand);
			for (int num8 = 0; num8 < ZoneBuilderSandbox.Points.Count; num8++)
			{
				if (Z.GetCell(ZoneBuilderSandbox.Points[num8]).Objects.Count == 0)
				{
					if (precreated != null)
					{
						Z.GetCell(ZoneBuilderSandbox.Points[num8]).AddObject(precreated, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).Physics.Owner = Owner;
					}
					else
					{
						Z.GetCell(ZoneBuilderSandbox.Points[num8]).AddObject(Object, null, null, null, null).Physics.Owner = Owner;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x002AB870 File Offset: 0x002A9A70
		public static void FilterPoints(string Hint, InfluenceMapRegion R, List<Location2D> Valid)
		{
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x002AB874 File Offset: 0x002A9A74
		public static void PlaceStructuredPopulationInRegion(Zone Z, InfluenceMapRegion R, PopulationStructuredResult P, List<Location2D> ValidPoints = null)
		{
			List<Location2D> list = new List<Location2D>();
			if (ValidPoints == null)
			{
				list.AddRange(R.Cells);
			}
			else
			{
				list.AddRange(ValidPoints);
			}
			ZoneBuilderSandbox.FilterPoints(P.Hint, R, list);
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x002AB8AC File Offset: 0x002A9AAC
		public static GameObject ApplyHintsToObject(GameObject gameObject, string hints, int group = 0)
		{
			if (hints == null || hints == "" || gameObject == null)
			{
				return gameObject;
			}
			foreach (string text in hints.Split(',', StringSplitOptions.None))
			{
				if (text.StartsWith("Template"))
				{
					string text2 = text.Replace("Template(", "");
					text2 = text2.Substring(0, text2.Length - 1);
					string[] array2 = text2.Split(' ', StringSplitOptions.None);
					string text3 = "each";
					if (array2.Length >= 3)
					{
						text3 = array2[2].ToLower();
					}
					int num = 100;
					if (array2.Length >= 2)
					{
						num = Convert.ToInt32(array2[1]);
					}
					if ((text3 == "each" && Stat.Roll(1, 100) <= num) || (text3 == "all" && ZoneBuilderSandbox.GetOracleIntFromString("populationgroup" + group.ToString(), 1, 100) <= 100))
					{
						if (!ZoneBuilderSandbox.templateApplicators.ContainsKey(array2[0]))
						{
							try
							{
								MethodInfo method = ModManager.ResolveType("XRL.World.Biomes." + array2[0], false, false, true).GetMethod("Apply");
								ZoneBuilderSandbox.templateApplicators.Add(array2[0], method);
							}
							catch (Exception)
							{
								Debug.LogError("Unresolved template type: " + array2[0]);
							}
						}
						ZoneBuilderSandbox.templateApplicators[array2[0]].Invoke(null, new object[] { gameObject });
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x002ABA30 File Offset: 0x002A9C30
		public static bool PlaceObjectInArea(Zone Z, ILocationArea A, GameObject gameObject, int group = 0, int groupordinal = 0, string Hints = null, string Builder = null, bool bAllowCaching = false)
		{
			if (gameObject == null)
			{
				MetricsManager.LogError("placing null object");
				return true;
			}
			gameObject = ZoneBuilderSandbox.ApplyHintsToObject(gameObject, Hints, group);
			if (gameObject.HasTag("PlacementHint:AlwaysPlace00"))
			{
				Z.GetCell(0, 0).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return true;
			}
			foreach (List<Location2D> list in ZoneBuilderSandbox.hintFields)
			{
				list.Clear();
			}
			ZoneBuilderSandbox.hintFieldtotals.Clear();
			ZoneBuilderSandbox.workingSet.Clear();
			if (string.IsNullOrEmpty(Hints))
			{
				Hints = gameObject.GetPropertyOrTag("PlacementHint", "");
			}
			gameObject.SetIntProperty("_populationgroup", group, false);
			gameObject.SetIntProperty("_populationgroupordinal", groupordinal, false);
			int num = 0;
			bool flag = gameObject.IsSpawnBlocker();
			Predicate<GameObject> <>9__10;
			Predicate<Cell> <>9__9;
			Predicate<Location2D> <>9__7;
			foreach (string text in Hints.Split(',', StringSplitOptions.None))
			{
				if (!(text == ""))
				{
					if (ZoneBuilderSandbox.Points == null)
					{
						ZoneBuilderSandbox.Points = new List<Location2D>();
					}
					else
					{
						ZoneBuilderSandbox.Points.Clear();
					}
					bool flag2 = false;
					if (bAllowCaching && ZoneBuilderSandbox.ObjectPlacementCache.ContainsKey(text))
					{
						ZoneBuilderSandbox.Points = ZoneBuilderSandbox.ObjectPlacementCache[text];
						flag2 = ZoneBuilderSandbox.Points.Count > 0;
					}
					if (!flag2)
					{
						bool flag3 = false;
						if (text.StartsWith("OnWall"))
						{
							foreach (Location2D location2D in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D).HasExternalWall(null))
								{
									ZoneBuilderSandbox.Points.Add(location2D);
								}
							}
							flag3 = true;
						}
						else if (text.StartsWith("Aquatic"))
						{
							foreach (Location2D location2D2 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D2).HasOpenLiquidVolume() && !Z.GetCell(location2D2).HasBridge())
								{
									ZoneBuilderSandbox.Points.Add(location2D2);
								}
							}
							if (ZoneBuilderSandbox.Points.Count == 0)
							{
								foreach (Cell cell in from c in Z.GetCells()
									where c.HasOpenLiquidVolume() && !c.HasBridge()
									select c)
								{
									if (!ZoneBuilderSandbox.Points.Contains(cell.Location))
									{
										ZoneBuilderSandbox.Points.Add(cell.Location);
									}
								}
							}
							flag3 = true;
						}
						else if (text.StartsWith("AdjacentToBlueprint:"))
						{
							string text2 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D3 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D3).HasObjectWithBlueprint(text2))
								{
									foreach (Cell cell2 in Z.GetCell(location2D3).GetLocalCardinalAdjacentCells())
									{
										if (!cell2.HasObjectWithBlueprint(text2) && !ZoneBuilderSandbox.Points.Contains(cell2.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell2.Location);
										}
									}
								}
							}
							bAllowCaching = false;
						}
						else if (text.StartsWith("StackWithStartsWith:"))
						{
							string text3 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D4 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D4).HasObjectWithBlueprintStartsWith(text3) && !ZoneBuilderSandbox.Points.Contains(location2D4))
								{
									ZoneBuilderSandbox.Points.Add(location2D4);
								}
							}
							if (ZoneBuilderSandbox.Points.Count == 0)
							{
								ZoneBuilderSandbox.Points.AddRange(A.EnumerateLocations());
							}
							flag3 = true;
							bAllowCaching = false;
						}
						else if (text.StartsWith("StackWith:"))
						{
							string text4 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D5 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D5).HasObjectWithBlueprint(text4) && !ZoneBuilderSandbox.Points.Contains(location2D5))
								{
									ZoneBuilderSandbox.Points.Add(location2D5);
								}
							}
							flag3 = true;
						}
						else if (text.StartsWith("StackWithWithTag:"))
						{
							string text5 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D6 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D6).HasObjectWithPropertyOrTag(text5) && !ZoneBuilderSandbox.Points.Contains(location2D6))
								{
									ZoneBuilderSandbox.Points.Add(location2D6);
								}
							}
							flag3 = true;
						}
						else if (text.StartsWith("StackWithSelf") || text == "Stack")
						{
							foreach (Location2D location2D7 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D7).HasObjectWithBlueprint(gameObject.Blueprint) && !ZoneBuilderSandbox.Points.Contains(location2D7))
								{
									ZoneBuilderSandbox.Points.Add(location2D7);
								}
							}
							if (ZoneBuilderSandbox.Points.Count == 0)
							{
								ZoneBuilderSandbox.Points.AddRange(A.EnumerateLocations());
							}
							flag3 = true;
						}
						else if (text.StartsWith("StackWithEndsWith:"))
						{
							string text6 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D8 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D8).HasObjectWithBlueprintEndsWith(text6) && !ZoneBuilderSandbox.Points.Contains(location2D8))
								{
									ZoneBuilderSandbox.Points.Add(location2D8);
								}
							}
							flag3 = true;
						}
						else if (text.StartsWith("AdjacentToStartsWith:"))
						{
							string text7 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D9 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D9).HasObjectWithBlueprintStartsWith(text7))
								{
									foreach (Cell cell3 in Z.GetCell(location2D9).GetLocalCardinalAdjacentCells())
									{
										if (!cell3.HasObjectWithBlueprintStartsWith(text7) && !ZoneBuilderSandbox.Points.Contains(cell3.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell3.Location);
										}
									}
								}
							}
							bAllowCaching = true;
						}
						else if (text.StartsWith("AdjacentToEndsWith:"))
						{
							string text8 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D10 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D10).HasObjectWithBlueprintEndsWith(text8))
								{
									foreach (Cell cell4 in Z.GetCell(location2D10).GetLocalCardinalAdjacentCells())
									{
										if (!cell4.HasObjectWithBlueprintEndsWith(text8) && !ZoneBuilderSandbox.Points.Contains(cell4.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell4.Location);
										}
									}
								}
							}
							bAllowCaching = false;
						}
						else if (text.StartsWith("AdjacentToWithTag:"))
						{
							string text9 = text.Split(':', StringSplitOptions.None)[1];
							foreach (Location2D location2D11 in A.EnumerateLocations())
							{
								if (Z.GetCell(location2D11).HasObjectWithPropertyOrTag(text9))
								{
									foreach (Cell cell5 in Z.GetCell(location2D11).GetLocalCardinalAdjacentCells())
									{
										if (!cell5.HasObjectWithPropertyOrTag(text9) && !ZoneBuilderSandbox.Points.Contains(cell5.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell5.Location);
										}
									}
								}
							}
							bAllowCaching = true;
						}
						else if (text.StartsWith("AdjacentGroup"))
						{
							int num2 = 100;
							if (text.Contains(":"))
							{
								num2 = int.Parse(text.Split(':', StringSplitOptions.None)[1]);
							}
							if (Stat.Random(1, 100) <= num2)
							{
								foreach (Location2D location2D12 in A.EnumerateLocations())
								{
									Cell cell6 = Z.GetCell(location2D12);
									Predicate<Cell> predicate;
									if ((predicate = <>9__9) == null)
									{
										predicate = (<>9__9 = delegate(Cell c)
										{
											Predicate<GameObject> predicate3;
											if ((predicate3 = <>9__10) == null)
											{
												predicate3 = (<>9__10 = (GameObject o) => o.GetIntProperty("_populationgroup", -1) == group);
											}
											return c.HasObject(predicate3);
										});
									}
									if (cell6.AnyLocalAdjacentCell(predicate) && !ZoneBuilderSandbox.Points.Contains(location2D12))
									{
										ZoneBuilderSandbox.Points.Add(location2D12);
									}
								}
							}
							bAllowCaching = false;
						}
						else if (text.StartsWith("Adjacent"))
						{
							int num3 = 100;
							if (text.Contains(":"))
							{
								num3 = int.Parse(text.Split(':', StringSplitOptions.None)[1]);
							}
							if (Stat.Random(1, 100) <= num3)
							{
								foreach (Location2D location2D13 in A.EnumerateLocations())
								{
									if (Z.GetCell(location2D13).HasObjectWithBlueprint(gameObject.Blueprint))
									{
										foreach (Cell cell7 in Z.GetCell(location2D13).GetLocalCardinalAdjacentCells())
										{
											if (!cell7.HasObjectWithBlueprint(gameObject.Blueprint) && !ZoneBuilderSandbox.Points.Contains(cell7.Location))
											{
												ZoneBuilderSandbox.Points.Add(cell7.Location);
											}
										}
									}
								}
							}
							bAllowCaching = false;
						}
						else
						{
							if (text.StartsWith("LivesOnWalls"))
							{
								flag3 = true;
								foreach (Location2D location2D14 in A.EnumerateLocations())
								{
									foreach (Cell cell8 in Z.GetCell(location2D14).GetLocalAdjacentCells())
									{
										if (cell8.HasWall() && !cell8.HasCombatObject(false) && !ZoneBuilderSandbox.Points.Contains(cell8.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell8.Location);
										}
									}
								}
								if (ZoneBuilderSandbox.Points.Count == 0)
								{
									foreach (Cell cell9 in Z.GetPassableCells(null))
									{
										Location2D location = cell9.Location;
										foreach (Cell cell10 in Z.GetCell(location).GetLocalAdjacentCells())
										{
											if (cell10.HasWall() && !cell10.HasCombatObject(false) && !ZoneBuilderSandbox.Points.Contains(cell10.Location))
											{
												ZoneBuilderSandbox.Points.Add(cell10.Location);
											}
										}
									}
								}
								if (ZoneBuilderSandbox.Points.Count == 0)
								{
									foreach (Cell cell11 in Z.GetReachableCells())
									{
										Location2D location2 = cell11.Location;
										foreach (Cell cell12 in Z.GetCell(location2).GetLocalAdjacentCells())
										{
											if (cell12.HasWall() && !cell12.HasCombatObject(false) && !ZoneBuilderSandbox.Points.Contains(cell12.Location))
											{
												ZoneBuilderSandbox.Points.Add(cell12.Location);
											}
										}
									}
								}
								if (ZoneBuilderSandbox.Points.Count != 0)
								{
									goto IL_12E4;
								}
								using (List<Cell>.Enumerator enumerator4 = Z.GetEmptyCells(null, null).GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										Cell cell13 = enumerator4.Current;
										Location2D location3 = cell13.Location;
										foreach (Cell cell14 in Z.GetCell(location3).GetLocalAdjacentCells())
										{
											if (cell14.HasWall() && !cell14.HasCombatObject(false) && !ZoneBuilderSandbox.Points.Contains(cell14.Location))
											{
												ZoneBuilderSandbox.Points.Add(cell14.Location);
											}
										}
									}
									goto IL_12E4;
								}
							}
							if (text == "InsideCorner")
							{
								foreach (Location2D location2D15 in A.EnumerateLocations())
								{
									Cell cell15 = Z.GetCell(location2D15);
									if (!cell15.HasWall() && ((cell15.HasWallInDirection("W") && cell15.HasWallInDirection("N") && cell15.HasWallInDirection("NW")) || (cell15.HasWallInDirection("N") && cell15.HasWallInDirection("E") && cell15.HasWallInDirection("NE")) || (cell15.HasWallInDirection("S") && cell15.HasWallInDirection("E") && cell15.HasWallInDirection("SE")) || (cell15.HasWallInDirection("S") && cell15.HasWallInDirection("W") && cell15.HasWallInDirection("SW"))))
									{
										ZoneBuilderSandbox.Points.Add(location2D15);
									}
								}
								if (ZoneBuilderSandbox.Points.Count != 0)
								{
									goto IL_12E4;
								}
								using (IEnumerator<Location2D> enumerator2 = A.EnumerateLocations().GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Location2D location2D16 = enumerator2.Current;
										if (Z.GetCell(location2D16).GetLocalAdjacentCells().Any<Cell>((Cell c) => c.HasWall()) && !ZoneBuilderSandbox.Points.Contains(location2D16))
										{
											ZoneBuilderSandbox.Points.Add(location2D16);
										}
									}
									goto IL_12E4;
								}
							}
							if (text.StartsWith("AlongWall:"))
							{
								string text10 = text.Split(':', StringSplitOptions.None)[1].ToString();
								using (IEnumerator<Location2D> enumerator2 = A.EnumerateLocations().GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Location2D location2D17 = enumerator2.Current;
										Cell cell16 = Z.GetCell(location2D17);
										Cell cellFromDirection = cell16.GetCellFromDirection(text10, true);
										if (cellFromDirection != null && cellFromDirection.HasWall() && !ZoneBuilderSandbox.Points.Contains(cell16.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell16.Location);
										}
									}
									goto IL_12E4;
								}
							}
							if (text.StartsWith("OutsideToThe:"))
							{
								string text11 = text.Split(':', StringSplitOptions.None)[1].ToString();
								using (IEnumerator<Location2D> enumerator2 = A.EnumerateLocations().GetEnumerator())
								{
									IL_1132:
									while (enumerator2.MoveNext())
									{
										Location2D location2D18 = enumerator2.Current;
										Cell cell17 = Z.GetCell(location2D18);
										for (Cell cell18 = cell17.GetCellFromDirection(text11, true); cell18 != null; cell18 = cell18.GetCellFromDirection(text11, true))
										{
											if (cell18.HasWall())
											{
												goto IL_1132;
											}
										}
										if (!ZoneBuilderSandbox.Points.Contains(cell17.Location))
										{
											ZoneBuilderSandbox.Points.Add(cell17.Location);
										}
									}
									goto IL_12E4;
								}
							}
							if (text == "AlongWall")
							{
								using (IEnumerator<Location2D> enumerator2 = A.EnumerateLocations().GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Location2D location2D19 = enumerator2.Current;
										if (Z.GetCell(location2D19).GetLocalAdjacentCells().Any<Cell>((Cell c) => c.HasWall()) && !ZoneBuilderSandbox.Points.Contains(location2D19))
										{
											ZoneBuilderSandbox.Points.Add(location2D19);
										}
									}
									goto IL_12E4;
								}
							}
							if (text.StartsWith("Center"))
							{
								ZoneBuilderSandbox.Points.Add(A.GetCenter());
							}
							else if (text.StartsWith("RadialFromCenter:"))
							{
								int num4 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
								List<Location2D> radialPoints = A.GetCenter().GetRadialPoints(num4);
								for (int j = 0; j < radialPoints.Count; j++)
								{
									if (A.PointIn(radialPoints[j]) && !A.EnumerateBorderLocations().Contains(radialPoints[j]))
									{
										ZoneBuilderSandbox.Points.Add(radialPoints[j]);
									}
								}
							}
							else if (text.StartsWith("Nonborder"))
							{
								ZoneBuilderSandbox.Points.AddRange(A.EnumerateNonBorderLocations());
							}
							else if (text.StartsWith("Border"))
							{
								ZoneBuilderSandbox.Points.AddRange(A.EnumerateBorderLocations());
							}
							else if (text.StartsWith("Any"))
							{
								ZoneBuilderSandbox.Points.AddRange(A.EnumerateLocations());
							}
						}
						IL_12E4:
						if (!flag3)
						{
							List<Location2D> points = ZoneBuilderSandbox.Points;
							Predicate<Location2D> predicate2;
							if ((predicate2 = <>9__7) == null)
							{
								predicate2 = (<>9__7 = (Location2D l) => !Z.GetCell(l).IsEmpty() || Z.GetCell(l).HasObject(gameObject.Blueprint));
							}
							points.RemoveAll(predicate2);
						}
					}
					if (bAllowCaching && !ZoneBuilderSandbox.ObjectPlacementCache.ContainsKey(text))
					{
						ZoneBuilderSandbox.ObjectPlacementCache.Add(text, ZoneBuilderSandbox.Points);
					}
					foreach (Location2D location2D20 in ZoneBuilderSandbox.Points)
					{
						if (ZoneBuilderSandbox.hintFieldtotals.ContainsKey(location2D20))
						{
							Dictionary<Location2D, int> dictionary = ZoneBuilderSandbox.hintFieldtotals;
							Location2D location2D21 = location2D20;
							int num5 = dictionary[location2D21];
							dictionary[location2D21] = num5 + 1;
						}
						else
						{
							ZoneBuilderSandbox.hintFieldtotals.Add(location2D20, 1);
						}
						if (ZoneBuilderSandbox.hintFieldtotals[location2D20] > num)
						{
							num = ZoneBuilderSandbox.hintFieldtotals[location2D20];
							ZoneBuilderSandbox.workingSet.Clear();
						}
						if (ZoneBuilderSandbox.hintFieldtotals[location2D20] >= num)
						{
							ZoneBuilderSandbox.workingSet.Add(location2D20);
						}
					}
				}
			}
			if (ZoneBuilderSandbox.workingSet.Count == 0)
			{
				ZoneBuilderSandbox.workingSet.AddRange(from l in A.EnumerateLocations()
					where Z.GetCell(l).IsReachable() && Z.GetCell(l).IsEmpty()
					select l);
				if (ZoneBuilderSandbox.workingSet.Count == 0)
				{
					ZoneBuilderSandbox.workingSet.AddRange(from l in A.EnumerateLocations()
						where Z.GetCell(l).IsEmpty()
						select l);
				}
				bAllowCaching = false;
			}
			if (gameObject.Brain != null)
			{
				if (gameObject.Brain.LivesOnWalls)
				{
					ZoneBuilderSandbox.workingSet.RemoveAll((Location2D p) => !Z.GetCell(p).HasWall());
				}
				if (gameObject.Brain.Aquatic)
				{
					ZoneBuilderSandbox.workingSet.RemoveAll((Location2D p) => !Z.GetCell(p).HasOpenLiquidVolume() || Z.GetCell(p).HasBridge());
				}
			}
			if (flag)
			{
				ZoneBuilderSandbox.workingSet.RemoveAll((Location2D l) => Z.GetCell(l).HasRealObject());
			}
			else
			{
				ZoneBuilderSandbox.workingSet.RemoveAll((Location2D l) => Z.GetCell(l).HasSpawnBlocker());
			}
			foreach (ZoneConnection zoneConnection in Z.EnumerateConnections())
			{
				ZoneBuilderSandbox.workingSet.Remove(zoneConnection.Loc2D);
			}
			if (gameObject.IsCombatObject())
			{
				ZoneBuilderSandbox.workingSet.RemoveAll((Location2D p) => Z.GetCell(p).HasCombatObject(false));
			}
			Location2D location2D22 = ZoneBuilderSandbox.workingSet.GetRandomElement<Location2D>(null);
			if (location2D22 == null)
			{
				Cell cell19 = (from ce in Z.GetCells()
					where ce.IsEmpty()
					select ce).GetRandomElement<Cell>(null);
				if (cell19 == null)
				{
					cell19 = Z.GetRandomCell(0);
				}
				if (!gameObject.IsImportant() && gameObject.HasTagOrProperty("PlacementIsSkippable") && cell19.HasObjectWithTagOrProperty("PlacementIsSkippable"))
				{
					return true;
				}
				location2D22 = cell19.Location;
			}
			if (gameObject.isFurniture() && Z.GetCell(location2D22).HasFurniture())
			{
				Cell closestPassableCell = Z.GetCell(location2D22).getClosestPassableCell((Cell c) => c != null && !c.HasFurniture());
				if (closestPassableCell != null)
				{
					closestPassableCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			else if (gameObject.IsCombatObject() && Z.GetCell(location2D22).HasCombatObject(false))
			{
				Z.GetCell(location2D22).getClosestPassableCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			else
			{
				Z.GetCell(location2D22).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			if (Options.GetOption("OptionDrawPopulationHintMaps", "") == "Yes")
			{
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				for (int k = 0; k < 80; k++)
				{
					for (int m = 0; m < 25; m++)
					{
						scrapBuffer.Goto(k, m);
						if (k == location2D22.X && m == location2D22.Y)
						{
							scrapBuffer.Write("&CX", true, false, false, null, -1);
						}
						else if (ZoneBuilderSandbox.hintFieldtotals.ContainsKey(Location2D.Get(k, m)))
						{
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 1)
							{
								scrapBuffer.Write("&R1", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 2)
							{
								scrapBuffer.Write("&O2", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 3)
							{
								scrapBuffer.Write("&W3", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 4)
							{
								scrapBuffer.Write("&G4", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 5)
							{
								scrapBuffer.Write("&B5", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] == 6)
							{
								scrapBuffer.Write("&M6", true, false, false, null, -1);
							}
							if (ZoneBuilderSandbox.hintFieldtotals[Location2D.Get(k, m)] >= 7)
							{
								scrapBuffer.Write("&Y7", true, false, false, null, -1);
							}
						}
						else if (ZoneBuilderSandbox.workingSet.Contains(Location2D.Get(k, m)))
						{
							if (Z.GetCell(k, m).HasWall())
							{
								scrapBuffer.Write("&g#", true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Write("&g.", true, false, false, null, -1);
							}
						}
						else if (Z.GetCell(k, m).HasWall())
						{
							scrapBuffer.Write("&K#", true, false, false, null, -1);
						}
						else
						{
							scrapBuffer.Write("&K.", true, false, false, null, -1);
						}
					}
				}
				scrapBuffer.Draw();
				Keyboard.getch();
			}
			if (bAllowCaching)
			{
				foreach (KeyValuePair<string, List<Location2D>> keyValuePair in ZoneBuilderSandbox.ObjectPlacementCache)
				{
					keyValuePair.Value.Remove(location2D22);
				}
			}
			ZoneBuilderSandbox.workingSet.Clear();
			return true;
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x002AD728 File Offset: 0x002AB928
		public static bool PlaceObjectInRegion(Zone Z, ILocationArea A, string Blueprint, int p = 0, int n = 0, string Hint = null, string Builder = null, bool bAllowCaching = false, GameObject precreated = null, Func<string, GameObject> customFabricator = null)
		{
			return ZoneBuilderSandbox.PlaceObjectInArea(Z, A, (precreated != null) ? precreated : ZoneBuilderSandbox.ApplyHintsToObject((customFabricator == null) ? GameObject.Create(Blueprint) : customFabricator(Blueprint), Hint, p), p, n, Hint, Builder, bAllowCaching);
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x002AD760 File Offset: 0x002AB960
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, Rect2D Exclude, string Object, string Hint = null, int group = 0, Func<string, GameObject> customFabricator = null)
		{
			R = R.enforceVailidity(R);
			List<Location2D> list = new List<Location2D>(Math.Max(1, R.Area - Exclude.Area));
			for (int i = R.y1; i <= R.y2; i++)
			{
				for (int j = R.x1; j <= R.x2; j++)
				{
					if (!Exclude.PointIn(Location2D.Get(j, i)))
					{
						list.Add(Location2D.Get(j, i));
					}
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), (customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), group, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x002AD804 File Offset: 0x002ABA04
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, Rect2D Exclude, string Object, string Hint, Action<GameObject> BeforeObjectCreated, Func<string, GameObject> customFabricator = null)
		{
			List<Location2D> list = new List<Location2D>(Math.Max(1, R.Area - Exclude.Area));
			for (int i = R.y1; i <= R.y2; i++)
			{
				for (int j = R.x1; j <= R.x2; j++)
				{
					if (!Exclude.PointIn(Location2D.Get(j, i)))
					{
						list.Add(Location2D.Get(j, i));
					}
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), (customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), 0, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x002AD8A0 File Offset: 0x002ABAA0
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, Rect2D Exclude, GameObject Object, string Hint = null)
		{
			List<Location2D> list = new List<Location2D>(R.Area - Exclude.Area);
			for (int i = R.y1; i <= R.y2; i++)
			{
				for (int j = R.x1; j <= R.x2; j++)
				{
					if (!Exclude.PointIn(Location2D.Get(j, i)))
					{
						list.Add(Location2D.Get(j, i));
					}
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), Object, 0, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x002AD920 File Offset: 0x002ABB20
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, string Object, string Hint = null, int group = 0, Func<string, GameObject> customFabricator = null)
		{
			if (R.Area <= 0)
			{
				ZoneBuilderSandbox.PlaceObject(ZoneBuilderSandbox.ApplyHintsToObject((customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), Hint, group), Z, null);
				return false;
			}
			List<Location2D> list = new List<Location2D>(R.Area);
			for (int i = R.x1; i <= R.x2; i++)
			{
				for (int j = R.y1; j <= R.y2; j++)
				{
					list.Add(Location2D.Get(i, j));
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), (customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), group, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x002AD9CC File Offset: 0x002ABBCC
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, string Object, string Hint, Action<GameObject> BeforeObjectCreated, Func<string, GameObject> customFabricator = null)
		{
			if (R.Area <= 0)
			{
				ZoneBuilderSandbox.PlaceObject(ZoneBuilderSandbox.ApplyHintsToObject((customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), Hint, 0), Z, null);
				return false;
			}
			List<Location2D> list = new List<Location2D>(R.Area);
			for (int i = R.x1; i <= R.x2; i++)
			{
				for (int j = R.y1; j <= R.y2; j++)
				{
					list.Add(Location2D.Get(i, j));
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), (customFabricator == null) ? GameObject.Create(Object) : customFabricator(Object), 0, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x002ADA78 File Offset: 0x002ABC78
		public static bool PlaceObjectInRect(Zone Z, Rect2D R, GameObject Object, string Hint = null)
		{
			R = R.enforceVailidity(R);
			List<Location2D> list = new List<Location2D>(R.Area);
			for (int i = R.y1; i <= R.y2; i++)
			{
				for (int j = R.x1; j <= R.x2; j++)
				{
					list.Add(Location2D.Get(j, i));
				}
			}
			ZoneBuilderSandbox.PlaceObjectInArea(Z, new LocationList(list), Object, 0, 0, Hint, null, false);
			return false;
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x002ADAEC File Offset: 0x002ABCEC
		public static void PlacePrefab(Zone Z, string mapName, ZoneBuilderSandbox.PlacePrefabAlign alignment, string ChunkHint = null, Action<Cell> PreAction = null, Action<Cell> PostAction = null, Func<string, Cell, bool> ShouldPlace = null, Func<string, Cell, string> Replace = null, Action<string, Cell> BeforePlacement = null, Action<string, Cell> AfterPlacement = null)
		{
			MapFile mapFile = MapFile.Resolve(mapName, true);
			Cell cell = null;
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.NW)
			{
				cell = Z.GetCell(0, 0);
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.N)
			{
				cell = Z.GetCell(Z.Width / 2 - mapFile.RightmostObject() / 2, 0);
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.NE)
			{
				cell = Z.GetCell(Z.Width - mapFile.RightmostObject(), 0);
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.E)
			{
				cell = Z.GetCell(Z.Width - mapFile.RightmostObject(), Z.Height / 2 - mapFile.BottommostObject() / 2);
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.SE)
			{
				cell = Z.GetCell(Z.Width - mapFile.RightmostObject(), Z.Height - mapFile.BottommostObject());
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.S)
			{
				cell = Z.GetCell(Z.Width / 2 - mapFile.RightmostObject() / 2, Z.Height - mapFile.BottommostObject());
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.SW)
			{
				cell = Z.GetCell(0, Z.Height - mapFile.BottommostObject());
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.W)
			{
				cell = Z.GetCell(0, Z.Height / 2 - mapFile.BottommostObject() / 2);
			}
			if (alignment == ZoneBuilderSandbox.PlacePrefabAlign.Center)
			{
				cell = Z.GetCell(Z.Width / 2 - mapFile.RightmostObject() / 2, Z.Height / 2 - mapFile.BottommostObject() / 2);
			}
			ZoneBuilderSandbox.PlacePrefab(Z, mapName, cell, 0, ChunkHint, PreAction, PostAction, ShouldPlace, Replace, BeforePlacement, AfterPlacement);
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x002ADC38 File Offset: 0x002ABE38
		public static void PlacePrefab(Zone Z, string mapName, Cell upperLeft, int Padding = 0, string ChunkHint = null, Action<Cell> PreAction = null, Action<Cell> PostAction = null, Func<string, Cell, bool> ShouldPlace = null, Func<string, Cell, string> Replace = null, Action<string, Cell> BeforePlacement = null, Action<string, Cell> AfterPlacement = null)
		{
			MapFile mapFile = MapFile.Resolve(mapName, true);
			Point2D pos2D = upperLeft.Pos2D;
			int num = mapFile.RightmostObject() + 1;
			int num2 = mapFile.BottommostObject() + 1;
			if (pos2D.x + num + Padding >= Z.Width)
			{
				pos2D.x -= pos2D.x + num + Padding - Z.Width;
			}
			if (pos2D.y + num2 + Padding >= Z.Height)
			{
				pos2D.y -= pos2D.y + num2 + Padding - Z.Height;
			}
			if (ChunkHint != null && ChunkHint == "Center")
			{
				pos2D.x = Z.Width / 2 - num / 2;
				pos2D.y = Z.Height / 2 - num2 / 2;
			}
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					Cell cell = Z.GetCell(pos2D.x + j, pos2D.y + i);
					if (cell != null)
					{
						mapFile.Cells[j, i].ApplyTo(cell, false, PreAction, PostAction, ShouldPlace, Replace, null, BeforePlacement, AfterPlacement);
					}
				}
			}
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x002ADD5C File Offset: 0x002ABF5C
		public void ClearRect(Zone Z, Rect2D R)
		{
			for (int i = R.x1; i <= R.x2; i++)
			{
				for (int j = R.y1; j <= R.y2; j++)
				{
					Cell cell = Z.GetCell(i, j);
					if (cell != null)
					{
						cell.Clear(null, false, false, null);
					}
				}
			}
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x002ADDB0 File Offset: 0x002ABFB0
		public void FillRect(Zone Z, Rect2D R, string Blueprint)
		{
			for (int i = R.x1; i <= R.x2; i++)
			{
				for (int j = R.y1; j <= R.y2; j++)
				{
					Cell cell = Z.GetCell(i, j);
					if (cell != null)
					{
						cell.AddObject(Blueprint, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x002ADE02 File Offset: 0x002AC002
		public static void PlaceObjectOnRect(Zone Z, string Blueprint, Rect2D R, bool bClear = false)
		{
			ZoneBuilderSandbox.PlaceObjectOnRect(Z, GameObjectFactory.Factory.CreateObject(Blueprint), R, bClear);
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x002ADE18 File Offset: 0x002AC018
		public static void PlaceObjectOnRect(Zone Z, GameObject objectToCopy, Rect2D R, bool bClear = false)
		{
			if (Z == null)
			{
				MetricsManager.LogError("zone missing");
				return;
			}
			if (objectToCopy == null)
			{
				MetricsManager.LogError("object missing");
				return;
			}
			int num = ((R.DoorDirection == "W" || R.DoorDirection == "E") ? 0 : 1);
			for (int i = R.x1 + num; i <= R.x2 - num; i++)
			{
				if (R.Pinch > -1 && ((R.DoorDirection == "W" && i - R.x1 <= R.Pinch) || (R.DoorDirection == "E" && i - R.x1 >= R.Pinch)))
				{
					Cell cell = Z.GetCellChecked(i, R.y1 + 1);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					cell = Z.GetCellChecked(i, R.y2 - 1);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					if (i - R.x1 == R.Pinch)
					{
						cell = Z.GetCellChecked(i, R.y1);
						if (cell != null)
						{
							cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
						}
						cell = Z.GetCellChecked(i, R.y2);
						if (cell != null)
						{
							cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
						}
					}
				}
				else
				{
					Cell cell = Z.GetCellChecked(i, R.y1);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					cell = Z.GetCellChecked(i, R.y2);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
				}
			}
			num = ((R.DoorDirection == "W" || R.DoorDirection == "E") ? 1 : 0);
			for (int j = R.y1 + num; j <= R.y2 - num; j++)
			{
				if (R.Pinch > -1 && ((R.DoorDirection == "S" && j - R.y1 <= R.Pinch) || (R.DoorDirection == "N" && j - R.y1 >= R.Pinch)))
				{
					Cell cell = Z.GetCellChecked(R.x1 + 1, j);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					cell = Z.GetCellChecked(R.x2 - 1, j);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					if (j - R.y1 == R.Pinch)
					{
						cell = Z.GetCellChecked(R.x1, j);
						if (cell != null)
						{
							cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
						}
						cell = Z.GetCellChecked(R.x2, j);
						if (cell != null)
						{
							cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
						}
					}
				}
				else
				{
					Cell cell = Z.GetCellChecked(R.x1, j);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
					cell = Z.GetCellChecked(R.x2, j);
					if (cell != null)
					{
						cell.ClearAndAddObject(objectToCopy.DeepCopy(false, false, null), bClear, null);
					}
				}
			}
		}

		// Token: 0x060071FC RID: 29180 RVA: 0x002AE158 File Offset: 0x002AC358
		public static InfluenceMap GenerateInfluenceMapNRegions(Zone Z, List<Point> AdditionalSeeds, InfluenceMapSeedStrategy SeedStrategy, int NumberOfRegions, List<Location2D> AdditionalWalls = null, bool bDraw = false, bool bAddExtraConnectionRegions = true)
		{
			InfluenceMap influenceMap = new InfluenceMap(Z.Width, Z.Height);
			influenceMap.bDraw = bDraw;
			Z.SetInfluenceMapWalls(influenceMap.Walls);
			if (AdditionalWalls != null)
			{
				foreach (Location2D location2D in AdditionalWalls)
				{
					influenceMap.Walls[location2D.X, location2D.Y] = 1;
				}
			}
			if (AdditionalSeeds != null)
			{
				foreach (Point point in AdditionalSeeds)
				{
					influenceMap.AddSeed(point.X, point.Y, false);
				}
			}
			if (bAddExtraConnectionRegions)
			{
				foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
				{
					if (!Z.GetCell(zoneConnection.X, zoneConnection.Y).IsSolid())
					{
						influenceMap.AddSeed(zoneConnection.X, zoneConnection.Y, false);
					}
				}
				foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
				{
					if (cachedZoneConnection.TargetDirection == "-" && !Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).IsSolid())
					{
						influenceMap.AddSeed(cachedZoneConnection.X, cachedZoneConnection.Y, false);
					}
				}
			}
			if (influenceMap.Seeds.Count > 0)
			{
				influenceMap.Recalculate(false);
				List<Cell> cells = Z.GetCells();
				Algorithms.RandomShuffleInPlace<Cell>(cells, Stat.Rand);
				foreach (Cell cell in cells)
				{
					if (influenceMap.GetSeedAt(Location2D.Get(cell.X, cell.Y)) == -1 && cell.IsEmpty())
					{
						influenceMap.AddSeed(cell.X, cell.Y, true);
						influenceMap.Recalculate(false);
					}
				}
			}
			influenceMap.bDraw = bDraw;
			for (int i = 0; i < NumberOfRegions; i++)
			{
				if (SeedStrategy == InfluenceMapSeedStrategy.RandomPoint)
				{
					influenceMap.AddSeedAtTrueRandom(0);
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.LargestRegion)
				{
					influenceMap.AddSeedAtMaximaInLargestSeed(false);
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.FurthestPoint)
				{
					influenceMap.AddSeedAtMaxima();
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.RandomPointFurtherThan4)
				{
					influenceMap.AddSeedAtRandom(4);
				}
			}
			influenceMap.bDraw = bDraw;
			influenceMap.Recalculate(false);
			return influenceMap;
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x002AE42C File Offset: 0x002AC62C
		public static InfluenceMap GenerateInfluenceMap(Zone Z, List<Point> AdditionalSeeds, InfluenceMapSeedStrategy SeedStrategy, int MaxRegionSize, List<Location2D> AdditionalWalls = null, bool bDraw = false, Func<Cell, int> wallGenerator = null)
		{
			InfluenceMap influenceMap = new InfluenceMap(Z.Width, Z.Height);
			influenceMap.bDraw = bDraw;
			if (wallGenerator != null)
			{
				for (int i = 0; i < Z.Width; i++)
				{
					for (int j = 0; j < Z.Height; j++)
					{
						influenceMap.Walls[i, j] = wallGenerator(Z.GetCell(i, j));
					}
				}
			}
			else
			{
				Z.SetInfluenceMapWalls(influenceMap.Walls);
			}
			if (AdditionalWalls != null)
			{
				foreach (Location2D location2D in AdditionalWalls)
				{
					influenceMap.Walls[location2D.X, location2D.Y] = 1;
				}
			}
			if (AdditionalSeeds != null)
			{
				foreach (Point point in AdditionalSeeds)
				{
					influenceMap.AddSeed(point.X, point.Y, false);
				}
			}
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (!Z.GetCell(zoneConnection.X, zoneConnection.Y).IsSolid())
				{
					influenceMap.AddSeed(zoneConnection.X, zoneConnection.Y, false);
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && !Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).IsSolid())
				{
					influenceMap.AddSeed(cachedZoneConnection.X, cachedZoneConnection.Y, false);
				}
			}
			if (influenceMap.Seeds.Count > 0)
			{
				influenceMap.Recalculate(false);
				List<Cell> cells = Z.GetCells();
				Algorithms.RandomShuffleInPlace<Cell>(cells, Stat.Rand);
				foreach (Cell cell in cells)
				{
					if (influenceMap.GetSeedAt(Location2D.Get(cell.X, cell.Y)) == -1 && cell.IsEmpty() && influenceMap.Walls[cell.X, cell.Y] == 0)
					{
						influenceMap.AddSeed(cell.X, cell.Y, true);
						influenceMap.Recalculate(false);
					}
				}
			}
			influenceMap.bDraw = bDraw;
			while (influenceMap.Seeds.Count == 0 || influenceMap.LargestSize() > MaxRegionSize)
			{
				if (SeedStrategy == InfluenceMapSeedStrategy.LargestRegion)
				{
					influenceMap.AddSeedAtMaximaInLargestSeed(false);
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.FurthestPoint)
				{
					influenceMap.AddSeedAtMaxima();
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.RandomPointFurtherThan4)
				{
					influenceMap.AddSeedAtRandom(5);
				}
				if (SeedStrategy == InfluenceMapSeedStrategy.RandomPointFurtherThan1)
				{
					influenceMap.AddSeedAtRandom(0);
				}
			}
			influenceMap.bDraw = bDraw;
			influenceMap.Recalculate(bDraw);
			return influenceMap;
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x002AE768 File Offset: 0x002AC968
		public static GameObject CreateObject(string Blueprint)
		{
			return GameObject.Create(Blueprint);
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x002AE770 File Offset: 0x002AC970
		public static int GetLevelOfObject(string Blueprint)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[Blueprint];
			if (gameObjectBlueprint.HasStat("Level"))
			{
				return gameObjectBlueprint.GetStat("Level", null).Value;
			}
			return 0;
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x002AE7B0 File Offset: 0x002AC9B0
		public static void PlaceParty(List<GameObject> Party, Zone Z, int Radius = -1)
		{
			if (Radius == -1)
			{
				Radius = Math.Min(4, Party.Count / 2);
			}
			for (int i = 0; i < 500; i++)
			{
				int num = Stat.Random(0, Z.Width - 1);
				int num2 = Stat.Random(0, Z.Height - 1);
				int num3 = 0;
				Rect2D rect2D = new Rect2D(num - Radius, num2 - Radius, num + Radius, num2 + Radius);
				rect2D = rect2D.Clamp(0, 0, Z.Width - 1, Z.Height - 1);
				List<Location2D> list = new List<Location2D>(rect2D.Area);
				for (int j = rect2D.y1; j <= rect2D.y2; j++)
				{
					for (int k = rect2D.x1; k <= rect2D.x2; k++)
					{
						Location2D location2D = Location2D.Get(k, j);
						if (Z.GetCell(location2D).IsEmpty())
						{
							num3++;
						}
						list.Add(location2D);
					}
				}
				if (num3 >= Party.Count)
				{
					LocationList locationList = new LocationList(list);
					foreach (GameObject gameObject in Party)
					{
						ZoneBuilderSandbox.PlaceObjectInArea(Z, locationList, gameObject, 0, 0, null, null, false);
					}
					return;
				}
			}
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x002AE904 File Offset: 0x002ACB04
		public static bool PlaceObject(string GO, Zone Z)
		{
			return ZoneBuilderSandbox.PlaceObject(GameObject.Create(GO), Z, null);
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x002AE913 File Offset: 0x002ACB13
		public static bool PlaceObject(GameObject GO, Zone Z, string Hints = null)
		{
			return ZoneBuilderSandbox.PlaceObjectInArea(Z, Z.area, GO, 0, 0, Hints, null, false);
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x002AE928 File Offset: 0x002ACB28
		public static void PlaceEncounter(List<GameObject> GOs, Zone Z)
		{
			foreach (GameObject gameObject in GOs)
			{
				ZoneBuilderSandbox.PlaceObject(gameObject, Z, null);
			}
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x002AE978 File Offset: 0x002ACB78
		public bool IsEmpty(Location2D loc)
		{
			return this.zone.GetCell(loc).IsEmptyForPopulation();
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x002AE98C File Offset: 0x002ACB8C
		public bool PlaceObjectInBuilding(GameObject obj, PopulationLayout building, string Hint = null, Predicate<Location2D> test = null)
		{
			ZoneBuilderSandbox.placementCells.Clear();
			if (string.IsNullOrEmpty(Hint))
			{
				Hint = obj.GetPropertyOrTag("PlacementHint", "");
			}
			if (building == null)
			{
				ZoneBuilderSandbox.PlaceObject(obj, this.zone, null);
				return true;
			}
			if (Hint == "")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
			}
			else if (Hint == "InsideCorner")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.insideCorner.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
				ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
			}
			else if (Hint == "OutsideCorner")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.outsideCorner.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
				ZoneBuilderSandbox.placementCells.AddRange(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
			}
			else if (Hint == "AlongInsideWall")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
			}
			else if (Hint == "AlongOutsideWall")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
			}
			else if (Hint == "AlongWall")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).Union<Location2D>(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty))).ToList<Location2D>()
					.ShuffleInPlace<Location2D>(null));
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).Union<Location2D>(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty))).ToList<Location2D>()
					.ShuffleInPlace<Location2D>(null));
			}
			else if (Hint == "Outside" || Hint == "Exterior")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
			}
			else if (Hint == "Inside" || Hint == "Interior")
			{
				ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
				ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
			}
			if (test != null)
			{
				ZoneBuilderSandbox.placementCells.RemoveAll((Location2D l) => !test(l));
			}
			if (ZoneBuilderSandbox.placementCells.Count > 0)
			{
				this.zone.GetCell(ZoneBuilderSandbox.placementCells[0]).AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return true;
			}
			ZoneBuilderSandbox.PlaceObject(obj, this.zone, null);
			return false;
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x002AEEB0 File Offset: 0x002AD0B0
		public int PlaceObjectInBuilding(PopulationResult result, PopulationLayout building, Predicate<Location2D> test = null)
		{
			int num = 0;
			Func<Location2D, bool> <>9__0;
			for (int i = 0; i < result.Number; i++)
			{
				ZoneBuilderSandbox.placementCells.Clear();
				string text = result.Hint;
				if (string.IsNullOrEmpty(text))
				{
					GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(result.Blueprint);
					if (blueprint != null)
					{
						text = blueprint.GetTag("PlacementHint", "");
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
					ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
					ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				}
				else if (text == "InsideCorner")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.insideCorner.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
					ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
					ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				}
				else if (text == "OutsideCorner")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.outsideCorner.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
					ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
					ZoneBuilderSandbox.placementCells.AddRange(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
					ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				}
				else if (text == "AlongInsideWall")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				}
				else if (text == "AlongOutsideWall")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
					ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).ToList<Location2D>().ShuffleInPlace<Location2D>(null));
				}
				else if (text == "AlongWall")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.insideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).Union<Location2D>(building.outsideWall.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty))).ToList<Location2D>()
						.ShuffleInPlace<Location2D>(null));
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)).Union<Location2D>(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty))).ToList<Location2D>()
						.ShuffleInPlace<Location2D>(null));
				}
				else if (text == "Outside" || text == "Exterior")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
					ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				}
				else if (text == "Inside" || text == "Interior")
				{
					ZoneBuilderSandbox.placementCells.AddRange(building.inside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
					ZoneBuilderSandbox.placementCells.ShuffleInPlace<Location2D>(null);
					ZoneBuilderSandbox.placementCells.AddRange(building.outside.Where<Location2D>(new Func<Location2D, bool>(this.IsEmpty)));
				}
				if (test != null && ZoneBuilderSandbox.placementCells.Count > 0)
				{
					IEnumerable<Location2D> enumerable = ZoneBuilderSandbox.placementCells;
					Func<Location2D, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Location2D l) => test(l));
					}
					ZoneBuilderSandbox.placementCells = enumerable.Where<Location2D>(func).ToList<Location2D>();
				}
				if (ZoneBuilderSandbox.placementCells.Count > 0)
				{
					this.zone.GetCell(ZoneBuilderSandbox.placementCells[0]).AddObject(result.Blueprint, null, null, null, null);
					num++;
				}
				else
				{
					ZoneBuilderSandbox.PlaceObject(GameObjectFactory.Factory.CreateObject(result.Blueprint), this.zone, null);
				}
			}
			return num;
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x002AF3A0 File Offset: 0x002AD5A0
		public static bool PlaceObject(Zone Z, InfluenceMapRegion R, GameObject obj, string Hint = null, string Builder = null, bool bAllowCaching = false)
		{
			ZoneBuilderSandbox.Points.Clear();
			bool flag = false;
			if (obj.Brain != null && obj.Brain.Aquatic)
			{
				Hint = "Aquatic";
			}
			else if (string.IsNullOrEmpty(Hint))
			{
				Hint = obj.GetPropertyOrTag("PlacementHint", "");
			}
			if (Hint.StartsWith("Aquatic"))
			{
				using (List<Location2D>.Enumerator enumerator = R.Cells.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Location2D location2D = enumerator.Current;
						if (Z.GetCell(location2D).HasWadingDepthLiquid() && !Z.GetCell(location2D).HasBridge())
						{
							ZoneBuilderSandbox.Points.Add(location2D);
						}
					}
					goto IL_09FA;
				}
			}
			if (Hint.StartsWith("AdjacentToBlueprint:"))
			{
				string text = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D2 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D2).HasObjectWithBlueprint(text))
					{
						foreach (Cell cell in Z.GetCell(location2D2).GetLocalCardinalAdjacentCells())
						{
							if (!cell.HasObjectWithBlueprint(text) && !ZoneBuilderSandbox.Points.Contains(cell.Location))
							{
								ZoneBuilderSandbox.Points.Add(cell.Location);
							}
						}
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				bAllowCaching = false;
			}
			else if (Hint.StartsWith("StackWithStartsWith:"))
			{
				string text2 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D3 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D3).HasObjectWithBlueprintStartsWith(text2) && !ZoneBuilderSandbox.Points.Contains(location2D3))
					{
						ZoneBuilderSandbox.Points.Add(location2D3);
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				flag = true;
			}
			else if (Hint.StartsWith("StackWith:"))
			{
				string text3 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D4 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D4).HasObjectWithBlueprint(text3) && !ZoneBuilderSandbox.Points.Contains(location2D4))
					{
						ZoneBuilderSandbox.Points.Add(location2D4);
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				flag = true;
			}
			else if (Hint.StartsWith("StackWithSelf") || Hint == "Stack")
			{
				foreach (Location2D location2D5 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D5).HasObjectWithBlueprint(obj.Blueprint) && !ZoneBuilderSandbox.Points.Contains(location2D5))
					{
						ZoneBuilderSandbox.Points.Add(location2D5);
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				flag = true;
			}
			else if (Hint.StartsWith("StackWithEndsWith:"))
			{
				string text4 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D6 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D6).HasObjectWithBlueprintEndsWith(text4) && !ZoneBuilderSandbox.Points.Contains(location2D6))
					{
						ZoneBuilderSandbox.Points.Add(location2D6);
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				flag = true;
			}
			else if (Hint.StartsWith("AdjacentToStartsWith:"))
			{
				string text5 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D7 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D7).HasObjectWithBlueprintStartsWith(text5))
					{
						foreach (Cell cell2 in Z.GetCell(location2D7).GetLocalCardinalAdjacentCells())
						{
							if (!cell2.HasObjectWithBlueprintStartsWith(text5) && !ZoneBuilderSandbox.Points.Contains(cell2.Location))
							{
								ZoneBuilderSandbox.Points.Add(cell2.Location);
							}
						}
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				bAllowCaching = false;
			}
			else if (Hint.StartsWith("AdjacentToEndsWith:"))
			{
				string text6 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D8 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D8).HasObjectWithBlueprintEndsWith(text6))
					{
						foreach (Cell cell3 in Z.GetCell(location2D8).GetLocalCardinalAdjacentCells())
						{
							if (!cell3.HasObjectWithBlueprintEndsWith(text6) && !ZoneBuilderSandbox.Points.Contains(cell3.Location))
							{
								ZoneBuilderSandbox.Points.Add(cell3.Location);
							}
						}
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				bAllowCaching = false;
			}
			else if (Hint.StartsWith("AdjacentToEndsWith:"))
			{
				string text7 = Hint.Split(':', StringSplitOptions.None)[1];
				foreach (Location2D location2D9 in R.NonBorderCells)
				{
					if (Z.GetCell(location2D9).HasObjectWithPropertyOrTag(text7))
					{
						foreach (Cell cell4 in Z.GetCell(location2D9).GetLocalCardinalAdjacentCells())
						{
							if (!cell4.HasObjectWithPropertyOrTag(text7) && !ZoneBuilderSandbox.Points.Contains(cell4.Location))
							{
								ZoneBuilderSandbox.Points.Add(cell4.Location);
							}
						}
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				bAllowCaching = true;
			}
			else if (Hint.StartsWith("Adjacent"))
			{
				foreach (Location2D location2D10 in R.Cells)
				{
					if (Z.GetCell(location2D10).HasObjectWithBlueprint(obj.Blueprint))
					{
						foreach (Cell cell5 in Z.GetCell(location2D10).GetLocalCardinalAdjacentCells())
						{
							if (!cell5.HasObjectWithBlueprint(obj.Blueprint) && !ZoneBuilderSandbox.Points.Contains(cell5.Location))
							{
								ZoneBuilderSandbox.Points.Add(cell5.Location);
							}
						}
					}
				}
				if (ZoneBuilderSandbox.Points.Count == 0)
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
				bAllowCaching = false;
			}
			else
			{
				if (Hint.StartsWith("LivesOnWalls"))
				{
					using (List<Location2D>.Enumerator enumerator = R.BorderCells.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Location2D location2D11 = enumerator.Current;
							foreach (Cell cell6 in Z.GetCell(location2D11).GetLocalAdjacentCells())
							{
								if (cell6.HasWall() && !ZoneBuilderSandbox.Points.Contains(cell6.Location))
								{
									ZoneBuilderSandbox.Points.Add(cell6.Location);
								}
							}
						}
						goto IL_09FA;
					}
				}
				if (Hint == "AlongWall")
				{
					using (List<Location2D>.Enumerator enumerator = R.Cells.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Location2D location2D12 = enumerator.Current;
							Cell cell7 = Z.GetCell(location2D12);
							if (cell7.HasWall())
							{
								cell7.ForeachLocalAdjacentCell(delegate(Cell AC)
								{
									if (!AC.HasWall() && !ZoneBuilderSandbox.Points.Contains(AC.Location))
									{
										ZoneBuilderSandbox.Points.Add(AC.Location);
									}
								});
							}
						}
						goto IL_09FA;
					}
				}
				if (Hint.StartsWith("Center"))
				{
					ZoneBuilderSandbox.Points.Add(R.Center);
				}
				else if (Hint.StartsWith("RadialFromCenter:"))
				{
					int num = Convert.ToInt32(Hint.Split(':', StringSplitOptions.None)[1]);
					List<Location2D> radialPoints = R.Center.GetRadialPoints(num);
					for (int i = 0; i < radialPoints.Count; i++)
					{
						if (R.PointIn(radialPoints[i]) && !R.BorderCells.Contains(radialPoints[i]))
						{
							ZoneBuilderSandbox.Points.Add(radialPoints[i]);
						}
					}
				}
				else
				{
					ZoneBuilderSandbox.Points.AddRange(R.NonBorderCells);
				}
			}
			IL_09FA:
			if (!flag)
			{
				ZoneBuilderSandbox.Points.RemoveAll((Location2D c) => !Z.GetCell(c).IsEmpty());
			}
			if (ZoneBuilderSandbox.Points.Count == 0)
			{
				if (!obj.IsImportant() && obj.HasTagOrProperty("DiscardInsteadOfGeneratingStacked"))
				{
					return true;
				}
				Cell[] emptyCellsShuffled = Z.GetEmptyCellsShuffled();
				if (emptyCellsShuffled.Count<Cell>() > 0)
				{
					emptyCellsShuffled[0].AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				else
				{
					List<Cell> list = new List<Cell>(Z.GetCells());
					list.ShuffleInPlace<Cell>(null);
					list[0].AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			else
			{
				ZoneBuilderSandbox.Points.ShuffleInPlace<Location2D>(null);
				Z.GetCell(ZoneBuilderSandbox.Points[0]).AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				if (bAllowCaching)
				{
					ZoneBuilderSandbox.Points.Remove(ZoneBuilderSandbox.Points[0]);
				}
				ZoneBuilderSandbox.Points.Clear();
			}
			return true;
		}

		// Token: 0x040034D4 RID: 13524
		protected Zone zone;

		// Token: 0x040034D5 RID: 13525
		protected static List<Location2D> placementCells = new List<Location2D>();

		// Token: 0x040034D6 RID: 13526
		protected static List<Location2D> Points = new List<Location2D>();

		// Token: 0x040034D7 RID: 13527
		private FastNoise simplexFastNoise;

		// Token: 0x040034D8 RID: 13528
		public static FastNoise pathNoise = new FastNoise(1337);

		// Token: 0x040034D9 RID: 13529
		[NonSerialized]
		public Dictionary<string, List<ColorOutputMap>> templates;

		// Token: 0x040034DA RID: 13530
		public static int currentpop = 0;

		// Token: 0x040034DB RID: 13531
		private static Dictionary<string, List<Location2D>> ObjectPlacementCache = new Dictionary<string, List<Location2D>>();

		// Token: 0x040034DC RID: 13532
		private static List<List<Location2D>> hintFields = new List<List<Location2D>>();

		// Token: 0x040034DD RID: 13533
		private static Dictionary<Location2D, int> hintFieldtotals = new Dictionary<Location2D, int>();

		// Token: 0x040034DE RID: 13534
		private static List<Location2D> workingSet = new List<Location2D>();

		// Token: 0x040034DF RID: 13535
		private static Dictionary<string, MethodInfo> templateApplicators = new Dictionary<string, MethodInfo>();

		// Token: 0x02001B62 RID: 7010
		public enum BridgeDirection
		{
			// Token: 0x04006CDF RID: 27871
			Horizontal,
			// Token: 0x04006CE0 RID: 27872
			Vertical,
			// Token: 0x04006CE1 RID: 27873
			Random
		}

		// Token: 0x02001B63 RID: 7011
		public enum PlacePrefabAlign
		{
			// Token: 0x04006CE3 RID: 27875
			NW,
			// Token: 0x04006CE4 RID: 27876
			N,
			// Token: 0x04006CE5 RID: 27877
			NE,
			// Token: 0x04006CE6 RID: 27878
			E,
			// Token: 0x04006CE7 RID: 27879
			SE,
			// Token: 0x04006CE8 RID: 27880
			S,
			// Token: 0x04006CE9 RID: 27881
			SW,
			// Token: 0x04006CEA RID: 27882
			W,
			// Token: 0x04006CEB RID: 27883
			Center
		}
	}
}
