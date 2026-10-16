using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F77 RID: 3959
	[Serializable]
	public class RemoveInterestingOnTake : IPart
	{
		// Token: 0x06009D97 RID: 40343 RVA: 0x00391617 File Offset: 0x0038F817
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID;
		}

		// Token: 0x06009D98 RID: 40344 RVA: 0x0039162D File Offset: 0x0038F82D
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.ParentObject.RemovePart<Interesting>();
			return base.HandleEvent(E);
		}
	}
}
