using System;

namespace System.IO.Compression
{
	// Token: 0x0200002A RID: 42
	internal sealed class OutputWindow
	{
		// Token: 0x06000106 RID: 262 RVA: 0x000062B0 File Offset: 0x000044B0
		public void Write(byte b)
		{
			byte[] window = this._window;
			int end = this._end;
			this._end = end + 1;
			window[end] = b;
			this._end &= 262143;
			this._bytesUsed++;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000062F8 File Offset: 0x000044F8
		public void WriteLengthDistance(int length, int distance)
		{
			this._bytesUsed += length;
			int num = (this._end - distance) & 262143;
			int num2 = 262144 - length;
			if (num > num2 || this._end >= num2)
			{
				while (length-- > 0)
				{
					byte[] window = this._window;
					int num3 = this._end;
					this._end = num3 + 1;
					window[num3] = this._window[num++];
					this._end &= 262143;
					num &= 262143;
				}
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this._window, num, this._window, this._end, length);
				this._end += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] window2 = this._window;
				int num3 = this._end;
				this._end = num3 + 1;
				window2[num3] = this._window[num++];
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000063E0 File Offset: 0x000045E0
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 262144 - this._bytesUsed), input.AvailableBytes);
			int num = 262144 - this._end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(this._window, this._end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(this._window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(this._window, this._end, length);
			}
			this._end = (this._end + num2) & 262143;
			this._bytesUsed += num2;
			return num2;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006481 File Offset: 0x00004681
		public int FreeBytes
		{
			get
			{
				return 262144 - this._bytesUsed;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000648F File Offset: 0x0000468F
		public int AvailableBytes
		{
			get
			{
				return this._bytesUsed;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006498 File Offset: 0x00004698
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > this._bytesUsed)
			{
				num = this._end;
				length = this._bytesUsed;
			}
			else
			{
				num = (this._end - this._bytesUsed + length) & 262143;
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(this._window, 262144 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(this._window, num - length, output, offset, length);
			this._bytesUsed -= num2;
			return num2;
		}

		// Token: 0x04000169 RID: 361
		private const int WindowSize = 262144;

		// Token: 0x0400016A RID: 362
		private const int WindowMask = 262143;

		// Token: 0x0400016B RID: 363
		private readonly byte[] _window = new byte[262144];

		// Token: 0x0400016C RID: 364
		private int _end;

		// Token: 0x0400016D RID: 365
		private int _bytesUsed;
	}
}
