using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200117C RID: 4476
	[Serializable]
	public class ModImprovedConfusion : ModImprovedMutationBase<Confusion>
	{
		// Token: 0x0600B214 RID: 45588 RVA: 0x0041331A File Offset: 0x0041151A
		public ModImprovedConfusion()
		{
		}

		// Token: 0x0600B215 RID: 45589 RVA: 0x00413322 File Offset: 0x00411522
		public ModImprovedConfusion(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B216 RID: 45590 RVA: 0x0041332B File Offset: 0x0041152B
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B217 RID: 45591 RVA: 0x00413341 File Offset: 0x00411541
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("chance", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
