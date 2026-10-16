using System;
using NAudio.Wave.Compression;

namespace NAudio.Wave
{
	// Token: 0x020001F3 RID: 499
	public class WaveFormatConversionStream : WaveStream
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x000215C8 File Offset: 0x0001F7C8
		public static WaveStream CreatePcmStream(WaveStream sourceStream)
		{
			if (sourceStream.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				return sourceStream;
			}
			WaveFormat waveFormat = AcmStream.SuggestPcmFormat(sourceStream.WaveFormat);
			if (waveFormat.SampleRate < 8000)
			{
				if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.G723)
				{
					throw new InvalidOperationException("Invalid suggested output format, please explicitly provide a target format");
				}
				waveFormat = new WaveFormat(8000, 16, 1);
			}
			return new WaveFormatConversionStream(waveFormat, sourceStream);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00021634 File Offset: 0x0001F834
		public WaveFormatConversionStream(WaveFormat targetFormat, WaveStream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.targetFormat = targetFormat;
			this.conversionStream = new AcmStream(sourceStream.WaveFormat, targetFormat);
			this.length = this.EstimateSourceToDest((long)((int)sourceStream.Length));
			this.position = 0L;
			this.preferredSourceReadSize = Math.Min(sourceStream.WaveFormat.AverageBytesPerSecond, this.conversionStream.SourceBuffer.Length);
			this.preferredSourceReadSize -= this.preferredSourceReadSize % sourceStream.WaveFormat.BlockAlign;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000216C5 File Offset: 0x0001F8C5
		[Obsolete("can be unreliable, use of this method not encouraged")]
		public int SourceToDest(int source)
		{
			return (int)this.EstimateSourceToDest((long)source);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000216D0 File Offset: 0x0001F8D0
		private long EstimateSourceToDest(long source)
		{
			long num = source * (long)this.targetFormat.AverageBytesPerSecond / (long)this.sourceStream.WaveFormat.AverageBytesPerSecond;
			return num - num % (long)this.targetFormat.BlockAlign;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00021710 File Offset: 0x0001F910
		private long EstimateDestToSource(long dest)
		{
			long num = dest * (long)this.sourceStream.WaveFormat.AverageBytesPerSecond / (long)this.targetFormat.AverageBytesPerSecond;
			num -= num % (long)this.sourceStream.WaveFormat.BlockAlign;
			return (long)((int)num);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00021757 File Offset: 0x0001F957
		[Obsolete("can be unreliable, use of this method not encouraged")]
		public int DestToSource(int dest)
		{
			return (int)this.EstimateDestToSource((long)dest);
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00021762 File Offset: 0x0001F962
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0002176A File Offset: 0x0001F96A
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00021774 File Offset: 0x0001F974
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				value -= value % (long)this.BlockAlign;
				long num = this.EstimateDestToSource(value);
				this.sourceStream.Position = num;
				this.position = this.EstimateSourceToDest(this.sourceStream.Position);
				this.leftoverDestBytes = 0;
				this.leftoverDestOffset = 0;
				this.conversionStream.Reposition();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000217D2 File Offset: 0x0001F9D2
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.targetFormat;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000217DC File Offset: 0x0001F9DC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = 0;
			if (count % this.BlockAlign != 0)
			{
				count -= count % this.BlockAlign;
			}
			while (i < count)
			{
				int num = Math.Min(count - i, this.leftoverDestBytes);
				if (num > 0)
				{
					Array.Copy(this.conversionStream.DestBuffer, this.leftoverDestOffset, buffer, offset + i, num);
					this.leftoverDestOffset += num;
					this.leftoverDestBytes -= num;
					i += num;
				}
				if (i >= count)
				{
					break;
				}
				int num2 = this.leftoverSourceBytes;
				int num3 = this.sourceStream.Read(this.conversionStream.SourceBuffer, 0, this.preferredSourceReadSize);
				if (num3 == 0)
				{
					break;
				}
				int num5;
				int num4 = this.conversionStream.Convert(num3, out num5);
				if (num5 == 0)
				{
					break;
				}
				if (num5 < num3)
				{
					this.sourceStream.Position -= (long)(num3 - num5);
				}
				if (num4 <= 0)
				{
					break;
				}
				int num6 = count - i;
				int num7 = Math.Min(num4, num6);
				if (num7 < num4)
				{
					this.leftoverDestBytes = num4 - num7;
					this.leftoverDestOffset = num7;
				}
				Array.Copy(this.conversionStream.DestBuffer, 0, buffer, i + offset, num7);
				i += num7;
			}
			this.position += (long)i;
			return i;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00021916 File Offset: 0x0001FB16
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.conversionStream != null)
				{
					this.conversionStream.Dispose();
					this.conversionStream = null;
				}
				if (this.sourceStream != null)
				{
					this.sourceStream.Dispose();
					this.sourceStream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000BC7 RID: 3015
		private AcmStream conversionStream;

		// Token: 0x04000BC8 RID: 3016
		private WaveStream sourceStream;

		// Token: 0x04000BC9 RID: 3017
		private WaveFormat targetFormat;

		// Token: 0x04000BCA RID: 3018
		private long length;

		// Token: 0x04000BCB RID: 3019
		private long position;

		// Token: 0x04000BCC RID: 3020
		private int preferredSourceReadSize;

		// Token: 0x04000BCD RID: 3021
		private int leftoverDestBytes;

		// Token: 0x04000BCE RID: 3022
		private int leftoverDestOffset;

		// Token: 0x04000BCF RID: 3023
		private int leftoverSourceBytes;
	}
}
