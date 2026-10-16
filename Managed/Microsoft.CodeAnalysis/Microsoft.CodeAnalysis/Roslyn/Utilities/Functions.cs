using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Functions<[Nullable(2)] T>
	{
		// Token: 0x04000064 RID: 100
		public static readonly Func<T, T> Identity = (T t) => t;

		// Token: 0x04000065 RID: 101
		public static readonly Func<T, bool> True = (T t) => true;
	}
}
