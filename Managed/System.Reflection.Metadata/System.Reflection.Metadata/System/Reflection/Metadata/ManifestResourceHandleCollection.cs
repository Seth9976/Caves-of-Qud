using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of <see cref="T:System.Reflection.Metadata.ManifestResourceHandle" /> instances.</summary>
	// Token: 0x0200009A RID: 154
	public readonly struct ManifestResourceHandleCollection : IReadOnlyCollection<ManifestResourceHandle>, IEnumerable<ManifestResourceHandle>, IEnumerable
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0000D03E File Offset: 0x0000B23E
		internal ManifestResourceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000D047 File Offset: 0x0000B247
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000D04F File Offset: 0x0000B24F
		public ManifestResourceHandleCollection.Enumerator GetEnumerator()
		{
			return new ManifestResourceHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000D05C File Offset: 0x0000B25C
		IEnumerator<ManifestResourceHandle> IEnumerable<ManifestResourceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060005B3 RID: 1459 RVA: 0x0000D069 File Offset: 0x0000B269
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400038C RID: 908
		private readonly int _lastRowId;

		// Token: 0x020001AB RID: 427
		public struct Enumerator : IEnumerator<ManifestResourceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CB2 RID: 3250 RVA: 0x00025954 File Offset: 0x00023B54
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00025964 File Offset: 0x00023B64
			public ManifestResourceHandle Current
			{
				get
				{
					return ManifestResourceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CB4 RID: 3252 RVA: 0x0002597A File Offset: 0x00023B7A
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
			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x000259A6 File Offset: 0x00023BA6
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CB6 RID: 3254 RVA: 0x000259B3 File Offset: 0x00023BB3
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CB7 RID: 3255 RVA: 0x000259BA File Offset: 0x00023BBA
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A62 RID: 2658
			private readonly int _lastRowId;

			// Token: 0x04000A63 RID: 2659
			private int _currentRowId;

			// Token: 0x04000A64 RID: 2660
			private const int EnumEnded = 16777216;
		}
	}
}
