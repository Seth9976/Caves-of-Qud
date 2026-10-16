using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BB RID: 187
	public readonly struct LocalConstantHandleCollection : IReadOnlyCollection<LocalConstantHandle>, IEnumerable<LocalConstantHandle>, IEnumerable
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x000118DF File Offset: 0x0000FADF
		[NullableContext(1)]
		internal LocalConstantHandleCollection(MetadataReader reader, LocalScopeHandle scope)
		{
			this._reader = reader;
			if (scope.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalConstantTable.NumberOfRows;
				return;
			}
			reader.GetLocalConstantRange(scope, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001191D File Offset: 0x0000FB1D
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001192E File Offset: 0x0000FB2E
		public LocalConstantHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalConstantHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600070C RID: 1804 RVA: 0x00011947 File Offset: 0x0000FB47
		IEnumerator<LocalConstantHandle> IEnumerable<LocalConstantHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600070D RID: 1805 RVA: 0x00011954 File Offset: 0x0000FB54
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000450 RID: 1104
		private readonly MetadataReader _reader;

		// Token: 0x04000451 RID: 1105
		private readonly int _firstRowId;

		// Token: 0x04000452 RID: 1106
		private readonly int _lastRowId;

		// Token: 0x020001B3 RID: 435
		public struct Enumerator : IEnumerator<LocalConstantHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CDD RID: 3293 RVA: 0x00025D48 File Offset: 0x00023F48
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000345 RID: 837
			// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00025D61 File Offset: 0x00023F61
			public LocalConstantHandle Current
			{
				get
				{
					return LocalConstantHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CDF RID: 3295 RVA: 0x00025D77 File Offset: 0x00023F77
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
			// Token: 0x17000346 RID: 838
			// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00025DA3 File Offset: 0x00023FA3
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CE1 RID: 3297 RVA: 0x00025DB0 File Offset: 0x00023FB0
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CE2 RID: 3298 RVA: 0x00025DB7 File Offset: 0x00023FB7
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A85 RID: 2693
			private readonly MetadataReader _reader;

			// Token: 0x04000A86 RID: 2694
			private readonly int _lastRowId;

			// Token: 0x04000A87 RID: 2695
			private int _currentRowId;

			// Token: 0x04000A88 RID: 2696
			private const int EnumEnded = 16777216;
		}
	}
}
