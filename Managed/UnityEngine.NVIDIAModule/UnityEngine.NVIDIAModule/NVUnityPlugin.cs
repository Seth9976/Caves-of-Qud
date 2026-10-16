using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/NVIDIA/NVPlugins.h")]
	public static class NVUnityPlugin
	{
		// Token: 0x06000001 RID: 1
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Load();

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLoaded();
	}
}
