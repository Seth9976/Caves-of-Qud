using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B8 RID: 184
	public readonly struct MethodDebugInformationHandleCollection : IReadOnlyCollection<MethodDebugInformationHandle>, IEnumerable<MethodDebugInformationHandle>, IEnumerable
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00011777 File Offset: 0x0000F977
		[NullableContext(1)]
		internal MethodDebugInformationHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.MethodDebugInformationTable.NumberOfRows;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00011798 File Offset: 0x0000F998
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000117A9 File Offset: 0x0000F9A9
		public MethodDebugInformationHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodDebugInformationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060006FD RID: 1789 RVA: 0x000117C2 File Offset: 0x0000F9C2
		IEnumerator<MethodDebugInformationHandle> IEnumerable<MethodDebugInformationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060006FE RID: 1790 RVA: 0x000117CF File Offset: 0x0000F9CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000447 RID: 1095
		private readonly MetadataReader _reader;

		// Token: 0x04000448 RID: 1096
		private readonly int _firstRowId;

		// Token: 0x04000449 RID: 1097
		private readonly int _lastRowId;

		// Token: 0x020001AF RID: 431
		public struct Enumerator : IEnumerator<MethodDebugInformationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000CC5 RID: 3269 RVA: 0x00025ACC File Offset: 0x00023CCC
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00025AE5 File Offset: 0x00023CE5
			public MethodDebugInformationHandle Current
			{
				get
				{
					return MethodDebugInformationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CC7 RID: 3271 RVA: 0x00025AFB File Offset: 0x00023CFB
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
			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00025B27 File Offset: 0x00023D27
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000CC9 RID: 3273 RVA: 0x00025B34 File Offset: 0x00023D34
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000CCA RID: 3274 RVA: 0x00025B3B File Offset: 0x00023D3B
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A73 RID: 2675
			private readonly MetadataReader _reader;

			// Token: 0x04000A74 RID: 2676
			private readonly int _lastRowId;

			// Token: 0x04000A75 RID: 2677
			private int _currentRowId;

			// Token: 0x04000A76 RID: 2678
			private const int EnumEnded = 16777216;
		}
	}
}
