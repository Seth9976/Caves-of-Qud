using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001181 RID: 4481
	[Serializable]
	public class ModImprovedTeleportation : ModImprovedMutationBase<Teleportation>
	{
		// Token: 0x0600B22E RID: 45614 RVA: 0x0041367C File Offset: 0x0041187C
		public ModImprovedTeleportation()
		{
		}

		// Token: 0x0600B22F RID: 45615 RVA: 0x00413684 File Offset: 0x00411884
		public ModImprovedTeleportation(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B230 RID: 45616 RVA: 0x0041368D File Offset: 0x0041188D
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600B231 RID: 45617 RVA: 0x004136A3 File Offset: 0x004118A3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}
	}
}
