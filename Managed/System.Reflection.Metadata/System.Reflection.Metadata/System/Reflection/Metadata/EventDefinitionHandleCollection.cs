using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008F RID: 143
	public readonly struct EventDefinitionHandleCollection : IReadOnlyCollection<EventDefinitionHandle>, IEnumerable<EventDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		[NullableContext(1)]
		internal EventDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.EventTable.NumberOfRows;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		[NullableContext(1)]
		internal EventDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetEventRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000CD01 File Offset: 0x0000AF01
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000CD12 File Offset: 0x0000AF12
		public EventDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new EventDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600057C RID: 1404 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		IEnumerator<EventDefinitionHandle> IEnumerable<EventDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600057D RID: 1405 RVA: 0x0000CD38 File Offset: 0x0000AF38
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000375 RID: 885
		private readonly MetadataReader _reader;

		// Token: 0x04000376 RID: 886
		private readonly int _firstRowId;

		// Token: 0x04000377 RID: 887
		private readonly int _lastRowId;

		// Token: 0x020001A2 RID: 418
		public struct Enumerator : IEnumerator<EventDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C7A RID: 3194 RVA: 0x000254A0 File Offset: 0x000236A0
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000254B9 File Offset: 0x000236B9
			public EventDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseEventPtrTable)
					{
						return this.GetCurrentEventIndirect();
					}
					return EventDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x000254E3 File Offset: 0x000236E3
			private EventDefinitionHandle GetCurrentEventIndirect()
			{
				return this._reader.EventPtrTable.GetEventFor(this._currentRowId & 16777215);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C7D RID: 3197 RVA: 0x00025501 File Offset: 0x00023701
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
			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002552D File Offset: 0x0002372D
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C7F RID: 3199 RVA: 0x0002553A File Offset: 0x0002373A
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C80 RID: 3200 RVA: 0x00025541 File Offset: 0x00023741
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A43 RID: 2627
			private readonly MetadataReader _reader;

			// Token: 0x04000A44 RID: 2628
			private readonly int _lastRowId;

			// Token: 0x04000A45 RID: 2629
			private int _currentRowId;

			// Token: 0x04000A46 RID: 2630
			private const int EnumEnded = 16777216;
		}
	}
}
