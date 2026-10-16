using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AMD
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/AMD/AMDPlugins.h")]
	public static class AMDUnityPlugin
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Load();

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLoaded();
	}
}
