using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableSortedSet`1" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableSortedSet
	{
		/// <summary>Creates an empty immutable sorted set.</summary>
		/// <typeparam name="T">The type of items to be stored in the immutable set.</typeparam>
		/// <returns>An empty immutable sorted set.</returns>
		// Token: 0x060002EE RID: 750 RVA: 0x000082A0 File Offset: 0x000064A0
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableSortedSet<T>.Empty;
		}

		/// <summary>Creates an empty immutable sorted set that uses the specified comparer.</summary>
		/// <param name="comparer">The implementation to use when comparing items in the set.</param>
		/// <typeparam name="T">The type of items in the immutable set.</typeparam>
		/// <returns>An empty immutable set.</returns>
		// Token: 0x060002EF RID: 751 RVA: 0x000082A7 File Offset: 0x000064A7
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
		}

		/// <summary>Creates a new immutable sorted set that contains the specified item.</summary>
		/// <param name="item">The item to prepopulate the set with.</param>
		/// <typeparam name="T">The type of items in the immutable set.</typeparam>
		/// <returns>A new immutable set that contains the specified item.</returns>
		// Token: 0x060002F0 RID: 752 RVA: 0x000082B4 File Offset: 0x000064B4
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableSortedSet<T>.Empty.Add(item);
		}

		/// <summary>Creates a new immutable sorted set that contains the specified item and uses the specified comparer.</summary>
		/// <param name="comparer">The implementation to use when comparing items in the set.</param>
		/// <param name="item">The item to prepopulate the set with.</param>
		/// <typeparam name="T">The type of items stored in the immutable set.</typeparam>
		/// <returns>A new immutable set that contains the specified item.</returns>
		// Token: 0x060002F1 RID: 753 RVA: 0x000082C1 File Offset: 0x000064C1
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, T item)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
		}

		/// <summary>Creates a new immutable collection that contains the specified items.</summary>
		/// <param name="items">The items to add to the set with before it's immutable.</param>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The new immutable set that contains the specified items.</returns>
		// Token: 0x060002F2 RID: 754 RVA: 0x000082D4 File Offset: 0x000064D4
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		/// <summary>Creates a new immutable collection that contains the specified items.</summary>
		/// <param name="comparer">The comparer to use to compare elements in this set.</param>
		/// <param name="items">The items to add to the set before it's immutable.</param>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The new immutable set that contains the specified items.</returns>
		// Token: 0x060002F3 RID: 755 RVA: 0x000082E1 File Offset: 0x000064E1
		public static ImmutableSortedSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, IEnumerable<T> items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		/// <summary>Creates a new immutable sorted set that contains the specified array of items.</summary>
		/// <param name="items">An array that contains the items to prepopulate the set with.</param>
		/// <typeparam name="T">The type of items in the immutable set.</typeparam>
		/// <returns>A new immutable set that contains the specified items.</returns>
		// Token: 0x060002F4 RID: 756 RVA: 0x000082F4 File Offset: 0x000064F4
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.Union(items);
		}

		/// <summary>Creates a new immutable sorted set that contains the specified array of items and uses the specified comparer.</summary>
		/// <param name="comparer">The implementation to use when comparing items in the set.</param>
		/// <param name="items">An array that contains the items to prepopulate the set with.</param>
		/// <typeparam name="T">The type of items in the immutable set.</typeparam>
		/// <returns>A new immutable set that contains the specified items.</returns>
		// Token: 0x060002F5 RID: 757 RVA: 0x00008301 File Offset: 0x00006501
		public static ImmutableSortedSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer, params T[] items)
		{
			return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
		}

		/// <summary>Returns a collection that can be used to build an immutable sorted set.</summary>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The immutable collection builder.</returns>
		// Token: 0x060002F6 RID: 758 RVA: 0x00008314 File Offset: 0x00006514
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableSortedSet.Create<T>().ToBuilder();
		}

		/// <summary>Returns a collection that can be used to build an immutable sorted set.</summary>
		/// <param name="comparer">The comparer used to compare items in the set for equality.</param>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The immutable collection.</returns>
		// Token: 0x060002F7 RID: 759 RVA: 0x00008320 File Offset: 0x00006520
		public static ImmutableSortedSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
		}

		/// <summary>Enumerates a sequence, produces an immutable sorted set of its contents, and uses the specified comparer.</summary>
		/// <param name="source">The sequence to enumerate.</param>
		/// <param name="comparer">The comparer to use for initializing and adding members to the sorted set.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <returns>An immutable sorted set that contains the items in the specified sequence.</returns>
		// Token: 0x060002F8 RID: 760 RVA: 0x00008330 File Offset: 0x00006530
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IComparer<TSource> comparer)
		{
			ImmutableSortedSet<TSource> immutableSortedSet = source as ImmutableSortedSet<TSource>;
			if (immutableSortedSet != null)
			{
				return immutableSortedSet.WithComparer(comparer);
			}
			return ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
		}

		/// <summary>Enumerates a sequence and produces an immutable sorted set of its contents.</summary>
		/// <param name="source">The sequence to enumerate.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <returns>An immutable sorted set that contains the items in the specified sequence.</returns>
		// Token: 0x060002F9 RID: 761 RVA: 0x00008360 File Offset: 0x00006560
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableSortedSet<TSource>(null);
		}

		/// <summary>Creates an immutable sorted set from the current contents of the builder's set.</summary>
		/// <param name="builder">The builder to create the immutable sorted set from.</param>
		/// <typeparam name="TSource">The type of the elements in the immutable sorted set.</typeparam>
		/// <returns>An immutable sorted set that contains the current contents in the builder's set.</returns>
		// Token: 0x060002FA RID: 762 RVA: 0x00008369 File Offset: 0x00006569
		public static ImmutableSortedSet<TSource> ToImmutableSortedSet<[Nullable(2)] TSource>(this ImmutableSortedSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}
	}
}
