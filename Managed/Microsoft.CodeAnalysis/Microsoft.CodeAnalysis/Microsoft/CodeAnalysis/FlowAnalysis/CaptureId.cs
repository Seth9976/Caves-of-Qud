using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D8 RID: 728
	public struct CaptureId : IEquatable<CaptureId>
	{
		// Token: 0x06001E3F RID: 7743 RVA: 0x0005721A File Offset: 0x0005541A
		internal CaptureId(int value)
		{
			this.Value = value;
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00057223 File Offset: 0x00055423
		internal readonly int Value { get; }

		// Token: 0x06001E41 RID: 7745 RVA: 0x0005722B File Offset: 0x0005542B
		public bool Equals(CaptureId other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0005723C File Offset: 0x0005543C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CaptureId && this.Equals((CaptureId)obj);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00057254 File Offset: 0x00055454
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}
	}
}
