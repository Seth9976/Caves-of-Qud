using System;

namespace XRL.World.Parts
{
	// Token: 0x02001226 RID: 4646
	[Serializable]
	public class SendEndSegment : IPart
	{
		// Token: 0x0600B85C RID: 47196 RVA: 0x004311DA File Offset: 0x0042F3DA
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B85D RID: 47197 RVA: 0x004311DD File Offset: 0x0042F3DD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x0600B85E RID: 47198 RVA: 0x004311F3 File Offset: 0x0042F3F3
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return base.HandleEvent(E);
		}

		// Token: 0x04004A46 RID: 19014
		public int Segments = 60;
	}
}
