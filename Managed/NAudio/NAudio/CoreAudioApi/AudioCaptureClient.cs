using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x0200000A RID: 10
	public class AudioCaptureClient : IDisposable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal AudioCaptureClient(IAudioCaptureClient audioCaptureClientInterface)
		{
			this.audioCaptureClientInterface = audioCaptureClientInterface;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003CFC File Offset: 0x00001EFC
		public IntPtr GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags, out long devicePosition, out long qpcPosition)
		{
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out intPtr, out numFramesToRead, out bufferFlags, out devicePosition, out qpcPosition));
			return intPtr;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003D24 File Offset: 0x00001F24
		public IntPtr GetBuffer(out int numFramesToRead, out AudioClientBufferFlags bufferFlags)
		{
			IntPtr intPtr;
			long num;
			long num2;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetBuffer(out intPtr, out numFramesToRead, out bufferFlags, out num, out num2));
			return intPtr;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003D4C File Offset: 0x00001F4C
		public int GetNextPacketSize()
		{
			int num;
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.GetNextPacketSize(out num));
			return num;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003D6C File Offset: 0x00001F6C
		public void ReleaseBuffer(int numFramesWritten)
		{
			Marshal.ThrowExceptionForHR(this.audioCaptureClientInterface.ReleaseBuffer(numFramesWritten));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003D7F File Offset: 0x00001F7F
		public void Dispose()
		{
			if (this.audioCaptureClientInterface != null)
			{
				Marshal.ReleaseComObject(this.audioCaptureClientInterface);
				this.audioCaptureClientInterface = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000033 RID: 51
		private IAudioCaptureClient audioCaptureClientInterface;
	}
}
