using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Roslyn.Utilities
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(0)]
	internal static class InterlockedOperations
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006208 File Offset: 0x00004408
		public static T Initialize<T>([Nullable(2)] [NotNull] ref T target, T value) where T : class
		{
			T t;
			if ((t = Interlocked.CompareExchange<T>(ref target, value, default(T))) == null)
			{
				t = value;
			}
			return t;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006230 File Offset: 0x00004430
		[return: NotNullIfNotNull("initializedValue")]
		public static T Initialize<[Nullable(2)] T>(ref T target, T initializedValue, T uninitializedValue) where T : class
		{
			T t = Interlocked.CompareExchange<T>(ref target, initializedValue, uninitializedValue);
			if (t != uninitializedValue)
			{
				return t;
			}
			return initializedValue;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006258 File Offset: 0x00004458
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Initialize<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> target, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> initializedValue)
		{
			ImmutableArray<T> immutableArray = ImmutableInterlocked.InterlockedCompareExchange<T>(ref target, initializedValue, default(ImmutableArray<T>));
			if (!immutableArray.IsDefault)
			{
				return immutableArray;
			}
			return initializedValue;
		}
	}
}
