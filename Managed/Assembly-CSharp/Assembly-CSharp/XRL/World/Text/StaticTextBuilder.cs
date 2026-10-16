using System;

namespace XRL.World.Text
{
	// Token: 0x02001460 RID: 5216
	public class StaticTextBuilder : CompositedTextBuilder
	{
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600CD06 RID: 52486 RVA: 0x004A56C9 File Offset: 0x004A38C9
		protected override bool Finalizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600CD07 RID: 52487 RVA: 0x004A56CC File Offset: 0x004A38CC
		public StaticTextBuilder(int Capacity = 64)
			: base(Capacity)
		{
		}
	}
}
