using System;
using XRL.World.Quests;

namespace XRL.World.Parts
{
	// Token: 0x020012FD RID: 4861
	[Serializable]
	public class GritGateWidget : IPart
	{
		// Token: 0x0600BE8D RID: 48781 RVA: 0x0045C4B0 File Offset: 0x0045A6B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BE8E RID: 48782 RVA: 0x0045C4CE File Offset: 0x0045A6CE
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			GritGateScripts.CheckGritGateDoors();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE8F RID: 48783 RVA: 0x0045C4DC File Offset: 0x0045A6DC
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			GritGateScripts.CheckGritGateDoors();
			return base.HandleEvent(E);
		}
	}
}
