using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Provides a set of initialization methods for instances of the <see cref="T:System.Collections.Immutable.ImmutableStack`1" /> class.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableStack
	{
		/// <summary>Creates an empty immutable stack.</summary>
		/// <typeparam name="T">The type of items to be stored in the immutable stack.</typeparam>
		/// <returns>An empty immutable stack.</returns>
		// Token: 0x06000346 RID: 838 RVA: 0x00008E25 File Offset: 0x00007025
		public static ImmutableStack<T> Create<[Nullable(2)] T>()
		{
			return ImmutableStack<T>.Empty;
		}

		/// <summary>Creates a new immutable stack that contains the specified item.</summary>
		/// <param name="item">The item to prepopulate the stack with.</param>
		/// <typeparam name="T">The type of items in the immutable stack.</typeparam>
		/// <returns>A new immutable collection that contains the specified item.</returns>
		// Token: 0x06000347 RID: 839 RVA: 0x00008E2C File Offset: 0x0000702C
		public static ImmutableStack<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		/// <summary>Creates a new immutable stack that contains the specified items.</summary>
		/// <param name="items">The items to add to the stack before it's immutable.</param>
		/// <typeparam name="T">The type of items in the stack.</typeparam>
		/// <returns>An immutable stack that contains the specified items.</returns>
		// Token: 0x06000348 RID: 840 RVA: 0x00008E3C File Offset: 0x0000703C
		public static ImmutableStack<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T t in items)
			{
				immutableStack = immutableStack.Push(t);
			}
			return immutableStack;
		}

		/// <summary>Creates a new immutable stack that contains the specified array of items.</summary>
		/// <param name="items">An array that contains the items to prepopulate the stack with.</param>
		/// <typeparam name="T">The type of items in the immutable stack.</typeparam>
		/// <returns>A new immutable stack that contains the specified items.</returns>
		// Token: 0x06000349 RID: 841 RVA: 0x00008E98 File Offset: 0x00007098
		public static ImmutableStack<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T t in items)
			{
				immutableStack = immutableStack.Push(t);
			}
			return immutableStack;
		}

		/// <summary>Removes the specified item from an immutable stack.</summary>
		/// <param name="stack">The stack to modify.</param>
		/// <param name="value">The item to remove from the stack.</param>
		/// <typeparam name="T">The type of items contained in the stack.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>A stack; never <see langword="null" />.</returns>
		// Token: 0x0600034A RID: 842 RVA: 0x00008ED7 File Offset: 0x000070D7
		public static IImmutableStack<T> Pop<[Nullable(2)] T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
