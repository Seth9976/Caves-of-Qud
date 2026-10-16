using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;
using NAudio.MediaFoundation;

namespace NAudio.Wave
{
	// Token: 0x020001E4 RID: 484
	public class MediaFoundationReader : WaveStream
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x0001F557 File Offset: 0x0001D757
		public MediaFoundationReader(string file)
			: this(file, new MediaFoundationReader.MediaFoundationReaderSettings())
		{
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001F568 File Offset: 0x0001D768
		public MediaFoundationReader(string file, MediaFoundationReader.MediaFoundationReaderSettings settings)
		{
			MediaFoundationApi.Startup();
			this.settings = settings;
			this.file = file;
			IMFSourceReader imfsourceReader = this.CreateReader(settings);
			this.waveFormat = this.GetCurrentWaveFormat(imfsourceReader);
			imfsourceReader.SetStreamSelection(-3, true);
			this.length = this.GetLength(imfsourceReader);
			if (settings.SingleReaderObject)
			{
				this.pReader = imfsourceReader;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		private WaveFormat GetCurrentWaveFormat(IMFSourceReader reader)
		{
			IMFMediaType imfmediaType;
			reader.GetCurrentMediaType(-3, out imfmediaType);
			MediaType mediaType = new MediaType(imfmediaType);
			Guid majorType = mediaType.MajorType;
			Guid subType = mediaType.SubType;
			int channelCount = mediaType.ChannelCount;
			int bitsPerSample = mediaType.BitsPerSample;
			int sampleRate = mediaType.SampleRate;
			if (!(subType == AudioSubtypes.MFAudioFormat_PCM))
			{
				return WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
			}
			return new WaveFormat(sampleRate, bitsPerSample, channelCount);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001F634 File Offset: 0x0001D834
		protected virtual IMFSourceReader CreateReader(MediaFoundationReader.MediaFoundationReaderSettings settings)
		{
			IMFSourceReader imfsourceReader;
			MediaFoundationInterop.MFCreateSourceReaderFromURL(this.file, null, out imfsourceReader);
			imfsourceReader.SetStreamSelection(-2, false);
			imfsourceReader.SetStreamSelection(-3, true);
			MediaType mediaType = new MediaType();
			mediaType.MajorType = MediaTypes.MFMediaType_Audio;
			mediaType.SubType = (settings.RequestFloatOutput ? AudioSubtypes.MFAudioFormat_Float : AudioSubtypes.MFAudioFormat_PCM);
			imfsourceReader.SetCurrentMediaType(-3, IntPtr.Zero, mediaType.MediaFoundationObject);
			return imfsourceReader;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		private long GetLength(IMFSourceReader reader)
		{
			PropVariant propVariant;
			int presentationAttribute = reader.GetPresentationAttribute(-1, MediaFoundationAttributes.MF_PD_DURATION, out propVariant);
			if (presentationAttribute == -1072875802)
			{
				return 0L;
			}
			if (presentationAttribute != 0)
			{
				Marshal.ThrowExceptionForHR(presentationAttribute);
			}
			long num = (long)propVariant.Value * (long)this.waveFormat.AverageBytesPerSecond / 10000000L;
			propVariant.Clear();
			return num;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001F6F9 File Offset: 0x0001D8F9
		private void EnsureBuffer(int bytesRequired)
		{
			if (this.decoderOutputBuffer == null || this.decoderOutputBuffer.Length < bytesRequired)
			{
				this.decoderOutputBuffer = new byte[bytesRequired];
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001F71C File Offset: 0x0001D91C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.pReader == null)
			{
				this.pReader = this.CreateReader(this.settings);
			}
			if (this.repositionTo != -1L)
			{
				this.Reposition(this.repositionTo);
			}
			int i = 0;
			if (this.decoderOutputCount > 0)
			{
				i += this.ReadFromDecoderBuffer(buffer, offset, count - i);
			}
			while (i < count)
			{
				int num;
				MF_SOURCE_READER_FLAG mf_SOURCE_READER_FLAG;
				ulong num2;
				IMFSample imfsample;
				this.pReader.ReadSample(-3, 0, out num, out mf_SOURCE_READER_FLAG, out num2, out imfsample);
				if ((mf_SOURCE_READER_FLAG & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_ENDOFSTREAM) != MF_SOURCE_READER_FLAG.None)
				{
					break;
				}
				if ((mf_SOURCE_READER_FLAG & MF_SOURCE_READER_FLAG.MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED) != MF_SOURCE_READER_FLAG.None)
				{
					this.waveFormat = this.GetCurrentWaveFormat(this.pReader);
					this.OnWaveFormatChanged();
				}
				else if (mf_SOURCE_READER_FLAG != MF_SOURCE_READER_FLAG.None)
				{
					throw new InvalidOperationException(string.Format("MediaFoundationReadError {0}", mf_SOURCE_READER_FLAG));
				}
				IMFMediaBuffer imfmediaBuffer;
				imfsample.ConvertToContiguousBuffer(out imfmediaBuffer);
				IntPtr intPtr;
				int num3;
				int num4;
				imfmediaBuffer.Lock(out intPtr, out num3, out num4);
				this.EnsureBuffer(num4);
				Marshal.Copy(intPtr, this.decoderOutputBuffer, 0, num4);
				this.decoderOutputOffset = 0;
				this.decoderOutputCount = num4;
				i += this.ReadFromDecoderBuffer(buffer, offset + i, count - i);
				imfmediaBuffer.Unlock();
				Marshal.ReleaseComObject(imfmediaBuffer);
				Marshal.ReleaseComObject(imfsample);
			}
			this.position += (long)i;
			return i;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001F84C File Offset: 0x0001DA4C
		private int ReadFromDecoderBuffer(byte[] buffer, int offset, int needed)
		{
			int num = Math.Min(needed, this.decoderOutputCount);
			Array.Copy(this.decoderOutputBuffer, this.decoderOutputOffset, buffer, offset, num);
			this.decoderOutputOffset += num;
			this.decoderOutputCount -= num;
			if (this.decoderOutputCount == 0)
			{
				this.decoderOutputOffset = 0;
			}
			return num;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001F8A6 File Offset: 0x0001DAA6
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001F8AE File Offset: 0x0001DAAE
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001F8B6 File Offset: 0x0001DAB6
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0001F8BE File Offset: 0x0001DABE
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Position cannot be less than 0");
				}
				if (this.settings.RepositionInRead)
				{
					this.repositionTo = value;
					this.position = value;
					return;
				}
				this.Reposition(value);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		private void Reposition(long desiredPosition)
		{
			long num = 10000000L * this.repositionTo / (long)this.waveFormat.AverageBytesPerSecond;
			PropVariant propVariant = PropVariant.FromLong(num);
			this.pReader.SetCurrentPosition(Guid.Empty, ref propVariant);
			this.decoderOutputCount = 0;
			this.decoderOutputOffset = 0;
			this.position = desiredPosition;
			this.repositionTo = -1L;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001F956 File Offset: 0x0001DB56
		protected override void Dispose(bool disposing)
		{
			if (this.pReader != null)
			{
				Marshal.ReleaseComObject(this.pReader);
				this.pReader = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000AAD RID: 2733 RVA: 0x0001F97C File Offset: 0x0001DB7C
		// (remove) Token: 0x06000AAE RID: 2734 RVA: 0x0001F9B4 File Offset: 0x0001DBB4
		public event EventHandler WaveFormatChanged;

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		private void OnWaveFormatChanged()
		{
			EventHandler waveFormatChanged = this.WaveFormatChanged;
			if (waveFormatChanged != null)
			{
				waveFormatChanged.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000B76 RID: 2934
		private WaveFormat waveFormat;

		// Token: 0x04000B77 RID: 2935
		private readonly long length;

		// Token: 0x04000B78 RID: 2936
		private readonly MediaFoundationReader.MediaFoundationReaderSettings settings;

		// Token: 0x04000B79 RID: 2937
		private readonly string file;

		// Token: 0x04000B7A RID: 2938
		private IMFSourceReader pReader;

		// Token: 0x04000B7B RID: 2939
		private long position;

		// Token: 0x04000B7C RID: 2940
		private byte[] decoderOutputBuffer;

		// Token: 0x04000B7D RID: 2941
		private int decoderOutputOffset;

		// Token: 0x04000B7E RID: 2942
		private int decoderOutputCount;

		// Token: 0x04000B7F RID: 2943
		private long repositionTo = -1L;

		// Token: 0x020001E5 RID: 485
		public class MediaFoundationReaderSettings
		{
			// Token: 0x06000AB0 RID: 2736 RVA: 0x0001FA0F File Offset: 0x0001DC0F
			public MediaFoundationReaderSettings()
			{
				this.RepositionInRead = true;
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001FA1E File Offset: 0x0001DC1E
			// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0001FA26 File Offset: 0x0001DC26
			public bool RequestFloatOutput { get; set; }

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001FA2F File Offset: 0x0001DC2F
			// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0001FA37 File Offset: 0x0001DC37
			public bool SingleReaderObject { get; set; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001FA40 File Offset: 0x0001DC40
			// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0001FA48 File Offset: 0x0001DC48
			public bool RepositionInRead { get; set; }
		}
	}
}
