using System;

namespace XRL.World.Parts
{
	// Token: 0x02001204 RID: 4612
	[Serializable]
	public class Unreplicable : IPart
	{
		// Token: 0x0600B77D RID: 46973 RVA: 0x0042C66D File Offset: 0x0042A86D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanBeReplicatedEvent.ID;
		}

		// Token: 0x0600B77E RID: 46974 RVA: 0x0042C683 File Offset: 0x0042A883
		public override bool HandleEvent(CanBeReplicatedEvent E)
		{
			return this.AllowTemporary && E.Temporary && base.HandleEvent(E);
		}

		// Token: 0x040049C9 RID: 18889
		public bool AllowTemporary;
	}
}
