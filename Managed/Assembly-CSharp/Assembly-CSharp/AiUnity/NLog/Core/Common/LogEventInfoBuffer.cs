using System;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002B7 RID: 695
	public class LogEventInfoBuffer
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x0007EBF1 File Offset: 0x0007CDF1
		public LogEventInfoBuffer(int size, bool growAsNeeded, int growLimit)
		{
			this.growAsNeeded = growAsNeeded;
			this.buffer = new AsyncLogEventInfo[size];
			this.growLimit = growLimit;
			this.getPointer = 0;
			this.putPointer = 0;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0007EC21 File Offset: 0x0007CE21
		public int Size
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0007EC2C File Offset: 0x0007CE2C
		public int Append(AsyncLogEventInfo eventInfo)
		{
			int num2;
			lock (this)
			{
				if (this.count >= this.buffer.Length)
				{
					if (this.growAsNeeded && this.buffer.Length < this.growLimit)
					{
						int num = this.buffer.Length * 2;
						if (num >= this.growLimit)
						{
							num = this.growLimit;
						}
						AsyncLogEventInfo[] array = new AsyncLogEventInfo[num];
						Array.Copy(this.buffer, 0, array, 0, this.buffer.Length);
						this.buffer = array;
					}
					else
					{
						this.getPointer++;
					}
				}
				this.putPointer %= this.buffer.Length;
				this.buffer[this.putPointer] = eventInfo;
				this.putPointer++;
				this.count++;
				if (this.count >= this.buffer.Length)
				{
					this.count = this.buffer.Length;
				}
				num2 = this.count;
			}
			return num2;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0007ED44 File Offset: 0x0007CF44
		public AsyncLogEventInfo[] GetEventsAndClear()
		{
			AsyncLogEventInfo[] array2;
			lock (this)
			{
				int num = this.count;
				AsyncLogEventInfo[] array = new AsyncLogEventInfo[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = (this.getPointer + i) % this.buffer.Length;
					AsyncLogEventInfo asyncLogEventInfo = this.buffer[num2];
					this.buffer[num2] = default(AsyncLogEventInfo);
					array[i] = asyncLogEventInfo;
				}
				this.count = 0;
				this.getPointer = 0;
				this.putPointer = 0;
				array2 = array;
			}
			return array2;
		}

		// Token: 0x04000E77 RID: 3703
		private readonly bool growAsNeeded;

		// Token: 0x04000E78 RID: 3704
		private readonly int growLimit;

		// Token: 0x04000E79 RID: 3705
		private AsyncLogEventInfo[] buffer;

		// Token: 0x04000E7A RID: 3706
		private int getPointer;

		// Token: 0x04000E7B RID: 3707
		private int putPointer;

		// Token: 0x04000E7C RID: 3708
		private int count;
	}
}
