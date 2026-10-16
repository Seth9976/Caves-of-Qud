using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable first-in, first-out collection of objects.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of elements in the queue.</typeparam>
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	public interface IImmutableQueue<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
	{
		/// <summary>Gets a value that indicates whether this immutable queue is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if this queue is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009D RID: 157
		bool IsEmpty { get; }

		/// <summary>Returns a new queue with all the elements removed.</summary>
		/// <returns>An empty immutable queue.</returns>
		// Token: 0x0600009E RID: 158
		IImmutableQueue<T> Clear();

		/// <summary>Returns the element at the beginning of the immutable queue without removing it.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The element at the beginning of the queue.</returns>
		// Token: 0x0600009F RID: 159
		T Peek();

		/// <summary>Adds an element to the end of the immutable queue, and returns the new queue.</summary>
		/// <param name="value">The element to add.</param>
		/// <returns>The new immutable queue with the specified element added.</returns>
		// Token: 0x060000A0 RID: 160
		IImmutableQueue<T> Enqueue(T value);

		/// <summary>Removes the first element in the immutable queue, and returns the new queue.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The new immutable queue with the first element removed. This value is never <see langword="null" />.</returns>
		// Token: 0x060000A1 RID: 161
		IImmutableQueue<T> Dequeue();
	}
}
