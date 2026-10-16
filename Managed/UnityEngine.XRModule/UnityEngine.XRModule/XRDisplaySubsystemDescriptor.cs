using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005470 File Offset: 0x00003670
		[NativeConditional("ENABLE_XR")]
		public bool disablesLegacyVr
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystemDescriptor.get_disablesLegacyVr_Injected(intPtr);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005494 File Offset: 0x00003694
		[NativeConditional("ENABLE_XR")]
		public bool enableBackBufferMSAA
		{
			get
			{
				IntPtr intPtr = XRDisplaySubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRDisplaySubsystemDescriptor.get_enableBackBufferMSAA_Injected(intPtr);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000054B8 File Offset: 0x000036B8
		[NativeMethod("TryGetAvailableMirrorModeCount")]
		[NativeConditional("ENABLE_XR")]
		public int GetAvailableMirrorBlitModeCount()
		{
			IntPtr intPtr = XRDisplaySubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRDisplaySubsystemDescriptor.GetAvailableMirrorBlitModeCount_Injected(intPtr);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000054DC File Offset: 0x000036DC
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetMirrorModeByIndex")]
		public void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode)
		{
			IntPtr intPtr = XRDisplaySubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRDisplaySubsystemDescriptor.GetMirrorBlitModeByIndex_Injected(intPtr, index, out mode);
		}

		// Token: 0x0600016C RID: 364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_disablesLegacyVr_Injected(IntPtr _unity_self);

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableBackBufferMSAA_Injected(IntPtr _unity_self);

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAvailableMirrorBlitModeCount_Injected(IntPtr _unity_self);

		// Token: 0x0600016F RID: 367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMirrorBlitModeByIndex_Injected(IntPtr _unity_self, int index, out XRMirrorViewBlitModeDesc mode);

		// Token: 0x02000025 RID: 37
		internal static class BindingsMarshaller
		{
			// Token: 0x06000170 RID: 368 RVA: 0x00005509 File Offset: 0x00003709
			public static IntPtr ConvertToNative(XRDisplaySubsystemDescriptor descriptor)
			{
				return descriptor.m_Ptr;
			}
		}
	}
}
