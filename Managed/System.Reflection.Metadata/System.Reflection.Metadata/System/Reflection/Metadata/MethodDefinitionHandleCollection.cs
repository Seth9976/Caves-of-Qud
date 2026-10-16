using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008C RID: 140
	public readonly struct MethodDefinitionHandleCollection : IReadOnlyCollection<MethodDefinitionHandle>, IEnumerable<MethodDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0000CB41 File Offset: 0x0000AD41
		[NullableContext(1)]
		internal MethodDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.MethodDefTable.NumberOfRows;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000CB62 File Offset: 0x0000AD62
		[NullableContext(1)]
		internal MethodDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetMethodRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000CB7E File Offset: 0x0000AD7E
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000CB8F File Offset: 0x0000AD8F
		public MethodDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600056A RID: 1386 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		IEnumerator<MethodDefinitionHandle> IEnumerable<MethodDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600056B RID: 1387 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400036C RID: 876
		private readonly MetadataReader _reader;

		// Token: 0x0400036D RID: 877
		private readonly int _firstRowId;

		// Token: 0x0400036E RID: 878
		private readonly int _lastRowId;

		// Token: 0x0200019F RID: 415
		public struct Enumerator : IEnumerator<MethodDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C65 RID: 3173 RVA: 0x000252B7 File Offset: 0x000234B7
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000252D0 File Offset: 0x000234D0
			public MethodDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseMethodPtrTable)
					{
						return this.GetCurrentMethodIndirect();
					}
					return MethodDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x000252FA File Offset: 0x000234FA
			private MethodDefinitionHandle GetCurrentMethodIndirect()
			{
				return this._reader.MethodPtrTable.GetMethodFor(this._currentRowId & 16777215);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C68 RID: 3176 RVA: 0x00025318 File Offset: 0x00023518
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
			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00025344 File Offset: 0x00023544
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C6A RID: 3178 RVA: 0x00025351 File Offset: 0x00023551
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C6B RID: 3179 RVA: 0x00025358 File Offset: 0x00023558
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A37 RID: 2615
			private readonly MetadataReader _reader;

			// Token: 0x04000A38 RID: 2616
			private readonly int _lastRowId;

			// Token: 0x04000A39 RID: 2617
			private int _currentRowId;

			// Token: 0x04000A3A RID: 2618
			private const int EnumEnded = 16777216;
		}
	}
}
