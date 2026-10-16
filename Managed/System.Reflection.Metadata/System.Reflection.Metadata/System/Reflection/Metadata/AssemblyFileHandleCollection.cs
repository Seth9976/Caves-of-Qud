using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" />.</summary>
	// Token: 0x0200009B RID: 155
	public readonly struct AssemblyFileHandleCollection : IReadOnlyCollection<AssemblyFileHandle>, IEnumerable<AssemblyFileHandle>, IEnumerable
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0000D076 File Offset: 0x0000B276
		internal AssemblyFileHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000D07F File Offset: 0x0000B27F
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000D087 File Offset: 0x0000B287
		public AssemblyFileHandleCollection.Enumerator GetEnumerator()
		{
			return new AssemblyFileHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000D094 File Offset: 0x0000B294
		IEnumerator<AssemblyFileHandle> IEnumerable<AssemblyFileHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060005B8 RID: 1464 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400038D RID: 909
		private readonly int _lastRowId;

		// Token: 0x020001AC RID: 428
		public struct Enumerator : IEnumerator<AssemblyFileHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CB8 RID: 3256 RVA: 0x000259BC File Offset: 0x00023BBC
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x000259CC File Offset: 0x00023BCC
			public AssemblyFileHandle Current
			{
				get
				{
					return AssemblyFileHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CBA RID: 3258 RVA: 0x000259E2 File Offset: 0x00023BE2
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
			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00025A0E File Offset: 0x00023C0E
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CBC RID: 3260 RVA: 0x00025A1B File Offset: 0x00023C1B
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CBD RID: 3261 RVA: 0x00025A22 File Offset: 0x00023C22
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A65 RID: 2661
			private readonly int _lastRowId;

			// Token: 0x04000A66 RID: 2662
			private int _currentRowId;

			// Token: 0x04000A67 RID: 2663
			private const int EnumEnded = 16777216;
		}
	}
}
