using System;
using NAudio.Dmo;
using NAudio.Wave;

namespace NAudio.FileFormats.Mp3
{
	// Token: 0x020000A6 RID: 166
	public class DmoMp3FrameDecompressor : IMp3FrameDecompressor, IDisposable
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000C400 File Offset: 0x0000A600
		public DmoMp3FrameDecompressor(WaveFormat sourceFormat)
		{
			this.mp3Decoder = new WindowsMediaMp3Decoder();
			if (!this.mp3Decoder.MediaObject.SupportsInputWaveFormat(0, sourceFormat))
			{
				throw new ArgumentException("Unsupported input format");
			}
			this.mp3Decoder.MediaObject.SetInputWaveFormat(0, sourceFormat);
			this.pcmFormat = new WaveFormat(sourceFormat.SampleRate, sourceFormat.Channels);
			if (!this.mp3Decoder.MediaObject.SupportsOutputWaveFormat(0, this.pcmFormat))
			{
				throw new ArgumentException(string.Format("Unsupported output format {0}", this.pcmFormat));
			}
			this.mp3Decoder.MediaObject.SetOutputWaveFormat(0, this.pcmFormat);
			this.inputMediaBuffer = new MediaBuffer(sourceFormat.AverageBytesPerSecond);
			this.outputBuffer = new DmoOutputDataBuffer(this.pcmFormat.AverageBytesPerSecond);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C4D3 File Offset: 0x0000A6D3
		public WaveFormat OutputFormat
		{
			get
			{
				return this.pcmFormat;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
		{
			this.inputMediaBuffer.LoadData(frame.RawData, frame.FrameLength);
			if (this.reposition)
			{
				this.mp3Decoder.MediaObject.Flush();
				this.reposition = false;
			}
			this.mp3Decoder.MediaObject.ProcessInput(0, this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
			this.outputBuffer.MediaBuffer.SetLength(0);
			this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
			this.mp3Decoder.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[] { this.outputBuffer });
			if (this.outputBuffer.Length == 0)
			{
				return 0;
			}
			this.outputBuffer.RetrieveData(dest, destOffset);
			return this.outputBuffer.Length;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public void Reset()
		{
			this.reposition = true;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		public void Dispose()
		{
			if (this.inputMediaBuffer != null)
			{
				this.inputMediaBuffer.Dispose();
				this.inputMediaBuffer = null;
			}
			this.outputBuffer.Dispose();
			if (this.mp3Decoder != null)
			{
				this.mp3Decoder.Dispose();
				this.mp3Decoder = null;
			}
		}

		// Token: 0x0400045C RID: 1116
		private WindowsMediaMp3Decoder mp3Decoder;

		// Token: 0x0400045D RID: 1117
		private WaveFormat pcmFormat;

		// Token: 0x0400045E RID: 1118
		private MediaBuffer inputMediaBuffer;

		// Token: 0x0400045F RID: 1119
		private DmoOutputDataBuffer outputBuffer;

		// Token: 0x04000460 RID: 1120
		private bool reposition;
	}
}
