using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E25 RID: 3621
	[Serializable]
	public class AIPassiveEnergyCellReload : IAIEnergyCellReload
	{
		// Token: 0x06009348 RID: 37704 RVA: 0x00350F54 File Offset: 0x0034F154
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetPassiveItemListEvent.ID;
		}

		// Token: 0x06009349 RID: 37705 RVA: 0x00350F6A File Offset: 0x0034F16A
		public override bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			base.CheckEnergyCellReload(E, 10, 110);
			return base.HandleEvent(E);
		}
	}
}
