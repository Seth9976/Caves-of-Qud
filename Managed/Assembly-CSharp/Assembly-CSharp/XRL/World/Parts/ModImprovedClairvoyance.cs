using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200117B RID: 4475
	[Serializable]
	public class ModImprovedClairvoyance : ModImprovedMutationBase<Clairvoyance>
	{
		// Token: 0x0600B210 RID: 45584 RVA: 0x004132D0 File Offset: 0x004114D0
		public ModImprovedClairvoyance()
		{
		}

		// Token: 0x0600B211 RID: 45585 RVA: 0x004132D8 File Offset: 0x004114D8
		public ModImprovedClairvoyance(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B212 RID: 45586 RVA: 0x004132E1 File Offset: 0x004114E1
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B213 RID: 45587 RVA: 0x004132F7 File Offset: 0x004114F7
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("glass", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
