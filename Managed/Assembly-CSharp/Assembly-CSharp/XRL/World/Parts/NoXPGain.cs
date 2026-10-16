using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EEA RID: 3818
	[Serializable]
	public class NoXPGain : IPart
	{
		// Token: 0x060098A0 RID: 39072 RVA: 0x00376BBB File Offset: 0x00374DBB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AwardingXPEvent.ID;
		}

		// Token: 0x060098A1 RID: 39073 RVA: 0x00376BD1 File Offset: 0x00374DD1
		public override bool HandleEvent(AwardingXPEvent E)
		{
			return false;
		}
	}
}
