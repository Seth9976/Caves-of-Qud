using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008E RID: 142
	public readonly struct PropertyDefinitionHandleCollection : IReadOnlyCollection<PropertyDefinitionHandle>, IEnumerable<PropertyDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0000CC43 File Offset: 0x0000AE43
		[NullableContext(1)]
		internal PropertyDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.PropertyTable.NumberOfRows;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000CC64 File Offset: 0x0000AE64
		[NullableContext(1)]
		internal PropertyDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetPropertyRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000CC91 File Offset: 0x0000AE91
		public PropertyDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new PropertyDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000576 RID: 1398 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		IEnumerator<PropertyDefinitionHandle> IEnumerable<PropertyDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000577 RID: 1399 RVA: 0x0000CCB7 File Offset: 0x0000AEB7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000372 RID: 882
		private readonly MetadataReader _reader;

		// Token: 0x04000373 RID: 883
		private readonly int _firstRowId;

		// Token: 0x04000374 RID: 884
		private readonly int _lastRowId;

		// Token: 0x020001A1 RID: 417
		public struct Enumerator : IEnumerator<PropertyDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C73 RID: 3187 RVA: 0x000253FD File Offset: 0x000235FD
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00025416 File Offset: 0x00023616
			public PropertyDefinitionHandle Current
			{
				get
				{
					if (this._reader.UsePropertyPtrTable)
					{
						return this.GetCurrentPropertyIndirect();
					}
					return PropertyDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x00025440 File Offset: 0x00023640
			private PropertyDefinitionHandle GetCurrentPropertyIndirect()
			{
				return this._reader.PropertyPtrTable.GetPropertyFor(this._currentRowId & 16777215);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C76 RID: 3190 RVA: 0x0002545E File Offset: 0x0002365E
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
			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002548A File Offset: 0x0002368A
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C78 RID: 3192 RVA: 0x00025497 File Offset: 0x00023697
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C79 RID: 3193 RVA: 0x0002549E File Offset: 0x0002369E
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A3F RID: 2623
			private readonly MetadataReader _reader;

			// Token: 0x04000A40 RID: 2624
			private readonly int _lastRowId;

			// Token: 0x04000A41 RID: 2625
			private int _currentRowId;

			// Token: 0x04000A42 RID: 2626
			private const int EnumEnded = 16777216;
		}
	}
}
