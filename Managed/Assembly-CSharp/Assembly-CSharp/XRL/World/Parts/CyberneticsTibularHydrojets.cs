using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F4E RID: 3918
	[Serializable]
	public class CyberneticsTibularHydrojets : IPart
	{
		// Token: 0x06009C6B RID: 40043 RVA: 0x0038B902 File Offset: 0x00389B02
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C6C RID: 40044 RVA: 0x0038B905 File Offset: 0x00389B05
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSwimmingPerformanceEvent>.ID;
		}

		// Token: 0x06009C6D RID: 40045 RVA: 0x0038B91B File Offset: 0x00389B1B
		public override bool HandleEvent(GetSwimmingPerformanceEvent E)
		{
			E.MoveSpeedPenalty -= this.Bonus;
			return base.HandleEvent(E);
		}

		// Token: 0x040040B0 RID: 16560
		public int Bonus = 200;
	}
}
