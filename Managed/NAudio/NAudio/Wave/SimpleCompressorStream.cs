using System;
using NAudio.Dsp;

namespace NAudio.Wave
{
	// Token: 0x020001EF RID: 495
	public class SimpleCompressorStream : WaveStream
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x00020870 File Offset: 0x0001EA70
		public SimpleCompressorStream(WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.channels = sourceStream.WaveFormat.Channels;
			this.bytesPerSample = sourceStream.WaveFormat.BitsPerSample / 8;
			this.simpleCompressor = new SimpleCompressor(5.0, 10.0, (double)sourceStream.WaveFormat.SampleRate);
			this.simpleCompressor.Threshold = 16.0;
			this.simpleCompressor.Ratio = 6.0;
			this.simpleCompressor.MakeUpGain = 16.0;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002091E File Offset: 0x0001EB1E
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0002092C File Offset: 0x0001EB2C
		public double MakeUpGain
		{
			get
			{
				return this.simpleCompressor.MakeUpGain;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.MakeUpGain = value;
				}
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002096C File Offset: 0x0001EB6C
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0002097C File Offset: 0x0001EB7C
		public double Threshold
		{
			get
			{
				return this.simpleCompressor.Threshold;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Threshold = value;
				}
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000209BC File Offset: 0x0001EBBC
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000209CC File Offset: 0x0001EBCC
		public double Ratio
		{
			get
			{
				return this.simpleCompressor.Ratio;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Ratio = value;
				}
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00020A0C File Offset: 0x0001EC0C
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00020A1C File Offset: 0x0001EC1C
		public double Attack
		{
			get
			{
				return this.simpleCompressor.Attack;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Attack = value;
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00020A5C File Offset: 0x0001EC5C
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00020A6C File Offset: 0x0001EC6C
		public double Release
		{
			get
			{
				return this.simpleCompressor.Release;
			}
			set
			{
				lock (this.lockObject)
				{
					this.simpleCompressor.Release = value;
				}
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00020AAC File Offset: 0x0001ECAC
		public override bool HasData(int count)
		{
			return this.sourceStream.HasData(count);
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00020ABA File Offset: 0x0001ECBA
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00020AC2 File Offset: 0x0001ECC2
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00020ACB File Offset: 0x0001ECCB
		public override long Length
		{
			get
			{
				return this.sourceStream.Length;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		public override long Position
		{
			get
			{
				return this.sourceStream.Position;
			}
			set
			{
				lock (this.lockObject)
				{
					this.sourceStream.Position = value;
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00020B28 File Offset: 0x0001ED28
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sourceStream.WaveFormat;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00020B38 File Offset: 0x0001ED38
		private void ReadSamples(byte[] buffer, int start, out double left, out double right)
		{
			if (this.bytesPerSample == 4)
			{
				left = (double)BitConverter.ToSingle(buffer, start);
				if (this.channels > 1)
				{
					right = (double)BitConverter.ToSingle(buffer, start + this.bytesPerSample);
					return;
				}
				right = left;
				return;
			}
			else
			{
				if (this.bytesPerSample != 2)
				{
					throw new InvalidOperationException(string.Format("Unsupported bytes per sample: {0}", this.bytesPerSample));
				}
				left = (double)BitConverter.ToInt16(buffer, start) / 32768.0;
				if (this.channels > 1)
				{
					right = (double)BitConverter.ToInt16(buffer, start + this.bytesPerSample) / 32768.0;
					return;
				}
				right = left;
				return;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		private void WriteSamples(byte[] buffer, int start, double left, double right)
		{
			if (this.bytesPerSample == 4)
			{
				Array.Copy(BitConverter.GetBytes((float)left), 0, buffer, start, this.bytesPerSample);
				if (this.channels > 1)
				{
					Array.Copy(BitConverter.GetBytes((float)right), 0, buffer, start + this.bytesPerSample, this.bytesPerSample);
					return;
				}
			}
			else if (this.bytesPerSample == 2)
			{
				Array.Copy(BitConverter.GetBytes((short)(left * 32768.0)), 0, buffer, start, this.bytesPerSample);
				if (this.channels > 1)
				{
					Array.Copy(BitConverter.GetBytes((short)(right * 32768.0)), 0, buffer, start + this.bytesPerSample, this.bytesPerSample);
				}
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00020C8C File Offset: 0x0001EE8C
		public override int Read(byte[] array, int offset, int count)
		{
			int num6;
			lock (this.lockObject)
			{
				if (this.Enabled)
				{
					if (this.sourceBuffer == null || this.sourceBuffer.Length < count)
					{
						this.sourceBuffer = new byte[count];
					}
					int num = this.sourceStream.Read(this.sourceBuffer, 0, count);
					int num2 = num / (this.bytesPerSample * this.channels);
					for (int i = 0; i < num2; i++)
					{
						int num3 = i * this.bytesPerSample * this.channels;
						double num4;
						double num5;
						this.ReadSamples(this.sourceBuffer, num3, out num4, out num5);
						this.simpleCompressor.Process(ref num4, ref num5);
						this.WriteSamples(array, offset + num3, num4, num5);
					}
					num6 = count;
				}
				else
				{
					num6 = this.sourceStream.Read(array, offset, count);
				}
			}
			return num6;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00020D70 File Offset: 0x0001EF70
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00020D96 File Offset: 0x0001EF96
		public override int BlockAlign
		{
			get
			{
				return this.sourceStream.BlockAlign;
			}
		}

		// Token: 0x04000BA9 RID: 2985
		private WaveStream sourceStream;

		// Token: 0x04000BAA RID: 2986
		private readonly SimpleCompressor simpleCompressor;

		// Token: 0x04000BAB RID: 2987
		private byte[] sourceBuffer;

		// Token: 0x04000BAC RID: 2988
		private bool enabled;

		// Token: 0x04000BAD RID: 2989
		private readonly int channels;

		// Token: 0x04000BAE RID: 2990
		private readonly int bytesPerSample;

		// Token: 0x04000BAF RID: 2991
		private readonly object lockObject = new object();
	}
}
