using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>A collection of assembly references.</summary>
	// Token: 0x02000099 RID: 153
	public readonly struct AssemblyReferenceHandleCollection : IReadOnlyCollection<AssemblyReferenceHandle>, IEnumerable<AssemblyReferenceHandle>, IEnumerable
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		[NullableContext(1)]
		internal AssemblyReferenceHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public int Count
		{
			get
			{
				return this._reader.AssemblyRefTable.NumberOfNonVirtualRows + this._reader.AssemblyRefTable.NumberOfVirtualRows;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000D017 File Offset: 0x0000B217
		public AssemblyReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new AssemblyReferenceHandleCollection.Enumerator(this._reader);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060005AD RID: 1453 RVA: 0x0000D024 File Offset: 0x0000B224
		IEnumerator<AssemblyReferenceHandle> IEnumerable<AssemblyReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060005AE RID: 1454 RVA: 0x0000D031 File Offset: 0x0000B231
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400038B RID: 907
		private readonly MetadataReader _reader;

		// Token: 0x020001AA RID: 426
		public struct Enumerator : IEnumerator<AssemblyReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CAC RID: 3244 RVA: 0x00025861 File Offset: 0x00023A61
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader)
			{
				this._reader = reader;
				this._currentRowId = 0;
				this._virtualRowId = -1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00025878 File Offset: 0x00023A78
			public AssemblyReferenceHandle Current
			{
				get
				{
					if (this._virtualRowId < 0)
					{
						return AssemblyReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
					}
					if (this._virtualRowId == 16777216)
					{
						return default(AssemblyReferenceHandle);
					}
					return AssemblyReferenceHandle.FromVirtualIndex((AssemblyReferenceHandle.VirtualIndex)this._virtualRowId);
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CAE RID: 3246 RVA: 0x000258C8 File Offset: 0x00023AC8
			public bool MoveNext()
			{
				if (this._currentRowId < this._reader.AssemblyRefTable.NumberOfNonVirtualRows)
				{
					this._currentRowId++;
					return true;
				}
				if (this._virtualRowId < this._reader.AssemblyRefTable.NumberOfVirtualRows - 1)
				{
					this._virtualRowId++;
					return true;
				}
				this._currentRowId = 16777216;
				this._virtualRowId = 16777216;
				return false;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002593E File Offset: 0x00023B3E
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CB0 RID: 3248 RVA: 0x0002594B File Offset: 0x00023B4B
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CB1 RID: 3249 RVA: 0x00025952 File Offset: 0x00023B52
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A5E RID: 2654
			private readonly MetadataReader _reader;

			// Token: 0x04000A5F RID: 2655
			private int _currentRowId;

			// Token: 0x04000A60 RID: 2656
			private const int EnumEnded = 16777216;

			// Token: 0x04000A61 RID: 2657
			private int _virtualRowId;
		}
	}
}
