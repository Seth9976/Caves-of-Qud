using System;

namespace System.IO.Compression
{
	// Token: 0x02000028 RID: 40
	internal sealed class OutputBuffer
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00006052 File Offset: 0x00004252
		internal void UpdateBuffer(byte[] output)
		{
			this._byteBuffer = output;
			this._pos = 0;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006062 File Offset: 0x00004262
		internal int BytesWritten
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000606A File Offset: 0x0000426A
		internal int FreeBytes
		{
			get
			{
				return this._byteBuffer.Length - this._pos;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000607C File Offset: 0x0000427C
		internal void WriteUInt16(ushort value)
		{
			byte[] byteBuffer = this._byteBuffer;
			int num = this._pos;
			this._pos = num + 1;
			byteBuffer[num] = (byte)value;
			byte[] byteBuffer2 = this._byteBuffer;
			num = this._pos;
			this._pos = num + 1;
			byteBuffer2[num] = (byte)(value >> 8);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000060C0 File Offset: 0x000042C0
		internal void WriteBits(int n, uint bits)
		{
			this._bitBuf |= bits << this._bitCount;
			this._bitCount += n;
			if (this._bitCount >= 16)
			{
				byte[] byteBuffer = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer[num] = (byte)this._bitBuf;
				byte[] byteBuffer2 = this._byteBuffer;
				num = this._pos;
				this._pos = num + 1;
				byteBuffer2[num] = (byte)(this._bitBuf >> 8);
				this._bitCount -= 16;
				this._bitBuf >>= 16;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000615C File Offset: 0x0000435C
		internal void FlushBits()
		{
			while (this._bitCount >= 8)
			{
				byte[] byteBuffer = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer[num] = (byte)this._bitBuf;
				this._bitCount -= 8;
				this._bitBuf >>= 8;
			}
			if (this._bitCount > 0)
			{
				byte[] byteBuffer2 = this._byteBuffer;
				int num = this._pos;
				this._pos = num + 1;
				byteBuffer2[num] = (byte)this._bitBuf;
				this._bitBuf = 0U;
				this._bitCount = 0;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000061E5 File Offset: 0x000043E5
		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (this._bitCount == 0)
			{
				Array.Copy(byteArray, offset, this._byteBuffer, this._pos, count);
				this._pos += count;
				return;
			}
			this.WriteBytesUnaligned(byteArray, offset, count);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000621C File Offset: 0x0000441C
		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				this.WriteByteUnaligned(b);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006242 File Offset: 0x00004442
		private void WriteByteUnaligned(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000624C File Offset: 0x0000444C
		internal int BitsInBuffer
		{
			get
			{
				return this._bitCount / 8 + 1;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006258 File Offset: 0x00004458
		internal OutputBuffer.BufferState DumpState()
		{
			return new OutputBuffer.BufferState(this._pos, this._bitBuf, this._bitCount);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006271 File Offset: 0x00004471
		internal void RestoreState(OutputBuffer.BufferState state)
		{
			this._pos = state._pos;
			this._bitBuf = state._bitBuf;
			this._bitCount = state._bitCount;
		}

		// Token: 0x04000162 RID: 354
		private byte[] _byteBuffer;

		// Token: 0x04000163 RID: 355
		private int _pos;

		// Token: 0x04000164 RID: 356
		private uint _bitBuf;

		// Token: 0x04000165 RID: 357
		private int _bitCount;

		// Token: 0x02000029 RID: 41
		internal readonly struct BufferState
		{
			// Token: 0x06000105 RID: 261 RVA: 0x00006297 File Offset: 0x00004497
			internal BufferState(int pos, uint bitBuf, int bitCount)
			{
				this._pos = pos;
				this._bitBuf = bitBuf;
				this._bitCount = bitCount;
			}

			// Token: 0x04000166 RID: 358
			internal readonly int _pos;

			// Token: 0x04000167 RID: 359
			internal readonly uint _bitBuf;

			// Token: 0x04000168 RID: 360
			internal readonly int _bitCount;
		}
	}
}
