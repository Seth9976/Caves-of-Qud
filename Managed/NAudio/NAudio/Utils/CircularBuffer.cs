using System;

namespace NAudio.Utils
{
	// Token: 0x02000110 RID: 272
	public class CircularBuffer
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00012F57 File Offset: 0x00011157
		public CircularBuffer(int size)
		{
			this.buffer = new byte[size];
			this.lockObject = new object();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00012F78 File Offset: 0x00011178
		public int Write(byte[] data, int offset, int count)
		{
			int num3;
			lock (this.lockObject)
			{
				int num = 0;
				if (count > this.buffer.Length - this.byteCount)
				{
					count = this.buffer.Length - this.byteCount;
				}
				int num2 = Math.Min(this.buffer.Length - this.writePosition, count);
				Array.Copy(data, offset, this.buffer, this.writePosition, num2);
				this.writePosition += num2;
				this.writePosition %= this.buffer.Length;
				num += num2;
				if (num < count)
				{
					Array.Copy(data, offset + num, this.buffer, this.writePosition, count - num);
					this.writePosition += count - num;
					num = count;
				}
				this.byteCount += num;
				num3 = num;
			}
			return num3;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00013060 File Offset: 0x00011260
		public int Read(byte[] data, int offset, int count)
		{
			int num3;
			lock (this.lockObject)
			{
				if (count > this.byteCount)
				{
					count = this.byteCount;
				}
				int num = 0;
				int num2 = Math.Min(this.buffer.Length - this.readPosition, count);
				Array.Copy(this.buffer, this.readPosition, data, offset, num2);
				num += num2;
				this.readPosition += num2;
				this.readPosition %= this.buffer.Length;
				if (num < count)
				{
					Array.Copy(this.buffer, this.readPosition, data, offset + num, count - num);
					this.readPosition += count - num;
					num = count;
				}
				this.byteCount -= num;
				num3 = num;
			}
			return num3;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00013138 File Offset: 0x00011338
		public int MaxLength
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00013142 File Offset: 0x00011342
		public int Count
		{
			get
			{
				return this.byteCount;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001314A File Offset: 0x0001134A
		public void Reset()
		{
			this.byteCount = 0;
			this.readPosition = 0;
			this.writePosition = 0;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00013164 File Offset: 0x00011364
		public void Advance(int count)
		{
			if (count >= this.byteCount)
			{
				this.Reset();
				return;
			}
			this.byteCount -= count;
			this.readPosition += count;
			this.readPosition %= this.MaxLength;
		}

		// Token: 0x0400069E RID: 1694
		private readonly byte[] buffer;

		// Token: 0x0400069F RID: 1695
		private readonly object lockObject;

		// Token: 0x040006A0 RID: 1696
		private int writePosition;

		// Token: 0x040006A1 RID: 1697
		private int readPosition;

		// Token: 0x040006A2 RID: 1698
		private int byteCount;
	}
}
