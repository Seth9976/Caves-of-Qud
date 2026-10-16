using System;
using System.IO;
using NAudio.Wave;

namespace NVorbis.NAudioSupport
{
	// Token: 0x0200018E RID: 398
	internal class VorbisWaveReader : WaveStream, IDisposable, ISampleProvider, IWaveProvider
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x0005FF11 File Offset: 0x0005E111
		public VorbisWaveReader(string fileName)
		{
			this._reader = new VorbisReader(fileName);
			this._waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(this._reader.SampleRate, this._reader.Channels);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0005FF46 File Offset: 0x0005E146
		public VorbisWaveReader(Stream sourceStream)
		{
			this._reader = new VorbisReader(sourceStream, false);
			this._waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(this._reader.SampleRate, this._reader.Channels);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0005FF7C File Offset: 0x0005E17C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._reader != null)
			{
				this._reader.Dispose();
				this._reader = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0005FFA2 File Offset: 0x0005E1A2
		public override WaveFormat WaveFormat
		{
			get
			{
				return this._waveFormat;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		public override long Length
		{
			get
			{
				return (long)(this._reader.TotalTime.TotalSeconds * (double)this._waveFormat.SampleRate * (double)this._waveFormat.Channels * 4.0);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0005FFF4 File Offset: 0x0005E1F4
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0006003C File Offset: 0x0005E23C
		public override long Position
		{
			get
			{
				return (long)(this._reader.DecodedTime.TotalMilliseconds * (double)this._reader.SampleRate * (double)this._reader.Channels * 4.0);
			}
			set
			{
				if (value < 0L || value > this.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._reader.DecodedTime = TimeSpan.FromSeconds((double)value / (double)this._reader.SampleRate / (double)this._reader.Channels / 4.0);
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00060098 File Offset: 0x0005E298
		public override int Read(byte[] buffer, int offset, int count)
		{
			count /= 4;
			count -= count % this._reader.Channels;
			float[] array;
			if ((array = VorbisWaveReader._conversionBuffer) == null)
			{
				array = (VorbisWaveReader._conversionBuffer = new float[count]);
			}
			float[] array2 = array;
			if (array2.Length < count)
			{
				array2 = (VorbisWaveReader._conversionBuffer = new float[count]);
			}
			int num = this.Read(array2, 0, count) * 4;
			Buffer.BlockCopy(array2, 0, buffer, offset, num);
			return num;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000600FB File Offset: 0x0005E2FB
		public int Read(float[] buffer, int offset, int count)
		{
			return this._reader.ReadSamples(buffer, offset, count);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0006010B File Offset: 0x0005E30B
		public bool IsParameterChange
		{
			get
			{
				return this._reader.IsParameterChange;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00060118 File Offset: 0x0005E318
		public void ClearParameterChange()
		{
			this._reader.ClearParameterChange();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00060125 File Offset: 0x0005E325
		public int StreamCount
		{
			get
			{
				return this._reader.StreamCount;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00060132 File Offset: 0x0005E332
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0006013A File Offset: 0x0005E33A
		public int? NextStreamIndex { get; set; }

		// Token: 0x06000F31 RID: 3889 RVA: 0x00060144 File Offset: 0x0005E344
		public bool GetNextStreamIndex()
		{
			if (this.NextStreamIndex == null)
			{
				int streamCount = this._reader.StreamCount;
				if (this._reader.FindNextStream())
				{
					this.NextStreamIndex = new int?(streamCount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00060189 File Offset: 0x0005E389
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x00060198 File Offset: 0x0005E398
		public int CurrentStream
		{
			get
			{
				return this._reader.StreamIndex;
			}
			set
			{
				if (!this._reader.SwitchStreams(value))
				{
					throw new InvalidDataException("The selected stream is not a valid Vorbis stream!");
				}
				if (this.NextStreamIndex != null && value == this.NextStreamIndex.Value)
				{
					this.NextStreamIndex = null;
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x000601EE File Offset: 0x0005E3EE
		public int UpperBitrate
		{
			get
			{
				return this._reader.UpperBitrate;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000601FB File Offset: 0x0005E3FB
		public int NominalBitrate
		{
			get
			{
				return this._reader.NominalBitrate;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00060208 File Offset: 0x0005E408
		public int LowerBitrate
		{
			get
			{
				return this._reader.LowerBitrate;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00060215 File Offset: 0x0005E415
		public string Vendor
		{
			get
			{
				return this._reader.Vendor;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00060222 File Offset: 0x0005E422
		public string[] Comments
		{
			get
			{
				return this._reader.Comments;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0006022F File Offset: 0x0005E42F
		public long ContainerOverheadBits
		{
			get
			{
				return this._reader.ContainerOverheadBits;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0006023C File Offset: 0x0005E43C
		public IVorbisStreamStatus[] Stats
		{
			get
			{
				return this._reader.Stats;
			}
		}

		// Token: 0x04000A6F RID: 2671
		private VorbisReader _reader;

		// Token: 0x04000A70 RID: 2672
		private WaveFormat _waveFormat;

		// Token: 0x04000A71 RID: 2673
		[ThreadStatic]
		private static float[] _conversionBuffer;
	}
}
