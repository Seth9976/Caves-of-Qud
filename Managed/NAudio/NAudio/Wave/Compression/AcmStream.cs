using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016B RID: 363
	public class AcmStream : IDisposable
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00015948 File Offset: 0x00013B48
		public AcmStream(WaveFormat sourceFormat, WaveFormat destFormat)
		{
			try
			{
				this.streamHandle = IntPtr.Zero;
				this.sourceFormat = sourceFormat;
				int num = Math.Max(65536, sourceFormat.AverageBytesPerSecond);
				num -= num % sourceFormat.BlockAlign;
				MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, IntPtr.Zero, sourceFormat, destFormat, null, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
				int num2 = this.SourceToDest(num);
				this.streamHeader = new AcmStreamHeader(this.streamHandle, num, num2);
				this.driverHandle = IntPtr.Zero;
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000159F4 File Offset: 0x00013BF4
		public AcmStream(IntPtr driverId, WaveFormat sourceFormat, WaveFilter waveFilter)
		{
			int num = Math.Max(16384, sourceFormat.AverageBytesPerSecond);
			this.sourceFormat = sourceFormat;
			num -= num % sourceFormat.BlockAlign;
			MmException.Try(AcmInterop.acmDriverOpen(out this.driverHandle, driverId, 0), "acmDriverOpen");
			MmException.Try(AcmInterop.acmStreamOpen(out this.streamHandle, this.driverHandle, sourceFormat, sourceFormat, waveFilter, IntPtr.Zero, IntPtr.Zero, AcmStreamOpenFlags.NonRealTime), "acmStreamOpen");
			this.streamHeader = new AcmStreamHeader(this.streamHandle, num, this.SourceToDest(num));
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00015A84 File Offset: 0x00013C84
		public int SourceToDest(int source)
		{
			if (source == 0)
			{
				return 0;
			}
			int num;
			MmResult mmResult = AcmInterop.acmStreamSize(this.streamHandle, source, out num, AcmStreamSizeFlags.Source);
			MmException.Try(mmResult, "acmStreamSize");
			return num;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00015AB4 File Offset: 0x00013CB4
		public int DestToSource(int dest)
		{
			if (dest == 0)
			{
				return 0;
			}
			int num;
			MmException.Try(AcmInterop.acmStreamSize(this.streamHandle, dest, out num, AcmStreamSizeFlags.Destination), "acmStreamSize");
			return num;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public static WaveFormat SuggestPcmFormat(WaveFormat compressedFormat)
		{
			WaveFormat waveFormat = new WaveFormat(compressedFormat.SampleRate, 16, compressedFormat.Channels);
			MmException.Try(AcmInterop.acmFormatSuggest(IntPtr.Zero, compressedFormat, waveFormat, Marshal.SizeOf(waveFormat), AcmFormatSuggestFlags.FormatTag), "acmFormatSuggest");
			return waveFormat;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00015B23 File Offset: 0x00013D23
		public byte[] SourceBuffer
		{
			get
			{
				return this.streamHeader.SourceBuffer;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00015B30 File Offset: 0x00013D30
		public byte[] DestBuffer
		{
			get
			{
				return this.streamHeader.DestBuffer;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00015B3D File Offset: 0x00013D3D
		public void Reposition()
		{
			this.streamHeader.Reposition();
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00015B4A File Offset: 0x00013D4A
		public int Convert(int bytesToConvert, out int sourceBytesConverted)
		{
			if (bytesToConvert % this.sourceFormat.BlockAlign != 0)
			{
				bytesToConvert -= bytesToConvert % this.sourceFormat.BlockAlign;
			}
			return this.streamHeader.Convert(bytesToConvert, out sourceBytesConverted);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00015B7C File Offset: 0x00013D7C
		[Obsolete("Call the version returning sourceBytesConverted instead")]
		public int Convert(int bytesToConvert)
		{
			int num2;
			int num = this.Convert(bytesToConvert, out num2);
			if (num2 != bytesToConvert)
			{
				throw new MmException(MmResult.NotSupported, "AcmStreamHeader.Convert didn't convert everything");
			}
			return num;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00015BA4 File Offset: 0x00013DA4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00015BB4 File Offset: 0x00013DB4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.streamHeader != null)
			{
				this.streamHeader.Dispose();
				this.streamHeader = null;
			}
			if (this.streamHandle != IntPtr.Zero)
			{
				MmResult mmResult = AcmInterop.acmStreamClose(this.streamHandle, 0);
				this.streamHandle = IntPtr.Zero;
				if (mmResult != MmResult.NoError)
				{
					throw new MmException(mmResult, "acmStreamClose");
				}
			}
			if (this.driverHandle != IntPtr.Zero)
			{
				AcmInterop.acmDriverClose(this.driverHandle, 0);
				this.driverHandle = IntPtr.Zero;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00015C44 File Offset: 0x00013E44
		~AcmStream()
		{
			this.Dispose(false);
		}

		// Token: 0x04000804 RID: 2052
		private IntPtr streamHandle;

		// Token: 0x04000805 RID: 2053
		private IntPtr driverHandle;

		// Token: 0x04000806 RID: 2054
		private AcmStreamHeader streamHeader;

		// Token: 0x04000807 RID: 2055
		private WaveFormat sourceFormat;
	}
}
