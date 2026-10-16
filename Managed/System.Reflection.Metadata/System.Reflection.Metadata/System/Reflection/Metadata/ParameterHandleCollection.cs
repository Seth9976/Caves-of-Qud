using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Contains a collection of parameters of a specified method.</summary>
	// Token: 0x02000091 RID: 145
	public readonly struct ParameterHandleCollection : IReadOnlyCollection<ParameterHandle>, IEnumerable<ParameterHandle>, IEnumerable
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		[NullableContext(1)]
		internal ParameterHandleCollection(MetadataReader reader, MethodDefinitionHandle containingMethod)
		{
			this._reader = reader;
			reader.GetParameterRange(containingMethod, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000CDDB File Offset: 0x0000AFDB
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public ParameterHandleCollection.Enumerator GetEnumerator()
		{
			return new ParameterHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000586 RID: 1414 RVA: 0x0000CE05 File Offset: 0x0000B005
		IEnumerator<ParameterHandle> IEnumerable<ParameterHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000587 RID: 1415 RVA: 0x0000CE12 File Offset: 0x0000B012
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400037A RID: 890
		private readonly MetadataReader _reader;

		// Token: 0x0400037B RID: 891
		private readonly int _firstRowId;

		// Token: 0x0400037C RID: 892
		private readonly int _lastRowId;

		// Token: 0x020001A4 RID: 420
		public struct Enumerator : IEnumerator<ParameterHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C87 RID: 3207 RVA: 0x000255AD File Offset: 0x000237AD
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000255C6 File Offset: 0x000237C6
			public ParameterHandle Current
			{
				get
				{
					if (this._reader.UseParamPtrTable)
					{
						return this.GetCurrentParameterIndirect();
					}
					return ParameterHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x000255F0 File Offset: 0x000237F0
			private ParameterHandle GetCurrentParameterIndirect()
			{
				return this._reader.ParamPtrTable.GetParamFor(this._currentRowId & 16777215);
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C8A RID: 3210 RVA: 0x0002560E File Offset: 0x0002380E
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
			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002563A File Offset: 0x0002383A
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C8C RID: 3212 RVA: 0x00025647 File Offset: 0x00023847
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C8D RID: 3213 RVA: 0x0002564E File Offset: 0x0002384E
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A4A RID: 2634
			private readonly MetadataReader _reader;

			// Token: 0x04000A4B RID: 2635
			private readonly int _lastRowId;

			// Token: 0x04000A4C RID: 2636
			private int _currentRowId;

			// Token: 0x04000A4D RID: 2637
			private const int EnumEnded = 16777216;
		}
	}
}
