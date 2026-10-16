using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002ED RID: 749
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SourceTextStream : Stream
	{
		// Token: 0x06002018 RID: 8216 RVA: 0x000634CC File Offset: 0x000616CC
		public SourceTextStream(SourceText source, int bufferSize = 2048, bool useDefaultEncodingIfNull = false)
		{
			this._source = source;
			this._encoding = source.Encoding ?? SourceTextStream.s_utf8EncodingWithNoBOM;
			this._encoder = this._encoding.GetEncoder();
			this._minimumTargetBufferCount = this._encoding.GetMaxByteCount(1);
			this._sourceOffset = 0;
			this._position = 0;
			this._charBuffer = new char[Math.Min(bufferSize, this._source.Length)];
			this._bufferOffset = 0;
			this._bufferUnreadChars = 0;
			this._preambleWritten = false;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0006355D File Offset: 0x0006175D
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x00063560 File Offset: 0x00061760
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x00063563 File Offset: 0x00061763
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00063566 File Offset: 0x00061766
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0006356D File Offset: 0x0006176D
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x00063574 File Offset: 0x00061774
		// (set) Token: 0x0600201F RID: 8223 RVA: 0x0006357D File Offset: 0x0006177D
		public override long Position
		{
			get
			{
				return (long)this._position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00063584 File Offset: 0x00061784
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count < this._minimumTargetBufferCount)
			{
				throw new ArgumentException(string.Format("{0} must be greater than or equal to {1}", "count", this._minimumTargetBufferCount), "count");
			}
			int num = count;
			if (!this._preambleWritten)
			{
				int num2 = this.WritePreamble(buffer, offset, count);
				offset += num2;
				count -= num2;
			}
			while (count >= this._minimumTargetBufferCount && this._position < this._source.Length)
			{
				if (this._bufferUnreadChars == 0)
				{
					this.FillBuffer();
				}
				int num3;
				int num4;
				bool flag;
				this._encoder.Convert(this._charBuffer, this._bufferOffset, this._bufferUnreadChars, buffer, offset, count, false, ref num3, ref num4, ref flag);
				this._position += num3;
				this._bufferOffset += num3;
				this._bufferUnreadChars -= num3;
				offset += num4;
				count -= num4;
			}
			return num - count;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0006366C File Offset: 0x0006186C
		private int WritePreamble(byte[] buffer, int offset, int count)
		{
			this._preambleWritten = true;
			byte[] preamble = this._encoding.GetPreamble();
			if (preamble == null)
			{
				return 0;
			}
			int num = Math.Min(count, preamble.Length);
			Array.Copy(preamble, 0, buffer, offset, num);
			return num;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000636A8 File Offset: 0x000618A8
		private void FillBuffer()
		{
			int num = Math.Min(this._charBuffer.Length, this._source.Length - this._sourceOffset);
			this._source.CopyTo(this._sourceOffset, this._charBuffer, 0, num);
			this._sourceOffset += num;
			this._bufferOffset = 0;
			this._bufferUnreadChars = num;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0006370A File Offset: 0x0006190A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00063711 File Offset: 0x00061911
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00063718 File Offset: 0x00061918
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000E0E RID: 3598
		private readonly SourceText _source;

		// Token: 0x04000E0F RID: 3599
		private readonly Encoding _encoding;

		// Token: 0x04000E10 RID: 3600
		private readonly Encoder _encoder;

		// Token: 0x04000E11 RID: 3601
		private readonly int _minimumTargetBufferCount;

		// Token: 0x04000E12 RID: 3602
		private int _position;

		// Token: 0x04000E13 RID: 3603
		private int _sourceOffset;

		// Token: 0x04000E14 RID: 3604
		private readonly char[] _charBuffer;

		// Token: 0x04000E15 RID: 3605
		private int _bufferOffset;

		// Token: 0x04000E16 RID: 3606
		private int _bufferUnreadChars;

		// Token: 0x04000E17 RID: 3607
		private bool _preambleWritten;

		// Token: 0x04000E18 RID: 3608
		private static readonly Encoding s_utf8EncodingWithNoBOM = new UTF8Encoding(false, false);
	}
}
