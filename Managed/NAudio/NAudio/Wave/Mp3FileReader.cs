using System;
using System.Collections.Generic;
using System.IO;

namespace NAudio.Wave
{
	// Token: 0x020001E7 RID: 487
	public class Mp3FileReader : WaveStream
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0001FA9D File Offset: 0x0001DC9D
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0001FAA5 File Offset: 0x0001DCA5
		public Mp3WaveFormat Mp3WaveFormat { get; private set; }

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001FAAE File Offset: 0x0001DCAE
		public Mp3FileReader(string mp3FileName)
			: this(File.OpenRead(mp3FileName))
		{
			this.ownInputStream = true;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001FAC3 File Offset: 0x0001DCC3
		public Mp3FileReader(string mp3FileName, Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder)
			: this(File.OpenRead(mp3FileName), frameDecompressorBuilder)
		{
			this.ownInputStream = true;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001FAD9 File Offset: 0x0001DCD9
		public Mp3FileReader(Stream inputStream)
			: this(inputStream, new Mp3FileReader.FrameDecompressorBuilder(Mp3FileReader.CreateAcmFrameDecompressor))
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
		public Mp3FileReader(Stream inputStream, Mp3FileReader.FrameDecompressorBuilder frameDecompressorBuilder)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			try
			{
				this.mp3Stream = inputStream;
				this.id3v2Tag = Id3v2Tag.ReadTag(this.mp3Stream);
				this.dataStartPosition = this.mp3Stream.Position;
				Mp3Frame mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream);
				if (mp3Frame == null)
				{
					throw new InvalidDataException("Invalid MP3 file - no MP3 Frames Detected");
				}
				double num = (double)mp3Frame.BitRate;
				this.xingHeader = XingHeader.LoadXingHeader(mp3Frame);
				if (this.xingHeader != null)
				{
					this.dataStartPosition = this.mp3Stream.Position;
				}
				Mp3Frame mp3Frame2 = Mp3Frame.LoadFromStream(this.mp3Stream);
				if (mp3Frame2 != null && (mp3Frame2.SampleRate != mp3Frame.SampleRate || mp3Frame2.ChannelMode != mp3Frame.ChannelMode))
				{
					this.dataStartPosition = mp3Frame2.FileOffset;
					mp3Frame = mp3Frame2;
				}
				this.mp3DataLength = this.mp3Stream.Length - this.dataStartPosition;
				this.mp3Stream.Position = this.mp3Stream.Length - 128L;
				byte[] array = new byte[128];
				this.mp3Stream.Read(array, 0, 128);
				if (array[0] == 84 && array[1] == 65 && array[2] == 71)
				{
					this.id3v1Tag = array;
					this.mp3DataLength -= 128L;
				}
				this.mp3Stream.Position = this.dataStartPosition;
				this.Mp3WaveFormat = new Mp3WaveFormat(mp3Frame.SampleRate, (mp3Frame.ChannelMode == ChannelMode.Mono) ? 1 : 2, mp3Frame.FrameLength, (int)num);
				this.CreateTableOfContents();
				this.tocIndex = 0;
				num = (double)this.mp3DataLength * 8.0 / this.TotalSeconds();
				this.mp3Stream.Position = this.dataStartPosition;
				this.Mp3WaveFormat = new Mp3WaveFormat(mp3Frame.SampleRate, (mp3Frame.ChannelMode == ChannelMode.Mono) ? 1 : 2, mp3Frame.FrameLength, (int)num);
				this.decompressor = frameDecompressorBuilder(this.Mp3WaveFormat);
				this.waveFormat = this.decompressor.OutputFormat;
				this.bytesPerSample = this.decompressor.OutputFormat.BitsPerSample / 8 * this.decompressor.OutputFormat.Channels;
				this.bytesPerDecodedFrame = 1152 * this.bytesPerSample;
				this.decompressBuffer = new byte[this.bytesPerDecodedFrame * 2];
			}
			catch (Exception)
			{
				if (this.ownInputStream)
				{
					inputStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		public static IMp3FrameDecompressor CreateAcmFrameDecompressor(WaveFormat mp3Format)
		{
			return new AcmMp3FrameDecompressor(mp3Format);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001FD84 File Offset: 0x0001DF84
		private void CreateTableOfContents()
		{
			try
			{
				this.tableOfContents = new List<Mp3Index>((int)(this.mp3DataLength / 400L));
				Mp3Frame mp3Frame;
				do
				{
					Mp3Index mp3Index = new Mp3Index();
					mp3Index.FilePosition = this.mp3Stream.Position;
					mp3Index.SamplePosition = this.totalSamples;
					mp3Frame = this.ReadNextFrame(false);
					if (mp3Frame != null)
					{
						this.ValidateFrameFormat(mp3Frame);
						this.totalSamples += (long)mp3Frame.SampleCount;
						mp3Index.SampleCount = mp3Frame.SampleCount;
						mp3Index.ByteCount = (int)(this.mp3Stream.Position - mp3Index.FilePosition);
						this.tableOfContents.Add(mp3Index);
					}
				}
				while (mp3Frame != null);
			}
			catch (EndOfStreamException)
			{
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001FE40 File Offset: 0x0001E040
		private void ValidateFrameFormat(Mp3Frame frame)
		{
			if (frame.SampleRate != this.Mp3WaveFormat.SampleRate)
			{
				string text = string.Format("Got a frame at sample rate {0}, in an MP3 with sample rate {1}. Mp3FileReader does not support sample rate changes.", frame.SampleRate, this.Mp3WaveFormat.SampleRate);
				throw new InvalidOperationException(text);
			}
			int num = ((frame.ChannelMode == ChannelMode.Mono) ? 1 : 2);
			if (num != this.Mp3WaveFormat.Channels)
			{
				string text2 = string.Format("Got a frame with channel mode {0}, in an MP3 with {1} channels. Mp3FileReader does not support changes to channel count.", frame.ChannelMode, this.Mp3WaveFormat.Channels);
				throw new InvalidOperationException(text2);
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001FED6 File Offset: 0x0001E0D6
		private double TotalSeconds()
		{
			return (double)this.totalSamples / (double)this.Mp3WaveFormat.SampleRate;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0001FEEC File Offset: 0x0001E0EC
		public Id3v2Tag Id3v2Tag
		{
			get
			{
				return this.id3v2Tag;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		public byte[] Id3v1Tag
		{
			get
			{
				return this.id3v1Tag;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public Mp3Frame ReadNextFrame()
		{
			return this.ReadNextFrame(true);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001FF08 File Offset: 0x0001E108
		private Mp3Frame ReadNextFrame(bool readData)
		{
			Mp3Frame mp3Frame = null;
			try
			{
				mp3Frame = Mp3Frame.LoadFromStream(this.mp3Stream, readData);
				if (mp3Frame != null)
				{
					this.tocIndex++;
				}
			}
			catch (EndOfStreamException)
			{
			}
			return mp3Frame;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001FF4C File Offset: 0x0001E14C
		public override long Length
		{
			get
			{
				return this.totalSamples * (long)this.bytesPerSample;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001FF5C File Offset: 0x0001E15C
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001FF64 File Offset: 0x0001E164
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0001FF6C File Offset: 0x0001E16C
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				lock (this.repositionLock)
				{
					value = Math.Max(Math.Min(value, this.Length), 0L);
					long num = value / (long)this.bytesPerSample;
					Mp3Index mp3Index = null;
					for (int i = 0; i < this.tableOfContents.Count; i++)
					{
						if (this.tableOfContents[i].SamplePosition + (long)this.tableOfContents[i].SampleCount > num)
						{
							mp3Index = this.tableOfContents[i];
							this.tocIndex = i;
							break;
						}
					}
					this.decompressBufferOffset = 0;
					this.decompressLeftovers = 0;
					this.repositionedFlag = true;
					if (mp3Index != null)
					{
						this.mp3Stream.Position = mp3Index.FilePosition;
						long num2 = num - mp3Index.SamplePosition;
						if (num2 > 0L)
						{
							this.decompressBufferOffset = (int)num2 * this.bytesPerSample;
						}
					}
					else
					{
						this.mp3Stream.Position = this.mp3DataLength + this.dataStartPosition;
					}
					this.position = value;
				}
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002007C File Offset: 0x0001E27C
		public override int Read(byte[] sampleBuffer, int offset, int numBytes)
		{
			int i = 0;
			lock (this.repositionLock)
			{
				if (this.decompressLeftovers != 0)
				{
					int num = Math.Min(this.decompressLeftovers, numBytes);
					Array.Copy(this.decompressBuffer, this.decompressBufferOffset, sampleBuffer, offset, num);
					this.decompressLeftovers -= num;
					if (this.decompressLeftovers == 0)
					{
						this.decompressBufferOffset = 0;
					}
					else
					{
						this.decompressBufferOffset += num;
					}
					i += num;
					offset += num;
				}
				int num2 = this.tocIndex;
				if (this.repositionedFlag)
				{
					this.decompressor.Reset();
					this.tocIndex = Math.Max(0, this.tocIndex - 3);
					this.mp3Stream.Position = this.tableOfContents[this.tocIndex].FilePosition;
					this.repositionedFlag = false;
				}
				while (i < numBytes)
				{
					Mp3Frame mp3Frame = this.ReadNextFrame();
					if (mp3Frame == null)
					{
						break;
					}
					int num3 = this.decompressor.DecompressFrame(mp3Frame, this.decompressBuffer, 0);
					if (this.tocIndex > num2 && num3 != 0)
					{
						if (this.tocIndex == num2 + 1 && num3 == this.bytesPerDecodedFrame * 2)
						{
							Array.Copy(this.decompressBuffer, this.bytesPerDecodedFrame, this.decompressBuffer, 0, this.bytesPerDecodedFrame);
							num3 = this.bytesPerDecodedFrame;
						}
						int num4 = Math.Min(num3 - this.decompressBufferOffset, numBytes - i);
						Array.Copy(this.decompressBuffer, this.decompressBufferOffset, sampleBuffer, offset, num4);
						if (num4 + this.decompressBufferOffset < num3)
						{
							this.decompressBufferOffset = num4 + this.decompressBufferOffset;
							this.decompressLeftovers = num3 - this.decompressBufferOffset;
						}
						else
						{
							this.decompressBufferOffset = 0;
							this.decompressLeftovers = 0;
						}
						offset += num4;
						i += num4;
					}
				}
			}
			this.position += (long)i;
			return i;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00020274 File Offset: 0x0001E474
		public XingHeader XingHeader
		{
			get
			{
				return this.xingHeader;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002027C File Offset: 0x0001E47C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mp3Stream != null)
				{
					if (this.ownInputStream)
					{
						this.mp3Stream.Dispose();
					}
					this.mp3Stream = null;
				}
				if (this.decompressor != null)
				{
					this.decompressor.Dispose();
					this.decompressor = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B88 RID: 2952
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B89 RID: 2953
		private Stream mp3Stream;

		// Token: 0x04000B8A RID: 2954
		private readonly long mp3DataLength;

		// Token: 0x04000B8B RID: 2955
		private readonly long dataStartPosition;

		// Token: 0x04000B8C RID: 2956
		private readonly Id3v2Tag id3v2Tag;

		// Token: 0x04000B8D RID: 2957
		private readonly XingHeader xingHeader;

		// Token: 0x04000B8E RID: 2958
		private readonly byte[] id3v1Tag;

		// Token: 0x04000B8F RID: 2959
		private readonly bool ownInputStream;

		// Token: 0x04000B90 RID: 2960
		private List<Mp3Index> tableOfContents;

		// Token: 0x04000B91 RID: 2961
		private int tocIndex;

		// Token: 0x04000B92 RID: 2962
		private long totalSamples;

		// Token: 0x04000B93 RID: 2963
		private readonly int bytesPerSample;

		// Token: 0x04000B94 RID: 2964
		private readonly int bytesPerDecodedFrame;

		// Token: 0x04000B95 RID: 2965
		private IMp3FrameDecompressor decompressor;

		// Token: 0x04000B96 RID: 2966
		private readonly byte[] decompressBuffer;

		// Token: 0x04000B97 RID: 2967
		private int decompressBufferOffset;

		// Token: 0x04000B98 RID: 2968
		private int decompressLeftovers;

		// Token: 0x04000B99 RID: 2969
		private bool repositionedFlag;

		// Token: 0x04000B9A RID: 2970
		private long position;

		// Token: 0x04000B9B RID: 2971
		private readonly object repositionLock = new object();

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000AD6 RID: 2774
		public delegate IMp3FrameDecompressor FrameDecompressorBuilder(WaveFormat mp3Format);
	}
}
