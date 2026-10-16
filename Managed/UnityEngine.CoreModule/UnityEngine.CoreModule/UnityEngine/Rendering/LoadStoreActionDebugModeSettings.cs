using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200047E RID: 1150
	public static class LoadStoreActionDebugModeSettings
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002D80 RID: 11648
		// (set) Token: 0x06002D81 RID: 11649
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern bool LoadStoreDebugModeEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
