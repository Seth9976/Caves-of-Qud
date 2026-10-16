using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E1D RID: 3613
	[Serializable]
	public class AIDefensiveEnergyCellReload : IAIEnergyCellReload
	{
		// Token: 0x06009325 RID: 37669 RVA: 0x0035058D File Offset: 0x0034E78D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID;
		}

		// Token: 0x06009326 RID: 37670 RVA: 0x003505A3 File Offset: 0x0034E7A3
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			base.CheckEnergyCellReload(E, 2, 110);
			return base.HandleEvent(E);
		}
	}
}
