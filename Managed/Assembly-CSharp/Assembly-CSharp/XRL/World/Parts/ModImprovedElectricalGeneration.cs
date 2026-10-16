using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200117D RID: 4477
	[Serializable]
	public class ModImprovedElectricalGeneration : ModImprovedMutationBase<ElectricalGeneration>
	{
		// Token: 0x0600B218 RID: 45592 RVA: 0x00413364 File Offset: 0x00411564
		public ModImprovedElectricalGeneration()
		{
		}

		// Token: 0x0600B219 RID: 45593 RVA: 0x0041336C File Offset: 0x0041156C
		public ModImprovedElectricalGeneration(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B21A RID: 45594 RVA: 0x00413375 File Offset: 0x00411575
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B21B RID: 45595 RVA: 0x0041338B File Offset: 0x0041158B
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
