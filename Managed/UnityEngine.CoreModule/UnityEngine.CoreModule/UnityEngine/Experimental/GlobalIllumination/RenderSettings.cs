using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000586 RID: 1414
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	public class RenderSettings
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060036B2 RID: 14002
		// (set) Token: 0x060036B3 RID: 14003
		public static extern bool useRadianceAmbientProbe
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
