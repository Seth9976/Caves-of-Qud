using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Roslyn.Utilities
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RoslynLazyInitializer
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00006F85 File Offset: 0x00005185
		public static T EnsureInitialized<T>([Nullable(2)] [NotNull] ref T target) where T : class
		{
			return LazyInitializer.EnsureInitialized<T>(ref target);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006F8D File Offset: 0x0000518D
		public static T EnsureInitialized<T>([Nullable(2)] [NotNull] ref T target, Func<T> valueFactory) where T : class
		{
			return LazyInitializer.EnsureInitialized<T>(ref target, valueFactory);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006F96 File Offset: 0x00005196
		[NullableContext(2)]
		[return: Nullable(1)]
		public static T EnsureInitialized<T>([NotNull] ref T target, ref bool initialized, [NotNullIfNotNull("syncLock")] ref object syncLock)
		{
			return LazyInitializer.EnsureInitialized<T>(ref target, ref initialized, ref syncLock);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006FA0 File Offset: 0x000051A0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static T EnsureInitialized<T>([NotNull] ref T target, ref bool initialized, [NotNullIfNotNull("syncLock")] ref object syncLock, [Nullable(1)] Func<T> valueFactory)
		{
			return LazyInitializer.EnsureInitialized<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}
	}
}
