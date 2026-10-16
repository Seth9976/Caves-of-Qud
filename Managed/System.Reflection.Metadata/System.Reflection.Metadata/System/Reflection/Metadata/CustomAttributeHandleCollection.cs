using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008A RID: 138
	public readonly struct CustomAttributeHandleCollection : IReadOnlyCollection<CustomAttributeHandle>, IEnumerable<CustomAttributeHandle>, IEnumerable
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x0000CA35 File Offset: 0x0000AC35
		[NullableContext(1)]
		internal CustomAttributeHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.CustomAttributeTable.NumberOfRows;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000CA56 File Offset: 0x0000AC56
		[NullableContext(1)]
		internal CustomAttributeHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.CustomAttributeTable.GetAttributeRange(handle, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000CA77 File Offset: 0x0000AC77
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public CustomAttributeHandleCollection.Enumerator GetEnumerator()
		{
			return new CustomAttributeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600055E RID: 1374 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		IEnumerator<CustomAttributeHandle> IEnumerable<CustomAttributeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600055F RID: 1375 RVA: 0x0000CAAE File Offset: 0x0000ACAE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000366 RID: 870
		private readonly MetadataReader _reader;

		// Token: 0x04000367 RID: 871
		private readonly int _firstRowId;

		// Token: 0x04000368 RID: 872
		private readonly int _lastRowId;

		// Token: 0x0200019D RID: 413
		public struct Enumerator : IEnumerator<CustomAttributeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C58 RID: 3160 RVA: 0x00025196 File Offset: 0x00023396
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000251AF File Offset: 0x000233AF
			public CustomAttributeHandle Current
			{
				get
				{
					if (this._reader.CustomAttributeTable.PtrTable != null)
					{
						return this.GetCurrentCustomAttributeIndirect();
					}
					return CustomAttributeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C5A RID: 3162 RVA: 0x000251DE File Offset: 0x000233DE
			private CustomAttributeHandle GetCurrentCustomAttributeIndirect()
			{
				return CustomAttributeHandle.FromRowId(this._reader.CustomAttributeTable.PtrTable[(this._currentRowId & 16777215) - 1]);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C5B RID: 3163 RVA: 0x00025204 File Offset: 0x00023404
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
			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00025230 File Offset: 0x00023430
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C5D RID: 3165 RVA: 0x0002523D File Offset: 0x0002343D
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C5E RID: 3166 RVA: 0x00025244 File Offset: 0x00023444
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A2F RID: 2607
			private readonly MetadataReader _reader;

			// Token: 0x04000A30 RID: 2608
			private readonly int _lastRowId;

			// Token: 0x04000A31 RID: 2609
			private int _currentRowId;

			// Token: 0x04000A32 RID: 2610
			private const int EnumEnded = 16777216;
		}
	}
}
