using System;

namespace XRL.World.Parts
{
	// Token: 0x02001307 RID: 4871
	[Serializable]
	public class RegionalObject : IPart
	{
		// Token: 0x0600BEC3 RID: 48835 RVA: 0x0045D869 File Offset: 0x0045BA69
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BEC4 RID: 48836 RVA: 0x0045D874 File Offset: 0x0045BA74
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ParentObject.Destroy(null, true, false, null);
			PopulationInfo populationInfo = PopulationManager.ResolvePopulation(this.Table, false);
			string terrainRegion = E.Cell.ParentZone.GetTerrainRegion();
			if (!terrainRegion.IsNullOrEmpty() && populationInfo != null)
			{
				PopulationGroup populationGroup;
				if (populationInfo.GroupLookup.TryGetValue(terrainRegion, out populationGroup) || populationInfo.GroupLookup.TryGetValue("Default", out populationGroup))
				{
					PopulationResult populationResult = populationGroup.GenerateOne(null, null);
					E.Cell.AddObject(populationResult.Blueprint, populationResult.Number, null, null, null);
				}
				else
				{
					MetricsManager.LogError(string.Concat(new string[] { "RegionalObject: No matching or default region found in table '", this.Table, "' for '", terrainRegion, "'." }));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004CD1 RID: 19665
		public string Table;
	}
}
