using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F7 RID: 759
	public abstract class TextLineCollection : IReadOnlyList<TextLine>, IEnumerable<TextLine>, IEnumerable, IReadOnlyCollection<TextLine>
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002079 RID: 8313
		public abstract int Count { get; }

		// Token: 0x17000858 RID: 2136
		public abstract TextLine this[int index] { get; }

		// Token: 0x0600207B RID: 8315
		public abstract int IndexOf(int position);

		// Token: 0x0600207C RID: 8316 RVA: 0x00064265 File Offset: 0x00062465
		public virtual TextLine GetLineFromPosition(int position)
		{
			return this[this.IndexOf(position)];
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00064274 File Offset: 0x00062474
		public virtual LinePosition GetLinePosition(int position)
		{
			TextLine lineFromPosition = this.GetLineFromPosition(position);
			return new LinePosition(lineFromPosition.LineNumber, position - lineFromPosition.Start);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0006429E File Offset: 0x0006249E
		public LinePositionSpan GetLinePositionSpan(TextSpan span)
		{
			return new LinePositionSpan(this.GetLinePosition(span.Start), this.GetLinePosition(span.End));
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000642C0 File Offset: 0x000624C0
		public int GetPosition(LinePosition position)
		{
			return this[position.Line].Start + position.Character;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000642EA File Offset: 0x000624EA
		public TextSpan GetTextSpan(LinePositionSpan span)
		{
			return TextSpan.FromBounds(this.GetPosition(span.Start), this.GetPosition(span.End));
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0006430B File Offset: 0x0006250B
		public TextLineCollection.Enumerator GetEnumerator()
		{
			return new TextLineCollection.Enumerator(this, -1);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00064314 File Offset: 0x00062514
		[NullableContext(1)]
		IEnumerator<TextLine> IEnumerable<TextLine>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00064321 File Offset: 0x00062521
		[NullableContext(1)]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x02000680 RID: 1664
		public struct Enumerator : IEnumerator<TextLine>, IEnumerator, IDisposable
		{
			// Token: 0x06003944 RID: 14660 RVA: 0x0009F172 File Offset: 0x0009D372
			[NullableContext(1)]
			internal Enumerator(TextLineCollection lines, int index = -1)
			{
				this._lines = lines;
				this._index = index;
			}

			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x06003945 RID: 14661 RVA: 0x0009F184 File Offset: 0x0009D384
			public TextLine Current
			{
				get
				{
					int index = this._index;
					if (index >= 0 && index < this._lines.Count)
					{
						return this._lines[index];
					}
					return default(TextLine);
				}
			}

			// Token: 0x06003946 RID: 14662 RVA: 0x0009F1C0 File Offset: 0x0009D3C0
			public bool MoveNext()
			{
				if (this._index < this._lines.Count - 1)
				{
					this._index++;
					return true;
				}
				return false;
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x06003947 RID: 14663 RVA: 0x0009F1E8 File Offset: 0x0009D3E8
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003948 RID: 14664 RVA: 0x0009F1F5 File Offset: 0x0009D3F5
			bool IEnumerator.MoveNext()
			{
				return this.MoveNext();
			}

			// Token: 0x06003949 RID: 14665 RVA: 0x0009F1FD File Offset: 0x0009D3FD
			void IEnumerator.Reset()
			{
			}

			// Token: 0x0600394A RID: 14666 RVA: 0x0009F1FF File Offset: 0x0009D3FF
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600394B RID: 14667 RVA: 0x0009F201 File Offset: 0x0009D401
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600394C RID: 14668 RVA: 0x0009F208 File Offset: 0x0009D408
			public override int GetHashCode()
			{
				throw new NotSupportedException();
			}

			// Token: 0x040018ED RID: 6381
			[Nullable(1)]
			private readonly TextLineCollection _lines;

			// Token: 0x040018EE RID: 6382
			private int _index;
		}
	}
}
