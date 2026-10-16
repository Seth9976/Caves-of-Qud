using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	/// <summary>Represents an immutable stack.  
	///
	///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
	/// <typeparam name="T">The type of element on the stack.</typeparam>
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableStack<[Nullable(2)] T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600034B RID: 843 RVA: 0x00008EF6 File Offset: 0x000070F6
		private ImmutableStack()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008EFE File Offset: 0x000070FE
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		/// <summary>Gets an empty immutable stack.</summary>
		/// <returns>An empty immutable stack.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00008F14 File Offset: 0x00007114
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		/// <summary>Removes all objects from the immutable stack.</summary>
		/// <returns>An empty immutable stack.</returns>
		// Token: 0x0600034E RID: 846 RVA: 0x00008F1B File Offset: 0x0000711B
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		/// <summary>Removes all elements from the immutable stack.</summary>
		/// <returns>The empty immutable stack.</returns>
		// Token: 0x0600034F RID: 847 RVA: 0x00008F22 File Offset: 0x00007122
		IImmutableStack<T> IImmutableStack<T>.Clear()
		{
			return this.Clear();
		}

		/// <summary>Gets a value that indicates whether this instance of the immutable stack is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00008F2A File Offset: 0x0000712A
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		/// <summary>Returns the object at the top of the stack without removing it.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>The object at the top of the stack.</returns>
		// Token: 0x06000351 RID: 849 RVA: 0x00008F35 File Offset: 0x00007135
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		/// <summary>Gets a read-only reference to the element on the top of the stack.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>A read-only reference to the element on the top of the stack.</returns>
		// Token: 0x06000352 RID: 850 RVA: 0x00008F50 File Offset: 0x00007150
		public readonly ref T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return ref this._head;
		}

		/// <summary>Inserts an object at the top of the immutable stack and returns the new stack.</summary>
		/// <param name="value">The object to push onto the stack.</param>
		/// <returns>The new stack.</returns>
		// Token: 0x06000353 RID: 851 RVA: 0x00008F6B File Offset: 0x0000716B
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		/// <summary>Inserts an element at the top of the immutable stack and returns the new stack.</summary>
		/// <param name="value">The element to push onto the stack.</param>
		/// <returns>The new stack.</returns>
		// Token: 0x06000354 RID: 852 RVA: 0x00008F74 File Offset: 0x00007174
		IImmutableStack<T> IImmutableStack<T>.Push(T value)
		{
			return this.Push(value);
		}

		/// <summary>Removes the element at the top of the immutable stack and returns the stack after the removal.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>A stack; never <see langword="null" />.</returns>
		// Token: 0x06000355 RID: 853 RVA: 0x00008F7D File Offset: 0x0000717D
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		/// <summary>Removes the specified element from the immutable stack and returns the stack after the removal.</summary>
		/// <param name="value">The value to remove from the stack.</param>
		/// <returns>A stack; never <see langword="null" />.</returns>
		// Token: 0x06000356 RID: 854 RVA: 0x00008F98 File Offset: 0x00007198
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		/// <summary>Removes the element at the top of the immutable stack and returns the new stack.</summary>
		/// <exception cref="T:System.InvalidOperationException">The stack is empty.</exception>
		/// <returns>The new stack; never <see langword="null" />.</returns>
		// Token: 0x06000357 RID: 855 RVA: 0x00008FAC File Offset: 0x000071AC
		IImmutableStack<T> IImmutableStack<T>.Pop()
		{
			return this.Pop();
		}

		/// <summary>Returns an enumerator that iterates through the immutable stack.</summary>
		/// <returns>An enumerator that can be used to iterate through the stack.</returns>
		// Token: 0x06000358 RID: 856 RVA: 0x00008FB4 File Offset: 0x000071B4
		[NullableContext(0)]
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator  that can be used to iterate through the collection.</returns>
		// Token: 0x06000359 RID: 857 RVA: 0x00008FBC File Offset: 0x000071BC
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableStack<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600035A RID: 858 RVA: 0x00008FE4 File Offset: 0x000071E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00008FEC File Offset: 0x000071EC
		internal ImmutableStack<T> Reverse()
		{
			ImmutableStack<T> immutableStack = this.Clear();
			ImmutableStack<T> immutableStack2 = this;
			while (!immutableStack2.IsEmpty)
			{
				immutableStack = immutableStack.Push(immutableStack2.Peek());
				immutableStack2 = immutableStack2.Pop();
			}
			return immutableStack;
		}

		// Token: 0x0400003A RID: 58
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x0400003B RID: 59
		private readonly T _head;

		// Token: 0x0400003C RID: 60
		private readonly ImmutableStack<T> _tail;

		/// <summary>Enumerates the contents of an immutable stack without allocating any memory.  
		///
		///  NuGet package: System.Collections.Immutable (about immutable collections and how to install)</summary>
		/// <typeparam name="T" />
		// Token: 0x02000079 RID: 121
		[Nullable(0)]
		[EditorBrowsable(2)]
		public struct Enumerator
		{
			// Token: 0x06000633 RID: 1587 RVA: 0x00010A6C File Offset: 0x0000EC6C
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element at the current position of the enumerator.</returns>
			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000634 RID: 1588 RVA: 0x00010A87 File Offset: 0x0000EC87
			public T Current
			{
				get
				{
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			/// <summary>Advances the enumerator to the next element of the immutable stack.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the stack.</returns>
			// Token: 0x06000635 RID: 1589 RVA: 0x00010AB0 File Offset: 0x0000ECB0
			public bool MoveNext()
			{
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x040000F8 RID: 248
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x040000F9 RID: 249
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x0200007A RID: 122
		private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000636 RID: 1590 RVA: 0x00010AFF File Offset: 0x0000ECFF
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000637 RID: 1591 RVA: 0x00010B19 File Offset: 0x0000ED19
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x00010B47 File Offset: 0x0000ED47
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x00010B54 File Offset: 0x0000ED54
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x00010BA9 File Offset: 0x0000EDA9
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x00010BB8 File Offset: 0x0000EDB8
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x00010BC1 File Offset: 0x0000EDC1
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x040000FA RID: 250
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x040000FB RID: 251
			private ImmutableStack<T> _remainingStack;

			// Token: 0x040000FC RID: 252
			private bool _disposed;
		}
	}
}
