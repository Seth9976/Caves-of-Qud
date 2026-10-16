using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02001308 RID: 4872
	[Serializable]
	public class RuinHydraulics : IPart
	{
		// Token: 0x0600BEC6 RID: 48838 RVA: 0x0045D94A File Offset: 0x0045BB4A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeZoneBuiltEvent.ID;
		}

		// Token: 0x0600BEC7 RID: 48839 RVA: 0x0045D960 File Offset: 0x0045BB60
		public override bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null)
			{
				Hydraulics hydraulics = new Hydraulics();
				hydraulics.DamageChance = this.DamageChance;
				hydraulics.Liquid = this.Liquid;
				hydraulics.LiquidTable = this.LiquidTable;
				if (this.MissingChance.in100())
				{
					hydraulics.MissingProducers = this.MissingProducers;
					hydraulics.MissingConsumers = this.MissingConsumers;
				}
				hydraulics.PreferWalls = this.PreferWalls;
				hydraulics.AvoidWalls = this.AvoidWalls;
				hydraulics.BuildZone(currentZone);
			}
			this.ParentObject.Obliterate(null, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CD2 RID: 19666
		public string DamageChance = "0-3x10";

		// Token: 0x04004CD3 RID: 19667
		public int MissingChance = 30;

		// Token: 0x04004CD4 RID: 19668
		public string MissingProducers = "1d2";

		// Token: 0x04004CD5 RID: 19669
		public string MissingConsumers = "1d6";

		// Token: 0x04004CD6 RID: 19670
		public string Liquid;

		// Token: 0x04004CD7 RID: 19671
		public string LiquidTable = "HydraulicFluid";

		// Token: 0x04004CD8 RID: 19672
		public bool PreferWalls = true;

		// Token: 0x04004CD9 RID: 19673
		public bool AvoidWalls;
	}
}
