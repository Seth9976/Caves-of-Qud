using System;

namespace Unity.Profiling.Memory
{
	// Token: 0x0200007D RID: 125
	public class MemorySnapshotMetadata
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00004D49 File Offset: 0x00002F49
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00004D51 File Offset: 0x00002F51
		public string Description { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00004D5A File Offset: 0x00002F5A
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00004D62 File Offset: 0x00002F62
		internal byte[] Data { get; set; }
	}
}
