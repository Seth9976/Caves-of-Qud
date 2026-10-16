using System;

namespace XRL.World.Parts
{
	// Token: 0x02001303 RID: 4867
	[Serializable]
	public class MemorialPlacement : IPart
	{
		// Token: 0x0600BEB4 RID: 48820 RVA: 0x0045D35B File Offset: 0x0045B55B
		public override bool SameAs(IPart Part)
		{
			return (Part as MemorialPlacement).FillOrder == this.FillOrder && base.SameAs(Part);
		}

		// Token: 0x04004CC5 RID: 19653
		public int FillOrder;
	}
}
