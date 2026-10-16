using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008B RID: 139
	public readonly struct DeclarativeSecurityAttributeHandleCollection : IReadOnlyCollection<DeclarativeSecurityAttributeHandle>, IEnumerable<DeclarativeSecurityAttributeHandle>, IEnumerable
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x0000CABB File Offset: 0x0000ACBB
		[NullableContext(1)]
		internal DeclarativeSecurityAttributeHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.DeclSecurityTable.NumberOfRows;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000CADC File Offset: 0x0000ACDC
		[NullableContext(1)]
		internal DeclarativeSecurityAttributeHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.DeclSecurityTable.GetAttributeRange(handle, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000CAFD File Offset: 0x0000ACFD
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		public DeclarativeSecurityAttributeHandleCollection.Enumerator GetEnumerator()
		{
			return new DeclarativeSecurityAttributeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000564 RID: 1380 RVA: 0x0000CB27 File Offset: 0x0000AD27
		IEnumerator<DeclarativeSecurityAttributeHandle> IEnumerable<DeclarativeSecurityAttributeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000565 RID: 1381 RVA: 0x0000CB34 File Offset: 0x0000AD34
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000369 RID: 873
		private readonly MetadataReader _reader;

		// Token: 0x0400036A RID: 874
		private readonly int _firstRowId;

		// Token: 0x0400036B RID: 875
		private readonly int _lastRowId;

		// Token: 0x0200019E RID: 414
		public struct Enumerator : IEnumerator<DeclarativeSecurityAttributeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C5F RID: 3167 RVA: 0x00025246 File Offset: 0x00023446
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002525F File Offset: 0x0002345F
			public DeclarativeSecurityAttributeHandle Current
			{
				get
				{
					return DeclarativeSecurityAttributeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C61 RID: 3169 RVA: 0x00025275 File Offset: 0x00023475
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
			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06000C62 RID: 3170 RVA: 0x000252A1 File Offset: 0x000234A1
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C63 RID: 3171 RVA: 0x000252AE File Offset: 0x000234AE
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C64 RID: 3172 RVA: 0x000252B5 File Offset: 0x000234B5
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A33 RID: 2611
			private readonly MetadataReader _reader;

			// Token: 0x04000A34 RID: 2612
			private readonly int _lastRowId;

			// Token: 0x04000A35 RID: 2613
			private int _currentRowId;

			// Token: 0x04000A36 RID: 2614
			private const int EnumEnded = 16777216;
		}
	}
}
