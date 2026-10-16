using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000090 RID: 144
	public readonly struct MethodImplementationHandleCollection : IReadOnlyCollection<MethodImplementationHandle>, IEnumerable<MethodImplementationHandle>, IEnumerable
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000CD45 File Offset: 0x0000AF45
		[NullableContext(1)]
		internal MethodImplementationHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			if (containingType.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.MethodImplTable.NumberOfRows;
				return;
			}
			reader.MethodImplTable.GetMethodImplRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000CD81 File Offset: 0x0000AF81
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public MethodImplementationHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodImplementationHandleCollection.Enumerator(this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000581 RID: 1409 RVA: 0x0000CDA5 File Offset: 0x0000AFA5
		IEnumerator<MethodImplementationHandle> IEnumerable<MethodImplementationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000582 RID: 1410 RVA: 0x0000CDB2 File Offset: 0x0000AFB2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000378 RID: 888
		private readonly int _firstRowId;

		// Token: 0x04000379 RID: 889
		private readonly int _lastRowId;

		// Token: 0x020001A3 RID: 419
		public struct Enumerator : IEnumerator<MethodImplementationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C81 RID: 3201 RVA: 0x00025543 File Offset: 0x00023743
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00025555 File Offset: 0x00023755
			public MethodImplementationHandle Current
			{
				get
				{
					return MethodImplementationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C83 RID: 3203 RVA: 0x0002556B File Offset: 0x0002376B
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
			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00025597 File Offset: 0x00023797
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C85 RID: 3205 RVA: 0x000255A4 File Offset: 0x000237A4
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C86 RID: 3206 RVA: 0x000255AB File Offset: 0x000237AB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A47 RID: 2631
			private readonly int _lastRowId;

			// Token: 0x04000A48 RID: 2632
			private int _currentRowId;

			// Token: 0x04000A49 RID: 2633
			private const int EnumEnded = 16777216;
		}
	}
}
