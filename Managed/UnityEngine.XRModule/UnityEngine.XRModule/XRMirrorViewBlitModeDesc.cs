using System;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	public struct XRMirrorViewBlitModeDesc
	{
		// Token: 0x040000E8 RID: 232
		public int blitMode;

		// Token: 0x040000E9 RID: 233
		public string blitModeDesc;
	}
}
