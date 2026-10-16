using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000025 RID: 37
	internal interface IStrongEnumerable<[Nullable(2)] out T, TEnumerator> where TEnumerator : struct, IStrongEnumerator<T>
	{
		// Token: 0x060000F8 RID: 248
		TEnumerator GetEnumerator();
	}
}
