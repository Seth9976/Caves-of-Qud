using System;

namespace XRL.World.Parts
{
	// Token: 0x0200102B RID: 4139
	[Serializable]
	public class Flaming : IPart
	{
		// Token: 0x0600A4D6 RID: 42198 RVA: 0x003C4639 File Offset: 0x003C2839
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x0600A4D7 RID: 42199 RVA: 0x003C464F File Offset: 0x003C284F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			E.AddAdjective("{{fiery|flaming}}", 0);
			return base.HandleEvent(E);
		}
	}
}
