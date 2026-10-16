using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	// Token: 0x020001DD RID: 477
	public class AudioFileReader : WaveStream, ISampleProvider
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E674 File Offset: 0x0001C874
		public AudioFileReader(string fileName)
		{
			this.lockObject = new object();
			this.fileName = fileName;
			this.CreateReaderStream(fileName);
			this.sourceBytesPerSample = this.readerStream.WaveFormat.BitsPerSample / 8 * this.readerStream.WaveFormat.Channels;
			this.sampleChannel = new SampleChannel(this.readerStream, false);
			this.destBytesPerSample = 4 * this.sampleChannel.WaveFormat.Channels;
			this.length = this.SourceToDest(this.readerStream.Length);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E70C File Offset: 0x0001C90C
		private void CreateReaderStream(string fileName)
		{
			if (fileName.EndsWith(".wav", 5))
			{
				this.readerStream = new WaveFileReader(fileName);
				if (this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && this.readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					this.readerStream = WaveFormatConversionStream.CreatePcmStream(this.readerStream);
					this.readerStream = new BlockAlignReductionStream(this.readerStream);
					return;
				}
			}
			else
			{
				if (fileName.EndsWith(".mp3", 5))
				{
					this.readerStream = new Mp3FileReader(fileName);
					return;
				}
				if (fileName.EndsWith(".aiff"))
				{
					this.readerStream = new AiffFileReader(fileName);
					return;
				}
				this.readerStream = new MediaFoundationReader(fileName);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0001E7BD File Offset: 0x0001C9BD
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.sampleChannel.WaveFormat;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001E7CA File Offset: 0x0001C9CA
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001E7D2 File Offset: 0x0001C9D2
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		public override long Position
		{
			get
			{
				return this.SourceToDest(this.readerStream.Position);
			}
			set
			{
				lock (this.lockObject)
				{
					this.readerStream.Position = this.DestToSource(value);
				}
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E830 File Offset: 0x0001CA30
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num = count / 4;
			int num2 = this.Read(waveBuffer.FloatBuffer, offset / 4, num);
			return num2 * 4;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public int Read(float[] buffer, int offset, int count)
		{
			int num;
			lock (this.lockObject)
			{
				num = this.sampleChannel.Read(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0001E8AD File Offset: 0x0001CAAD
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

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001E8BB File Offset: 0x0001CABB
		private long SourceToDest(long sourceBytes)
		{
			return (long)this.destBytesPerSample * (sourceBytes / (long)this.sourceBytesPerSample);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E8CE File Offset: 0x0001CACE
		private long DestToSource(long destBytes)
		{
			return (long)this.sourceBytesPerSample * (destBytes / (long)this.destBytesPerSample);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E8E1 File Offset: 0x0001CAE1
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.readerStream.Dispose();
				this.readerStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B5C RID: 2908
		private string fileName;

		// Token: 0x04000B5D RID: 2909
		private WaveStream readerStream;

		// Token: 0x04000B5E RID: 2910
		private readonly SampleChannel sampleChannel;

		// Token: 0x04000B5F RID: 2911
		private readonly int destBytesPerSample;

		// Token: 0x04000B60 RID: 2912
		private readonly int sourceBytesPerSample;

		// Token: 0x04000B61 RID: 2913
		private readonly long length;

		// Token: 0x04000B62 RID: 2914
		private readonly object lockObject;
	}
}
