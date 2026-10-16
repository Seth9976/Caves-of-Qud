using System;

namespace NAudio.Wave
{
	// Token: 0x020001F0 RID: 496
	public class Wave32To16Stream : WaveStream
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public Wave32To16Stream(WaveStream sourceStream)
		{
			if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Only 32 bit Floating point supported");
			}
			if (sourceStream.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Only 32 bit Floating point supported");
			}
			this.waveFormat = new WaveFormat(sourceStream.WaveFormat.SampleRate, 16, sourceStream.WaveFormat.Channels);
			this.volume = 1f;
			this.sourceStream = sourceStream;
			this.length = sourceStream.Length / 2L;
			this.position = sourceStream.Position / 2L;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00020E48 File Offset: 0x0001F048
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00020E50 File Offset: 0x0001F050
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00020E59 File Offset: 0x0001F059
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign / 2;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00020E68 File Offset: 0x0001F068
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00020E70 File Offset: 0x0001F070
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00020E78 File Offset: 0x0001F078
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
					this.sourceStream.Position = value * 2L;
					this.position = value;
				}
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		public override int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num2;
			lock (this.lockObject)
			{
				byte[] array = new byte[numBytes * 2];
				int num = this.sourceStream.Read(array, 0, numBytes * 2);
				this.Convert32To16(destBuffer, offset, array, num);
				this.position += (long)(num / 2);
				num2 = num / 2;
			}
			return num2;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00020F40 File Offset: 0x0001F140
		private unsafe void Convert32To16(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
			{
				short* ptr3 = (short*)ptr;
				float* ptr4 = (float*)ptr2;
				int num = bytesRead / 4;
				for (int i = 0; i < num; i++)
				{
					float num2 = ptr4[i] * this.volume;
					if (num2 > 1f)
					{
						ptr3[i] = short.MaxValue;
						this.clip = true;
					}
					else if (num2 < -1f)
					{
						ptr3[i] = short.MinValue;
						this.clip = true;
					}
					else
					{
						ptr3[i] = (short)(num2 * 32767f);
					}
				}
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00020FE8 File Offset: 0x0001F1E8
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		public bool Clip
		{
			get
			{
				return this.clip;
			}
			set
			{
				this.clip = value;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00020FF9 File Offset: 0x0001F1F9
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BB0 RID: 2992
		private WaveStream sourceStream;

		// Token: 0x04000BB1 RID: 2993
		private readonly WaveFormat waveFormat;

		// Token: 0x04000BB2 RID: 2994
		private readonly long length;

		// Token: 0x04000BB3 RID: 2995
		private long position;

		// Token: 0x04000BB4 RID: 2996
		private bool clip;

		// Token: 0x04000BB5 RID: 2997
		private float volume;

		// Token: 0x04000BB6 RID: 2998
		private readonly object lockObject = new object();
	}
}
