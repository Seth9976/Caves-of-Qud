using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200003F RID: 63
	internal static class Predicates<[Nullable(2)] T>
	{
		// Token: 0x04000066 RID: 102
		[Nullable(1)]
		public static readonly Predicate<T> True = (T t) => true;
	}
}
