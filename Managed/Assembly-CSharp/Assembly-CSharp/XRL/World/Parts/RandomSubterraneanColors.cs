using System;

namespace XRL.World.Parts
{
	// Token: 0x020012E7 RID: 4839
	[Serializable]
	public class RandomSubterraneanColors : RandomColors
	{
		// Token: 0x0600BE41 RID: 48705 RVA: 0x0045B6BD File Offset: 0x004598BD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BE42 RID: 48706 RVA: 0x0045B6D3 File Offset: 0x004598D3
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.RandomizeColors();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE43 RID: 48707 RVA: 0x0045B6E4 File Offset: 0x004598E4
		public override void RandomizeColors()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone != null)
			{
				if (currentZone.IsInside())
				{
					base.RandomizeColors();
					return;
				}
				if (!this.KeepPartAround())
				{
					this.ParentObject.RemovePart(this);
				}
			}
		}
	}
}
