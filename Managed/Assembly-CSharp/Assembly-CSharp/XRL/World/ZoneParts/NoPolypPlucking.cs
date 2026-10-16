using System;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C5 RID: 2501
	[Serializable]
	public class NoPolypPlucking : IZonePart
	{
		// Token: 0x06006B70 RID: 27504 RVA: 0x00229AA5 File Offset: 0x00227CA5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowPolypPluckingEvent.ID;
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x00229ABB File Offset: 0x00227CBB
		public override bool HandleEvent(AllowPolypPluckingEvent E)
		{
			return false;
		}
	}
}
