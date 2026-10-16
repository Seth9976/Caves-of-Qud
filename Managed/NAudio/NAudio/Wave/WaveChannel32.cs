using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	// Token: 0x020001F1 RID: 497
	public class WaveChannel32 : WaveStream, ISampleNotifier
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x00021020 File Offset: 0x0001F220
		public WaveChannel32(WaveStream sourceStream, float volume, float pan)
		{
			this.PadWithZeroes = true;
			ISampleChunkConverter[] array = new ISampleChunkConverter[]
			{
				new Mono8SampleChunkConverter(),
				new Stereo8SampleChunkConverter(),
				new Mono16SampleChunkConverter(),
				new Stereo16SampleChunkConverter(),
				new Mono24SampleChunkConverter(),
				new Stereo24SampleChunkConverter(),
				new MonoFloatSampleChunkConverter(),
				new StereoFloatSampleChunkConverter()
			};
			foreach (ISampleChunkConverter sampleChunkConverter in array)
			{
				if (sampleChunkConverter.Supports(sourceStream.WaveFormat))
				{
					this.sampleProvider = sampleChunkConverter;
					break;
				}
			}
			if (this.sampleProvider == null)
			{
				throw new ArgumentException("Unsupported sourceStream format");
			}
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceStream.WaveFormat.SampleRate, 2);
			this.destBytesPerSample = 8;
			this.sourceStream = sourceStream;
			this.volume = volume;
			this.pan = pan;
			this.sourceBytesPerSample = sourceStream.WaveFormat.Channels * sourceStream.WaveFormat.BitsPerSample / 8;
			this.length = this.SourceToDest(sourceStream.Length);
			this.position = 0L;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00021156 File Offset: 0x0001F356
		private long SourceToDest(long sourceBytes)
		{
			return sourceBytes / (long)this.sourceBytesPerSample * (long)this.destBytesPerSample;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00021169 File Offset: 0x0001F369
		private long DestToSource(long destBytes)
		{
			return destBytes / (long)this.destBytesPerSample * (long)this.sourceBytesPerSample;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002117C File Offset: 0x0001F37C
		public WaveChannel32(WaveStream sourceStream)
			: this(sourceStream, 1f, 0f)
		{
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002118F File Offset: 0x0001F38F
		public override int BlockAlign
		{
			get
			{
				return (int)this.SourceToDest((long)this.sourceStream.BlockAlign);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x000211A4 File Offset: 0x0001F3A4
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x000211AC File Offset: 0x0001F3AC
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x000211B4 File Offset: 0x0001F3B4
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
					if (value < 0L)
					{
						this.sourceStream.Position = 0L;
					}
					else
					{
						this.sourceStream.Position = this.DestToSource(value);
					}
					this.position = this.SourceToDest(this.sourceStream.Position);
				}
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00021234 File Offset: 0x0001F434
		public override int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num5;
			lock (this.lockObject)
			{
				int num = 0;
				WaveBuffer waveBuffer = new WaveBuffer(destBuffer);
				if (this.position < 0L)
				{
					num = (int)Math.Min((long)numBytes, -this.position);
					for (int i = 0; i < num; i++)
					{
						destBuffer[i + offset] = 0;
					}
				}
				if (num < numBytes)
				{
					this.sampleProvider.LoadNextChunk(this.sourceStream, (numBytes - num) / 8);
					int num2 = offset / 4 + num / 4;
					float num3;
					float num4;
					while (this.sampleProvider.GetNextSample(out num3, out num4) && num < numBytes)
					{
						num3 = ((this.pan <= 0f) ? num3 : (num3 * (1f - this.pan) / 2f));
						num4 = ((this.pan >= 0f) ? num4 : (num4 * (this.pan + 1f) / 2f));
						num3 *= this.volume;
						num4 *= this.volume;
						waveBuffer.FloatBuffer[num2++] = num3;
						waveBuffer.FloatBuffer[num2++] = num4;
						num += 8;
						if (this.Sample != null)
						{
							this.RaiseSample(num3, num4);
						}
					}
				}
				if (this.PadWithZeroes && num < numBytes)
				{
					Array.Clear(destBuffer, offset + num, numBytes - num);
					num = numBytes;
				}
				this.position += (long)num;
				num5 = num;
			}
			return num5;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000213BC File Offset: 0x0001F5BC
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x000213C4 File Offset: 0x0001F5C4
		public bool PadWithZeroes { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000213CD File Offset: 0x0001F5CD
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000213D5 File Offset: 0x0001F5D5
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x000213DF File Offset: 0x0001F5DF
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x000213EA File Offset: 0x0001F5EA
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x000213F4 File Offset: 0x0001F5F4
		public float Pan
		{
			get
			{
				return this.pan;
			}
			set
			{
				this.pan = value;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00021400 File Offset: 0x0001F600
		public override bool HasData(int count)
		{
			bool flag = this.sourceStream.HasData(count);
			return flag && this.position + (long)count >= 0L && this.position < this.length && this.volume != 0f;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00021450 File Offset: 0x0001F650
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000B2C RID: 2860 RVA: 0x00021478 File Offset: 0x0001F678
		// (remove) Token: 0x06000B2D RID: 2861 RVA: 0x000214B0 File Offset: 0x0001F6B0
		public event EventHandler<SampleEventArgs> Sample;

		// Token: 0x06000B2E RID: 2862 RVA: 0x000214E5 File Offset: 0x0001F6E5
		private void RaiseSample(float left, float right)
		{
			this.sampleEventArgs.Left = left;
			this.sampleEventArgs.Right = right;
			this.Sample.Invoke(this, this.sampleEventArgs);
		}

		// Token: 0x04000BB7 RID: 2999
		private WaveStream sourceStream;

		// Token: 0x04000BB8 RID: 3000
		private readonly WaveFormat waveFormat;

		// Token: 0x04000BB9 RID: 3001
		private readonly long length;

		// Token: 0x04000BBA RID: 3002
		private readonly int destBytesPerSample;

		// Token: 0x04000BBB RID: 3003
		private readonly int sourceBytesPerSample;

		// Token: 0x04000BBC RID: 3004
		private volatile float volume;

		// Token: 0x04000BBD RID: 3005
		private volatile float pan;

		// Token: 0x04000BBE RID: 3006
		private long position;

		// Token: 0x04000BBF RID: 3007
		private readonly ISampleChunkConverter sampleProvider;

		// Token: 0x04000BC0 RID: 3008
		private readonly object lockObject = new object();

		// Token: 0x04000BC2 RID: 3010
		private SampleEventArgs sampleEventArgs = new SampleEventArgs(0f, 0f);
	}
}
