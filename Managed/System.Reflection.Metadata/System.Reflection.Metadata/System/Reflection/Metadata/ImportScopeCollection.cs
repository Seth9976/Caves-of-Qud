using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BC RID: 188
	public readonly struct ImportScopeCollection : IReadOnlyCollection<ImportScopeHandle>, IEnumerable<ImportScopeHandle>, IEnumerable
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x00011961 File Offset: 0x0000FB61
		[NullableContext(1)]
		internal ImportScopeCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.ImportScopeTable.NumberOfRows;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00011982 File Offset: 0x0000FB82
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00011993 File Offset: 0x0000FB93
		public ImportScopeCollection.Enumerator GetEnumerator()
		{
			return new ImportScopeCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000711 RID: 1809 RVA: 0x000119AC File Offset: 0x0000FBAC
		IEnumerator<ImportScopeHandle> IEnumerable<ImportScopeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000712 RID: 1810 RVA: 0x000119B9 File Offset: 0x0000FBB9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000453 RID: 1107
		private readonly MetadataReader _reader;

		// Token: 0x04000454 RID: 1108
		private readonly int _firstRowId;

		// Token: 0x04000455 RID: 1109
		private readonly int _lastRowId;

		// Token: 0x020001B4 RID: 436
		public struct Enumerator : IEnumerator<ImportScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CE3 RID: 3299 RVA: 0x00025DB9 File Offset: 0x00023FB9
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000347 RID: 839
			// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00025DD2 File Offset: 0x00023FD2
			public ImportScopeHandle Current
			{
				get
				{
					return ImportScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CE5 RID: 3301 RVA: 0x00025DE8 File Offset: 0x00023FE8
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
			// Token: 0x17000348 RID: 840
			// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00025E14 File Offset: 0x00024014
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CE7 RID: 3303 RVA: 0x00025E21 File Offset: 0x00024021
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CE8 RID: 3304 RVA: 0x00025E28 File Offset: 0x00024028
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A89 RID: 2697
			private readonly MetadataReader _reader;

			// Token: 0x04000A8A RID: 2698
			private readonly int _lastRowId;

			// Token: 0x04000A8B RID: 2699
			private int _currentRowId;

			// Token: 0x04000A8C RID: 2700
			private const int EnumEnded = 16777216;
		}
	}
}
