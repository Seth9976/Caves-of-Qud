using System;

namespace XRL.World.Parts
{
	// Token: 0x020012E4 RID: 4836
	[Serializable]
	public class RandomColorsOnMove : RandomColors
	{
		// Token: 0x0600BE32 RID: 48690 RVA: 0x0045B539 File Offset: 0x00459739
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BE33 RID: 48691 RVA: 0x0045B557 File Offset: 0x00459757
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.RandomizeColors();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE34 RID: 48692 RVA: 0x0045B566 File Offset: 0x00459766
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.RandomizeColors();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE35 RID: 48693 RVA: 0x0045B575 File Offset: 0x00459775
		public override bool KeepPartAround()
		{
			return true;
		}
	}
}
