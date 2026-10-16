using System;

namespace NAudio.Wave
{
	// Token: 0x020001E3 RID: 483
	public class SampleEventArgs : EventArgs
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001F51F File Offset: 0x0001D71F
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0001F527 File Offset: 0x0001D727
		public float Left { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001F530 File Offset: 0x0001D730
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0001F538 File Offset: 0x0001D738
		public float Right { get; set; }

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001F541 File Offset: 0x0001D741
		public SampleEventArgs(float left, float right)
		{
			this.Left = left;
			this.Right = right;
		}
	}
}
