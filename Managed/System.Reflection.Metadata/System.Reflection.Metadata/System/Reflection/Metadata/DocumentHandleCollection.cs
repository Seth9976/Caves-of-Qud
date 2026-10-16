using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B7 RID: 183
	public readonly struct DocumentHandleCollection : IReadOnlyCollection<DocumentHandle>, IEnumerable<DocumentHandle>, IEnumerable
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00011712 File Offset: 0x0000F912
		[NullableContext(1)]
		internal DocumentHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.DocumentTable.NumberOfRows;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00011733 File Offset: 0x0000F933
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00011744 File Offset: 0x0000F944
		public DocumentHandleCollection.Enumerator GetEnumerator()
		{
			return new DocumentHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001175D File Offset: 0x0000F95D
		IEnumerator<DocumentHandle> IEnumerable<DocumentHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001176A File Offset: 0x0000F96A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000444 RID: 1092
		private readonly MetadataReader _reader;

		// Token: 0x04000445 RID: 1093
		private readonly int _firstRowId;

		// Token: 0x04000446 RID: 1094
		private readonly int _lastRowId;

		// Token: 0x020001AE RID: 430
		public struct Enumerator : IEnumerator<DocumentHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CBF RID: 3263 RVA: 0x00025A5B File Offset: 0x00023C5B
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700033B RID: 827
			// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00025A74 File Offset: 0x00023C74
			public DocumentHandle Current
			{
				get
				{
					return DocumentHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CC1 RID: 3265 RVA: 0x00025A8A File Offset: 0x00023C8A
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
			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00025AB6 File Offset: 0x00023CB6
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CC3 RID: 3267 RVA: 0x00025AC3 File Offset: 0x00023CC3
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CC4 RID: 3268 RVA: 0x00025ACA File Offset: 0x00023CCA
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A6F RID: 2671
			private readonly MetadataReader _reader;

			// Token: 0x04000A70 RID: 2672
			private readonly int _lastRowId;

			// Token: 0x04000A71 RID: 2673
			private int _currentRowId;

			// Token: 0x04000A72 RID: 2674
			private const int EnumEnded = 16777216;
		}
	}
}
