using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001CA RID: 458
	public class BufferedWaveProvider : IWaveProvider
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x0001C772 File Offset: 0x0001A972
		public BufferedWaveProvider(WaveFormat waveFormat)
		{
			this.waveFormat = waveFormat;
			this.BufferLength = waveFormat.AverageBytesPerSecond * 5;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001C78F File Offset: 0x0001A98F
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0001C797 File Offset: 0x0001A997
		public int BufferLength { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0001C7BB File Offset: 0x0001A9BB
		public TimeSpan BufferDuration
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.BufferLength / (double)this.WaveFormat.AverageBytesPerSecond);
			}
			set
			{
				this.BufferLength = (int)(value.TotalSeconds * (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public bool DiscardOnBufferOverflow { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001C7E9 File Offset: 0x0001A9E9
		public int BufferedBytes
		{
			get
			{
				if (this.circularBuffer != null)
				{
					return this.circularBuffer.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0001C800 File Offset: 0x0001AA00
		public TimeSpan BufferedDuration
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.BufferedBytes / (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001C81B File Offset: 0x0001AA1B
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001C824 File Offset: 0x0001AA24
		public void AddSamples(byte[] buffer, int offset, int count)
		{
			if (this.circularBuffer == null)
			{
				this.circularBuffer = new CircularBuffer(this.BufferLength);
			}
			int num = this.circularBuffer.Write(buffer, offset, count);
			if (num < count && !this.DiscardOnBufferOverflow)
			{
				throw new InvalidOperationException("Buffer full");
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001C870 File Offset: 0x0001AA70
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.circularBuffer != null)
			{
				num = this.circularBuffer.Read(buffer, offset, count);
			}
			if (num < count)
			{
				Array.Clear(buffer, offset + num, count - num);
			}
			return count;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001C8A7 File Offset: 0x0001AAA7
		public void ClearBuffer()
		{
			if (this.circularBuffer != null)
			{
				this.circularBuffer.Reset();
			}
		}

		// Token: 0x04000B18 RID: 2840
		private CircularBuffer circularBuffer;

		// Token: 0x04000B19 RID: 2841
		private readonly WaveFormat waveFormat;
	}
}
