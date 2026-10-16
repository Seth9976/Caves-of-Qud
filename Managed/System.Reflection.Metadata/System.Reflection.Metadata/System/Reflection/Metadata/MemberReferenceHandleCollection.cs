using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a collection of <see cref="T:System.Reflection.Metadata.MemberReferenceHandle" /> instances.</summary>
	// Token: 0x02000096 RID: 150
	public readonly struct MemberReferenceHandleCollection : IReadOnlyCollection<MemberReferenceHandle>, IEnumerable<MemberReferenceHandle>, IEnumerable
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0000CF2C File Offset: 0x0000B12C
		internal MemberReferenceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000CF35 File Offset: 0x0000B135
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000CF3D File Offset: 0x0000B13D
		public MemberReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new MemberReferenceHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600059F RID: 1439 RVA: 0x0000CF4A File Offset: 0x0000B14A
		IEnumerator<MemberReferenceHandle> IEnumerable<MemberReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060005A0 RID: 1440 RVA: 0x0000CF57 File Offset: 0x0000B157
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000383 RID: 899
		private readonly int _lastRowId;

		// Token: 0x020001A9 RID: 425
		public struct Enumerator : IEnumerator<MemberReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CA6 RID: 3238 RVA: 0x000257F9 File Offset: 0x000239F9
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000333 RID: 819
			// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00025809 File Offset: 0x00023A09
			public MemberReferenceHandle Current
			{
				get
				{
					return MemberReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CA8 RID: 3240 RVA: 0x0002581F File Offset: 0x00023A1F
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
			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002584B File Offset: 0x00023A4B
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CAA RID: 3242 RVA: 0x00025858 File Offset: 0x00023A58
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CAB RID: 3243 RVA: 0x0002585F File Offset: 0x00023A5F
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A5B RID: 2651
			private readonly int _lastRowId;

			// Token: 0x04000A5C RID: 2652
			private int _currentRowId;

			// Token: 0x04000A5D RID: 2653
			private const int EnumEnded = 16777216;
		}
	}
}
