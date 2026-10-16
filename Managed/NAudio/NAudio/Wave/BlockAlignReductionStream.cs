using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001DE RID: 478
	public class BlockAlignReductionStream : WaveStream
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001E8FF File Offset: 0x0001CAFF
		public BlockAlignReductionStream(WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.circularBuffer = new CircularBuffer(sourceStream.WaveFormat.AverageBytesPerSecond * 4);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001E931 File Offset: 0x0001CB31
		private byte[] GetSourceBuffer(int size)
		{
			if (this.sourceBuffer == null || this.sourceBuffer.Length < size)
			{
				this.sourceBuffer = new byte[size * 2];
			}
			return this.sourceBuffer;
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001E95A File Offset: 0x0001CB5A
		public override int BlockAlign
		{
			get
			{
				return this.WaveFormat.BitsPerSample / 8 * this.WaveFormat.Channels;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x0001E975 File Offset: 0x0001CB75
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001E982 File Offset: 0x0001CB82
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001E98F File Offset: 0x0001CB8F
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001E998 File Offset: 0x0001CB98
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				lock (this.lockObject)
				{
					if (this.position != value)
					{
						if (this.position % (long)this.BlockAlign != 0L)
						{
							throw new ArgumentException("Position must be block aligned");
						}
						long num = value - value % (long)this.sourceStream.BlockAlign;
						if (this.sourceStream.Position != num)
						{
							this.sourceStream.Position = num;
							this.circularBuffer.Reset();
							this.bufferStartPosition = this.sourceStream.Position;
						}
						this.position = value;
					}
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private long BufferEndPosition
		{
			get
			{
				return this.bufferStartPosition + (long)this.circularBuffer.Count;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001EA55 File Offset: 0x0001CC55
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num4;
			lock (this.lockObject)
			{
				while (this.BufferEndPosition < this.position + (long)count)
				{
					int num = count;
					if (num % this.sourceStream.BlockAlign != 0)
					{
						num = count + this.sourceStream.BlockAlign - count % this.sourceStream.BlockAlign;
					}
					int num2 = this.sourceStream.Read(this.GetSourceBuffer(num), 0, num);
					this.circularBuffer.Write(this.GetSourceBuffer(num), 0, num2);
					if (num2 == 0)
					{
						break;
					}
				}
				if (this.bufferStartPosition < this.position)
				{
					this.circularBuffer.Advance((int)(this.position - this.bufferStartPosition));
					this.bufferStartPosition = this.position;
				}
				int num3 = this.circularBuffer.Read(buffer, offset, count);
				this.position += (long)num3;
				this.bufferStartPosition = this.position;
				num4 = num3;
			}
			return num4;
		}

		// Token: 0x04000B63 RID: 2915
		private WaveStream sourceStream;

		// Token: 0x04000B64 RID: 2916
		private long position;

		// Token: 0x04000B65 RID: 2917
		private readonly CircularBuffer circularBuffer;

		// Token: 0x04000B66 RID: 2918
		private long bufferStartPosition;

		// Token: 0x04000B67 RID: 2919
		private byte[] sourceBuffer;

		// Token: 0x04000B68 RID: 2920
		private readonly object lockObject = new object();
	}
}
