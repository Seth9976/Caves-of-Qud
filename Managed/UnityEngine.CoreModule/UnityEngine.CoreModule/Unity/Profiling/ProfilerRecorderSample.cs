using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200006E RID: 110
	[UsedByNativeCode]
	[DebuggerDisplay("Value = {Value}; Count = {Count}")]
	public struct ProfilerRecorderSample
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004140 File Offset: 0x00002340
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004148 File Offset: 0x00002348
		public long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0400018E RID: 398
		private long value;

		// Token: 0x0400018F RID: 399
		private long count;

		// Token: 0x04000190 RID: 400
		private long refValue;
	}
}
