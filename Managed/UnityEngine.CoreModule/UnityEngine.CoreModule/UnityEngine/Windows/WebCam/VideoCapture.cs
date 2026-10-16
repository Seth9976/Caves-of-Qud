using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x0200032B RID: 811
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StaticAccessor("VideoCaptureBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("PlatformDependent/Win/Webcam/VideoCaptureBindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class VideoCapture : IDisposable
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x0004BAC8 File Offset: 0x00049CC8
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(VideoCapture.CaptureResultType resultType, long hResult)
		{
			return new VideoCapture.VideoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		private static VideoCapture.VideoCaptureResult MakeCaptureResult(long hResult)
		{
			VideoCapture.VideoCaptureResult videoCaptureResult = default(VideoCapture.VideoCaptureResult);
			bool flag = hResult == VideoCapture.HR_SUCCESS;
			VideoCapture.CaptureResultType captureResultType;
			if (flag)
			{
				captureResultType = VideoCapture.CaptureResultType.Success;
			}
			else
			{
				captureResultType = VideoCapture.CaptureResultType.UnknownError;
			}
			videoCaptureResult.resultType = captureResultType;
			videoCaptureResult.hResult = hResult;
			return videoCaptureResult;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0004BB38 File Offset: 0x00049D38
		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = VideoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					VideoCapture.s_SupportedResolutions = VideoCapture.GetSupportedResolutions_Internal();
				}
				return VideoCapture.s_SupportedResolutions;
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0004BB68 File Offset: 0x00049D68
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("GetSupportedResolutions")]
		private static Resolution[] GetSupportedResolutions_Internal()
		{
			Resolution[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				VideoCapture.GetSupportedResolutions_Internal_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Resolution[] array;
				blittableArrayWrapper.Unmarshal<Resolution>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0004BB9C File Offset: 0x00049D9C
		public static IEnumerable<float> GetSupportedFrameRatesForResolution(Resolution resolution)
		{
			return VideoCapture.GetSupportedFrameRatesForResolution_Internal(resolution.width, resolution.height);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0004BBC8 File Offset: 0x00049DC8
		[NativeName("GetSupportedFrameRatesForResolution")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private static float[] GetSupportedFrameRatesForResolution_Internal(int resolutionWidth, int resolutionHeight)
		{
			float[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				VideoCapture.GetSupportedFrameRatesForResolution_Internal_Injected(resolutionWidth, resolutionHeight, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0004BC00 File Offset: 0x00049E00
		public bool IsRecording
		{
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
			[NativeMethod("VideoCaptureBindings::IsRecording", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoCapture.get_IsRecording_Injected(intPtr);
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0004BC24 File Offset: 0x00049E24
		public static void CreateAsync(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0004BC50 File Offset: 0x00049E50
		public static void CreateAsync(VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			VideoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		// Token: 0x06002773 RID: 10099
		[NativeName("Instantiate")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Instantiate_Internal(bool showHolograms, VideoCapture.OnVideoCaptureResourceCreatedCallback onCreatedCallback);

		// Token: 0x06002774 RID: 10100 RVA: 0x0004BC7C File Offset: 0x00049E7C
		[RequiredByNativeCode]
		private static void InvokeOnCreatedVideoCaptureResourceDelegate(VideoCapture.OnVideoCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new VideoCapture(nativePtr));
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0004BCB4 File Offset: 0x00049EB4
		private VideoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0004BCC8 File Offset: 0x00049EC8
		public void StartVideoModeAsync(CameraParameters setupParams, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			bool flag = onVideoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onVideoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			bool flag3 = setupParams.frameRate == 0f;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera frame rate must be set to a supported recording frame rate.");
			}
			this.StartVideoMode_Internal(setupParams, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0004BD44 File Offset: 0x00049F44
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StartVideoMode", HasExplicitThis = true)]
		private void StartVideoMode_Internal(CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback)
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.StartVideoMode_Internal_Injected(intPtr, ref cameraParameters, audioState, onVideoModeStartedCallback);
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0004BD6A File Offset: 0x00049F6A
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStartedDelegate(VideoCapture.OnVideoModeStartedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0004BD7C File Offset: 0x00049F7C
		[NativeMethod("VideoCaptureBindings::StopVideoMode", HasExplicitThis = true)]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		public void StopVideoModeAsync([NotNull] VideoCapture.OnVideoModeStoppedCallback onVideoModeStoppedCallback)
		{
			if (onVideoModeStoppedCallback == null)
			{
				ThrowHelper.ThrowArgumentNullException(onVideoModeStoppedCallback, "onVideoModeStoppedCallback");
			}
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.StopVideoModeAsync_Injected(intPtr, onVideoModeStoppedCallback);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0004BDAE File Offset: 0x00049FAE
		[RequiredByNativeCode]
		private static void InvokeOnVideoModeStoppedDelegate(VideoCapture.OnVideoModeStoppedCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0004BDC0 File Offset: 0x00049FC0
		public void StartRecordingAsync(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback)
		{
			bool flag = onStartedRecordingVideoCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onStartedRecordingVideoCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			string directoryName = Path.GetDirectoryName(filename);
			bool flag3 = !string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName);
			if (flag3)
			{
				throw new ArgumentException("The specified directory does not exist.", "filename");
			}
			FileInfo fileInfo = new FileInfo(filename);
			bool flag4 = fileInfo.Exists && fileInfo.IsReadOnly;
			if (flag4)
			{
				throw new ArgumentException("Cannot write to the file because it is read-only.", "filename");
			}
			this.StartRecordingVideoToDisk_Internal(fileInfo.FullName, onStartedRecordingVideoCallback);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0004BE68 File Offset: 0x0004A068
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StartRecordingVideoToDisk", HasExplicitThis = true)]
		private unsafe void StartRecordingVideoToDisk_Internal(string filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback)
		{
			try
			{
				IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				VideoCapture.StartRecordingVideoToDisk_Internal_Injected(intPtr, ref managedSpanWrapper, onStartedRecordingVideoCallback);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0004BED0 File Offset: 0x0004A0D0
		[RequiredByNativeCode]
		private static void InvokeOnStartedRecordingVideoToDiskDelegate(VideoCapture.OnStartedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0004BEE0 File Offset: 0x0004A0E0
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::StopRecordingVideoToDisk", HasExplicitThis = true)]
		public void StopRecordingAsync([NotNull] VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback)
		{
			if (onStoppedRecordingVideoCallback == null)
			{
				ThrowHelper.ThrowArgumentNullException(onStoppedRecordingVideoCallback, "onStoppedRecordingVideoCallback");
			}
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.StopRecordingAsync_Injected(intPtr, onStoppedRecordingVideoCallback);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0004BF12 File Offset: 0x0004A112
		[RequiredByNativeCode]
		private static void InvokeOnStoppedRecordingVideoToDiskDelegate(VideoCapture.OnStoppedRecordingVideoCallback callback, long hResult)
		{
			callback(VideoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0004BF24 File Offset: 0x0004A124
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::GetUnsafePointerToVideoDeviceController", HasExplicitThis = true)]
		public IntPtr GetUnsafePointerToVideoDeviceController()
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoCapture.GetUnsafePointerToVideoDeviceController_Injected(intPtr);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0004BF48 File Offset: 0x0004A148
		public void Dispose()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0004BF88 File Offset: 0x0004A188
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::Dispose", HasExplicitThis = true)]
		private void Dispose_Internal()
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0004BFAC File Offset: 0x0004A1AC
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_NativePtr != IntPtr.Zero;
				if (flag)
				{
					this.DisposeThreaded_Internal();
					this.m_NativePtr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0004C000 File Offset: 0x0004A200
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeMethod("VideoCaptureBindings::DisposeThreaded", HasExplicitThis = true)]
		[ThreadAndSerializationSafe]
		private void DisposeThreaded_Internal()
		{
			IntPtr intPtr = VideoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoCapture.DisposeThreaded_Internal_Injected(intPtr);
		}

		// Token: 0x06002785 RID: 10117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSupportedResolutions_Internal_Injected(out BlittableArrayWrapper ret);

		// Token: 0x06002786 RID: 10118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSupportedFrameRatesForResolution_Internal_Injected(int resolutionWidth, int resolutionHeight, out BlittableArrayWrapper ret);

		// Token: 0x06002787 RID: 10119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_IsRecording_Injected(IntPtr _unity_self);

		// Token: 0x06002788 RID: 10120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartVideoMode_Internal_Injected(IntPtr _unity_self, [In] ref CameraParameters cameraParameters, VideoCapture.AudioState audioState, VideoCapture.OnVideoModeStartedCallback onVideoModeStartedCallback);

		// Token: 0x06002789 RID: 10121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopVideoModeAsync_Injected(IntPtr _unity_self, VideoCapture.OnVideoModeStoppedCallback onVideoModeStoppedCallback);

		// Token: 0x0600278A RID: 10122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartRecordingVideoToDisk_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper filename, VideoCapture.OnStartedRecordingVideoCallback onStartedRecordingVideoCallback);

		// Token: 0x0600278B RID: 10123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopRecordingAsync_Injected(IntPtr _unity_self, VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback);

		// Token: 0x0600278C RID: 10124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnsafePointerToVideoDeviceController_Injected(IntPtr _unity_self);

		// Token: 0x0600278D RID: 10125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0600278E RID: 10126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeThreaded_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000B07 RID: 2823
		internal IntPtr m_NativePtr;

		// Token: 0x04000B08 RID: 2824
		private static Resolution[] s_SupportedResolutions;

		// Token: 0x04000B09 RID: 2825
		private static readonly long HR_SUCCESS;

		// Token: 0x0200032C RID: 812
		public enum CaptureResultType
		{
			// Token: 0x04000B0B RID: 2827
			Success,
			// Token: 0x04000B0C RID: 2828
			UnknownError
		}

		// Token: 0x0200032D RID: 813
		public enum AudioState
		{
			// Token: 0x04000B0E RID: 2830
			MicAudio,
			// Token: 0x04000B0F RID: 2831
			ApplicationAudio,
			// Token: 0x04000B10 RID: 2832
			ApplicationAndMicAudio,
			// Token: 0x04000B11 RID: 2833
			None
		}

		// Token: 0x0200032E RID: 814
		public struct VideoCaptureResult
		{
			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x0600278F RID: 10127 RVA: 0x0004C024 File Offset: 0x0004A224
			public bool success
			{
				get
				{
					return this.resultType == VideoCapture.CaptureResultType.Success;
				}
			}

			// Token: 0x04000B12 RID: 2834
			public VideoCapture.CaptureResultType resultType;

			// Token: 0x04000B13 RID: 2835
			public long hResult;
		}

		// Token: 0x0200032F RID: 815
		// (Invoke) Token: 0x06002791 RID: 10129
		public delegate void OnVideoCaptureResourceCreatedCallback(VideoCapture captureObject);

		// Token: 0x02000330 RID: 816
		// (Invoke) Token: 0x06002795 RID: 10133
		public delegate void OnVideoModeStartedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000331 RID: 817
		// (Invoke) Token: 0x06002799 RID: 10137
		public delegate void OnVideoModeStoppedCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000332 RID: 818
		// (Invoke) Token: 0x0600279D RID: 10141
		public delegate void OnStartedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000333 RID: 819
		// (Invoke) Token: 0x060027A1 RID: 10145
		public delegate void OnStoppedRecordingVideoCallback(VideoCapture.VideoCaptureResult result);

		// Token: 0x02000334 RID: 820
		internal static class BindingsMarshaller
		{
			// Token: 0x060027A4 RID: 10148 RVA: 0x0004C03F File Offset: 0x0004A23F
			public static IntPtr ConvertToNative(VideoCapture videoCapture)
			{
				return videoCapture.m_NativePtr;
			}
		}
	}
}
