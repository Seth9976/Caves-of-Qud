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
	// Token: 0x02000320 RID: 800
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCapture.h")]
	[StaticAccessor("PhotoCapture", StaticAccessorType.DoubleColon)]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[StructLayout(LayoutKind.Sequential)]
	public class PhotoCapture : IDisposable
	{
		// Token: 0x06002711 RID: 10001 RVA: 0x0004B160 File Offset: 0x00049360
		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(PhotoCapture.CaptureResultType resultType, long hResult)
		{
			return new PhotoCapture.PhotoCaptureResult
			{
				resultType = resultType,
				hResult = hResult
			};
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0004B18C File Offset: 0x0004938C
		private static PhotoCapture.PhotoCaptureResult MakeCaptureResult(long hResult)
		{
			PhotoCapture.PhotoCaptureResult photoCaptureResult = default(PhotoCapture.PhotoCaptureResult);
			bool flag = hResult == PhotoCapture.HR_SUCCESS;
			PhotoCapture.CaptureResultType captureResultType;
			if (flag)
			{
				captureResultType = PhotoCapture.CaptureResultType.Success;
			}
			else
			{
				captureResultType = PhotoCapture.CaptureResultType.UnknownError;
			}
			photoCaptureResult.resultType = captureResultType;
			photoCaptureResult.hResult = hResult;
			return photoCaptureResult;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x0004B1D0 File Offset: 0x000493D0
		public static IEnumerable<Resolution> SupportedResolutions
		{
			get
			{
				bool flag = PhotoCapture.s_SupportedResolutions == null;
				if (flag)
				{
					PhotoCapture.s_SupportedResolutions = PhotoCapture.GetSupportedResolutions_Internal();
				}
				return PhotoCapture.s_SupportedResolutions;
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0004B200 File Offset: 0x00049400
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("GetSupportedResolutions")]
		private static Resolution[] GetSupportedResolutions_Internal()
		{
			Resolution[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PhotoCapture.GetSupportedResolutions_Internal_Injected(out blittableArrayWrapper);
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

		// Token: 0x06002715 RID: 10005 RVA: 0x0004B234 File Offset: 0x00049434
		public static void CreateAsync(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(showHolograms, onCreatedCallback);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0004B260 File Offset: 0x00049460
		public static void CreateAsync(PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback)
		{
			bool flag = onCreatedCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCreatedCallback");
			}
			PhotoCapture.Instantiate_Internal(false, onCreatedCallback);
		}

		// Token: 0x06002717 RID: 10007
		[NativeName("Instantiate")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Instantiate_Internal(bool showHolograms, PhotoCapture.OnCaptureResourceCreatedCallback onCreatedCallback);

		// Token: 0x06002718 RID: 10008 RVA: 0x0004B28C File Offset: 0x0004948C
		[RequiredByNativeCode]
		private static void InvokeOnCreatedResourceDelegate(PhotoCapture.OnCaptureResourceCreatedCallback callback, IntPtr nativePtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				callback(null);
			}
			else
			{
				callback(new PhotoCapture(nativePtr));
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0004B2C4 File Offset: 0x000494C4
		private PhotoCapture(IntPtr nativeCaptureObject)
		{
			this.m_NativePtr = nativeCaptureObject;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0004B2D8 File Offset: 0x000494D8
		public void StartPhotoModeAsync(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			bool flag = onPhotoModeStartedCallback == null;
			if (flag)
			{
				throw new ArgumentException("onPhotoModeStartedCallback");
			}
			bool flag2 = setupParams.cameraResolutionWidth == 0 || setupParams.cameraResolutionHeight == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("setupParams", "The camera resolution must be set to a supported resolution.");
			}
			this.StartPhotoMode_Internal(setupParams, onPhotoModeStartedCallback);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0004B330 File Offset: 0x00049530
		[NativeName("StartPhotoMode")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void StartPhotoMode_Internal(CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback)
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.StartPhotoMode_Internal_Injected(intPtr, ref setupParams, onPhotoModeStartedCallback);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0004B355 File Offset: 0x00049555
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStartedDelegate(PhotoCapture.OnPhotoModeStartedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0004B368 File Offset: 0x00049568
		[NativeName("StopPhotoMode")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		public void StopPhotoModeAsync(PhotoCapture.OnPhotoModeStoppedCallback onPhotoModeStoppedCallback)
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.StopPhotoModeAsync_Injected(intPtr, onPhotoModeStoppedCallback);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0004B38B File Offset: 0x0004958B
		[RequiredByNativeCode]
		private static void InvokeOnPhotoModeStoppedDelegate(PhotoCapture.OnPhotoModeStoppedCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0004B39C File Offset: 0x0004959C
		public void TakePhotoAsync(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback)
		{
			bool flag = onCapturedPhotoToDiskCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToDiskCallback");
			}
			bool flag2 = string.IsNullOrEmpty(filename);
			if (flag2)
			{
				throw new ArgumentNullException("filename");
			}
			filename = filename.Replace("/", "\\");
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
			this.CapturePhotoToDisk_Internal(filename, fileOutputFormat, onCapturedPhotoToDiskCallback);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0004B454 File Offset: 0x00049654
		[NativeName("CapturePhotoToDisk")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private unsafe void CapturePhotoToDisk_Internal(string filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback)
		{
			try
			{
				IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
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
				PhotoCapture.CapturePhotoToDisk_Internal_Injected(intPtr, ref managedSpanWrapper, fileOutputFormat, onCapturedPhotoToDiskCallback);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0004B4BC File Offset: 0x000496BC
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToDiskDelegate(PhotoCapture.OnCapturedToDiskCallback callback, long hResult)
		{
			callback(PhotoCapture.MakeCaptureResult(hResult));
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0004B4CC File Offset: 0x000496CC
		public void TakePhotoAsync(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback)
		{
			bool flag = onCapturedPhotoToMemoryCallback == null;
			if (flag)
			{
				throw new ArgumentNullException("onCapturedPhotoToMemoryCallback");
			}
			this.CapturePhotoToMemory_Internal(onCapturedPhotoToMemoryCallback);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0004B4F8 File Offset: 0x000496F8
		[NativeName("CapturePhotoToMemory")]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void CapturePhotoToMemory_Internal(PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback)
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.CapturePhotoToMemory_Internal_Injected(intPtr, onCapturedPhotoToMemoryCallback);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0004B51C File Offset: 0x0004971C
		[RequiredByNativeCode]
		private static void InvokeOnCapturedPhotoToMemoryDelegate(PhotoCapture.OnCapturedToMemoryCallback callback, long hResult, IntPtr photoCaptureFramePtr)
		{
			PhotoCaptureFrame photoCaptureFrame = null;
			bool flag = photoCaptureFramePtr != IntPtr.Zero;
			if (flag)
			{
				photoCaptureFrame = new PhotoCaptureFrame(photoCaptureFramePtr);
			}
			callback(PhotoCapture.MakeCaptureResult(hResult), photoCaptureFrame);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0004B554 File Offset: 0x00049754
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[ThreadAndSerializationSafe]
		[NativeName("GetUnsafePointerToVideoDeviceController")]
		public IntPtr GetUnsafePointerToVideoDeviceController()
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCapture.GetUnsafePointerToVideoDeviceController_Injected(intPtr);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0004B578 File Offset: 0x00049778
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

		// Token: 0x06002727 RID: 10023 RVA: 0x0004B5B8 File Offset: 0x000497B8
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("Dispose")]
		private void Dispose_Internal()
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0004B5DC File Offset: 0x000497DC
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

		// Token: 0x06002729 RID: 10025 RVA: 0x0004B630 File Offset: 0x00049830
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("DisposeThreaded")]
		private void DisposeThreaded_Internal()
		{
			IntPtr intPtr = PhotoCapture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCapture.DisposeThreaded_Internal_Injected(intPtr);
		}

		// Token: 0x0600272A RID: 10026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSupportedResolutions_Internal_Injected(out BlittableArrayWrapper ret);

		// Token: 0x0600272B RID: 10027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartPhotoMode_Internal_Injected(IntPtr _unity_self, [In] ref CameraParameters setupParams, PhotoCapture.OnPhotoModeStartedCallback onPhotoModeStartedCallback);

		// Token: 0x0600272C RID: 10028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopPhotoModeAsync_Injected(IntPtr _unity_self, PhotoCapture.OnPhotoModeStoppedCallback onPhotoModeStoppedCallback);

		// Token: 0x0600272D RID: 10029
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CapturePhotoToDisk_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper filename, PhotoCaptureFileOutputFormat fileOutputFormat, PhotoCapture.OnCapturedToDiskCallback onCapturedPhotoToDiskCallback);

		// Token: 0x0600272E RID: 10030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CapturePhotoToMemory_Internal_Injected(IntPtr _unity_self, PhotoCapture.OnCapturedToMemoryCallback onCapturedPhotoToMemoryCallback);

		// Token: 0x0600272F RID: 10031
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnsafePointerToVideoDeviceController_Injected(IntPtr _unity_self);

		// Token: 0x06002730 RID: 10032
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x06002731 RID: 10033
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeThreaded_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000AFB RID: 2811
		internal IntPtr m_NativePtr;

		// Token: 0x04000AFC RID: 2812
		private static Resolution[] s_SupportedResolutions;

		// Token: 0x04000AFD RID: 2813
		private static readonly long HR_SUCCESS;

		// Token: 0x02000321 RID: 801
		public enum CaptureResultType
		{
			// Token: 0x04000AFF RID: 2815
			Success,
			// Token: 0x04000B00 RID: 2816
			UnknownError
		}

		// Token: 0x02000322 RID: 802
		public struct PhotoCaptureResult
		{
			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x06002732 RID: 10034 RVA: 0x0004B654 File Offset: 0x00049854
			public bool success
			{
				get
				{
					return this.resultType == PhotoCapture.CaptureResultType.Success;
				}
			}

			// Token: 0x04000B01 RID: 2817
			public PhotoCapture.CaptureResultType resultType;

			// Token: 0x04000B02 RID: 2818
			public long hResult;
		}

		// Token: 0x02000323 RID: 803
		// (Invoke) Token: 0x06002734 RID: 10036
		public delegate void OnCaptureResourceCreatedCallback(PhotoCapture captureObject);

		// Token: 0x02000324 RID: 804
		// (Invoke) Token: 0x06002738 RID: 10040
		public delegate void OnPhotoModeStartedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000325 RID: 805
		// (Invoke) Token: 0x0600273C RID: 10044
		public delegate void OnPhotoModeStoppedCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000326 RID: 806
		// (Invoke) Token: 0x06002740 RID: 10048
		public delegate void OnCapturedToDiskCallback(PhotoCapture.PhotoCaptureResult result);

		// Token: 0x02000327 RID: 807
		// (Invoke) Token: 0x06002744 RID: 10052
		public delegate void OnCapturedToMemoryCallback(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame);

		// Token: 0x02000328 RID: 808
		internal static class BindingsMarshaller
		{
			// Token: 0x06002747 RID: 10055 RVA: 0x0004B66F File Offset: 0x0004986F
			public static IntPtr ConvertToNative(PhotoCapture photoCapture)
			{
				return photoCapture.m_NativePtr;
			}
		}
	}
}
