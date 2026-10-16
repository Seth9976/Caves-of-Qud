using System;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C7 RID: 2503
	[Serializable]
	public class ShevaBoundary : IZonePart
	{
		// Token: 0x06006B81 RID: 27521 RVA: 0x0022B57C File Offset: 0x0022977C
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == EnteringZoneEvent.ID;
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x0022B586 File Offset: 0x00229786
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			return false;
		}
	}
}
