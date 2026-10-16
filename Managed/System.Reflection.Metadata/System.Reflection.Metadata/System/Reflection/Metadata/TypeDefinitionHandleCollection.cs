using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Contains a collection of <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> instances.</summary>
	// Token: 0x02000093 RID: 147
	public readonly struct TypeDefinitionHandleCollection : IReadOnlyCollection<TypeDefinitionHandle>, IEnumerable<TypeDefinitionHandle>, IEnumerable
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0000CE84 File Offset: 0x0000B084
		internal TypeDefinitionHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000CE8D File Offset: 0x0000B08D
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000CE95 File Offset: 0x0000B095
		public TypeDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new TypeDefinitionHandleCollection.Enumerator(this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06000590 RID: 1424 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		IEnumerator<TypeDefinitionHandle> IEnumerable<TypeDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000591 RID: 1425 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000380 RID: 896
		private readonly int _lastRowId;

		// Token: 0x020001A6 RID: 422
		public struct Enumerator : IEnumerator<TypeDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C94 RID: 3220 RVA: 0x000256C1 File Offset: 0x000238C1
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000256D1 File Offset: 0x000238D1
			public TypeDefinitionHandle Current
			{
				get
				{
					return TypeDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C96 RID: 3222 RVA: 0x000256E7 File Offset: 0x000238E7
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
			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00025713 File Offset: 0x00023913
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C98 RID: 3224 RVA: 0x00025720 File Offset: 0x00023920
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C99 RID: 3225 RVA: 0x00025727 File Offset: 0x00023927
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A52 RID: 2642
			private readonly int _lastRowId;

			// Token: 0x04000A53 RID: 2643
			private int _currentRowId;

			// Token: 0x04000A54 RID: 2644
			private const int EnumEnded = 16777216;
		}
	}
}
