using System;

namespace XRL.World.Parts
{
	// Token: 0x020012E9 RID: 4841
	[Serializable]
	public class RandomTileOnMove : RandomTile
	{
		// Token: 0x0600BE4C RID: 48716 RVA: 0x0045B7CB File Offset: 0x004599CB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BE4D RID: 48717 RVA: 0x0045B7E9 File Offset: 0x004599E9
		public override bool HandleEvent(EnteredCellEvent E)
		{
			base.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE4E RID: 48718 RVA: 0x0045B7F8 File Offset: 0x004599F8
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			base.RandomizeTile();
			return base.HandleEvent(E);
		}
	}
}
