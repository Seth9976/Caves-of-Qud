using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008D RID: 141
	public readonly struct FieldDefinitionHandleCollection : IReadOnlyCollection<FieldDefinitionHandle>, IEnumerable<FieldDefinitionHandle>, IEnumerable
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		[NullableContext(1)]
		internal FieldDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.FieldTable.NumberOfRows;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000CBE3 File Offset: 0x0000ADE3
		[NullableContext(1)]
		internal FieldDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetFieldRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public FieldDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new FieldDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000570 RID: 1392 RVA: 0x0000CC29 File Offset: 0x0000AE29
		IEnumerator<FieldDefinitionHandle> IEnumerable<FieldDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000571 RID: 1393 RVA: 0x0000CC36 File Offset: 0x0000AE36
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400036F RID: 879
		private readonly MetadataReader _reader;

		// Token: 0x04000370 RID: 880
		private readonly int _firstRowId;

		// Token: 0x04000371 RID: 881
		private readonly int _lastRowId;

		// Token: 0x020001A0 RID: 416
		public struct Enumerator : IEnumerator<FieldDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C6C RID: 3180 RVA: 0x0002535A File Offset: 0x0002355A
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00025373 File Offset: 0x00023573
			public FieldDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseFieldPtrTable)
					{
						return this.GetCurrentFieldIndirect();
					}
					return FieldDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C6E RID: 3182 RVA: 0x0002539D File Offset: 0x0002359D
			private FieldDefinitionHandle GetCurrentFieldIndirect()
			{
				return this._reader.FieldPtrTable.GetFieldFor(this._currentRowId & 16777215);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C6F RID: 3183 RVA: 0x000253BB File Offset: 0x000235BB
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
			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000253E7 File Offset: 0x000235E7
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C71 RID: 3185 RVA: 0x000253F4 File Offset: 0x000235F4
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C72 RID: 3186 RVA: 0x000253FB File Offset: 0x000235FB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A3B RID: 2619
			private readonly MetadataReader _reader;

			// Token: 0x04000A3C RID: 2620
			private readonly int _lastRowId;

			// Token: 0x04000A3D RID: 2621
			private int _currentRowId;

			// Token: 0x04000A3E RID: 2622
			private const int EnumEnded = 16777216;
		}
	}
}
