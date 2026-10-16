using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200004F RID: 79
	public static class MediaFoundationApi
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x000070B8 File Offset: 0x000052B8
		public static void Startup()
		{
			if (!MediaFoundationApi.initialized)
			{
				int num = 2;
				OperatingSystem osversion = Environment.OSVersion;
				if (osversion.Version.Major == 6 && osversion.Version.Minor == 0)
				{
					num = 1;
				}
				MediaFoundationInterop.MFStartup((num << 16) | 112, 0);
				MediaFoundationApi.initialized = true;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007310 File Offset: 0x00005510
		public static IEnumerable<IMFActivate> EnumerateTransforms(Guid category)
		{
			IntPtr interfacesPointer;
			int interfaceCount;
			MediaFoundationInterop.MFTEnumEx(category, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, null, out interfacesPointer, out interfaceCount);
			IMFActivate[] interfaces = new IMFActivate[interfaceCount];
			for (int j = 0; j < interfaceCount; j++)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(new IntPtr(interfacesPointer.ToInt64() + (long)(j * Marshal.SizeOf(interfacesPointer))));
				interfaces[j] = (IMFActivate)Marshal.GetObjectForIUnknown(intPtr);
			}
			foreach (IMFActivate i in interfaces)
			{
				yield return i;
			}
			Marshal.FreeCoTaskMem(interfacesPointer);
			yield break;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000732D File Offset: 0x0000552D
		public static void Shutdown()
		{
			if (MediaFoundationApi.initialized)
			{
				MediaFoundationInterop.MFShutdown();
				MediaFoundationApi.initialized = false;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007344 File Offset: 0x00005544
		public static IMFMediaType CreateMediaType()
		{
			IMFMediaType imfmediaType;
			MediaFoundationInterop.MFCreateMediaType(out imfmediaType);
			return imfmediaType;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000735C File Offset: 0x0000555C
		public static IMFMediaType CreateMediaTypeFromWaveFormat(WaveFormat waveFormat)
		{
			IMFMediaType imfmediaType = MediaFoundationApi.CreateMediaType();
			try
			{
				MediaFoundationInterop.MFInitMediaTypeFromWaveFormatEx(imfmediaType, waveFormat, Marshal.SizeOf(waveFormat));
			}
			catch (Exception)
			{
				Marshal.ReleaseComObject(imfmediaType);
				throw;
			}
			return imfmediaType;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000739C File Offset: 0x0000559C
		public static IMFMediaBuffer CreateMemoryBuffer(int bufferSize)
		{
			IMFMediaBuffer imfmediaBuffer;
			MediaFoundationInterop.MFCreateMemoryBuffer(bufferSize, out imfmediaBuffer);
			return imfmediaBuffer;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000073B4 File Offset: 0x000055B4
		public static IMFSample CreateSample()
		{
			IMFSample imfsample;
			MediaFoundationInterop.MFCreateSample(out imfsample);
			return imfsample;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000073CC File Offset: 0x000055CC
		public static IMFAttributes CreateAttributes(int initialSize)
		{
			IMFAttributes imfattributes;
			MediaFoundationInterop.MFCreateAttributes(out imfattributes, initialSize);
			return imfattributes;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000073E4 File Offset: 0x000055E4
		public static IMFByteStream CreateByteStream(object stream)
		{
			IMFByteStream imfbyteStream;
			MediaFoundationInterop.MFCreateMFByteStreamOnStreamEx(stream, out imfbyteStream);
			return imfbyteStream;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000073FC File Offset: 0x000055FC
		public static IMFSourceReader CreateSourceReaderFromByteStream(IMFByteStream byteStream)
		{
			IMFSourceReader imfsourceReader;
			MediaFoundationInterop.MFCreateSourceReaderFromByteStream(byteStream, null, out imfsourceReader);
			return imfsourceReader;
		}

		// Token: 0x040002D0 RID: 720
		private static bool initialized;
	}
}
