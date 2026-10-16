using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD0 RID: 2768
	public class Ruins : ZoneBuilderSandbox
	{
		// Token: 0x06006FD5 RID: 28629 RVA: 0x00276AA8 File Offset: 0x00274CA8
		public bool BuildZone(Zone Z)
		{
			ZoneManager zoneManager = The.ZoneManager;
			BuildingZoneTemplate buildingZoneTemplate = zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.BuildingZoneTemplate", null) as BuildingZoneTemplate;
			if (buildingZoneTemplate == null)
			{
				buildingZoneTemplate = new BuildingZoneTemplate();
				buildingZoneTemplate.New(Z.Width, Z.Height, this.ZonesWide.RollCached(), this.ZonesHigh.RollCached(), "Ruins_PrimaryTemplate_*Default");
				zoneManager.SetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.BuildingZoneTemplate", buildingZoneTemplate);
				string text = "";
				GameObject terrainObject = Z.GetTerrainObject();
				string text2;
				if (!string.Equals((terrainObject != null) ? terrainObject.Blueprint : null, "TerrainRuins"))
				{
					GameObject terrainObject2 = Z.GetTerrainObject();
					if (!string.Equals((terrainObject2 != null) ? terrainObject2.Blueprint : null, "TerrainBaroqueRuins"))
					{
						text2 = "DefaultRuinsSemantics";
						goto IL_00BC;
					}
				}
				text2 = "WorldRuinsSemantics";
				IL_00BC:
				foreach (PopulationResult populationResult in PopulationManager.Generate(text2, null, null, null, null, null))
				{
					if (text != "")
					{
						text += ",";
					}
					text += populationResult.Blueprint;
				}
				if (text == "")
				{
					text = "*Default";
				}
				zoneManager.SetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.SemanticTags", text);
			}
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					Cell cell = Z.GetCell(j, i);
					BuildingTemplateTile buildingTemplateTile = buildingZoneTemplate.Template.Map[j, i];
					if (buildingTemplateTile == BuildingTemplateTile.Inside)
					{
						cell.AddSemanticTag("Inside");
						cell.AddSemanticTag("Room");
						cell.AddSemanticTag("Perimeter");
					}
					if (buildingTemplateTile == BuildingTemplateTile.Wall)
					{
						cell.AddSemanticTag("Wall");
						cell.AddSemanticTag("Inner");
						cell.AddSemanticTag("Perimeter");
					}
					if (buildingTemplateTile == BuildingTemplateTile.Door)
					{
						cell.AddSemanticTag("Door");
						cell.AddSemanticTag("Perimeter");
					}
					if (buildingTemplateTile == BuildingTemplateTile.OutsideWall)
					{
						cell.AddSemanticTag("Wall");
						cell.AddSemanticTag("Outer");
					}
					if (buildingTemplateTile == BuildingTemplateTile.StairsUp)
					{
						cell.AddSemanticTag("Connection");
						cell.AddSemanticTag("Stairs");
						cell.AddSemanticTag("Up");
					}
					if (buildingTemplateTile == BuildingTemplateTile.StairsDown)
					{
						cell.AddSemanticTag("Connection");
						cell.AddSemanticTag("Stairs");
						cell.AddSemanticTag("Up");
					}
					if (buildingTemplateTile == BuildingTemplateTile.Outside)
					{
						cell.AddSemanticTag("Outside");
					}
					if (buildingTemplateTile == BuildingTemplateTile.Void)
					{
						cell.AddSemanticTag("Inside");
						cell.AddSemanticTag("Isolated");
					}
				}
			}
			Z.DebugDrawSemantics();
			foreach (string text3 in zoneManager.GetZoneColumnProperty(Z.ZoneID, "Builder.Ruins.SemanticTags", "*Default").ToString().Split(',', StringSplitOptions.None))
			{
				string text4 = text3 + "Replace";
				if (PopulationManager.HasTable(text4))
				{
					string blueprint = PopulationManager.RollOneFrom(text4, null, null).Blueprint;
					if (!string.Equals(blueprint, "*None"))
					{
						if (string.Equals(blueprint, "Extra"))
						{
							Z.AddSemanticTag("Extra" + text3);
						}
						else
						{
							Z.AddSemanticTag(text3);
						}
					}
				}
				else
				{
					Z.AddSemanticTag(text3);
				}
			}
			Ruiner ruiner = new Ruiner();
			bool flag = Z.GetZoneZ() > 10;
			buildingZoneTemplate.BuildZone(Z, flag);
			if (this.RuinLevel > 0)
			{
				ruiner.RuinZone(Z, this.RuinLevel, flag, 100000, 100000);
			}
			Z.RebuildReachableMap();
			return true;
		}

		// Token: 0x0400336F RID: 13167
		public int RuinLevel = 100;

		// Token: 0x04003370 RID: 13168
		public string ZonesWide = "1d3";

		// Token: 0x04003371 RID: 13169
		public string ZonesHigh = "1d2";
	}
}
