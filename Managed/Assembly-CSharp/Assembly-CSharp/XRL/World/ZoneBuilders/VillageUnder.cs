using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B0C RID: 2828
	public class VillageUnder : ZoneBuilderSandbox
	{
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600719B RID: 29083 RVA: 0x002A642C File Offset: 0x002A462C
		// (set) Token: 0x0600719C RID: 29084 RVA: 0x002A6464 File Offset: 0x002A4664
		public HistoricEntity villageEntity
		{
			get
			{
				if (this._VillageEntity == null && !this.VillageEntityID.IsNullOrEmpty())
				{
					this._VillageEntity = The.Game.sultanHistory.GetEntity(this.VillageEntityID);
				}
				return this._VillageEntity;
			}
			set
			{
				this._VillageEntity = value;
				this.VillageEntityID = ((value != null) ? value.id : null);
			}
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x002A6480 File Offset: 0x002A4680
		public bool BuildZone(Zone Z)
		{
			this.zone = Z;
			Z.SetZoneProperty("DisableForcedConnections", "Yes");
			Z.SetZoneProperty("relaxedbiomes", "true");
			List<Location2D> list = new List<Location2D>();
			foreach (Cell cell in Z.GetCells())
			{
				cell.ClearObjectsWithPart("Combat", false, false);
			}
			GameObject anyObjectWithTag = Z.GetAnyObjectWithTag("Wall");
			string text = ((anyObjectWithTag != null) ? anyObjectWithTag.Blueprint : null) ?? "Shale";
			List<string> list2 = new List<string>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type.StartsWith("burrow"))
				{
					list.Add(Location2D.Get(cachedZoneConnection.X, cachedZoneConnection.Y));
					string[] array = cachedZoneConnection.Type.Split(',', StringSplitOptions.None);
					if (array.Length > 1 && !string.IsNullOrEmpty(array[1]))
					{
						list2.Add(array[1]);
					}
					else
					{
						list2.Add("Villages_BuildingContents_Dwelling_*Default");
					}
				}
			}
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type == "burrow")
				{
					list.Add(Location2D.Get(zoneConnection.X, zoneConnection.Y));
					string[] array2 = zoneConnection.Type.Split(',', StringSplitOptions.None);
					if (array2.Length > 1 && !string.IsNullOrEmpty(array2[1]))
					{
						list2.Add(array2[1]);
					}
					else
					{
						list2.Add("Villages_BuildingContents_Dwelling_*Default");
					}
				}
			}
			if (list.Count > 0)
			{
				List<Box> list3 = new List<Box>();
				foreach (Location2D location2D in list)
				{
					int num = Stat.Random(4, 6);
					Box box = new Box(location2D.X - num, location2D.Y - num, location2D.X + num, location2D.Y + num).clamp(1, 1, 78, 23);
					list3.Add(box);
					Z.ClearBox(box, null);
				}
				InfluenceMap influenceMap = new InfluenceMap(Z.Width, Z.Height);
				Z.SetInfluenceMapWalls(influenceMap.Walls);
				foreach (Box box2 in list3)
				{
					influenceMap.AddSeed(box2.center, false);
				}
				influenceMap.Recalculate(false);
				influenceMap.SeedAllUnseeded(false, true);
				while (influenceMap.LargestSeed() > 100)
				{
					influenceMap.AddSeedAtMaxima();
				}
				for (int i = 0; i < list3.Count; i++)
				{
					foreach (Location2D location2D2 in influenceMap.Regions[i].getBorder(2))
					{
						Z.GetCell(location2D2).AddObject(text, null, null, null, null);
					}
					List<PopulationResult> list4 = PopulationManager.Generate(list2[i], null, null, null, null, null);
					PopulationLayout populationLayout = new PopulationLayout(this.zone, influenceMap.Regions[i], list3[i].rect, list[i]);
					populationLayout.inside.AddRange(influenceMap.Regions[i].reducyBy(2));
					populationLayout.insideWall.AddRange(influenceMap.Regions[i].reducyBy(2).Except<Location2D>(influenceMap.Regions[i].reducyBy(3)));
					populationLayout.insideCorner.AddRange(influenceMap.Regions[i].reducyBy(2).Except<Location2D>(influenceMap.Regions[i].reducyBy(3)));
					foreach (PopulationResult populationResult in list4)
					{
						base.PlaceObjectInBuilding(GameObject.Create(populationResult.Blueprint), populationLayout, null, null);
					}
					Z.GetCell(list[i]).AddObject("StairsUp", null, null, null, null).SetIntProperty("IdleStairs", 1, false);
				}
			}
			HistoricEntitySnapshot currentSnapshot = this.villageEntity.GetCurrentSnapshot();
			string text2 = ((currentSnapshot.GetProperty("abandoned", "unknown") == "true") ? Stat.Random(5, 25).ToString() : (10 - currentSnapshot.TechTier).ToString());
			PowerGrid powerGrid = new PowerGrid();
			powerGrid.DamageChance = text2;
			if ((10 + currentSnapshot.TechTier * 3).in100())
			{
				powerGrid.MissingConsumers = "1d6";
				powerGrid.MissingProducers = "1d3";
			}
			powerGrid.BuildZone(Z);
			Hydraulics hydraulics = new Hydraulics();
			hydraulics.DamageChance = text2;
			if ((10 + currentSnapshot.TechTier * 3).in100())
			{
				hydraulics.MissingConsumers = "1d6";
				hydraulics.MissingProducers = "1d3";
			}
			hydraulics.BuildZone(Z);
			MechanicalPower mechanicalPower = new MechanicalPower();
			mechanicalPower.DamageChance = text2;
			if ((20 - currentSnapshot.TechTier).in100())
			{
				mechanicalPower.MissingConsumers = "1d6";
				mechanicalPower.MissingProducers = "1d3";
			}
			mechanicalPower.BuildZone(Z);
			return true;
		}

		// Token: 0x040034BA RID: 13498
		public string VillageEntityID;

		// Token: 0x040034BB RID: 13499
		[NonSerialized]
		private HistoricEntity _VillageEntity;
	}
}
