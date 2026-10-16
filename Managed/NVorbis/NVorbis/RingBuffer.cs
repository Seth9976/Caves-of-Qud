using System;

namespace NVorbis
{
	// Token: 0x02000014 RID: 20
	internal class RingBuffer
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005510 File Offset: 0x00003710
		internal RingBuffer(int size)
		{
			this._buffer = new float[size];
			this._start = (this._end = 0);
			this._bufLen = size;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005548 File Offset: 0x00003748
		internal void EnsureSize(int size)
		{
			size += this.Channels;
			if (this._bufLen < size)
			{
				float[] array = new float[size];
				Array.Copy(this._buffer, this._start, array, 0, this._bufLen - this._start);
				if (this._end < this._start)
				{
					Array.Copy(this._buffer, 0, array, this._bufLen - this._start, this._end);
				}
				int length = this.Length;
				this._start = 0;
				this._end = length;
				this._buffer = array;
				this._bufLen = size;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000055E0 File Offset: 0x000037E0
		internal void CopyTo(float[] buffer, int index, int count)
		{
			if (index < 0 || index + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int start = this._start;
			this.RemoveItems(count);
			int num = (this._end - start + this._bufLen) % this._bufLen;
			if (count > num)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num2 = Math.Min(count, this._bufLen - start);
			Buffer.BlockCopy(this._buffer, start * 4, buffer, index * 4, num2 * 4);
			if (num2 < count)
			{
				Buffer.BlockCopy(this._buffer, 0, buffer, (index + num2) * 4, (count - num2) * 4);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005678 File Offset: 0x00003878
		internal void RemoveItems(int count)
		{
			int num = (count + this._start) % this._bufLen;
			if (this._end > this._start)
			{
				if (num > this._end || num < this._start)
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			else if (num < this._start && num > this._end)
			{
				throw new ArgumentOutOfRangeException();
			}
			this._start = num;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000056DC File Offset: 0x000038DC
		internal void Clear()
		{
			this._start = (this._end = 0);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000056FC File Offset: 0x000038FC
		internal int Length
		{
			get
			{
				int num = this._end - this._start;
				if (num < 0)
				{
					num += this._bufLen;
				}
				return num;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005728 File Offset: 0x00003928
		internal void Write(int channel, int index, int start, int switchPoint, int end, float[] pcm, float[] window)
		{
			int i;
			for (i = (index + start) * this.Channels + channel + this._start; i >= this._bufLen; i -= this._bufLen)
			{
			}
			if (i < 0)
			{
				start -= index;
				i = channel;
			}
			while (i < this._bufLen && start < switchPoint)
			{
				this._buffer[i] += pcm[start] * window[start];
				i += this.Channels;
				start++;
			}
			if (i >= this._bufLen)
			{
				i -= this._bufLen;
				while (start < switchPoint)
				{
					this._buffer[i] += pcm[start] * window[start];
					i += this.Channels;
					start++;
				}
			}
			while (i < this._bufLen && start < end)
			{
				this._buffer[i] = pcm[start] * window[start];
				i += this.Channels;
				start++;
			}
			if (i >= this._bufLen)
			{
				i -= this._bufLen;
				while (start < end)
				{
					this._buffer[i] = pcm[start] * window[start];
					i += this.Channels;
					start++;
				}
			}
			this._end = i;
		}

		// Token: 0x04000060 RID: 96
		private float[] _buffer;

		// Token: 0x04000061 RID: 97
		private int _start;

		// Token: 0x04000062 RID: 98
		private int _end;

		// Token: 0x04000063 RID: 99
		private int _bufLen;

		// Token: 0x04000064 RID: 100
		internal int Channels;
	}
}
