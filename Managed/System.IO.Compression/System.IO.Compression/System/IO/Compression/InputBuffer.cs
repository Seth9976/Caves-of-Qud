using System;

namespace System.IO.Compression
{
	// Token: 0x02000025 RID: 37
	internal sealed class InputBuffer
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005CDC File Offset: 0x00003EDC
		public int AvailableBits
		{
			get
			{
				return this._bitsInBuffer;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005CE4 File Offset: 0x00003EE4
		public int AvailableBytes
		{
			get
			{
				return this._end - this._start + this._bitsInBuffer / 8;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005CFC File Offset: 0x00003EFC
		public bool EnsureBitsAvailable(int count)
		{
			if (this._bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					return false;
				}
				uint bitBuffer = this._bitBuffer;
				byte[] buffer = this._buffer;
				int num = this._start;
				this._start = num + 1;
				this._bitBuffer = bitBuffer | (buffer[num] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
				if (this._bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						return false;
					}
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer2 | (buffer2[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public uint TryLoad16Bits()
		{
			if (this._bitsInBuffer < 8)
			{
				if (this._start < this._end)
				{
					uint bitBuffer = this._bitBuffer;
					byte[] buffer = this._buffer;
					int num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer | (buffer[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
				if (this._start < this._end)
				{
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					int num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer2 | (buffer2[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			else if (this._bitsInBuffer < 16 && this._start < this._end)
			{
				uint bitBuffer3 = this._bitBuffer;
				byte[] buffer3 = this._buffer;
				int num = this._start;
				this._start = num + 1;
				this._bitBuffer = bitBuffer3 | (buffer3[num] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
			}
			return this._bitBuffer;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005EBF File Offset: 0x000040BF
		private uint GetBitMask(int count)
		{
			return (1U << count) - 1U;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005EC9 File Offset: 0x000040C9
		public int GetBits(int count)
		{
			if (!this.EnsureBitsAvailable(count))
			{
				return -1;
			}
			int num = (int)(this._bitBuffer & this.GetBitMask(count));
			this._bitBuffer >>= count;
			this._bitsInBuffer -= count;
			return num;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005F04 File Offset: 0x00004104
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this._bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)this._bitBuffer;
				this._bitBuffer >>= 8;
				this._bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this._end - this._start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this._buffer, this._start, output, offset, length);
			this._start += length;
			return num + length;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005F95 File Offset: 0x00004195
		public bool NeedsInput()
		{
			return this._start == this._end;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005FA5 File Offset: 0x000041A5
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this._buffer = buffer;
			this._start = offset;
			this._end = offset + length;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005FBE File Offset: 0x000041BE
		public void SkipBits(int n)
		{
			this._bitBuffer >>= n;
			this._bitsInBuffer -= n;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005FDF File Offset: 0x000041DF
		public void SkipToByteBoundary()
		{
			this._bitBuffer >>= this._bitsInBuffer % 8;
			this._bitsInBuffer -= this._bitsInBuffer % 8;
		}

		// Token: 0x04000155 RID: 341
		private byte[] _buffer;

		// Token: 0x04000156 RID: 342
		private int _start;

		// Token: 0x04000157 RID: 343
		private int _end;

		// Token: 0x04000158 RID: 344
		private uint _bitBuffer;

		// Token: 0x04000159 RID: 345
		private int _bitsInBuffer;
	}
}
