using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Internal
{
	// Token: 0x0200043B RID: 1083
	[NativeHeader("Runtime/GfxDevice/HDROutputSettings.h")]
	[ExcludeFromDocs]
	internal static class InternalHDROutputFaking
	{
		// Token: 0x060029EF RID: 10735
		[ExcludeFromDocs]
		[FreeFunction("HDROutputSettingsBindings::SetFakeHDROutputEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetEnabled(bool enabled);
	}
}
