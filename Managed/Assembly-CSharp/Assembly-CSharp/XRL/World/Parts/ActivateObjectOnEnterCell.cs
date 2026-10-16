using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E4A RID: 3658
	[Serializable]
	public class ActivateObjectOnEnterCell : IPart
	{
		// Token: 0x06009433 RID: 37939 RVA: 0x00355A6E File Offset: 0x00353C6E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x06009434 RID: 37940 RVA: 0x00355A84 File Offset: 0x00353C84
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ParentObject.MakeActive(false);
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x06009435 RID: 37941 RVA: 0x00355AA5 File Offset: 0x00353CA5
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
