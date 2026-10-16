using System;

namespace XRL.World.Parts
{
	// Token: 0x02001315 RID: 4885
	[Serializable]
	public class StressSmokecaster : IPart
	{
		// Token: 0x0600BF0D RID: 48909 RVA: 0x0045F8B8 File Offset: 0x0045DAB8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BF0E RID: 48910 RVA: 0x0045F8BB File Offset: 0x0045DABB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600BF0F RID: 48911 RVA: 0x0045F8D4 File Offset: 0x0045DAD4
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			for (int i = 0; i < 200; i++)
			{
				this.ParentObject.Smoke(this.RangeLow, this.RangeHigh);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004D09 RID: 19721
		public int RangeLow = 170;

		// Token: 0x04004D0A RID: 19722
		public int RangeHigh = 350;
	}
}
