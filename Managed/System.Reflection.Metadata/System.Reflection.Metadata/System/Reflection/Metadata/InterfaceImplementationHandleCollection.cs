using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000092 RID: 146
	public readonly struct InterfaceImplementationHandleCollection : IReadOnlyCollection<InterfaceImplementationHandle>, IEnumerable<InterfaceImplementationHandle>, IEnumerable
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x0000CE1F File Offset: 0x0000B01F
		[NullableContext(1)]
		internal InterfaceImplementationHandleCollection(MetadataReader reader, TypeDefinitionHandle implementingType)
		{
			this._reader = reader;
			reader.InterfaceImplTable.GetInterfaceImplRange(implementingType, out this._firstRowId, out this._lastRowId);
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000CE40 File Offset: 0x0000B040
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000CE51 File Offset: 0x0000B051
		public InterfaceImplementationHandleCollection.Enumerator GetEnumerator()
		{
			return new InterfaceImplementationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x0600058B RID: 1419 RVA: 0x0000CE6A File Offset: 0x0000B06A
		IEnumerator<InterfaceImplementationHandle> IEnumerable<InterfaceImplementationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x0600058C RID: 1420 RVA: 0x0000CE77 File Offset: 0x0000B077
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400037D RID: 893
		private readonly MetadataReader _reader;

		// Token: 0x0400037E RID: 894
		private readonly int _firstRowId;

		// Token: 0x0400037F RID: 895
		private readonly int _lastRowId;

		// Token: 0x020001A5 RID: 421
		public struct Enumerator : IEnumerator<InterfaceImplementationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C8E RID: 3214 RVA: 0x00025650 File Offset: 0x00023850
			[NullableContext(1)]
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00025669 File Offset: 0x00023869
			public InterfaceImplementationHandle Current
			{
				get
				{
					return InterfaceImplementationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C90 RID: 3216 RVA: 0x0002567F File Offset: 0x0002387F
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
			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000C91 RID: 3217 RVA: 0x000256AB File Offset: 0x000238AB
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C92 RID: 3218 RVA: 0x000256B8 File Offset: 0x000238B8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C93 RID: 3219 RVA: 0x000256BF File Offset: 0x000238BF
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A4E RID: 2638
			private readonly MetadataReader _reader;

			// Token: 0x04000A4F RID: 2639
			private readonly int _lastRowId;

			// Token: 0x04000A50 RID: 2640
			private int _currentRowId;

			// Token: 0x04000A51 RID: 2641
			private const int EnumEnded = 16777216;
		}
	}
}
