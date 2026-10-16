using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001056 RID: 4182
	public class IsTechScannableUnlessAlive : IPart
	{
		// Token: 0x0600A68A RID: 42634 RVA: 0x003CEC35 File Offset: 0x003CCE35
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AnimateEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID;
		}

		// Token: 0x0600A68B RID: 42635 RVA: 0x003CEC54 File Offset: 0x003CCE54
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject && E.ScanType == Scanning.Scan.Structure && !E.Object.IsAlive)
			{
				Examiner part = E.Object.GetPart<Examiner>();
				if (part != null && part.Complexity > 0)
				{
					E.ScanType = Scanning.Scan.Tech;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A68C RID: 42636 RVA: 0x003CECAB File Offset: 0x003CCEAB
		public override bool HandleEvent(AnimateEvent E)
		{
			E.WantToRemove(this);
			return base.HandleEvent(E);
		}
	}
}
