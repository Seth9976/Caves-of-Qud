using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Streaming/StreamingController.h")]
	[RequireComponent(typeof(Camera))]
	public class StreamingController : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		public float streamingMipmapBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<StreamingController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return StreamingController.get_streamingMipmapBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<StreamingController>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				StreamingController.set_streamingMipmapBias_Injected(intPtr, value);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000298
		public void SetPreloading(float timeoutSeconds = 0f, bool activateCameraOnTimeout = false, Camera disableCameraCuttingFrom = null)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<StreamingController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			StreamingController.SetPreloading_Injected(intPtr, timeoutSeconds, activateCameraOnTimeout, Object.MarshalledUnityObject.Marshal<Camera>(disableCameraCuttingFrom));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		public void CancelPreloading()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<StreamingController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			StreamingController.CancelPreloading_Injected(intPtr);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E8 File Offset: 0x000002E8
		public bool IsPreloading()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<StreamingController>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return StreamingController.IsPreloading_Injected(intPtr);
		}

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_streamingMipmapBias_Injected(IntPtr _unity_self);

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_streamingMipmapBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPreloading_Injected(IntPtr _unity_self, float timeoutSeconds, bool activateCameraOnTimeout, IntPtr disableCameraCuttingFrom);

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelPreloading_Injected(IntPtr _unity_self);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPreloading_Injected(IntPtr _unity_self);
	}
}
