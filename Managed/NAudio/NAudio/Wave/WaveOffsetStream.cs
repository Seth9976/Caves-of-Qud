using System;

namespace NAudio.Wave
{
	// Token: 0x020001F6 RID: 502
	public class WaveOffsetStream : WaveStream
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x000220A8 File Offset: 0x000202A8
		public WaveOffsetStream(WaveStream sourceStream, TimeSpan startTime, TimeSpan sourceOffset, TimeSpan sourceLength)
		{
			if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Only PCM supported");
			}
			this.sourceStream = sourceStream;
			this.position = 0L;
			this.bytesPerSample = sourceStream.WaveFormat.BitsPerSample / 8 * sourceStream.WaveFormat.Channels;
			this.StartTime = startTime;
			this.SourceOffset = sourceOffset;
			this.SourceLength = sourceLength;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00022123 File Offset: 0x00020323
		public WaveOffsetStream(WaveStream sourceStream)
			: this(sourceStream, TimeSpan.Zero, TimeSpan.Zero, sourceStream.TotalTime)
		{
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002213C File Offset: 0x0002033C
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00022144 File Offset: 0x00020344
		public TimeSpan StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				lock (this.lockObject)
				{
					this.startTime = value;
					this.audioStartPosition = (long)(this.startTime.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.length = this.audioStartPosition + this.sourceLengthBytes;
					this.Position = this.Position;
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000221CC File Offset: 0x000203CC
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x000221D4 File Offset: 0x000203D4
		public TimeSpan SourceOffset
		{
			get
			{
				return this.sourceOffset;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceOffset = value;
					this.sourceOffsetBytes = (long)(this.sourceOffset.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.Position = this.Position;
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00022248 File Offset: 0x00020448
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00022250 File Offset: 0x00020450
		public TimeSpan SourceLength
		{
			get
			{
				return this.sourceLength;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceLength = value;
					this.sourceLengthBytes = (long)(this.sourceLength.TotalSeconds * (double)this.sourceStream.WaveFormat.SampleRate) * (long)this.bytesPerSample;
					this.length = this.audioStartPosition + this.sourceLengthBytes;
					this.Position = this.Position;
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x000222D8 File Offset: 0x000204D8
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x000222E5 File Offset: 0x000204E5
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000222ED File Offset: 0x000204ED
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x000222F8 File Offset: 0x000204F8
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
					value -= value % (long)this.BlockAlign;
					if (value < this.audioStartPosition)
					{
						this.sourceStream.Position = this.sourceOffsetBytes;
					}
					else
					{
						this.sourceStream.Position = this.sourceOffsetBytes + (value - this.audioStartPosition);
					}
					this.position = value;
				}
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00022378 File Offset: 0x00020578
		public override int Read(byte[] destBuffer, int offset, int numBytes)
		{
			lock (this.lockObject)
			{
				int num = 0;
				if (this.position < this.audioStartPosition)
				{
					num = (int)Math.Min((long)numBytes, this.audioStartPosition - this.position);
					for (int i = 0; i < num; i++)
					{
						destBuffer[i + offset] = 0;
					}
				}
				if (num < numBytes)
				{
					int num2 = (int)Math.Min((long)(numBytes - num), this.sourceLengthBytes + this.sourceOffsetBytes - this.sourceStream.Position);
					int num3 = this.sourceStream.Read(destBuffer, num + offset, num2);
					num += num3;
				}
				for (int j = num; j < numBytes; j++)
				{
					destBuffer[offset + j] = 0;
				}
				this.position += (long)numBytes;
			}
			return numBytes;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00022450 File Offset: 0x00020650
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002245D File Offset: 0x0002065D
		public override bool HasData(int count)
		{
			return this.position + (long)count >= this.audioStartPosition && this.position < this.length && this.sourceStream.HasData(count);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002248E File Offset: 0x0002068E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BDE RID: 3038
		private WaveStream sourceStream;

		// Token: 0x04000BDF RID: 3039
		private long audioStartPosition;

		// Token: 0x04000BE0 RID: 3040
		private long sourceOffsetBytes;

		// Token: 0x04000BE1 RID: 3041
		private long sourceLengthBytes;

		// Token: 0x04000BE2 RID: 3042
		private long length;

		// Token: 0x04000BE3 RID: 3043
		private readonly int bytesPerSample;

		// Token: 0x04000BE4 RID: 3044
		private long position;

		// Token: 0x04000BE5 RID: 3045
		private TimeSpan startTime;

		// Token: 0x04000BE6 RID: 3046
		private TimeSpan sourceOffset;

		// Token: 0x04000BE7 RID: 3047
		private TimeSpan sourceLength;

		// Token: 0x04000BE8 RID: 3048
		private readonly object lockObject = new object();
	}
}
