using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A89 RID: 2697
	public class FungalJungle : ZoneBuilderSandbox
	{
		// Token: 0x06006E9A RID: 28314 RVA: 0x0025E930 File Offset: 0x0025CB30
		public static void FungalUpAZone(Zone Z, int biomeTier = -1, bool bBiome = false)
		{
			if (biomeTier == 0)
			{
				return;
			}
			float num = 1f;
			if (biomeTier == -1)
			{
				biomeTier = 3;
			}
			else if (biomeTier == 0)
			{
				num = 0f;
			}
			else if (biomeTier == 1)
			{
				num = 0.15f;
			}
			else if (biomeTier == 2)
			{
				num = 0.525f;
			}
			else if (biomeTier == 3)
			{
				num = 0.9f;
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMapNRegions(Z, null, InfluenceMapSeedStrategy.RandomPoint, biomeTier, null, false, false);
			NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 6, 80, 80, 4, 3, 0, 1, null);
			foreach (InfluenceMapRegion influenceMapRegion in influenceMap.Regions)
			{
				string text = (bBiome ? PopulationManager.RollOneFrom("MinorLiquidWeeps", null, null).Blueprint : PopulationManager.RollOneFrom("LiquidWeeps", null, null).Blueprint);
				Match match = Regex.Match(text, "^.*?(?=Lichen)");
				List<string> list;
				if (match != null && !string.IsNullOrEmpty(match.Value))
				{
					list = LiquidVolume.GetLiquidColors(match.Groups[0].Value);
				}
				else
				{
					list = new List<string>(new string[] { "y", "Y" });
				}
				if (Z.GetZoneProperty("relaxedbiomes", "") != "true")
				{
					GameObject gameObject = GameObject.Create(text);
					if (!bBiome)
					{
						gameObject.RemovePart<SecretObject>();
					}
					Z.GetCell(influenceMapRegion.Cells.GetRandomElement<Location2D>(null)).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				foreach (Location2D location2D in influenceMapRegion.Cells)
				{
					int x = location2D.X;
					int y = location2D.Y;
					if (noiseMap.Noise[x, y] > 1 && Z.GetCell(x, y).IsPassable(null, true))
					{
						if ((20f * num).in100())
						{
							if (!Z.GetCell(x, y).IsOccluding())
							{
								GameObject gameObject2 = GameObject.Create("Spotted Shagspook");
								string text2 = list.GetRandomElement<string>(null);
								string text3 = new List<string>(list).GetRandomElement<string>(null);
								gameObject2.Render.SetForegroundColor(text2);
								gameObject2.Render.DetailColor = text3;
								Z.GetCell(x, y).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if ((20f * num).in100())
						{
							if (!Z.GetCell(x, y).IsOccluding())
							{
								GameObject gameObject3 = GameObject.Create("Spotted Shagspook");
								string text2 = list.GetRandomElement<string>(null);
								string text3 = new List<string>(list).GetRandomElement<string>(null);
								gameObject3.Render.SetForegroundColor(text2);
								gameObject3.Render.DetailColor = text3;
								Z.GetCell(x, y).AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if ((30f * num).in100() && !Z.GetCell(x, y).IsOccluding())
						{
							GameObject gameObject4 = GameObject.Create("Dandy Cap");
							string text2 = list.GetRandomElement<string>(null);
							string text3 = new List<string>(list).GetRandomElement<string>(null);
							gameObject4.Render.SetForegroundColor(text2);
							gameObject4.Render.DetailColor = text3;
							Z.GetCell(x, y).AddObject(gameObject4, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			if (biomeTier == -1 && Z.Z == 10)
			{
				Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			}
			Z.GetCell(0, 0).AddObject("Mushroomy", null, null, null, null);
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x0025ED24 File Offset: 0x0025CF24
		public bool BuildZone(Zone Z)
		{
			Maze maze = RecursiveBacktrackerMaze.Generate((FungalJungle.LowerRight.x - FungalJungle.UpperLeft.x + 1) * 3, (FungalJungle.LowerRight.y - FungalJungle.UpperLeft.y + 1) * 3, false, "JungleMaze");
			if (!XRLCore.Core.Game.HasIntGameState("FungalJungleMazeSeed"))
			{
				XRLCore.Core.Game.SetIntGameState("FungalJungleMazeSeed", Stat.Random(0, 2147483646));
			}
			int intGameState = XRLCore.Core.Game.GetIntGameState("FungalJungleMazeSeed", 0);
			Stat.ReseedFrom("FungalJungle" + Z.ZoneID + Z.BuildTries.ToString(), false);
			int num = (Z.wX - FungalJungle.UpperLeft.x) * 3 + Z.X;
			int num2 = (Z.wY - FungalJungle.UpperLeft.y) * 3 + Z.Y;
			MazeCell mazeCell = maze.Cell[num, num2];
			int num3 = Z.wX * 3 + Z.X;
			int num4 = Z.wY * 3 + Z.Y;
			if (mazeCell.N)
			{
				Z.CacheZoneConnection("-", base.GetSeededRange(num4 + intGameState * Z.Z, 10, 70), 0, "RiverNorthMouth", null);
			}
			if (mazeCell.S)
			{
				Z.CacheZoneConnection("-", base.GetSeededRange(num4 + 1 + intGameState * Z.Z, 10, 70), 24, "RiverSouthMouth", null);
			}
			if (mazeCell.E)
			{
				Z.CacheZoneConnection("-", 79, base.GetSeededRange(num3 + 1 + intGameState * Z.Z, 5, 20), "RiverEastMouth", null);
			}
			if (mazeCell.W)
			{
				Z.CacheZoneConnection("-", 0, base.GetSeededRange(num3 + intGameState * Z.Z, 5, 20), "RiverWestMouth", null);
			}
			new RiverBuilder
			{
				Puddle = "ProteanDeepPool"
			}.BuildZone(Z);
			FungalJungle.FungalUpAZone(Z, -1, false);
			if (!this.Underground)
			{
				Z.ClearReachableMap();
				if (Z.BuildReachableMap(0, 0) < 400)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400329C RID: 12956
		public static Vector2i UpperLeft = new Vector2i(0, 0);

		// Token: 0x0400329D RID: 12957
		public static Vector2i Middle = new Vector2i(53, 17);

		// Token: 0x0400329E RID: 12958
		public static Vector2i LowerRight = new Vector2i(79, 24);

		// Token: 0x0400329F RID: 12959
		public bool Underground;

		// Token: 0x040032A0 RID: 12960
		public bool Outskirts;

		// Token: 0x040032A1 RID: 12961
		public List<Vector2i> HutPath = new List<Vector2i>();

		// Token: 0x040032A2 RID: 12962
		public List<string> HutPathDir = new List<string>();
	}
}
