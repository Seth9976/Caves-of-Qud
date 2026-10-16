using System;
using NAudio.Dmo;

namespace NAudio.Wave
{
	// Token: 0x020001ED RID: 493
	public class ResamplerDmoStream : WaveStream
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x00020480 File Offset: 0x0001E680
		public ResamplerDmoStream(IWaveProvider inputProvider, WaveFormat outputFormat)
		{
			this.inputProvider = inputProvider;
			this.inputStream = inputProvider as WaveStream;
			this.outputFormat = outputFormat;
			this.dmoResampler = new DmoResampler();
			if (!this.dmoResampler.MediaObject.SupportsInputWaveFormat(0, inputProvider.WaveFormat))
			{
				throw new ArgumentException("Unsupported Input Stream format", "inputStream");
			}
			this.dmoResampler.MediaObject.SetInputWaveFormat(0, inputProvider.WaveFormat);
			if (!this.dmoResampler.MediaObject.SupportsOutputWaveFormat(0, outputFormat))
			{
				throw new ArgumentException("Unsupported Output Stream format", "outputStream");
			}
			this.dmoResampler.MediaObject.SetOutputWaveFormat(0, outputFormat);
			if (this.inputStream != null)
			{
				this.position = this.InputToOutputPosition(this.inputStream.Position);
			}
			this.inputMediaBuffer = new MediaBuffer(inputProvider.WaveFormat.AverageBytesPerSecond);
			this.outputBuffer = new DmoOutputDataBuffer(outputFormat.AverageBytesPerSecond);
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00020574 File Offset: 0x0001E774
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002057C File Offset: 0x0001E77C
		private long InputToOutputPosition(long inputPosition)
		{
			double num = (double)this.outputFormat.AverageBytesPerSecond / (double)this.inputProvider.WaveFormat.AverageBytesPerSecond;
			long num2 = (long)((double)inputPosition * num);
			if (num2 % (long)this.outputFormat.BlockAlign != 0L)
			{
				num2 -= num2 % (long)this.outputFormat.BlockAlign;
			}
			return num2;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000205D4 File Offset: 0x0001E7D4
		private long OutputToInputPosition(long outputPosition)
		{
			double num = (double)this.outputFormat.AverageBytesPerSecond / (double)this.inputProvider.WaveFormat.AverageBytesPerSecond;
			long num2 = (long)((double)outputPosition / num);
			if (num2 % (long)this.inputProvider.WaveFormat.BlockAlign != 0L)
			{
				num2 -= num2 % (long)this.inputProvider.WaveFormat.BlockAlign;
			}
			return num2;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00020634 File Offset: 0x0001E834
		public override long Length
		{
			get
			{
				if (this.inputStream == null)
				{
					throw new InvalidOperationException("Cannot report length if the input was an IWaveProvider");
				}
				return this.InputToOutputPosition(this.inputStream.Length);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002065A File Offset: 0x0001E85A
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00020664 File Offset: 0x0001E864
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.inputStream == null)
				{
					throw new InvalidOperationException("Cannot set position if the input was not a WaveStream");
				}
				this.inputStream.Position = this.OutputToInputPosition(value);
				this.position = this.InputToOutputPosition(this.inputStream.Position);
				this.dmoResampler.MediaObject.Discontinuity(0);
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000206C0 File Offset: 0x0001E8C0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				if (this.dmoResampler.MediaObject.IsAcceptingData(0))
				{
					int num = (int)this.OutputToInputPosition((long)(count - i));
					byte[] array = new byte[num];
					int num2 = this.inputProvider.Read(array, 0, num);
					if (num2 == 0)
					{
						break;
					}
					this.inputMediaBuffer.LoadData(array, num2);
					this.dmoResampler.MediaObject.ProcessInput(0, this.inputMediaBuffer, DmoInputDataBufferFlags.None, 0L, 0L);
					this.outputBuffer.MediaBuffer.SetLength(0);
					this.outputBuffer.StatusFlags = DmoOutputDataBufferFlags.None;
					this.dmoResampler.MediaObject.ProcessOutput(DmoProcessOutputFlags.None, 1, new DmoOutputDataBuffer[] { this.outputBuffer });
					if (this.outputBuffer.Length == 0)
					{
						break;
					}
					this.outputBuffer.RetrieveData(buffer, offset + i);
					i += this.outputBuffer.Length;
				}
			}
			this.position += (long)i;
			return i;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000207C9 File Offset: 0x0001E9C9
		protected override void Dispose(bool disposing)
		{
			if (this.inputMediaBuffer != null)
			{
				this.inputMediaBuffer.Dispose();
				this.inputMediaBuffer = null;
			}
			this.outputBuffer.Dispose();
			if (this.dmoResampler != null)
			{
				this.dmoResampler = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B9F RID: 2975
		private readonly IWaveProvider inputProvider;

		// Token: 0x04000BA0 RID: 2976
		private readonly WaveStream inputStream;

		// Token: 0x04000BA1 RID: 2977
		private readonly WaveFormat outputFormat;

		// Token: 0x04000BA2 RID: 2978
		private DmoOutputDataBuffer outputBuffer;

		// Token: 0x04000BA3 RID: 2979
		private DmoResampler dmoResampler;

		// Token: 0x04000BA4 RID: 2980
		private MediaBuffer inputMediaBuffer;

		// Token: 0x04000BA5 RID: 2981
		private long position;
	}
}
