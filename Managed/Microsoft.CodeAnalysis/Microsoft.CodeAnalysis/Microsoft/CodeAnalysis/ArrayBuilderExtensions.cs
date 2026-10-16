using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000110 RID: 272
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ArrayBuilderExtensions
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0001AF94 File Offset: 0x00019194
		public static bool Any<[Nullable(2)] T>(this ArrayBuilder<T> builder, Func<T, bool> predicate)
		{
			foreach (T t in builder)
			{
				if (predicate.Invoke(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001AFC8 File Offset: 0x000191C8
		public static bool Any<[Nullable(2)] T, [Nullable(2)] A>(this ArrayBuilder<T> builder, Func<T, A, bool> predicate, A arg)
		{
			foreach (T t in builder)
			{
				if (predicate.Invoke(t, arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001AFFC File Offset: 0x000191FC
		public static bool All<[Nullable(2)] T>(this ArrayBuilder<T> builder, Func<T, bool> predicate)
		{
			foreach (T t in builder)
			{
				if (!predicate.Invoke(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001B030 File Offset: 0x00019230
		public static bool All<[Nullable(2)] T, [Nullable(2)] A>(this ArrayBuilder<T> builder, Func<T, A, bool> predicate, A arg)
		{
			foreach (T t in builder)
			{
				if (!predicate.Invoke(t, arg))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001B064 File Offset: 0x00019264
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<[Nullable(2)] TItem, [Nullable(2)] TResult>(this ArrayBuilder<TItem> items, Func<TItem, TResult> map)
		{
			switch (items.Count)
			{
			case 0:
				return ImmutableArray<TResult>.Empty;
			case 1:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0]));
			case 2:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0]), map.Invoke(items[1]));
			case 3:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0]), map.Invoke(items[1]), map.Invoke(items[2]));
			case 4:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0]), map.Invoke(items[1]), map.Invoke(items[2]), map.Invoke(items[3]));
			default:
			{
				ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance(items.Count);
				foreach (TItem titem in items)
				{
					instance.Add(map.Invoke(titem));
				}
				return instance.ToImmutableAndFree();
			}
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001B170 File Offset: 0x00019370
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<[Nullable(2)] TItem, [Nullable(2)] TArg, [Nullable(2)] TResult>(this ArrayBuilder<TItem> items, Func<TItem, TArg, TResult> map, TArg arg)
		{
			switch (items.Count)
			{
			case 0:
				return ImmutableArray<TResult>.Empty;
			case 1:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], arg));
			case 2:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], arg), map.Invoke(items[1], arg));
			case 3:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], arg), map.Invoke(items[1], arg), map.Invoke(items[2], arg));
			case 4:
				return ImmutableArray.Create<TResult>(map.Invoke(items[0], arg), map.Invoke(items[1], arg), map.Invoke(items[2], arg), map.Invoke(items[3], arg));
			default:
			{
				ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance(items.Count);
				foreach (TItem titem in items)
				{
					instance.Add(map.Invoke(titem, arg));
				}
				return instance.ToImmutableAndFree();
			}
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001B287 File Offset: 0x00019487
		public static void AddOptional<T>(this ArrayBuilder<T> builder, [Nullable(2)] T item) where T : class
		{
			if (item != null)
			{
				builder.Add(item);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001B298 File Offset: 0x00019498
		public static void Push<[Nullable(2)] T>(this ArrayBuilder<T> builder, T e)
		{
			builder.Add(e);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001B2A1 File Offset: 0x000194A1
		public static T Pop<[Nullable(2)] T>(this ArrayBuilder<T> builder)
		{
			T t = builder.Peek<T>();
			builder.RemoveAt(builder.Count - 1);
			return t;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001B2B7 File Offset: 0x000194B7
		public static bool TryPop<[Nullable(2)] T>(this ArrayBuilder<T> builder, [MaybeNullWhen(false)] out T result)
		{
			if (builder.Count > 0)
			{
				result = builder.Pop<T>();
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001B2D8 File Offset: 0x000194D8
		public static T Peek<[Nullable(2)] T>(this ArrayBuilder<T> builder)
		{
			return builder[builder.Count - 1];
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001B2E8 File Offset: 0x000194E8
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> ToImmutableOrEmptyAndFree<T>([Nullable(new byte[] { 2, 1 })] this ArrayBuilder<T> builder)
		{
			if (builder == null)
			{
				return ImmutableArray<T>.Empty;
			}
			return builder.ToImmutableAndFree();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001B2F9 File Offset: 0x000194F9
		[NullableContext(0)]
		public static void AddIfNotNull<T>([Nullable(new byte[] { 1, 0 })] this ArrayBuilder<T> builder, T? value) where T : struct
		{
			if (value != null)
			{
				builder.Add(value.Value);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001B311 File Offset: 0x00019511
		public static void AddIfNotNull<T>(this ArrayBuilder<T> builder, [Nullable(2)] T value) where T : class
		{
			if (value != null)
			{
				builder.Add(value);
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001B324 File Offset: 0x00019524
		[NullableContext(0)]
		public static void FreeAll<T>(this ArrayBuilder<T> builder, Func<T, ArrayBuilder<T>> getNested)
		{
			foreach (T t in builder)
			{
				ArrayBuilder<T> arrayBuilder = getNested.Invoke(t);
				if (arrayBuilder != null)
				{
					arrayBuilder.FreeAll<T>(getNested);
				}
			}
			builder.Free();
		}
	}
}
