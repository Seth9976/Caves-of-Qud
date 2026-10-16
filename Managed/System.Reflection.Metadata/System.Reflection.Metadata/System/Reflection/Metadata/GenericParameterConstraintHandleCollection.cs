using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of constraints of a generic type parameter.</summary>
	// Token: 0x02000089 RID: 137
	public readonly struct GenericParameterConstraintHandleCollection : IReadOnlyList<GenericParameterConstraintHandle>, IEnumerable<GenericParameterConstraintHandle>, IEnumerable, IReadOnlyCollection<GenericParameterConstraintHandle>
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		internal GenericParameterConstraintHandleCollection(int firstRowId, ushort count)
		{
			this._firstRowId = firstRowId;
			this._count = count;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C9D6 File Offset: 0x0000ABD6
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
		// Token: 0x170001D8 RID: 472
		public GenericParameterConstraintHandle this[int index]
		{
			get
			{
				if (index < 0 || index >= (int)this._count)
				{
					Throw.IndexOutOfRange();
				}
				return GenericParameterConstraintHandle.FromRowId(this._firstRowId + index);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000C9FF File Offset: 0x0000ABFF
		public GenericParameterConstraintHandleCollection.Enumerator GetEnumerator()
		{
			return new GenericParameterConstraintHandleCollection.Enumerator(this._firstRowId, this._firstRowId + (int)this._count - 1);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000558 RID: 1368 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		IEnumerator<GenericParameterConstraintHandle> IEnumerable<GenericParameterConstraintHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000559 RID: 1369 RVA: 0x0000CA28 File Offset: 0x0000AC28
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000364 RID: 868
		private readonly int _firstRowId;

		// Token: 0x04000365 RID: 869
		private readonly ushort _count;

		// Token: 0x0200019C RID: 412
		public struct Enumerator : IEnumerator<GenericParameterConstraintHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C52 RID: 3154 RVA: 0x0002512C File Offset: 0x0002332C
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002513E File Offset: 0x0002333E
			public GenericParameterConstraintHandle Current
			{
				get
				{
					return GenericParameterConstraintHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C54 RID: 3156 RVA: 0x00025154 File Offset: 0x00023354
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
			// Token: 0x1700031A RID: 794
			// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00025180 File Offset: 0x00023380
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C56 RID: 3158 RVA: 0x0002518D File Offset: 0x0002338D
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C57 RID: 3159 RVA: 0x00025194 File Offset: 0x00023394
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A2C RID: 2604
			private readonly int _lastRowId;

			// Token: 0x04000A2D RID: 2605
			private int _currentRowId;

			// Token: 0x04000A2E RID: 2606
			private const int EnumEnded = 16777216;
		}
	}
}
