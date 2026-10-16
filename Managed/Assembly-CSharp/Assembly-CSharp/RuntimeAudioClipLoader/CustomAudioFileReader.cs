using System;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NVorbis.NAudioSupport;
using UnityEngine;

namespace RuntimeAudioClipLoader
{
	// Token: 0x02000190 RID: 400
	internal class CustomAudioFileReader : WaveStream, ISampleProvider
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0006024C File Offset: 0x0005E44C
		public CustomAudioFileReader(Stream stream, AudioFormat format)
		{
			this.lockObject = new object();
			this.CreateReaderStream(stream, format);
			this.sourceBytesPerSample = this.readerStream.WaveFormat.BitsPerSample / 8 * this.readerStream.WaveFormat.Channels;
			this.sampleChannel = new SampleChannel(this.readerStream, false);
			this.destBytesPerSample = 4 * this.sampleChannel.WaveFormat.Channels;
			this.length = this.SourceToDest(this.readerStream.Length);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000602DC File Offset: 0x0005E4DC
		private void CreateReaderStream(Stream stream, AudioFormat format)
		{
			if (format == AudioFormat.wav)
			{
				this.readerStream = new WaveFileReader(stream);
				if (this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					this.readerStream = WaveFormatConversionStream.CreatePcmStream(this.readerStream);
					this.readerStream = new BlockAlignReductionStream(this.readerStream);
					return;
				}
			}
			else
			{
				if (format == AudioFormat.mp3)
				{
					this.readerStream = new Mp3FileReader(stream);
					return;
				}
				if (format == AudioFormat.aiff)
				{
					this.readerStream = new AiffFileReader(stream);
					return;
				}
				if (format == AudioFormat.ogg)
				{
					this.readerStream = new VorbisWaveReader(stream);
					return;
				}
				Debug.LogWarning("Audio format " + format.ToString() + " is not supported");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00060398 File Offset: 0x0005E598
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sampleChannel.WaveFormat;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x000603A5 File Offset: 0x0005E5A5
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x000603AD File Offset: 0x0005E5AD
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x000603C0 File Offset: 0x0005E5C0
		public override long Position
		{
			get
			{
				return this.SourceToDest(this.readerStream.Position);
			}
			set
			{
				object obj = this.lockObject;
				lock (obj)
				{
					this.readerStream.Position = this.DestToSource(value);
				}
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0006040C File Offset: 0x0005E60C
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num = count / 4;
			return this.Read(waveBuffer.FloatBuffer, offset / 4, num) * 4;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00060438 File Offset: 0x0005E638
		public int Read(float[] buffer, int offset, int count)
		{
			object obj = this.lockObject;
			int num;
			lock (obj)
			{
				num = this.sampleChannel.Read(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00060484 File Offset: 0x0005E684
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00060491 File Offset: 0x0005E691
		public float Volume
		{
			get
			{
				return this.sampleChannel.Volume;
			}
			set
			{
				this.sampleChannel.Volume = value;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0006049F File Offset: 0x0005E69F
		private long SourceToDest(long sourceBytes)
		{
			return (long)this.destBytesPerSample * (sourceBytes / (long)this.sourceBytesPerSample);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000604B2 File Offset: 0x0005E6B2
		private long DestToSource(long destBytes)
		{
			return (long)this.sourceBytesPerSample * (destBytes / (long)this.destBytesPerSample);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000604C5 File Offset: 0x0005E6C5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.readerStream != null)
			{
				this.readerStream.Dispose();
				this.readerStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000A79 RID: 2681
		private WaveStream readerStream;

		// Token: 0x04000A7A RID: 2682
		private readonly SampleChannel sampleChannel;

		// Token: 0x04000A7B RID: 2683
		private readonly int destBytesPerSample;

		// Token: 0x04000A7C RID: 2684
		private readonly int sourceBytesPerSample;

		// Token: 0x04000A7D RID: 2685
		private readonly long length;

		// Token: 0x04000A7E RID: 2686
		private readonly object lockObject;
	}
}
