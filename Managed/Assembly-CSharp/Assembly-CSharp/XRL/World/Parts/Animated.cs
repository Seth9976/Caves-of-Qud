using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FF7 RID: 4087
	[Obsolete]
	[Serializable]
	public class Animated : IPart
	{
		// Token: 0x0600A2E4 RID: 41700 RVA: 0x003B87DF File Offset: 0x003B69DF
		public override bool SameAs(IPart p)
		{
			return base.SameAs(p);
		}

		// Token: 0x0600A2E5 RID: 41701 RVA: 0x003B87E8 File Offset: 0x003B69E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A2E6 RID: 41702 RVA: 0x003B87FE File Offset: 0x003B69FE
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			AnimateObject.Animate(this.ParentObject, null, null);
			return base.HandleEvent(E);
		}
	}
}
