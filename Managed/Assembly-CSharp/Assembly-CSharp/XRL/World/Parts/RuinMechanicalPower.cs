using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02001309 RID: 4873
	[Serializable]
	public class RuinMechanicalPower : IPart
	{
		// Token: 0x0600BEC9 RID: 48841 RVA: 0x0045DA52 File Offset: 0x0045BC52
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeZoneBuiltEvent.ID;
		}

		// Token: 0x0600BECA RID: 48842 RVA: 0x0045DA68 File Offset: 0x0045BC68
		public override bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null)
			{
				MechanicalPower mechanicalPower = new MechanicalPower();
				mechanicalPower.DamageChance = this.DamageChance;
				if (this.MissingChance.in100())
				{
					mechanicalPower.MissingProducers = this.MissingProducers;
					mechanicalPower.MissingConsumers = this.MissingConsumers;
				}
				mechanicalPower.PreferWalls = this.PreferWalls;
				mechanicalPower.AvoidWalls = this.AvoidWalls;
				mechanicalPower.BuildZone(currentZone);
			}
			this.ParentObject.Obliterate(null, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CDA RID: 19674
		public string DamageChance = "0-3x10";

		// Token: 0x04004CDB RID: 19675
		public int MissingChance = 30;

		// Token: 0x04004CDC RID: 19676
		public string MissingProducers = "1d2";

		// Token: 0x04004CDD RID: 19677
		public string MissingConsumers = "1d4";

		// Token: 0x04004CDE RID: 19678
		public bool PreferWalls = true;

		// Token: 0x04004CDF RID: 19679
		public bool AvoidWalls;
	}
}
