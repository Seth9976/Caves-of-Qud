using System;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C4 RID: 2500
	[Serializable]
	public class Memorialization : IZonePart
	{
		// Token: 0x06006B6B RID: 27499 RVA: 0x002299F5 File Offset: 0x00227BF5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06006B6C RID: 27500 RVA: 0x00229A0B File Offset: 0x00227C0B
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckMemorialization();
			return base.HandleEvent(E);
		}

		// Token: 0x06006B6D RID: 27501 RVA: 0x00229A1A File Offset: 0x00227C1A
		public void CheckMemorialization()
		{
		}

		// Token: 0x06006B6E RID: 27502 RVA: 0x00229A1C File Offset: 0x00227C1C
		private string GetBlueprintFor(MemorialTracking Memorial)
		{
			if (this.BlueprintInorganic.IsNullOrEmpty())
			{
				return this.Blueprint;
			}
			string blueprint = Memorial.Blueprint;
			if (blueprint.IsNullOrEmpty())
			{
				return this.Blueprint;
			}
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(blueprint);
			if (blueprintIfExists == null)
			{
				return this.Blueprint;
			}
			if (!blueprintIfExists.GetPartParameter<bool>("Physics", "Organic", false))
			{
				return this.BlueprintInorganic;
			}
			return this.Blueprint;
		}

		// Token: 0x04003124 RID: 12580
		public string Faction;

		// Token: 0x04003125 RID: 12581
		public string RequiresObjectWithTagOrProperty;

		// Token: 0x04003126 RID: 12582
		public string RequiresObjectWithOperationalPart;

		// Token: 0x04003127 RID: 12583
		public string RequiresObjectOfFaction;

		// Token: 0x04003128 RID: 12584
		public string Blueprint;

		// Token: 0x04003129 RID: 12585
		public string BlueprintInorganic;

		// Token: 0x0400312A RID: 12586
		public long Delay = 2400L;

		// Token: 0x0400312B RID: 12587
		public long LastTurn;

		// Token: 0x0400312C RID: 12588
		public int MinTurnsForCheck = 50;
	}
}
