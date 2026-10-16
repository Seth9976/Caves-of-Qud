using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Contains a collection of <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> instances.</summary>
	// Token: 0x02000094 RID: 148
	public readonly struct TypeReferenceHandleCollection : IReadOnlyCollection<TypeReferenceHandle>, IEnumerable<TypeReferenceHandle>, IEnumerable
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		internal TypeReferenceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000CECD File Offset: 0x0000B0CD
		public TypeReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new TypeReferenceHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000595 RID: 1429 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		IEnumerator<TypeReferenceHandle> IEnumerable<TypeReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000596 RID: 1430 RVA: 0x0000CEE7 File Offset: 0x0000B0E7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000381 RID: 897
		private readonly int _lastRowId;

		// Token: 0x020001A7 RID: 423
		public struct Enumerator : IEnumerator<TypeReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C9A RID: 3226 RVA: 0x00025729 File Offset: 0x00023929
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00025739 File Offset: 0x00023939
			public TypeReferenceHandle Current
			{
				get
				{
					return TypeReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C9C RID: 3228 RVA: 0x0002574F File Offset: 0x0002394F
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
			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002577B File Offset: 0x0002397B
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C9E RID: 3230 RVA: 0x00025788 File Offset: 0x00023988
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C9F RID: 3231 RVA: 0x0002578F File Offset: 0x0002398F
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A55 RID: 2645
			private readonly int _lastRowId;

			// Token: 0x04000A56 RID: 2646
			private int _currentRowId;

			// Token: 0x04000A57 RID: 2647
			private const int EnumEnded = 16777216;
		}
	}
}
