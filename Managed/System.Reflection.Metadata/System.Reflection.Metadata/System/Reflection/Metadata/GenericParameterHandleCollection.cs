using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of generic type parameters of a method or type.</summary>
	// Token: 0x02000088 RID: 136
	public readonly struct GenericParameterHandleCollection : IReadOnlyList<GenericParameterHandle>, IEnumerable<GenericParameterHandle>, IEnumerable, IReadOnlyCollection<GenericParameterHandle>
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0000C957 File Offset: 0x0000AB57
		internal GenericParameterHandleCollection(int firstRowId, ushort count)
		{
			this._firstRowId = firstRowId;
			this._count = count;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000C967 File Offset: 0x0000AB67
		public int Count
		{
			get
			{
				return (int)this._count;
			}
		}

		/// <summary>Gets the element at the specified index in the read-only list.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index in the read-only list.</returns>
		// Token: 0x170001D6 RID: 470
		public GenericParameterHandle this[int index]
		{
			get
			{
				if (index < 0 || index >= (int)this._count)
				{
					Throw.IndexOutOfRange();
				}
				return GenericParameterHandle.FromRowId(this._firstRowId + index);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000C990 File Offset: 0x0000AB90
		public GenericParameterHandleCollection.Enumerator GetEnumerator()
		{
			return new GenericParameterHandleCollection.Enumerator(this._firstRowId, this._firstRowId + (int)this._count - 1);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000552 RID: 1362 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		IEnumerator<GenericParameterHandle> IEnumerable<GenericParameterHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000553 RID: 1363 RVA: 0x0000C9B9 File Offset: 0x0000ABB9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000362 RID: 866
		private readonly int _firstRowId;

		// Token: 0x04000363 RID: 867
		private readonly ushort _count;

		// Token: 0x0200019B RID: 411
		public struct Enumerator : IEnumerator<GenericParameterHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C4C RID: 3148 RVA: 0x000250C2 File Offset: 0x000232C2
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000250D4 File Offset: 0x000232D4
			public GenericParameterHandle Current
			{
				get
				{
					return GenericParameterHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C4E RID: 3150 RVA: 0x000250EA File Offset: 0x000232EA
			public bool MoveNext()
			{
				if (this._currentRowId >= this._lastRowId)
				{
					this._currentRowId = 16777216;
					return false;
				}
				this._currentRowId++;
				return true;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00025116 File Offset: 0x00023316
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C50 RID: 3152 RVA: 0x00025123 File Offset: 0x00023323
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C51 RID: 3153 RVA: 0x0002512A File Offset: 0x0002332A
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A29 RID: 2601
			private readonly int _lastRowId;

			// Token: 0x04000A2A RID: 2602
			private int _currentRowId;

			// Token: 0x04000A2B RID: 2603
			private const int EnumEnded = 16777216;
		}
	}
}
