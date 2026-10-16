using System;
using NAudio.Wave.Compression;

namespace NAudio.Wave
{
	// Token: 0x020000A9 RID: 169
	public class AcmMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000D054 File Offset: 0x0000B254
		public AcmMp3FrameDecompressor(WaveFormat sourceFormat)
		{
			this.pcmFormat = AcmStream.SuggestPcmFormat(sourceFormat);
			try
			{
				this.conversionStream = new AcmStream(sourceFormat, this.pcmFormat);
			}
			catch (Exception)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
				throw;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public WaveFormat OutputFormat
		{
			get
			{
				return this.pcmFormat;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame", "You must provide a non-null Mp3Frame to decompress");
			}
			Array.Copy(frame.RawData, this.conversionStream.SourceBuffer, frame.FrameLength);
			int num = 0;
			int num2 = this.conversionStream.Convert(frame.FrameLength, out num);
			if (num != frame.FrameLength)
			{
				throw new InvalidOperationException(string.Format("Couldn't convert the whole MP3 frame (converted {0}/{1})", num, frame.FrameLength));
			}
			Array.Copy(this.conversionStream.DestBuffer, 0, dest, destOffset, num2);
			return num2;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000D141 File Offset: 0x0000B341
		public void Reset()
		{
			this.conversionStream.Reposition();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D14E File Offset: 0x0000B34E
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.conversionStream != null)
				{
					this.conversionStream.Dispose();
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000D178 File Offset: 0x0000B378
		~AcmMp3FrameDecompressor()
		{
			this.Dispose();
		}

		// Token: 0x04000477 RID: 1143
		private readonly AcmStream conversionStream;

		// Token: 0x04000478 RID: 1144
		private readonly WaveFormat pcmFormat;

		// Token: 0x04000479 RID: 1145
		private bool disposed;
	}
}
