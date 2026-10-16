using System;

namespace XRL.World.Parts
{
	// Token: 0x020010F2 RID: 4338
	[Serializable]
	public class RequireSlots : IPart
	{
		// Token: 0x0600ABD3 RID: 43987 RVA: 0x003EF2DA File Offset: 0x003ED4DA
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<GetSlotsRequiredEvent>.ID;
		}

		// Token: 0x0600ABD4 RID: 43988 RVA: 0x003EF2E4 File Offset: 0x003ED4E4
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.Base += this.Base;
				E.Increases += this.Increases;
				E.Decreases += this.Decreases;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004703 RID: 18179
		public int Base;

		// Token: 0x04004704 RID: 18180
		public int Increases;

		// Token: 0x04004705 RID: 18181
		public int Decreases;
	}
}
