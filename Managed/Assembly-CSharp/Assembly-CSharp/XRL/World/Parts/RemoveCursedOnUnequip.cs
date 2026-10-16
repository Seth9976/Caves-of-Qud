using System;

namespace XRL.World.Parts
{
	// Token: 0x0200108E RID: 4238
	[Serializable]
	public class RemoveCursedOnUnequip : IPart
	{
		// Token: 0x0600A8A8 RID: 43176 RVA: 0x003DB29C File Offset: 0x003D949C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A8A9 RID: 43177 RVA: 0x003DB2B2 File Offset: 0x003D94B2
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.ParentObject.RemovePart<Cursed>();
			return base.HandleEvent(E);
		}
	}
}
