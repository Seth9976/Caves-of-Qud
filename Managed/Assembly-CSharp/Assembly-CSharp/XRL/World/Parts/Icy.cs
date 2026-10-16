using System;

namespace XRL.World.Parts
{
	// Token: 0x02001051 RID: 4177
	[Serializable]
	public class Icy : IPart
	{
		// Token: 0x0600A658 RID: 42584 RVA: 0x003CDF85 File Offset: 0x003CC185
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x0600A659 RID: 42585 RVA: 0x003CDF9B File Offset: 0x003CC19B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.AddAdjective("{{icy|icy}}", 0);
			return base.HandleEvent(E);
		}
	}
}
