using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000037 RID: 55
	[NativeType(Header = "Modules/XR/Subsystems/Planes/XRMeshSubsystemDescriptor.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRMeshSubsystemDescriptor : IntegratedSubsystemDescriptor<XRMeshSubsystem>
	{
		// Token: 0x02000038 RID: 56
		internal static class BindingsMarshaller
		{
			// Token: 0x060001DC RID: 476 RVA: 0x00005509 File Offset: 0x00003709
			public static IntPtr ConvertToNative(XRMeshSubsystemDescriptor descriptor)
			{
				return descriptor.m_Ptr;
			}
		}
	}
}
