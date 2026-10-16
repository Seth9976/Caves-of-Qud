using System;

namespace System.Net.Mime
{
	// Token: 0x020007ED RID: 2029
	internal class WriteStateInfoBase
	{
		// Token: 0x060040E4 RID: 16612 RVA: 0x000DE424 File Offset: 0x000DC624
		internal WriteStateInfoBase()
		{
			this._header = Array.Empty<byte>();
			this._footer = Array.Empty<byte>();
			this._maxLineLength = 70;
			this._buffer = new byte[1024];
			this._currentLineLength = 0;
			this._currentBufferUsed = 0;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000DE473 File Offset: 0x000DC673
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength)
			: this(bufferSize, header, footer, maxLineLength, 0)
		{
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000DE481 File Offset: 0x000DC681
		internal WriteStateInfoBase(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength)
		{
			this._buffer = new byte[bufferSize];
			this._header = header;
			this._footer = footer;
			this._maxLineLength = maxLineLength;
			this._currentLineLength = mimeHeaderLength;
			this._currentBufferUsed = 0;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000DE4BA File Offset: 0x000DC6BA
		internal int FooterLength
		{
			get
			{
				return this._footer.Length;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000DE4C4 File Offset: 0x000DC6C4
		internal byte[] Footer
		{
			get
			{
				return this._footer;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000DE4CC File Offset: 0x000DC6CC
		internal byte[] Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		internal byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x000DE4DC File Offset: 0x000DC6DC
		internal int Length
		{
			get
			{
				return this._currentBufferUsed;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000DE4E4 File Offset: 0x000DC6E4
		internal int CurrentLineLength
		{
			get
			{
				return this._currentLineLength;
			}
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000DE4EC File Offset: 0x000DC6EC
		private void EnsureSpaceInBuffer(int moreBytes)
		{
			int num = this.Buffer.Length;
			while (this._currentBufferUsed + moreBytes >= num)
			{
				num *= 2;
			}
			if (num > this.Buffer.Length)
			{
				byte[] array = new byte[num];
				this._buffer.CopyTo(array, 0);
				this._buffer = array;
			}
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000DE53C File Offset: 0x000DC73C
		internal void Append(byte aByte)
		{
			this.EnsureSpaceInBuffer(1);
			byte[] buffer = this.Buffer;
			int currentBufferUsed = this._currentBufferUsed;
			this._currentBufferUsed = currentBufferUsed + 1;
			buffer[currentBufferUsed] = aByte;
			this._currentLineLength++;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000DE577 File Offset: 0x000DC777
		internal void Append(params byte[] bytes)
		{
			this.EnsureSpaceInBuffer(bytes.Length);
			bytes.CopyTo(this._buffer, this.Length);
			this._currentLineLength += bytes.Length;
			this._currentBufferUsed += bytes.Length;
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000DE5B4 File Offset: 0x000DC7B4
		internal void AppendCRLF(bool includeSpace)
		{
			this.AppendFooter();
			this.Append(new byte[] { 13, 10 });
			this._currentLineLength = 0;
			if (includeSpace)
			{
				this.Append(32);
			}
			this.AppendHeader();
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000DE5EA File Offset: 0x000DC7EA
		internal void AppendHeader()
		{
			if (this.Header != null && this.Header.Length != 0)
			{
				this.Append(this.Header);
			}
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000DE609 File Offset: 0x000DC809
		internal void AppendFooter()
		{
			if (this.Footer != null && this.Footer.Length != 0)
			{
				this.Append(this.Footer);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x000DE628 File Offset: 0x000DC828
		internal int MaxLineLength
		{
			get
			{
				return this._maxLineLength;
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000DE630 File Offset: 0x000DC830
		internal void Reset()
		{
			this._currentBufferUsed = 0;
			this._currentLineLength = 0;
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000DE640 File Offset: 0x000DC840
		internal void BufferFlushed()
		{
			this._currentBufferUsed = 0;
		}

		// Token: 0x04002705 RID: 9989
		protected readonly byte[] _header;

		// Token: 0x04002706 RID: 9990
		protected readonly byte[] _footer;

		// Token: 0x04002707 RID: 9991
		protected readonly int _maxLineLength;

		// Token: 0x04002708 RID: 9992
		protected byte[] _buffer;

		// Token: 0x04002709 RID: 9993
		protected int _currentLineLength;

		// Token: 0x0400270A RID: 9994
		protected int _currentBufferUsed;

		// Token: 0x0400270B RID: 9995
		protected const int DefaultBufferSize = 1024;
	}
}
