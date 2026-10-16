using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	internal interface IStrongEnumerator<[Nullable(2)] T>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F9 RID: 249
		T Current { get; }

		// Token: 0x060000FA RID: 250
		bool MoveNext();
	}
}
