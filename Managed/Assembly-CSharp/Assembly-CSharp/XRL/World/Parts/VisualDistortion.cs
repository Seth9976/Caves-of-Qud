using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012F2 RID: 4850
	[Serializable]
	public class VisualDistortion : IPart
	{
		// Token: 0x0600BE67 RID: 48743 RVA: 0x0045BD67 File Offset: 0x00459F67
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600BE68 RID: 48744 RVA: 0x0045BD80 File Offset: 0x00459F80
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (Stat.RandomCosmetic(1, 1000) <= this.Chance && this.LastTrigger < DateTime.Now.Ticks - (long)this.MinimumTicks)
			{
				this.LastTrigger = DateTime.Now.Ticks;
				this.ParentObject.DilationSplat();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004CAB RID: 19627
		public int Chance = 2;

		// Token: 0x04004CAC RID: 19628
		public int MinimumTicks = 10000000;

		// Token: 0x04004CAD RID: 19629
		public long LastTrigger;
	}
}
