using System;

namespace XRL.World.Parts
{
	// Token: 0x020011E4 RID: 4580
	[Serializable]
	public class Aquatic : IPart
	{
		// Token: 0x0600B67D RID: 46717 RVA: 0x00429760 File Offset: 0x00427960
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B67E RID: 46718 RVA: 0x00429763 File Offset: 0x00427963
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSwimmingPerformanceEvent>.ID;
		}

		// Token: 0x0600B67F RID: 46719 RVA: 0x00429779 File Offset: 0x00427979
		public override bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			E.MoveSpeedPenalty -= 25;
			return base.HandleEvent(E);
		}
	}
}
