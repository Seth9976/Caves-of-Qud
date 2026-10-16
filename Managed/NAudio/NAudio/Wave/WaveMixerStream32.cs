using System;
using System.Collections.Generic;

namespace NAudio.Wave
{
	// Token: 0x020001F5 RID: 501
	public class WaveMixerStream32 : WaveStream
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00021BBC File Offset: 0x0001FDBC
		public WaveMixerStream32()
		{
			this.AutoStop = true;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
			this.bytesPerSample = 4;
			this.inputStreams = new List<WaveStream>();
			this.inputsLock = new object();
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00021BFC File Offset: 0x0001FDFC
		public WaveMixerStream32(IEnumerable<WaveStream> inputStreams, bool autoStop)
			: this()
		{
			this.AutoStop = autoStop;
			foreach (WaveStream waveStream in inputStreams)
			{
				this.AddInputStream(waveStream);
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00021C54 File Offset: 0x0001FE54
		public void AddInputStream(WaveStream waveStream)
		{
			if (waveStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be IEEE floating point", "waveStream");
			}
			if (waveStream.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Only 32 bit audio currently supported", "waveStream");
			}
			if (this.inputStreams.Count == 0)
			{
				int sampleRate = waveStream.WaveFormat.SampleRate;
				int channels = waveStream.WaveFormat.Channels;
				this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			}
			else if (!waveStream.WaveFormat.Equals(this.waveFormat))
			{
				throw new ArgumentException("All incoming channels must have the same format", "waveStream");
			}
			lock (this.inputsLock)
			{
				this.inputStreams.Add(waveStream);
				this.length = Math.Max(this.length, waveStream.Length);
				waveStream.Position = this.Position;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public void RemoveInputStream(WaveStream waveStream)
		{
			lock (this.inputsLock)
			{
				if (this.inputStreams.Remove(waveStream))
				{
					long num = 0L;
					foreach (WaveStream waveStream2 in this.inputStreams)
					{
						num = Math.Max(num, waveStream2.Length);
					}
					this.length = num;
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		public int InputCount
		{
			get
			{
				return this.inputStreams.Count;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00021DED File Offset: 0x0001FFED
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00021DF5 File Offset: 0x0001FFF5
		public bool AutoStop { get; set; }

		// Token: 0x06000B54 RID: 2900 RVA: 0x00021E00 File Offset: 0x00020000
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.AutoStop && this.position + (long)count > this.length)
			{
				count = (int)(this.length - this.position);
			}
			if (count % this.bytesPerSample != 0)
			{
				throw new ArgumentException("Must read an whole number of samples", "count");
			}
			Array.Clear(buffer, offset, count);
			int num = 0;
			byte[] array = new byte[count];
			lock (this.inputsLock)
			{
				foreach (WaveStream waveStream in this.inputStreams)
				{
					if (waveStream.HasData(count))
					{
						int num2 = waveStream.Read(array, 0, count);
						num = Math.Max(num, num2);
						if (num2 > 0)
						{
							WaveMixerStream32.Sum32BitAudio(buffer, offset, array, num2);
						}
					}
					else
					{
						num = Math.Max(num, count);
						waveStream.Position += (long)count;
					}
				}
			}
			this.position += (long)count;
			return count;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00021F18 File Offset: 0x00020118
		private unsafe static void Sum32BitAudio(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* ptr = &destBuffer[offset], ptr2 = &sourceBuffer[0])
			{
				float* ptr3 = (float*)ptr;
				float* ptr4 = (float*)ptr2;
				int num = bytesRead / 4;
				for (int i = 0; i < num; i++)
				{
					ptr3[i] += ptr4[i];
				}
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00021F6A File Offset: 0x0002016A
		public override int BlockAlign
		{
			get
			{
				return this.waveFormat.BlockAlign;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00021F77 File Offset: 0x00020177
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00021F7F File Offset: 0x0002017F
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00021F88 File Offset: 0x00020188
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				lock (this.inputsLock)
				{
					value = Math.Min(value, this.Length);
					foreach (WaveStream waveStream in this.inputStreams)
					{
						waveStream.Position = Math.Min(value, waveStream.Length);
					}
					this.position = value;
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00022020 File Offset: 0x00020220
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00022028 File Offset: 0x00020228
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.inputsLock)
				{
					foreach (WaveStream waveStream in this.inputStreams)
					{
						waveStream.Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BD7 RID: 3031
		private readonly List<WaveStream> inputStreams;

		// Token: 0x04000BD8 RID: 3032
		private readonly object inputsLock;

		// Token: 0x04000BD9 RID: 3033
		private WaveFormat waveFormat;

		// Token: 0x04000BDA RID: 3034
		private long length;

		// Token: 0x04000BDB RID: 3035
		private long position;

		// Token: 0x04000BDC RID: 3036
		private readonly int bytesPerSample;
	}
}
