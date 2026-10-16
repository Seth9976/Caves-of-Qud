using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x020003A3 RID: 931
	internal class EncoderNLS : Encoder
	{
		// Token: 0x0600262B RID: 9771 RVA: 0x000873B2 File Offset: 0x000855B2
		internal EncoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.EncoderFallback;
			this.Reset();
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000873D8 File Offset: 0x000855D8
		internal EncoderNLS()
		{
			this._encoding = null;
			this.Reset();
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000873ED File Offset: 0x000855ED
		public override void Reset()
		{
			this._charLeftOver = '\0';
			if (this._fallbackBuffer != null)
			{
				this._fallbackBuffer.Reset();
			}
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0008740C File Offset: 0x0008560C
		public unsafe override int GetByteCount(char[] chars, int index, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			int byteCount;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				byteCount = this.GetByteCount(ptr + index, count, flush);
			}
			return byteCount;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00087488 File Offset: 0x00085688
		public unsafe override int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetByteCount(chars, count, this);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000874DC File Offset: 0x000856DC
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = bytes.Length - byteIndex;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, num, flush);
				}
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000875A0 File Offset: 0x000857A0
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00087618 File Offset: 0x00085818
		public unsafe override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", "Array cannot be null.");
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", "Non-negative number required.");
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", "Non-negative number required.");
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", "Index and count must refer to a location within the buffer.");
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", "Index and count must refer to a location within the buffer.");
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					this.Convert(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00087704 File Offset: 0x00085904
		public unsafe override void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", "Array cannot be null.");
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", "Non-negative number required.");
			}
			this._mustFlush = flush;
			this._throwOnOverflow = false;
			this._charsUsed = 0;
			bytesUsed = this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
			charsUsed = this._charsUsed;
			completed = charsUsed == charCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000877BF File Offset: 0x000859BF
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000877C7 File Offset: 0x000859C7
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000877CF File Offset: 0x000859CF
		internal virtual bool HasState
		{
			get
			{
				return this._charLeftOver > '\0';
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000877DA File Offset: 0x000859DA
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x04001DBB RID: 7611
		internal char _charLeftOver;

		// Token: 0x04001DBC RID: 7612
		private Encoding _encoding;

		// Token: 0x04001DBD RID: 7613
		private bool _mustFlush;

		// Token: 0x04001DBE RID: 7614
		internal bool _throwOnOverflow;

		// Token: 0x04001DBF RID: 7615
		internal int _charsUsed;
	}
}
