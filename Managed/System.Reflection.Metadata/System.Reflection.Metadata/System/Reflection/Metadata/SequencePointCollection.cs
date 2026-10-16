using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000CF RID: 207
	public readonly struct SequencePointCollection : IEnumerable<SequencePoint>, IEnumerable
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00012665 File Offset: 0x00010865
		internal SequencePointCollection(MemoryBlock block, DocumentHandle document)
		{
			this._block = block;
			this._document = document;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00012675 File Offset: 0x00010875
		public SequencePointCollection.Enumerator GetEnumerator()
		{
			return new SequencePointCollection.Enumerator(this._block, this._document);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060007AD RID: 1965 RVA: 0x00012688 File Offset: 0x00010888
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060007AE RID: 1966 RVA: 0x00012695 File Offset: 0x00010895
		IEnumerator<SequencePoint> IEnumerable<SequencePoint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000491 RID: 1169
		private readonly MemoryBlock _block;

		// Token: 0x04000492 RID: 1170
		private readonly DocumentHandle _document;

		// Token: 0x020001B7 RID: 439
		public struct Enumerator : IEnumerator<SequencePoint>, IEnumerator, IDisposable
		{
			// Token: 0x06000CF5 RID: 3317 RVA: 0x00026128 File Offset: 0x00024328
			internal Enumerator(MemoryBlock block, DocumentHandle document)
			{
				this._reader = new BlobReader(block);
				this._current = new SequencePoint(document, -1);
				this._previousNonHiddenStartLine = -1;
				this._previousNonHiddenStartColumn = 0;
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000CF6 RID: 3318 RVA: 0x00026154 File Offset: 0x00024354
			public bool MoveNext()
			{
				if (this._reader.RemainingBytes == 0)
				{
					return false;
				}
				DocumentHandle documentHandle = this._current.Document;
				int num;
				if (this._reader.Offset == 0)
				{
					this._reader.ReadCompressedInteger();
					if (documentHandle.IsNil)
					{
						documentHandle = this.ReadDocumentHandle();
					}
					num = this._reader.ReadCompressedInteger();
				}
				else
				{
					int num2;
					while ((num2 = this._reader.ReadCompressedInteger()) == 0)
					{
						documentHandle = this.ReadDocumentHandle();
					}
					num = this.AddOffsets(this._current.Offset, num2);
				}
				int num3;
				int num4;
				this.ReadDeltaLinesAndColumns(out num3, out num4);
				if (num3 == 0 && num4 == 0)
				{
					this._current = new SequencePoint(documentHandle, num);
					return true;
				}
				int num5;
				ushort num6;
				if (this._previousNonHiddenStartLine < 0)
				{
					num5 = this.ReadLine();
					num6 = this.ReadColumn();
				}
				else
				{
					num5 = this.AddLines(this._previousNonHiddenStartLine, this._reader.ReadCompressedSignedInteger());
					num6 = this.AddColumns(this._previousNonHiddenStartColumn, this._reader.ReadCompressedSignedInteger());
				}
				this._previousNonHiddenStartLine = num5;
				this._previousNonHiddenStartColumn = num6;
				this._current = new SequencePoint(documentHandle, num, num5, num6, this.AddLines(num5, num3), this.AddColumns(num6, num4));
				return true;
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x0002627E File Offset: 0x0002447E
			private void ReadDeltaLinesAndColumns(out int deltaLines, out int deltaColumns)
			{
				deltaLines = this._reader.ReadCompressedInteger();
				deltaColumns = ((deltaLines == 0) ? this._reader.ReadCompressedInteger() : this._reader.ReadCompressedSignedInteger());
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x000262AB File Offset: 0x000244AB
			private int ReadLine()
			{
				return this._reader.ReadCompressedInteger();
			}

			// Token: 0x06000CF9 RID: 3321 RVA: 0x000262B8 File Offset: 0x000244B8
			private ushort ReadColumn()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num > 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06000CFA RID: 3322 RVA: 0x000262E0 File Offset: 0x000244E0
			private int AddOffsets(int value, int delta)
			{
				int num = value + delta;
				if (num < 0)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06000CFB RID: 3323 RVA: 0x000262FC File Offset: 0x000244FC
			private int AddLines(int value, int delta)
			{
				int num = value + delta;
				if (num < 0 || num >= 16707566)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return num;
			}

			// Token: 0x06000CFC RID: 3324 RVA: 0x00026320 File Offset: 0x00024520
			private ushort AddColumns(ushort value, int delta)
			{
				int num = (int)value + delta;
				if (num < 0 || num >= 65535)
				{
					Throw.SequencePointValueOutOfRange();
				}
				return (ushort)num;
			}

			// Token: 0x06000CFD RID: 3325 RVA: 0x00026344 File Offset: 0x00024544
			private DocumentHandle ReadDocumentHandle()
			{
				int num = this._reader.ReadCompressedInteger();
				if (num == 0 || !TokenTypeIds.IsValidRowId(num))
				{
					Throw.InvalidHandle();
				}
				return DocumentHandle.FromRowId(num);
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700034D RID: 845
			// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00026373 File Offset: 0x00024573
			public SequencePoint Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002637B File Offset: 0x0002457B
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000D00 RID: 3328 RVA: 0x00026388 File Offset: 0x00024588
			public void Reset()
			{
				this._reader.Reset();
				this._current = default(SequencePoint);
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000D01 RID: 3329 RVA: 0x000263A1 File Offset: 0x000245A1
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A93 RID: 2707
			private BlobReader _reader;

			// Token: 0x04000A94 RID: 2708
			private SequencePoint _current;

			// Token: 0x04000A95 RID: 2709
			private int _previousNonHiddenStartLine;

			// Token: 0x04000A96 RID: 2710
			private ushort _previousNonHiddenStartColumn;
		}
	}
}
