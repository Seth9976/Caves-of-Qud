using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.Dmo
{
	// Token: 0x02000091 RID: 145
	public class MediaObject : IDisposable
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000A36B File Offset: 0x0000856B
		internal MediaObject(IMediaObject mediaObject)
		{
			this.mediaObject = mediaObject;
			mediaObject.GetStreamCount(out this.inputStreams, out this.outputStreams);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000A38D File Offset: 0x0000858D
		public int InputStreamCount
		{
			get
			{
				return this.inputStreams;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000A395 File Offset: 0x00008595
		public int OutputStreamCount
		{
			get
			{
				return this.outputStreams;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public DmoMediaType? GetInputType(int inputStream, int inputTypeIndex)
		{
			try
			{
				DmoMediaType dmoMediaType;
				if (this.mediaObject.GetInputType(inputStream, inputTypeIndex, out dmoMediaType) == 0)
				{
					DmoInterop.MoFreeMediaType(ref dmoMediaType);
					return new DmoMediaType?(dmoMediaType);
				}
			}
			catch (COMException ex)
			{
				if (ex.GetHResult() != -2147220986)
				{
					throw;
				}
			}
			return default(DmoMediaType?);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A404 File Offset: 0x00008604
		public DmoMediaType? GetOutputType(int outputStream, int outputTypeIndex)
		{
			try
			{
				DmoMediaType dmoMediaType;
				if (this.mediaObject.GetOutputType(outputStream, outputTypeIndex, out dmoMediaType) == 0)
				{
					DmoInterop.MoFreeMediaType(ref dmoMediaType);
					return new DmoMediaType?(dmoMediaType);
				}
			}
			catch (COMException ex)
			{
				if (ex.GetHResult() != -2147220986)
				{
					throw;
				}
			}
			return default(DmoMediaType?);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A468 File Offset: 0x00008668
		public DmoMediaType GetOutputCurrentType(int outputStreamIndex)
		{
			DmoMediaType dmoMediaType;
			int outputCurrentType = this.mediaObject.GetOutputCurrentType(outputStreamIndex, out dmoMediaType);
			if (outputCurrentType == 0)
			{
				DmoInterop.MoFreeMediaType(ref dmoMediaType);
				return dmoMediaType;
			}
			if (outputCurrentType == -2147220989)
			{
				throw new InvalidOperationException("Media type was not set.");
			}
			throw Marshal.GetExceptionForHR(outputCurrentType);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A5D4 File Offset: 0x000087D4
		public IEnumerable<DmoMediaType> GetInputTypes(int inputStreamIndex)
		{
			int typeIndex = 0;
			for (;;)
			{
				DmoMediaType? mediaType;
				DmoMediaType? dmoMediaType = (mediaType = this.GetInputType(inputStreamIndex, typeIndex));
				if (dmoMediaType == null)
				{
					break;
				}
				yield return mediaType.Value;
				typeIndex++;
			}
			yield break;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A720 File Offset: 0x00008920
		public IEnumerable<DmoMediaType> GetOutputTypes(int outputStreamIndex)
		{
			int typeIndex = 0;
			for (;;)
			{
				DmoMediaType? mediaType;
				DmoMediaType? dmoMediaType = (mediaType = this.GetOutputType(outputStreamIndex, typeIndex));
				if (dmoMediaType == null)
				{
					break;
				}
				yield return mediaType.Value;
				typeIndex++;
			}
			yield break;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A744 File Offset: 0x00008944
		public bool SupportsInputType(int inputStreamIndex, DmoMediaType mediaType)
		{
			return this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A750 File Offset: 0x00008950
		private bool SetInputType(int inputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
		{
			int num = this.mediaObject.SetInputType(inputStreamIndex, ref mediaType, flags);
			if (num == 0)
			{
				return true;
			}
			if (num == -2147220991)
			{
				throw new ArgumentException("Invalid stream index");
			}
			return false;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A78E File Offset: 0x0000898E
		public void SetInputType(int inputStreamIndex, DmoMediaType mediaType)
		{
			if (!this.SetInputType(inputStreamIndex, mediaType, DmoSetTypeFlags.None))
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public void SetInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType dmoMediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetInputType(inputStreamIndex, dmoMediaType, DmoSetTypeFlags.None);
			DmoInterop.MoFreeMediaType(ref dmoMediaType);
			if (!flag)
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public bool SupportsInputWaveFormat(int inputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType dmoMediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetInputType(inputStreamIndex, dmoMediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
			DmoInterop.MoFreeMediaType(ref dmoMediaType);
			return flag;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A808 File Offset: 0x00008A08
		private DmoMediaType CreateDmoMediaTypeForWaveFormat(WaveFormat waveFormat)
		{
			DmoMediaType dmoMediaType = default(DmoMediaType);
			int num = Marshal.SizeOf(waveFormat);
			DmoInterop.MoInitMediaType(ref dmoMediaType, num);
			dmoMediaType.SetWaveFormat(waveFormat);
			return dmoMediaType;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A836 File Offset: 0x00008A36
		public bool SupportsOutputType(int outputStreamIndex, DmoMediaType mediaType)
		{
			return this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A844 File Offset: 0x00008A44
		public bool SupportsOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType dmoMediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetOutputType(outputStreamIndex, dmoMediaType, DmoSetTypeFlags.DMO_SET_TYPEF_TEST_ONLY);
			DmoInterop.MoFreeMediaType(ref dmoMediaType);
			return flag;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000A86C File Offset: 0x00008A6C
		private bool SetOutputType(int outputStreamIndex, DmoMediaType mediaType, DmoSetTypeFlags flags)
		{
			int num = this.mediaObject.SetOutputType(outputStreamIndex, ref mediaType, flags);
			if (num == -2147220987)
			{
				return false;
			}
			if (num == 0)
			{
				return true;
			}
			throw Marshal.GetExceptionForHR(num);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A89E File Offset: 0x00008A9E
		public void SetOutputType(int outputStreamIndex, DmoMediaType mediaType)
		{
			if (!this.SetOutputType(outputStreamIndex, mediaType, DmoSetTypeFlags.None))
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void SetOutputWaveFormat(int outputStreamIndex, WaveFormat waveFormat)
		{
			DmoMediaType dmoMediaType = this.CreateDmoMediaTypeForWaveFormat(waveFormat);
			bool flag = this.SetOutputType(outputStreamIndex, dmoMediaType, DmoSetTypeFlags.None);
			DmoInterop.MoFreeMediaType(ref dmoMediaType);
			if (!flag)
			{
				throw new ArgumentException("Media Type not supported");
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public MediaObjectSizeInfo GetInputSizeInfo(int inputStreamIndex)
		{
			int num;
			int num2;
			int num3;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetInputSizeInfo(inputStreamIndex, out num, out num2, out num3));
			return new MediaObjectSizeInfo(num, num2, num3);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000A91C File Offset: 0x00008B1C
		public MediaObjectSizeInfo GetOutputSizeInfo(int outputStreamIndex)
		{
			int num;
			int num2;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetOutputSizeInfo(outputStreamIndex, out num, out num2));
			return new MediaObjectSizeInfo(num, 0, num2);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A946 File Offset: 0x00008B46
		public void ProcessInput(int inputStreamIndex, IMediaBuffer mediaBuffer, DmoInputDataBufferFlags flags, long timestamp, long duration)
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.ProcessInput(inputStreamIndex, mediaBuffer, flags, timestamp, duration));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A960 File Offset: 0x00008B60
		public void ProcessOutput(DmoProcessOutputFlags flags, int outputBufferCount, DmoOutputDataBuffer[] outputBuffers)
		{
			int num;
			Marshal.ThrowExceptionForHR(this.mediaObject.ProcessOutput(flags, outputBufferCount, outputBuffers, out num));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000A982 File Offset: 0x00008B82
		public void AllocateStreamingResources()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.AllocateStreamingResources());
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000A994 File Offset: 0x00008B94
		public void FreeStreamingResources()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.FreeStreamingResources());
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public long GetInputMaxLatency(int inputStreamIndex)
		{
			long num;
			Marshal.ThrowExceptionForHR(this.mediaObject.GetInputMaxLatency(inputStreamIndex, out num));
			return num;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A9C9 File Offset: 0x00008BC9
		public void Flush()
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.Flush());
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A9DB File Offset: 0x00008BDB
		public void Discontinuity(int inputStreamIndex)
		{
			Marshal.ThrowExceptionForHR(this.mediaObject.Discontinuity(inputStreamIndex));
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public bool IsAcceptingData(int inputStreamIndex)
		{
			DmoInputStatusFlags dmoInputStatusFlags;
			int inputStatus = this.mediaObject.GetInputStatus(inputStreamIndex, out dmoInputStatusFlags);
			Marshal.ThrowExceptionForHR(inputStatus);
			return (dmoInputStatusFlags & DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA) == DmoInputStatusFlags.DMO_INPUT_STATUSF_ACCEPT_DATA;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000AA18 File Offset: 0x00008C18
		public void Dispose()
		{
			if (this.mediaObject != null)
			{
				Marshal.ReleaseComObject(this.mediaObject);
				this.mediaObject = null;
			}
		}

		// Token: 0x040003FE RID: 1022
		private IMediaObject mediaObject;

		// Token: 0x040003FF RID: 1023
		private int inputStreams;

		// Token: 0x04000400 RID: 1024
		private int outputStreams;
	}
}
