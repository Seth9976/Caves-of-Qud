using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E23 RID: 3619
	[Serializable]
	public class AIOffensiveEnergyCellReload : IAIEnergyCellReload
	{
		// Token: 0x0600933D RID: 37693 RVA: 0x00350B50 File Offset: 0x0034ED50
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveItemListEvent.ID;
		}

		// Token: 0x0600933E RID: 37694 RVA: 0x00350B66 File Offset: 0x0034ED66
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			base.CheckEnergyCellReload(E, 2, 110);
			return base.HandleEvent(E);
		}
	}
}
