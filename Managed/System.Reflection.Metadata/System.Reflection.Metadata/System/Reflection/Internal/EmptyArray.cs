using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200017D RID: 381
	internal static class EmptyArray<[Nullable(2)] T>
	{
		// Token: 0x0400095D RID: 2397
		[Nullable(1)]
		internal static readonly T[] Instance = new T[0];
	}
}
