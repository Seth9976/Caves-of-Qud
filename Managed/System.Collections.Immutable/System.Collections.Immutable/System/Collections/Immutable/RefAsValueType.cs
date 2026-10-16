using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value,nq}")]
	internal struct RefAsValueType<[Nullable(2)] T>
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00009228 File Offset: 0x00007428
		internal RefAsValueType(T value)
		{
			this.Value = value;
		}

		// Token: 0x0400003F RID: 63
		internal T Value;
	}
}
