using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E5 RID: 741
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LargeTextWriter : SourceTextWriter
	{
		// Token: 0x06001FBD RID: 8125 RVA: 0x000621C9 File Offset: 0x000603C9
		[NullableContext(2)]
		public LargeTextWriter(Encoding encoding, SourceHashAlgorithm checksumAlgorithm, int length)
		{
			this._encoding = encoding;
			this._checksumAlgorithm = checksumAlgorithm;
			this._chunks = ArrayBuilder<char[]>.GetInstance(1 + length / 40960);
			this._bufferSize = Math.Min(40960, length);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00062204 File Offset: 0x00060404
		public override SourceText ToSourceText()
		{
			this.Flush();
			return new LargeText(this._chunks.ToImmutableAndFree(), this._encoding, default(ImmutableArray<byte>), this._checksumAlgorithm, default(ImmutableArray<byte>));
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x00062245 File Offset: 0x00060445
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0006224D File Offset: 0x0006044D
		public bool CanFitInAllocatedBuffer(int chars)
		{
			return this._buffer != null && chars <= this._buffer.Length - this._currentUsed;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00062270 File Offset: 0x00060470
		public override void Write(char value)
		{
			if (this._buffer != null && this._currentUsed < this._buffer.Length)
			{
				this._buffer[this._currentUsed] = value;
				this._currentUsed++;
				return;
			}
			this.Write(new char[] { value }, 0, 1);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000622C4 File Offset: 0x000604C4
		[NullableContext(2)]
		public override void Write(string value)
		{
			if (value != null)
			{
				int i = value.Length;
				int num = 0;
				while (i > 0)
				{
					this.EnsureBuffer();
					int num2 = Math.Min(this._buffer.Length - this._currentUsed, i);
					value.CopyTo(num, this._buffer, this._currentUsed, num2);
					this._currentUsed += num2;
					num += num2;
					i -= num2;
					if (this._currentUsed == this._buffer.Length)
					{
						this.Flush();
					}
				}
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00062340 File Offset: 0x00060540
		public override void Write(char[] chars, int index, int count)
		{
			if (index < 0 || index >= chars.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > chars.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				this.EnsureBuffer();
				int num = Math.Min(this._buffer.Length - this._currentUsed, count);
				Array.Copy(chars, index, this._buffer, this._currentUsed, num);
				this._currentUsed += num;
				index += num;
				count -= num;
				if (this._currentUsed == this._buffer.Length)
				{
					this.Flush();
				}
			}
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000623DB File Offset: 0x000605DB
		internal void AppendChunk(char[] chunk)
		{
			if (this.CanFitInAllocatedBuffer(chunk.Length))
			{
				this.Write(chunk, 0, chunk.Length);
				return;
			}
			this.Flush();
			this._chunks.Add(chunk);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00062408 File Offset: 0x00060608
		public override void Flush()
		{
			if (this._buffer != null && this._currentUsed > 0)
			{
				if (this._currentUsed < this._buffer.Length)
				{
					Array.Resize<char>(ref this._buffer, this._currentUsed);
				}
				this._chunks.Add(this._buffer);
				this._buffer = null;
				this._currentUsed = 0;
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00062466 File Offset: 0x00060666
		private void EnsureBuffer()
		{
			if (this._buffer == null)
			{
				this._buffer = new char[this._bufferSize];
			}
		}

		// Token: 0x04000DF4 RID: 3572
		[Nullable(2)]
		private readonly Encoding _encoding;

		// Token: 0x04000DF5 RID: 3573
		private readonly SourceHashAlgorithm _checksumAlgorithm;

		// Token: 0x04000DF6 RID: 3574
		private readonly ArrayBuilder<char[]> _chunks;

		// Token: 0x04000DF7 RID: 3575
		private readonly int _bufferSize;

		// Token: 0x04000DF8 RID: 3576
		[Nullable(2)]
		private char[] _buffer;

		// Token: 0x04000DF9 RID: 3577
		private int _currentUsed;
	}
}
