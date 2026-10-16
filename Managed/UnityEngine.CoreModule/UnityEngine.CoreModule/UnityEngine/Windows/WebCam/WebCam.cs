using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x02000337 RID: 823
	[NativeHeader("PlatformDependent/Win/Webcam/WebCam.h")]
	[StaticAccessor("WebCam::GetInstance()", StaticAccessorType.Dot)]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	public class WebCam
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060027A5 RID: 10149
		public static extern WebCamMode Mode
		{
			[NativeName("GetWebCamMode")]
			[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
