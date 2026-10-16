using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x0200130A RID: 4874
	[Serializable]
	public class RuinPowerGrid : IPart
	{
		// Token: 0x0600BECC RID: 48844 RVA: 0x0045DB29 File Offset: 0x0045BD29
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeZoneBuiltEvent.ID;
		}

		// Token: 0x0600BECD RID: 48845 RVA: 0x0045DB40 File Offset: 0x0045BD40
		public override bool HandleEvent(BeforeZoneBuiltEvent E)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null)
			{
				PowerGrid powerGrid = new PowerGrid();
				powerGrid.DamageChance = this.DamageChance;
				if (this.MissingChance.in100())
				{
					powerGrid.MissingProducers = this.MissingProducers;
					powerGrid.MissingConsumers = this.MissingConsumers;
				}
				powerGrid.PreferWalls = this.PreferWalls;
				powerGrid.AvoidWalls = this.AvoidWalls;
				powerGrid.BuildZone(currentZone);
			}
			this.ParentObject.Obliterate(null, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CE0 RID: 19680
		public string DamageChance = "0-3x10";

		// Token: 0x04004CE1 RID: 19681
		public int MissingChance = 30;

		// Token: 0x04004CE2 RID: 19682
		public string MissingProducers = "1d3";

		// Token: 0x04004CE3 RID: 19683
		public string MissingConsumers = "1d6";

		// Token: 0x04004CE4 RID: 19684
		public bool PreferWalls = true;

		// Token: 0x04004CE5 RID: 19685
		public bool AvoidWalls;
	}
}
