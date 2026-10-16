using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000EF7 RID: 3831
	[Serializable]
	public class ReplaceObject : IPart
	{
		// Token: 0x06009903 RID: 39171 RVA: 0x003788C7 File Offset: 0x00376AC7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x06009904 RID: 39172 RVA: 0x003788E0 File Offset: 0x00376AE0
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			if (!this.Table.IsNullOrEmpty())
			{
				if (this.SeedWithZoneID && !ZoneManager.zoneGenerationContextZoneID.IsNullOrEmpty())
				{
					List<string> each = PopulationManager.GetEach(this.Table, null, null);
					int num = Stat.SeededRandom(ZoneManager.zoneGenerationContextZoneID, 0, each.Count - 1);
					E.ReplacementObject = GameObject.Create(each[num]);
				}
				else
				{
					PopulationResult populationResult = PopulationManager.GenerateOne(this.Table, null, null, null, null, null);
					E.ReplacementObject = GameObject.Create(populationResult.Blueprint);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003FBD RID: 16317
		public string Table;

		// Token: 0x04003FBE RID: 16318
		public bool SeedWithZoneID = true;
	}
}
