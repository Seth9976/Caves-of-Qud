using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable last-in-first-out (LIFO) collection.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of elements in the stack.</typeparam>
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	public interface IImmutableStack<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		/// <summary>Gets a value that indicates whether this immutable stack is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if this stack is empty; otherwise,<see langword="false" />.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B1 RID: 177
		bool IsEmpty { get; }

		/// <summary>Removes all objects from the immutable stack.</summary>
		/// <returns>An empty immutable stack.</returns>
		// Token: 0x060000B2 RID: 178
		IImmutableStack<T> Clear();

		/// <summary>Inserts an element at the top of the immutable stack and returns the new stack.</summary>
		/// <param name="value">The element to push onto the stack.</param>
		/// <returns>The new stack.</returns>
		// Token: 0x060000B3 RID: 179
		IImmutableStack<T> Push(T value);

		/// <summary>Removes the element at the top of the immutable stack and returns the new stack.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>The new stack; never <see langword="null" />.</returns>
		// Token: 0x060000B4 RID: 180
		IImmutableStack<T> Pop();

		/// <summary>Returns the element at the top of the immutable stack without removing it.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>The element at the top of the stack.</returns>
		// Token: 0x060000B5 RID: 181
		T Peek();
	}
}
