using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BD RID: 189
	public readonly struct CustomDebugInformationHandleCollection : IReadOnlyCollection<CustomDebugInformationHandle>, IEnumerable<CustomDebugInformationHandle>, IEnumerable
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x000119C6 File Offset: 0x0000FBC6
		[NullableContext(1)]
		internal CustomDebugInformationHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.CustomDebugInformationTable.NumberOfRows;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000119E7 File Offset: 0x0000FBE7
		[NullableContext(1)]
		internal CustomDebugInformationHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.CustomDebugInformationTable.GetRange(handle, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00011A08 File Offset: 0x0000FC08
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00011A19 File Offset: 0x0000FC19
		public CustomDebugInformationHandleCollection.Enumerator GetEnumerator()
		{
			return new CustomDebugInformationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000717 RID: 1815 RVA: 0x00011A32 File Offset: 0x0000FC32
		IEnumerator<CustomDebugInformationHandle> IEnumerable<CustomDebugInformationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000718 RID: 1816 RVA: 0x00011A3F File Offset: 0x0000FC3F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000456 RID: 1110
		private readonly MetadataReader _reader;

		// Token: 0x04000457 RID: 1111
		private readonly int _firstRowId;

		// Token: 0x04000458 RID: 1112
		private readonly int _lastRowId;

		// Token: 0x020001B5 RID: 437
		public struct Enumerator : IEnumerator<CustomDebugInformationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CE9 RID: 3305 RVA: 0x00025E2A File Offset: 0x0002402A
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000349 RID: 841
			// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00025E43 File Offset: 0x00024043
			public CustomDebugInformationHandle Current
			{
				get
				{
					return CustomDebugInformationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CEB RID: 3307 RVA: 0x00025E59 File Offset: 0x00024059
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
			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00025E85 File Offset: 0x00024085
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CED RID: 3309 RVA: 0x00025E92 File Offset: 0x00024092
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CEE RID: 3310 RVA: 0x00025E99 File Offset: 0x00024099
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A8D RID: 2701
			private readonly MetadataReader _reader;

			// Token: 0x04000A8E RID: 2702
			private readonly int _lastRowId;

			// Token: 0x04000A8F RID: 2703
			private int _currentRowId;

			// Token: 0x04000A90 RID: 2704
			private const int EnumEnded = 16777216;
		}
	}
}
