using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001180 RID: 4480
	[Serializable]
	public class ModImprovedTelekinesis : ModImprovedMutationBase<Telekinesis>
	{
		// Token: 0x0600B22A RID: 45610 RVA: 0x0041362B File Offset: 0x0041182B
		public ModImprovedTelekinesis()
		{
		}

		// Token: 0x0600B22B RID: 45611 RVA: 0x00413633 File Offset: 0x00411833
		public ModImprovedTelekinesis(int Tier)
			: base(Tier)
		{
			this.Tier = Tier;
		}

		// Token: 0x0600B22C RID: 45612 RVA: 0x00413643 File Offset: 0x00411843
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B22D RID: 45613 RVA: 0x00413659 File Offset: 0x00411859
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
