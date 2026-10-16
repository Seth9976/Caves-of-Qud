using System;
using System.IO;

namespace NAudio.Wave
{
	// Token: 0x020001DA RID: 474
	public abstract class WaveStream : Stream, IWaveProvider
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A47 RID: 2631
		public abstract WaveFormat WaveFormat { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001DFA3 File Offset: 0x0001C1A3
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001DFA6 File Offset: 0x0001C1A6
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DFA9 File Offset: 0x0001C1A9
		public override void Flush()
		{
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DFAB File Offset: 0x0001C1AB
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == null)
			{
				this.Position = offset;
			}
			else if (origin == 1)
			{
				this.Position += offset;
			}
			else
			{
				this.Position = this.Length + offset;
			}
			return this.Position;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DFE1 File Offset: 0x0001C1E1
		public override void SetLength(long length)
		{
			throw new NotSupportedException("Can't set length of a WaveFormatString");
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DFED File Offset: 0x0001C1ED
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Can't write to a WaveFormatString");
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001DFF9 File Offset: 0x0001C1F9
		public virtual int BlockAlign
		{
			get
			{
				return this.WaveFormat.BlockAlign;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001E008 File Offset: 0x0001C208
		public void Skip(int seconds)
		{
			long num = this.Position + (long)(this.WaveFormat.AverageBytesPerSecond * seconds);
			if (num > this.Length)
			{
				this.Position = this.Length;
				return;
			}
			if (num < 0L)
			{
				this.Position = 0L;
				return;
			}
			this.Position = num;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001E056 File Offset: 0x0001C256
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0001E071 File Offset: 0x0001C271
		public virtual TimeSpan CurrentTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.Position / (double)this.WaveFormat.AverageBytesPerSecond);
			}
			set
			{
				this.Position = (long)(value.TotalSeconds * (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001E08E File Offset: 0x0001C28E
		public virtual TimeSpan TotalTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.Length / (double)this.WaveFormat.AverageBytesPerSecond);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E0A9 File Offset: 0x0001C2A9
		public virtual bool HasData(int count)
		{
			return this.Position < this.Length;
		}
	}
}
