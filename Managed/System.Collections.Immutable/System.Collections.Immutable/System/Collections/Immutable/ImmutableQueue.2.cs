using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable queue.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of elements in the queue.</typeparam>
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableQueue<[Nullable(2)] T> : IImmutableQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000288 RID: 648 RVA: 0x000076E0 File Offset: 0x000058E0
		internal ImmutableQueue(ImmutableStack<T> forwards, ImmutableStack<T> backwards)
		{
			this._forwards = forwards;
			this._backwards = backwards;
		}

		/// <summary>Removes all objects from the immutable queue.</summary>
		/// <returns>The empty immutable queue.</returns>
		// Token: 0x06000289 RID: 649 RVA: 0x000076F6 File Offset: 0x000058F6
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		/// <summary>Gets a value that indicates whether this immutable queue is empty.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <returns>
		///   <see langword="true" /> if this queue is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000076FD File Offset: 0x000058FD
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty;
			}
		}

		/// <summary>Gets an empty immutable queue.</summary>
		/// <returns>An empty immutable queue.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000770A File Offset: 0x0000590A
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		/// <summary>Removes all elements from the immutable queue.</summary>
		/// <returns>The empty immutable queue.</returns>
		// Token: 0x0600028C RID: 652 RVA: 0x00007711 File Offset: 0x00005911
		IImmutableQueue<T> IImmutableQueue<T>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00007719 File Offset: 0x00005919
		private ImmutableStack<T> BackwardsReversed
		{
			get
			{
				if (this._backwardsReversed == null)
				{
					this._backwardsReversed = this._backwards.Reverse();
				}
				return this._backwardsReversed;
			}
		}

		/// <summary>Returns the element at the beginning of the immutable queue without removing it.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The element at the beginning of the queue.</returns>
		// Token: 0x0600028E RID: 654 RVA: 0x0000773A File Offset: 0x0000593A
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		/// <summary>Gets a read-only reference to the element at the front of the queue.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>Read-only reference to the element at the front of the queue.</returns>
		// Token: 0x0600028F RID: 655 RVA: 0x0000775A File Offset: 0x0000595A
		public readonly ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.PeekRef();
		}

		/// <summary>Adds an element to the end of the immutable queue, and returns the new queue.</summary>
		/// <param name="value">The element to add.</param>
		/// <returns>The new immutable queue.</returns>
		// Token: 0x06000290 RID: 656 RVA: 0x0000777A File Offset: 0x0000597A
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack.Create<T>(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		/// <summary>Adds an element to the end of the immutable queue, and returns the new queue.</summary>
		/// <param name="value">The element to add.</param>
		/// <returns>The new immutable queue.</returns>
		// Token: 0x06000291 RID: 657 RVA: 0x000077AC File Offset: 0x000059AC
		IImmutableQueue<T> IImmutableQueue<T>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		/// <summary>Removes the element at the beginning of the immutable queue, and returns the new queue.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The new immutable queue; never <see langword="null" />.</returns>
		// Token: 0x06000292 RID: 658 RVA: 0x000077B8 File Offset: 0x000059B8
		public ImmutableQueue<T> Dequeue()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			ImmutableStack<T> immutableStack = this._forwards.Pop();
			if (!immutableStack.IsEmpty)
			{
				return new ImmutableQueue<T>(immutableStack, this._backwards);
			}
			if (this._backwards.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			return new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
		}

		/// <summary>Removes the item at the beginning of the immutable queue, and returns the new queue.</summary>
		/// <param name="value">When this method returns, contains the element from the beginning of the queue.</param>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The new immutable queue with the beginning element removed.</returns>
		// Token: 0x06000293 RID: 659 RVA: 0x0000781C File Offset: 0x00005A1C
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		/// <summary>Removes the element at the beginning of the immutable queue, and returns the new queue.</summary>
		/// <exception cref="T:System.InvalidOperationException">The queue is empty.</exception>
		/// <returns>The new immutable queue; never <see langword="null" />.</returns>
		// Token: 0x06000294 RID: 660 RVA: 0x00007830 File Offset: 0x00005A30
		IImmutableQueue<T> IImmutableQueue<T>.Dequeue()
		{
			return this.Dequeue();
		}

		/// <summary>Returns an enumerator that iterates through the immutable queue.</summary>
		/// <returns>An enumerator that can be used to iterate through the queue.</returns>
		// Token: 0x06000295 RID: 661 RVA: 0x00007838 File Offset: 0x00005A38
		[NullableContext(0)]
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator  that can be used to iterate through the collection.</returns>
		// Token: 0x06000296 RID: 662 RVA: 0x00007840 File Offset: 0x00005A40
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableQueue<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000297 RID: 663 RVA: 0x00007868 File Offset: 0x00005A68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x0400002A RID: 42
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x0400002B RID: 43
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x0400002C RID: 44
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x0400002D RID: 45
		private ImmutableStack<T> _backwardsReversed;

		/// <summary>Enumerates the contents of an immutable queue without allocating any memory.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x0200006F RID: 111
		[Nullable(0)]
		[EditorBrowsable(2)]
		public struct Enumerator
		{
			// Token: 0x06000562 RID: 1378 RVA: 0x0000E45A File Offset: 0x0000C65A
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element at the current position of the enumerator.</returns>
			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000E474 File Offset: 0x0000C674
			public T Current
			{
				get
				{
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			/// <summary>Advances the enumerator to the next element of the immutable queue.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the queue.</returns>
			// Token: 0x06000564 RID: 1380 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
			public bool MoveNext()
			{
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x040000C5 RID: 197
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000C6 RID: 198
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000C7 RID: 199
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x02000070 RID: 112
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000565 RID: 1381 RVA: 0x0000E55C File Offset: 0x0000C75C
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000E56C File Offset: 0x0000C76C
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x0000E66E File Offset: 0x0000C86E
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x0000E684 File Offset: 0x0000C884
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x0000E68D File Offset: 0x0000C88D
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x040000C8 RID: 200
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x040000C9 RID: 201
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x040000CA RID: 202
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x040000CB RID: 203
			private bool _disposed;
		}
	}
}
