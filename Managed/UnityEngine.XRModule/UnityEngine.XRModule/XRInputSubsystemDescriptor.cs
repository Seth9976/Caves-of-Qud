using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h")]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000058D8 File Offset: 0x00003AD8
		[NativeConditional("ENABLE_XR")]
		public bool disablesLegacyInput
		{
			get
			{
				IntPtr intPtr = XRInputSubsystemDescriptor.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRInputSubsystemDescriptor.get_disablesLegacyInput_Injected(intPtr);
			}
		}

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_disablesLegacyInput_Injected(IntPtr _unity_self);

		// Token: 0x0200002A RID: 42
		internal static class BindingsMarshaller
		{
			// Token: 0x0600018C RID: 396 RVA: 0x00005509 File Offset: 0x00003709
			public static IntPtr ConvertToNative(XRInputSubsystemDescriptor descriptor)
			{
				return descriptor.m_Ptr;
			}
		}
	}
}
