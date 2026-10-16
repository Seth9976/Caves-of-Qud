using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000329 RID: 809
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h")]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x0004B677 File Offset: 0x00049877
		// (set) Token: 0x06002749 RID: 10057 RVA: 0x0004B67F File Offset: 0x0004987F
		public int dataLength { get; private set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x0004B688 File Offset: 0x00049888
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x0004B690 File Offset: 0x00049890
		public bool hasLocationData { get; private set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x0004B699 File Offset: 0x00049899
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x0004B6A1 File Offset: 0x000498A1
		public CapturePixelFormat pixelFormat { get; private set; }

		// Token: 0x0600274E RID: 10062 RVA: 0x0004B6AC File Offset: 0x000498AC
		[ThreadAndSerializationSafe]
		private int GetDataLength()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetDataLength_Injected(intPtr);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0004B6D0 File Offset: 0x000498D0
		[ThreadAndSerializationSafe]
		private bool GetHasLocationData()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetHasLocationData_Injected(intPtr);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0004B6F4 File Offset: 0x000498F4
		[ThreadAndSerializationSafe]
		private CapturePixelFormat GetCapturePixelFormat()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetCapturePixelFormat_Injected(intPtr);
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0004B718 File Offset: 0x00049918
		public bool TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix)
		{
			cameraToWorldMatrix = Matrix4x4.identity;
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				cameraToWorldMatrix = this.GetCameraToWorldMatrix();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0004B754 File Offset: 0x00049954
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[NativeName("GetCameraToWorld")]
		[ThreadAndSerializationSafe]
		private Matrix4x4 GetCameraToWorldMatrix()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			PhotoCaptureFrame.GetCameraToWorldMatrix_Injected(intPtr, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0004B77C File Offset: 0x0004997C
		public bool TryGetProjectionMatrix(out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				projectionMatrix = this.GetProjection();
				flag = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0004B7B8 File Offset: 0x000499B8
		public bool TryGetProjectionMatrix(float nearClipPlane, float farClipPlane, out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag3;
			if (hasLocationData)
			{
				float num = 0.01f;
				bool flag = nearClipPlane < num;
				if (flag)
				{
					nearClipPlane = num;
				}
				bool flag2 = farClipPlane < nearClipPlane + num;
				if (flag2)
				{
					farClipPlane = nearClipPlane + num;
				}
				projectionMatrix = this.GetProjection();
				float num2 = 1f / (farClipPlane - nearClipPlane);
				float num3 = -(farClipPlane + nearClipPlane) * num2;
				float num4 = -(2f * farClipPlane * nearClipPlane) * num2;
				projectionMatrix.m22 = num3;
				projectionMatrix.m23 = num4;
				flag3 = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0004B84C File Offset: 0x00049A4C
		[ThreadAndSerializationSafe]
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		private Matrix4x4 GetProjection()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			PhotoCaptureFrame.GetProjection_Injected(intPtr, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0004B874 File Offset: 0x00049A74
		public void UploadImageDataToTexture(Texture2D targetTexture)
		{
			bool flag = targetTexture == null;
			if (flag)
			{
				throw new ArgumentNullException("targetTexture");
			}
			bool flag2 = this.pixelFormat > CapturePixelFormat.BGRA32;
			if (flag2)
			{
				throw new ArgumentException("Uploading PhotoCaptureFrame to a texture is only supported with BGRA32 CameraFrameFormat!");
			}
			this.UploadImageDataToTexture_Internal(targetTexture);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0004B8BC File Offset: 0x00049ABC
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("UploadImageDataToTexture")]
		private void UploadImageDataToTexture_Internal(Texture2D targetTexture)
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCaptureFrame.UploadImageDataToTexture_Internal_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture2D>(targetTexture));
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		public IntPtr GetUnsafePointerToBuffer()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PhotoCaptureFrame.GetUnsafePointerToBuffer_Injected(intPtr);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0004B908 File Offset: 0x00049B08
		public void CopyRawImageDataIntoBuffer(List<byte> byteBuffer)
		{
			bool flag = byteBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("byteBuffer");
			}
			byte[] array = new byte[this.dataLength];
			this.CopyRawImageDataIntoBuffer_Internal(array);
			bool flag2 = byteBuffer.Capacity < array.Length;
			if (flag2)
			{
				byteBuffer.Capacity = array.Length;
			}
			byteBuffer.Clear();
			byteBuffer.AddRange(array);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0004B968 File Offset: 0x00049B68
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("CopyRawImageDataIntoBuffer")]
		[ThreadAndSerializationSafe]
		internal unsafe void CopyRawImageDataIntoBuffer_Internal([Out] byte[] byteArray)
		{
			try
			{
				IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (byteArray != null)
				{
					fixed (byte[] array = byteArray)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				PhotoCaptureFrame.CopyRawImageDataIntoBuffer_Internal_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				byte[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0004B9C8 File Offset: 0x00049BC8
		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0004BA18 File Offset: 0x00049C18
		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0004BA5C File Offset: 0x00049C5C
		[NativeName("Dispose")]
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		private void Dispose_Internal()
		{
			IntPtr intPtr = PhotoCaptureFrame.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PhotoCaptureFrame.Dispose_Internal_Injected(intPtr);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x0004BA7E File Offset: 0x00049C7E
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0004BA90 File Offset: 0x00049C90
		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		// Token: 0x06002760 RID: 10080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDataLength_Injected(IntPtr _unity_self);

		// Token: 0x06002761 RID: 10081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHasLocationData_Injected(IntPtr _unity_self);

		// Token: 0x06002762 RID: 10082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CapturePixelFormat GetCapturePixelFormat_Injected(IntPtr _unity_self);

		// Token: 0x06002763 RID: 10083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCameraToWorldMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06002764 RID: 10084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetProjection_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x06002765 RID: 10085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UploadImageDataToTexture_Internal_Injected(IntPtr _unity_self, IntPtr targetTexture);

		// Token: 0x06002766 RID: 10086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnsafePointerToBuffer_Injected(IntPtr _unity_self);

		// Token: 0x06002767 RID: 10087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyRawImageDataIntoBuffer_Internal_Injected(IntPtr _unity_self, out BlittableArrayWrapper byteArray);

		// Token: 0x06002768 RID: 10088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispose_Internal_Injected(IntPtr _unity_self);

		// Token: 0x04000B03 RID: 2819
		private IntPtr m_NativePtr;

		// Token: 0x0200032A RID: 810
		internal static class BindingsMarshaller
		{
			// Token: 0x06002769 RID: 10089 RVA: 0x0004BAC0 File Offset: 0x00049CC0
			public static IntPtr ConvertToNative(PhotoCaptureFrame photoCaptureFrame)
			{
				return photoCaptureFrame.m_NativePtr;
			}
		}
	}
}
