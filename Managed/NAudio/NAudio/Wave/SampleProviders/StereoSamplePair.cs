using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000190 RID: 400
	public struct StereoSamplePair
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000172B8 File Offset: 0x000154B8
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x000172C0 File Offset: 0x000154C0
		public float Left { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000172C9 File Offset: 0x000154C9
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x000172D1 File Offset: 0x000154D1
		public float Right { get; set; }
	}
}
