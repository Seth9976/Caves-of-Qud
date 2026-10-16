using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BA RID: 186
	public readonly struct LocalVariableHandleCollection : IReadOnlyCollection<LocalVariableHandle>, IEnumerable<LocalVariableHandle>, IEnumerable
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x0001185D File Offset: 0x0000FA5D
		[NullableContext(1)]
		internal LocalVariableHandleCollection(MetadataReader reader, LocalScopeHandle scope)
		{
			this._reader = reader;
			if (scope.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalVariableTable.NumberOfRows;
				return;
			}
			reader.GetLocalVariableRange(scope, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001189B File Offset: 0x0000FA9B
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000118AC File Offset: 0x0000FAAC
		public LocalVariableHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalVariableHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000707 RID: 1799 RVA: 0x000118C5 File Offset: 0x0000FAC5
		IEnumerator<LocalVariableHandle> IEnumerable<LocalVariableHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000708 RID: 1800 RVA: 0x000118D2 File Offset: 0x0000FAD2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400044D RID: 1101
		private readonly MetadataReader _reader;

		// Token: 0x0400044E RID: 1102
		private readonly int _firstRowId;

		// Token: 0x0400044F RID: 1103
		private readonly int _lastRowId;

		// Token: 0x020001B2 RID: 434
		public struct Enumerator : IEnumerator<LocalVariableHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CD7 RID: 3287 RVA: 0x00025CD7 File Offset: 0x00023ED7
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00025CF0 File Offset: 0x00023EF0
			public LocalVariableHandle Current
			{
				get
				{
					return LocalVariableHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CD9 RID: 3289 RVA: 0x00025D06 File Offset: 0x00023F06
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
			// Token: 0x17000344 RID: 836
			// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00025D32 File Offset: 0x00023F32
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CDB RID: 3291 RVA: 0x00025D3F File Offset: 0x00023F3F
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CDC RID: 3292 RVA: 0x00025D46 File Offset: 0x00023F46
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A81 RID: 2689
			private readonly MetadataReader _reader;

			// Token: 0x04000A82 RID: 2690
			private readonly int _lastRowId;

			// Token: 0x04000A83 RID: 2691
			private int _currentRowId;

			// Token: 0x04000A84 RID: 2692
			private const int EnumEnded = 16777216;
		}
	}
}
