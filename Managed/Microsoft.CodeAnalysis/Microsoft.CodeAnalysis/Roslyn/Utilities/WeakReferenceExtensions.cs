using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200006D RID: 109
	internal static class WeakReferenceExtensions
	{
		// Token: 0x06000293 RID: 659 RVA: 0x000093E4 File Offset: 0x000075E4
		[NullableContext(2)]
		public static T GetTarget<T>([Nullable(1)] this WeakReference<T> reference) where T : class
		{
			T t;
			reference.TryGetTarget(ref t);
			return t;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000093FC File Offset: 0x000075FC
		[NullableContext(1)]
		public static bool IsNull<[Nullable(2)] T>(this WeakReference<T> reference) where T : class
		{
			T t;
			return !reference.TryGetTarget(ref t);
		}
	}
}
