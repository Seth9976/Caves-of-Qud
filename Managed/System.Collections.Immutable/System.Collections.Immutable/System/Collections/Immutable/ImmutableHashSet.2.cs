using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableHashSet`1" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableHashSet
	{
		/// <summary>Creates an empty immutable hash set.</summary>
		/// <typeparam name="T">The type of items to be stored in the immutable hash set.</typeparam>
		/// <returns>An empty immutable hash set.</returns>
		// Token: 0x060001EE RID: 494 RVA: 0x000064B4 File Offset: 0x000046B4
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>()
		{
			return ImmutableHashSet<T>.Empty;
		}

		/// <summary>Creates an empty immutable hash set that uses the specified equality comparer.</summary>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <typeparam name="T">The type of items in the immutable hash set.</typeparam>
		/// <returns>An empty immutable hash set.</returns>
		// Token: 0x060001EF RID: 495 RVA: 0x000064BB File Offset: 0x000046BB
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
		}

		/// <summary>Creates a new immutable hash set that contains the specified item.</summary>
		/// <param name="item">The item to prepopulate the hash set with.</param>
		/// <typeparam name="T">The type of items in the immutable hash set.</typeparam>
		/// <returns>A new immutable hash set that contains the specified item.</returns>
		// Token: 0x060001F0 RID: 496 RVA: 0x000064C8 File Offset: 0x000046C8
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableHashSet<T>.Empty.Add(item);
		}

		/// <summary>Creates a new immutable hash set that contains the specified item and uses the specified equality comparer for the set type.</summary>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <param name="item">The item to prepopulate the hash set with.</param>
		/// <typeparam name="T">The type of items in the immutable hash set.</typeparam>
		/// <returns>A new immutable hash set that contains the specified item.</returns>
		// Token: 0x060001F1 RID: 497 RVA: 0x000064D5 File Offset: 0x000046D5
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, T item)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
		}

		/// <summary>Creates a new immutable hash set prefilled with the specified items.</summary>
		/// <param name="items">The items to add to the hash set.</param>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The new immutable hash set that contains the specified items.</returns>
		// Token: 0x060001F2 RID: 498 RVA: 0x000064E8 File Offset: 0x000046E8
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		/// <summary>Creates a new immutable hash set that contains the specified items and uses the specified equality comparer for the set type.</summary>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <param name="items">The items add to the collection before immutability is applied.</param>
		/// <typeparam name="T">The type of items stored in the collection.</typeparam>
		/// <returns>The new immutable hash set.</returns>
		// Token: 0x060001F3 RID: 499 RVA: 0x000064F5 File Offset: 0x000046F5
		public static ImmutableHashSet<T> CreateRange<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, IEnumerable<T> items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		/// <summary>Creates a new immutable hash set that contains the specified array of items.</summary>
		/// <param name="items">An array that contains the items to prepopulate the hash set with.</param>
		/// <typeparam name="T">The type of items in the immutable hash set.</typeparam>
		/// <returns>A new immutable hash set that contains the specified items.</returns>
		// Token: 0x060001F4 RID: 500 RVA: 0x00006508 File Offset: 0x00004708
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>(params T[] items)
		{
			return ImmutableHashSet<T>.Empty.Union(items);
		}

		/// <summary>Creates a new immutable hash set that contains the items in the specified collection and uses the specified equality comparer for the set type.</summary>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <param name="items">An array that contains the items to prepopulate the hash set with.</param>
		/// <typeparam name="T">The type of items stored in the immutable hash set.</typeparam>
		/// <returns>A new immutable hash set that contains the specified items.</returns>
		// Token: 0x060001F5 RID: 501 RVA: 0x00006515 File Offset: 0x00004715
		public static ImmutableHashSet<T> Create<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer, params T[] items)
		{
			return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
		}

		/// <summary>Creates a new immutable hash set builder.</summary>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The immutable hash set builder.</returns>
		// Token: 0x060001F6 RID: 502 RVA: 0x00006528 File Offset: 0x00004728
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>()
		{
			return ImmutableHashSet.Create<T>().ToBuilder();
		}

		/// <summary>Creates a new immutable hash set builder.</summary>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <typeparam name="T">The type of items stored by the collection.</typeparam>
		/// <returns>The new immutable hash set builder.</returns>
		// Token: 0x060001F7 RID: 503 RVA: 0x00006534 File Offset: 0x00004734
		public static ImmutableHashSet<T>.Builder CreateBuilder<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> equalityComparer)
		{
			return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
		}

		/// <summary>Enumerates a sequence, produces an immutable hash set of its contents, and uses the specified equality comparer for the set type.</summary>
		/// <param name="source">The sequence to enumerate.</param>
		/// <param name="equalityComparer">The object to use for comparing objects in the set for equality.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <returns>An immutable hash set that contains the items in the specified sequence and uses the specified equality comparer.</returns>
		// Token: 0x060001F8 RID: 504 RVA: 0x00006544 File Offset: 0x00004744
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<TSource> equalityComparer)
		{
			ImmutableHashSet<TSource> immutableHashSet = source as ImmutableHashSet<TSource>;
			if (immutableHashSet != null)
			{
				return immutableHashSet.WithComparer(equalityComparer);
			}
			return ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
		}

		/// <summary>Creates an immutable hash set from the current contents of the builder's set.</summary>
		/// <param name="builder">The builder to create the immutable hash set from.</param>
		/// <typeparam name="TSource">The type of the elements in the hash set.</typeparam>
		/// <returns>An immutable hash set that contains the current contents in the builder's set.</returns>
		// Token: 0x060001F9 RID: 505 RVA: 0x00006574 File Offset: 0x00004774
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this ImmutableHashSet<TSource>.Builder builder)
		{
			Requires.NotNull<ImmutableHashSet<TSource>.Builder>(builder, "builder");
			return builder.ToImmutable();
		}

		/// <summary>Enumerates a sequence and produces an immutable hash set of its contents.</summary>
		/// <param name="source">The sequence to enumerate.</param>
		/// <typeparam name="TSource">The type of the elements in the sequence.</typeparam>
		/// <returns>An immutable hash set that contains the items in the specified sequence.</returns>
		// Token: 0x060001FA RID: 506 RVA: 0x00006587 File Offset: 0x00004787
		public static ImmutableHashSet<TSource> ToImmutableHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return source.ToImmutableHashSet<TSource>(null);
		}
	}
}
