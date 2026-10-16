using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200117E RID: 4478
	[Serializable]
	public class ModImprovedLightManipulation : ModImprovedMutationBase<LightManipulation>
	{
		// Token: 0x0600B21C RID: 45596 RVA: 0x004133AE File Offset: 0x004115AE
		public ModImprovedLightManipulation()
		{
		}

		// Token: 0x0600B21D RID: 45597 RVA: 0x004133B6 File Offset: 0x004115B6
		public ModImprovedLightManipulation(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B21E RID: 45598 RVA: 0x004133BF File Offset: 0x004115BF
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B21F RID: 45599 RVA: 0x004133D5 File Offset: 0x004115D5
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("stars", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
