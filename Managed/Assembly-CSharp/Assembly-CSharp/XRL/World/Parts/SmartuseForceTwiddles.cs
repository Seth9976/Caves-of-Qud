using System;

namespace XRL.World.Parts
{
	// Token: 0x02001286 RID: 4742
	[Serializable]
	public class SmartuseForceTwiddles : IPart
	{
		// Token: 0x0600BBA3 RID: 48035 RVA: 0x00448733 File Offset: 0x00446933
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID;
		}

		// Token: 0x0600BBA4 RID: 48036 RVA: 0x00448751 File Offset: 0x00446951
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600BBA5 RID: 48037 RVA: 0x00448754 File Offset: 0x00446954
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			this.ParentObject.Twiddle(false);
			return false;
		}

		// Token: 0x0600BBA6 RID: 48038 RVA: 0x00448764 File Offset: 0x00446964
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
