using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200011E RID: 286
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ImmutableArrayExtensions
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0001C21E File Offset: 0x0001A41E
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutable<[Nullable(2)] T>(this IEnumerable<T> items)
		{
			return ImmutableArray.CreateRange<T>(items);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001C226 File Offset: 0x0001A426
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutableOrEmpty<T>([Nullable(new byte[] { 2, 1 })] this IEnumerable<T> items)
		{
			if (items == null)
			{
				return ImmutableArray<T>.Empty;
			}
			return ImmutableArray.CreateRange<T>(items);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001C238 File Offset: 0x0001A438
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutableOrNull<T>([Nullable(new byte[] { 2, 1 })] this IEnumerable<T> items)
		{
			if (items == null)
			{
				return default(ImmutableArray<T>);
			}
			return ImmutableArray.CreateRange<T>(items);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001C258 File Offset: 0x0001A458
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutable<[Nullable(2)] T>(this T[] items)
		{
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001C260 File Offset: 0x0001A460
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutableOrNull<T>([Nullable(new byte[] { 2, 1 })] this T[] items)
		{
			if (items == null)
			{
				return default(ImmutableArray<T>);
			}
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001C280 File Offset: 0x0001A480
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> AsImmutableOrEmpty<T>([Nullable(new byte[] { 2, 1 })] this T[] items)
		{
			if (items == null)
			{
				return ImmutableArray<T>.Empty;
			}
			return ImmutableArray.Create<T>(items);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001C291 File Offset: 0x0001A491
		[NullableContext(0)]
		public static ImmutableArray<byte> ToImmutable([Nullable(1)] this MemoryStream stream)
		{
			return ImmutableArray.Create<byte>(stream.ToArray());
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001C29E File Offset: 0x0001A49E
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<TItem, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TItem> items, [Nullable(1)] Func<TItem, TResult> map)
		{
			return ImmutableArray.CreateRange<TItem, TResult>(items, map);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001C2A7 File Offset: 0x0001A4A7
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<TItem, TArg, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TItem> items, [Nullable(1)] Func<TItem, TArg, TResult> map, [Nullable(1)] TArg arg)
		{
			return ImmutableArray.CreateRange<TItem, TArg, TResult>(items, map, arg);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<TItem, TArg, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TItem> items, [Nullable(1)] Func<TItem, int, TArg, TResult> map, [Nullable(1)] TArg arg)
		{
			switch (items.Length)
			{
			case 0:
				return ImmutableArray<TResult>.Empty;
			case 1:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], 0, arg));
			case 2:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], 0, arg), map.Invoke(items[1], 1, arg));
			case 3:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], 0, arg), map.Invoke(items[1], 1, arg), map.Invoke(items[2], 2, arg));
			case 4:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], 0, arg), map.Invoke(items[1], 1, arg), map.Invoke(items[2], 2, arg), map.Invoke(items[3], 3, arg));
			default:
			{
				ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance(items.Length);
				for (int i = 0; i < items.Length; i++)
				{
					instance.Add(map.Invoke(items[i], i, arg));
				}
				return instance.ToImmutableAndFree();
			}
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<[Nullable(2)] TItem, [Nullable(2)] TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TItem> array, Func<TItem, bool> predicate, Func<TItem, TResult> selector)
		{
			if (array.Length == 0)
			{
				return ImmutableArray<TResult>.Empty;
			}
			ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance();
			foreach (TItem titem in array)
			{
				if (predicate.Invoke(titem))
				{
					instance.Add(selector.Invoke(titem));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001C440 File Offset: 0x0001A640
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 0, 1 })]
		public static ValueTask<ImmutableArray<TResult>> SelectAsArrayAsync<TItem, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TItem> array, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<TItem, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken)
		{
			ImmutableArrayExtensions.<SelectAsArrayAsync>d__11<TItem, TResult> <SelectAsArrayAsync>d__;
			<SelectAsArrayAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<ImmutableArray<TResult>>.Create();
			<SelectAsArrayAsync>d__.array = array;
			<SelectAsArrayAsync>d__.selector = selector;
			<SelectAsArrayAsync>d__.cancellationToken = cancellationToken;
			<SelectAsArrayAsync>d__.<>1__state = -1;
			<SelectAsArrayAsync>d__.<>t__builder.Start<ImmutableArrayExtensions.<SelectAsArrayAsync>d__11<TItem, TResult>>(ref <SelectAsArrayAsync>d__);
			return <SelectAsArrayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001C494 File Offset: 0x0001A694
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> ZipAsArray<T1, T2, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T1> self, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T2> other, [Nullable(1)] Func<T1, T2, TResult> map)
		{
			switch (self.Length)
			{
			case 0:
				return ImmutableArray<TResult>.Empty;
			case 1:
				return ImmutableArray.Create<TResult>(map.Invoke(self[0], other[0]));
			case 2:
				return ImmutableArray.Create<TResult>(map.Invoke(self[0], other[0]), map.Invoke(self[1], other[1]));
			case 3:
				return ImmutableArray.Create<TResult>(map.Invoke(self[0], other[0]), map.Invoke(self[1], other[1]), map.Invoke(self[2], other[2]));
			case 4:
				return ImmutableArray.Create<TResult>(map.Invoke(self[0], other[0]), map.Invoke(self[1], other[1]), map.Invoke(self[2], other[2]), map.Invoke(self[3], other[3]));
			default:
			{
				ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance(self.Length);
				for (int i = 0; i < self.Length; i++)
				{
					instance.Add(map.Invoke(self[i], other[i]));
				}
				return instance.ToImmutableAndFree();
			}
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001C604 File Offset: 0x0001A804
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> ZipAsArray<T1, T2, TArg, TResult>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T1> self, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T2> other, [Nullable(1)] TArg arg, [Nullable(1)] Func<T1, T2, int, TArg, TResult> map)
		{
			if (self.IsEmpty)
			{
				return ImmutableArray<TResult>.Empty;
			}
			ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance(self.Length);
			for (int i = 0; i < self.Length; i++)
			{
				instance.Add(map.Invoke(self[i], other[i], i, arg));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001C663 File Offset: 0x0001A863
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> WhereAsArray<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, bool> predicate)
		{
			return ImmutableArrayExtensions.WhereAsArrayImpl<T, object>(array, predicate, null, null);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001C66E File Offset: 0x0001A86E
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> WhereAsArray<[Nullable(2)] T, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, TArg, bool> predicate, TArg arg)
		{
			return ImmutableArrayExtensions.WhereAsArrayImpl<T, TArg>(array, null, predicate, arg);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001C67C File Offset: 0x0001A87C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<T> WhereAsArrayImpl<T, TArg>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> array, [Nullable(new byte[] { 2, 1 })] Func<T, bool> predicateWithoutArg, [Nullable(new byte[] { 2, 1, 1 })] Func<T, TArg, bool> predicateWithArg, [Nullable(1)] TArg arg)
		{
			ArrayBuilder<T> arrayBuilder = null;
			bool flag = true;
			bool flag2 = true;
			int length = array.Length;
			for (int i = 0; i < length; i++)
			{
				T t = array[i];
				if ((predicateWithoutArg != null) ? predicateWithoutArg.Invoke(t) : predicateWithArg.Invoke(t, arg))
				{
					flag = false;
					if (!flag2)
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<T>.GetInstance();
						}
						arrayBuilder.Add(t);
					}
				}
				else if (flag)
				{
					flag2 = false;
				}
				else if (flag2)
				{
					flag2 = false;
					arrayBuilder = ArrayBuilder<T>.GetInstance();
					for (int j = 0; j < i; j++)
					{
						arrayBuilder.Add(array[j]);
					}
				}
			}
			if (arrayBuilder != null)
			{
				return arrayBuilder.ToImmutableAndFree();
			}
			if (flag2)
			{
				return array;
			}
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001C728 File Offset: 0x0001A928
		public static bool Any<[Nullable(2)] T, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, TArg, bool> predicate, TArg arg)
		{
			int length = array.Length;
			for (int i = 0; i < length; i++)
			{
				T t = array[i];
				if (predicate.Invoke(t, arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001C760 File Offset: 0x0001A960
		public static bool All<[Nullable(2)] T, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, TArg, bool> predicate, TArg arg)
		{
			int length = array.Length;
			for (int i = 0; i < length; i++)
			{
				T t = array[i];
				if (!predicate.Invoke(t, arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001C798 File Offset: 0x0001A998
		public static Task<bool> AnyAsync<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, Task<bool>> predicateAsync)
		{
			ImmutableArrayExtensions.<AnyAsync>d__19<T> <AnyAsync>d__;
			<AnyAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AnyAsync>d__.array = array;
			<AnyAsync>d__.predicateAsync = predicateAsync;
			<AnyAsync>d__.<>1__state = -1;
			<AnyAsync>d__.<>t__builder.Start<ImmutableArrayExtensions.<AnyAsync>d__19<T>>(ref <AnyAsync>d__);
			return <AnyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		public static Task<bool> AnyAsync<[Nullable(2)] T, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, TArg, Task<bool>> predicateAsync, TArg arg)
		{
			ImmutableArrayExtensions.<AnyAsync>d__20<T, TArg> <AnyAsync>d__;
			<AnyAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AnyAsync>d__.array = array;
			<AnyAsync>d__.predicateAsync = predicateAsync;
			<AnyAsync>d__.arg = arg;
			<AnyAsync>d__.<>1__state = -1;
			<AnyAsync>d__.<>t__builder.Start<ImmutableArrayExtensions.<AnyAsync>d__20<T, TArg>>(ref <AnyAsync>d__);
			return <AnyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001C838 File Offset: 0x0001AA38
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<T> FirstOrDefaultAsync<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, Func<T, Task<bool>> predicateAsync)
		{
			ImmutableArrayExtensions.<FirstOrDefaultAsync>d__21<T> <FirstOrDefaultAsync>d__;
			<FirstOrDefaultAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<T>.Create();
			<FirstOrDefaultAsync>d__.array = array;
			<FirstOrDefaultAsync>d__.predicateAsync = predicateAsync;
			<FirstOrDefaultAsync>d__.<>1__state = -1;
			<FirstOrDefaultAsync>d__.<>t__builder.Start<ImmutableArrayExtensions.<FirstOrDefaultAsync>d__21<T>>(ref <FirstOrDefaultAsync>d__);
			return <FirstOrDefaultAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001C883 File Offset: 0x0001AA83
		[MethodImpl(256)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TBase> Cast<TDerived, [Nullable(2)] TBase>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TDerived> items) where TDerived : class, TBase
		{
			return ImmutableArray<TBase>.CastUp<TDerived>(items);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public static bool SetEquals<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array1, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> array2, IEqualityComparer<T> comparer)
		{
			if (array1.IsDefault)
			{
				return array2.IsDefault;
			}
			if (array2.IsDefault)
			{
				return false;
			}
			int length = array1.Length;
			int length2 = array2.Length;
			if (length == 0)
			{
				return length2 == 0;
			}
			if (length2 == 0)
			{
				return false;
			}
			if (length == 1 && length2 == 1)
			{
				T t = array1[0];
				T t2 = array2[0];
				return comparer.Equals(t, t2);
			}
			HashSet<T> hashSet = new HashSet<T>(array1, comparer);
			HashSet<T> hashSet2 = new HashSet<T>(array2, comparer);
			return hashSet.SetEquals(hashSet2);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001C916 File Offset: 0x0001AB16
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> NullToEmpty<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array)
		{
			if (!array.IsDefault)
			{
				return array;
			}
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001C928 File Offset: 0x0001AB28
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> Distinct<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer = null)
		{
			if (array.Length < 2)
			{
				return array;
			}
			HashSet<T> hashSet = new HashSet<T>(comparer);
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
			foreach (T t in array)
			{
				if (hashSet.Add(t))
				{
					instance.Add(t);
				}
			}
			ImmutableArray<T> immutableArray = ((instance.Count == array.Length) ? array : instance.ToImmutable());
			instance.Free();
			return immutableArray;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001C998 File Offset: 0x0001AB98
		[NullableContext(0)]
		internal static bool HasAnyErrors<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> diagnostics) where T : Diagnostic
		{
			ImmutableArray<T>.Enumerator enumerator = diagnostics.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Severity == DiagnosticSeverity.Error)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001C9CF File Offset: 0x0001ABCF
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> ConditionallyDeOrder<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array)
		{
			return array;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001C9D4 File Offset: 0x0001ABD4
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<TValue> Flatten<TKey, [Nullable(2)] TValue>([Nullable(new byte[] { 1, 1, 0, 1 })] this Dictionary<TKey, ImmutableArray<TValue>> dictionary, [Nullable(new byte[] { 2, 1 })] IComparer<TValue> comparer = null)
		{
			if (dictionary.Count == 0)
			{
				return ImmutableArray<TValue>.Empty;
			}
			ArrayBuilder<TValue> instance = ArrayBuilder<TValue>.GetInstance();
			foreach (KeyValuePair<TKey, ImmutableArray<TValue>> keyValuePair in dictionary)
			{
				instance.AddRange(keyValuePair.Value);
			}
			if (comparer != null && instance.Count > 1)
			{
				instance.Sort(comparer);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001CA58 File Offset: 0x0001AC58
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> Concat<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> first, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> second)
		{
			return first.AddRange(second);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001CA62 File Offset: 0x0001AC62
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> Concat<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> first, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> second, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> third)
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance(first.Length + second.Length + third.Length);
			instance.AddRange(first);
			instance.AddRange(second);
			instance.AddRange(third);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> Concat<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> first, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> second, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> third, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> fourth)
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance(first.Length + second.Length + third.Length + fourth.Length);
			instance.AddRange(first);
			instance.AddRange(second);
			instance.AddRange(third);
			instance.AddRange(fourth);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001CAEE File Offset: 0x0001ACEE
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<T> Concat<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> first, T second)
		{
			return first.Add(second);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		internal static bool HasDuplicates<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, IEqualityComparer<T> comparer)
		{
			int length = array.Length;
			if (length <= 1)
			{
				return false;
			}
			if (length != 2)
			{
				HashSet<T> hashSet = new HashSet<T>(comparer);
				foreach (T t in array)
				{
					if (!hashSet.Add(t))
					{
						return true;
					}
				}
				return false;
			}
			return comparer.Equals(array[0], array[1]);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001CB60 File Offset: 0x0001AD60
		public static int Count<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> items, Func<T, bool> predicate)
		{
			if (items.IsEmpty)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (predicate.Invoke(items[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001CBA1 File Offset: 0x0001ADA1
		internal static T[] DangerousGetUnderlyingArray<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array)
		{
			return Unsafe.As<ImmutableArray<T>, ImmutableArrayExtensions.ImmutableArrayProxy<T>>(ref array).MutableArray;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001CBAF File Offset: 0x0001ADAF
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ReadOnlySpan<T> AsSpan<T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array)
		{
			return array.DangerousGetUnderlyingArray<T>();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		[return: Nullable(new byte[] { 0, 1 })]
		internal unsafe static ImmutableArray<T> DangerousCreateFromUnderlyingArray<[Nullable(2)] T>([MaybeNull] ref T[] array)
		{
			ImmutableArrayExtensions.ImmutableArrayProxy<T> immutableArrayProxy = new ImmutableArrayExtensions.ImmutableArrayProxy<T>
			{
				MutableArray = array
			};
			array = null;
			return *Unsafe.As<ImmutableArrayExtensions.ImmutableArrayProxy<T>, ImmutableArray<T>>(ref immutableArrayProxy);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		internal static Dictionary<K, ImmutableArray<T>> ToDictionary<K, [Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> items, Func<T, K> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<K> comparer = null)
		{
			if (items.Length == 1)
			{
				Dictionary<K, ImmutableArray<T>> dictionary = new Dictionary<K, ImmutableArray<T>>(1, comparer);
				T t = items[0];
				dictionary.Add(keySelector.Invoke(t), ImmutableArray.Create<T>(t));
				return dictionary;
			}
			if (items.Length == 0)
			{
				return new Dictionary<K, ImmutableArray<T>>(comparer);
			}
			Dictionary<K, ArrayBuilder<T>> dictionary2 = new Dictionary<K, ArrayBuilder<T>>(items.Length, comparer);
			for (int i = 0; i < items.Length; i++)
			{
				T t2 = items[i];
				K k = keySelector.Invoke(t2);
				ArrayBuilder<T> instance;
				if (!dictionary2.TryGetValue(k, ref instance))
				{
					instance = ArrayBuilder<T>.GetInstance();
					dictionary2.Add(k, instance);
				}
				instance.Add(t2);
			}
			Dictionary<K, ImmutableArray<T>> dictionary3 = new Dictionary<K, ImmutableArray<T>>(dictionary2.Count, comparer);
			foreach (KeyValuePair<K, ArrayBuilder<T>> keyValuePair in dictionary2)
			{
				dictionary3.Add(keyValuePair.Key, keyValuePair.Value.ToImmutableAndFree());
			}
			return dictionary3;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		internal static Location FirstOrNone([Nullable(new byte[] { 0, 1 })] this ImmutableArray<Location> items)
		{
			if (!items.IsEmpty)
			{
				return items[0];
			}
			return Location.None;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001CD10 File Offset: 0x0001AF10
		internal static bool SequenceEqual<[Nullable(2)] TElement, [Nullable(2)] TArg>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<TElement> array1, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TElement> array2, TArg arg, Func<TElement, TElement, TArg, bool> predicate)
		{
			if (array1.IsDefault)
			{
				throw new NullReferenceException();
			}
			if (array2.IsDefault)
			{
				throw new NullReferenceException();
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!predicate.Invoke(array1[i], array2[i], arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001CD7B File Offset: 0x0001AF7B
		internal static int IndexOf<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] this ImmutableArray<T> array, T item, IEqualityComparer<T> comparer)
		{
			return array.IndexOf(item, 0, comparer);
		}

		// Token: 0x02000545 RID: 1349
		[NullableContext(0)]
		private struct ImmutableArrayProxy<[Nullable(2)] T>
		{
			// Token: 0x040014F1 RID: 5361
			[Nullable(1)]
			internal T[] MutableArray;
		}
	}
}
