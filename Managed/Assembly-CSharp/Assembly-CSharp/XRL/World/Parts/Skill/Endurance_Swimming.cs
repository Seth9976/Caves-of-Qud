using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001341 RID: 4929
	[Serializable]
	public class Endurance_Swimming : BaseSkill
	{
		// Token: 0x0600C065 RID: 49253 RVA: 0x00467B1C File Offset: 0x00465D1C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSwimmingPerformanceEvent>.ID;
		}

		// Token: 0x0600C066 RID: 49254 RVA: 0x00467B32 File Offset: 0x00465D32
		public override bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			E.MoveSpeedPenalty -= 25;
			return base.HandleEvent(E);
		}
	}
}
