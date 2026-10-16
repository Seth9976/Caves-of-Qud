using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001314 RID: 4884
	[Serializable]
	public class Smokecaster : IPart
	{
		// Token: 0x0600BF09 RID: 48905 RVA: 0x0045F849 File Offset: 0x0045DA49
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BF0A RID: 48906 RVA: 0x0045F84C File Offset: 0x0045DA4C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600BF0B RID: 48907 RVA: 0x0045F862 File Offset: 0x0045DA62
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (Stat.Random(1, 60) <= 1 && this.ParentObject.CurrentCell != null)
			{
				this.ParentObject.Smoke(this.RangeLow, this.RangeHigh);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004D07 RID: 19719
		public int RangeLow = 170;

		// Token: 0x04004D08 RID: 19720
		public int RangeHigh = 350;
	}
}
