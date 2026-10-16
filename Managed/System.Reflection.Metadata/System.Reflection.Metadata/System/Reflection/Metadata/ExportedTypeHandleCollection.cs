using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> instances.</summary>
	// Token: 0x02000095 RID: 149
	public readonly struct ExportedTypeHandleCollection : IReadOnlyCollection<ExportedTypeHandle>, IEnumerable<ExportedTypeHandle>, IEnumerable
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		internal ExportedTypeHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000CEFD File Offset: 0x0000B0FD
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000CF05 File Offset: 0x0000B105
		public ExportedTypeHandleCollection.Enumerator GetEnumerator()
		{
			return new ExportedTypeHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600059A RID: 1434 RVA: 0x0000CF12 File Offset: 0x0000B112
		IEnumerator<ExportedTypeHandle> IEnumerable<ExportedTypeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600059B RID: 1435 RVA: 0x0000CF1F File Offset: 0x0000B11F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000382 RID: 898
		private readonly int _lastRowId;

		// Token: 0x020001A8 RID: 424
		public struct Enumerator : IEnumerator<ExportedTypeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CA0 RID: 3232 RVA: 0x00025791 File Offset: 0x00023991
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000257A1 File Offset: 0x000239A1
			public ExportedTypeHandle Current
			{
				get
				{
					return ExportedTypeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CA2 RID: 3234 RVA: 0x000257B7 File Offset: 0x000239B7
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
			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000257E3 File Offset: 0x000239E3
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CA4 RID: 3236 RVA: 0x000257F0 File Offset: 0x000239F0
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CA5 RID: 3237 RVA: 0x000257F7 File Offset: 0x000239F7
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A58 RID: 2648
			private readonly int _lastRowId;

			// Token: 0x04000A59 RID: 2649
			private int _currentRowId;

			// Token: 0x04000A5A RID: 2650
			private const int EnumEnded = 16777216;
		}
	}
}
