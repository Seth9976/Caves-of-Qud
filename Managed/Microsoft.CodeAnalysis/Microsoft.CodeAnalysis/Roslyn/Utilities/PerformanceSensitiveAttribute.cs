using System;
using System.Diagnostics;

namespace Roslyn.Utilities
{
	// Token: 0x02000076 RID: 118
	[Conditional("EMIT_CODE_ANALYSIS_ATTRIBUTES")]
	[AttributeUsage(480, AllowMultiple = true, Inherited = false)]
	internal sealed class PerformanceSensitiveAttribute : Attribute
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000B6AB File Offset: 0x000098AB
		public PerformanceSensitiveAttribute(string uri)
		{
			this.Uri = uri;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000B6BA File Offset: 0x000098BA
		public string Uri { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B6C2 File Offset: 0x000098C2
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000B6CA File Offset: 0x000098CA
		public string Constraint { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B6D3 File Offset: 0x000098D3
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000B6DB File Offset: 0x000098DB
		public bool AllowCaptures { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000B6E4 File Offset: 0x000098E4
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000B6EC File Offset: 0x000098EC
		public bool AllowImplicitBoxing { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000B6F5 File Offset: 0x000098F5
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000B6FD File Offset: 0x000098FD
		public bool AllowGenericEnumeration { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B706 File Offset: 0x00009906
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000B70E File Offset: 0x0000990E
		public bool AllowLocks { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B717 File Offset: 0x00009917
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000B71F File Offset: 0x0000991F
		public bool OftenCompletesSynchronously { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000B728 File Offset: 0x00009928
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000B730 File Offset: 0x00009930
		public bool IsParallelEntry { get; set; }
	}
}
