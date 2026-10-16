using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableQueue`1" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableQueue
	{
		/// <summary>Creates an empty immutable queue.</summary>
		/// <typeparam name="T">The type of items to be stored in the immutable queue.</typeparam>
		/// <returns>An empty immutable queue.</returns>
		// Token: 0x06000283 RID: 643 RVA: 0x000075C8 File Offset: 0x000057C8
		public static ImmutableQueue<T> Create<[Nullable(2)] T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		/// <summary>Creates a new immutable queue that contains the specified item.</summary>
		/// <param name="item">The item to prepopulate the queue with.</param>
		/// <typeparam name="T">The type of items in the immutable queue.</typeparam>
		/// <returns>A new immutable queue that contains the specified item.</returns>
		// Token: 0x06000284 RID: 644 RVA: 0x000075CF File Offset: 0x000057CF
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		/// <summary>Creates a new immutable queue that contains the specified items.</summary>
		/// <param name="items">The items to add to the queue before immutability is applied.</param>
		/// <typeparam name="T">The type of elements in the queue.</typeparam>
		/// <returns>An immutable queue that contains the specified items.</returns>
		// Token: 0x06000285 RID: 645 RVA: 0x000075DC File Offset: 0x000057DC
		public static ImmutableQueue<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			T[] array = items as T[];
			if (array != null)
			{
				return ImmutableQueue.Create<T>(array);
			}
			ImmutableQueue<T> immutableQueue;
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					immutableQueue = ImmutableQueue<T>.Empty;
				}
				else
				{
					ImmutableStack<T> immutableStack = ImmutableStack.Create<T>(enumerator.Current);
					ImmutableStack<T> immutableStack2 = ImmutableStack<T>.Empty;
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						immutableStack2 = immutableStack2.Push(t);
					}
					immutableQueue = new ImmutableQueue<T>(immutableStack, immutableStack2);
				}
			}
			return immutableQueue;
		}

		/// <summary>Creates a new immutable queue that contains the specified array of items.</summary>
		/// <param name="items">An array that contains the items to prepopulate the queue with.</param>
		/// <typeparam name="T">The type of items in the immutable queue.</typeparam>
		/// <returns>A new immutable queue that contains the specified items.</returns>
		// Token: 0x06000286 RID: 646 RVA: 0x00007670 File Offset: 0x00005870
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			if (items.Length == 0)
			{
				return ImmutableQueue<T>.Empty;
			}
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			for (int i = items.Length - 1; i >= 0; i--)
			{
				immutableStack = immutableStack.Push(items[i]);
			}
			return new ImmutableQueue<T>(immutableStack, ImmutableStack<T>.Empty);
		}

		/// <summary>Removes the item at the beginning of the immutable queue, and returns the new queue.</summary>
		/// <param name="queue">The queue to remove the item from.</param>
		/// <param name="value">When this method returns, contains the item from the beginning of the queue.</param>
		/// <typeparam name="T">The type of elements in the immutable queue.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>The new queue with the item removed.</returns>
		// Token: 0x06000287 RID: 647 RVA: 0x000076C1 File Offset: 0x000058C1
		public static IImmutableQueue<T> Dequeue<[Nullable(2)] T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
