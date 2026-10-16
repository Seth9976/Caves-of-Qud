using System;
using System.Collections.Generic;
using Genkit;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.ZoneBuilders;

namespace XRL
{
	// Token: 0x0200053D RID: 1341
	public class ZoneTemplate
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x0011BB24 File Offset: 0x00119D24
		public bool Execute(Zone Z, InfluenceMap influenceMap = null)
		{
			ZoneTemplateGenerationContext zoneTemplateGenerationContext = new ZoneTemplateGenerationContext();
			zoneTemplateGenerationContext.CurrentRegion = 0;
			zoneTemplateGenerationContext.Z = Z;
			if (influenceMap != null)
			{
				zoneTemplateGenerationContext.Regions = influenceMap;
			}
			else
			{
				zoneTemplateGenerationContext.Regions = ZoneTemplate.GenerateInfluenceMap(Z, new List<Point>(), InfluenceMapSeedStrategy.RandomPointFurtherThan4, this.RegionSize, null, false);
			}
			foreach (InfluenceMapRegion influenceMapRegion in zoneTemplateGenerationContext.Regions.Regions)
			{
				bool flag = false;
				for (int i = 0; i < zoneTemplateGenerationContext.Z.ZoneConnectionCache.Count; i++)
				{
					if (influenceMapRegion.PointIn(zoneTemplateGenerationContext.Z.ZoneConnectionCache[i].Pos2D))
					{
						influenceMapRegion.Tags.Add("connection");
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					List<ZoneConnection> zoneConnections = XRLCore.Core.Game.ZoneManager.GetZoneConnections(zoneTemplateGenerationContext.Z.ZoneID);
					for (int j = 0; j < zoneConnections.Count; j++)
					{
						if (influenceMapRegion.PointIn(zoneConnections[j].Pos2D))
						{
							influenceMapRegion.Tags.Add("connection");
							break;
						}
					}
				}
			}
			zoneTemplateGenerationContext.Variables = new Dictionary<string, string>();
			zoneTemplateGenerationContext.Variables.Add("{zonetier}", Z.NewTier.ToString());
			zoneTemplateGenerationContext.Variables.Add("{faction0}", PopulationManager.RollOneFrom("RandomZoneTemplateFaction", null, null).Blueprint);
			zoneTemplateGenerationContext.Variables.Add("{faction1}", PopulationManager.RollOneFrom("RandomZoneTemplateFaction", null, null).Blueprint);
			zoneTemplateGenerationContext.Variables.Add("{faction2}", PopulationManager.RollOneFrom("RandomZoneTemplateFaction", null, null).Blueprint);
			List<int> list = new List<int>();
			for (int k = 0; k < zoneTemplateGenerationContext.Regions.Regions.Count; k++)
			{
				list.Add(k);
			}
			Algorithms.RandomShuffleInPlace<int>(list);
			int num = zoneTemplateGenerationContext.Regions.AddGlobalRegion();
			zoneTemplateGenerationContext.CurrentRegion = num;
			if (!this.GlobalRoot.Execute(zoneTemplateGenerationContext))
			{
				return true;
			}
			zoneTemplateGenerationContext.CurrentRegion = list.GetRandomElement<int>(null);
			if (!this.SingleRoot.Execute(zoneTemplateGenerationContext))
			{
				return true;
			}
			for (int l = 0; l < list.Count; l++)
			{
				zoneTemplateGenerationContext.CurrentRegion = list[l];
				this.Execute(zoneTemplateGenerationContext);
				if (!zoneTemplateGenerationContext.PopulatedRegions.Contains(zoneTemplateGenerationContext.CurrentRegion))
				{
					zoneTemplateGenerationContext.PopulatedRegions.Add(zoneTemplateGenerationContext.CurrentRegion);
				}
			}
			return true;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x0011BDC4 File Offset: 0x00119FC4
		private bool Execute(ZoneTemplateGenerationContext Context)
		{
			return this.Root.Execute(Context);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0011BDD8 File Offset: 0x00119FD8
		public static InfluenceMap GenerateInfluenceMap(Zone Z, List<Point> AdditionalSeeds, InfluenceMapSeedStrategy SeedStrategy, int MaxRegionSize, List<Point2D> AdditionalWalls = null, bool bDraw = false)
		{
			InfluenceMap influenceMap = new InfluenceMap(Z.Width, Z.Height);
			influenceMap.bDraw = bDraw;
			Z.SetInfluenceMapWalls(influenceMap.Walls);
			if (AdditionalWalls != null)
			{
				foreach (Point2D point2D in AdditionalWalls)
				{
					influenceMap.Walls[point2D.x, point2D.y] = 1;
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
				if (!Z.GetCell(zoneConnection.X, zoneConnection.Y).IsSolid() && !Z.GetCell(zoneConnection.X, zoneConnection.Y).HasObjectWithTag("InfluenceMapBlocker"))
				{
					influenceMap.AddSeed(zoneConnection.X, zoneConnection.Y, false);
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && !Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).IsSolid() && !Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).HasObjectWithTag("InfluenceMapBlocker"))
				{
					influenceMap.AddSeed(cachedZoneConnection.X, cachedZoneConnection.Y, false);
				}
			}
			if (influenceMap.Seeds.Count > 0)
			{
				influenceMap.Recalculate(false);
			}
			List<Cell> cells = Z.GetCells();
			Algorithms.RandomShuffleInPlace<Cell>(cells, Stat.Rand);
			for (int i = 0; i < cells.Count; i++)
			{
				if (influenceMap.GetSeedAt(Location2D.Get(cells[i].X, cells[i].Y)) == -1 && cells[i].IsEmpty() && !cells[i].HasObjectWithTag("InfluenceMapBlocker"))
				{
					influenceMap.AddSeed(cells[i].X, cells[i].Y, true);
					influenceMap.Recalculate(false);
				}
			}
			influenceMap.bDraw = bDraw;
			int num = 50;
			while (num >= 0 && (influenceMap.Seeds.Count == 0 || influenceMap.LargestSize() > MaxRegionSize))
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
				num--;
			}
			if (influenceMap.Seeds.Count == 0)
			{
				Z.ClearWalkableBorders();
				Z.SetInfluenceMapWalls(influenceMap.Walls);
				influenceMap.AddSeedAtRandom(0);
				if (influenceMap.Seeds.Count == 0)
				{
					Z.GetCell(0, 0).Clear(null, false, false, null);
					Z.SetInfluenceMapWalls(influenceMap.Walls);
					influenceMap.AddSeedAtRandom(0);
				}
			}
			influenceMap.bDraw = bDraw;
			influenceMap.Recalculate(false);
			if (bDraw || Options.DrawInfluenceMaps)
			{
				influenceMap.Draw(false, true);
			}
			return influenceMap;
		}

		// Token: 0x04001CF2 RID: 7410
		public int RegionSize = 100;

		// Token: 0x04001CF3 RID: 7411
		public string Name;

		// Token: 0x04001CF4 RID: 7412
		public ZoneTemplateNode Root = new ZTGroupNode();

		// Token: 0x04001CF5 RID: 7413
		public ZoneTemplateNode GlobalRoot = new ZTGroupNode();

		// Token: 0x04001CF6 RID: 7414
		public ZoneTemplateNode SingleRoot = new ZTGroupNode();
	}
}
