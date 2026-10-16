using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	// Token: 0x0200000D RID: 13
	[StaticAccessor("GraphicsDeviceDebug", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Graphics/GraphicsDeviceDebug.bindings.h")]
	internal static class GraphicsDeviceDebug
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000020C8 File Offset: 0x000002C8
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static GraphicsDeviceDebugSettings settings
		{
			get
			{
				GraphicsDeviceDebugSettings graphicsDeviceDebugSettings;
				GraphicsDeviceDebug.get_settings_Injected(out graphicsDeviceDebugSettings);
				return graphicsDeviceDebugSettings;
			}
			set
			{
				GraphicsDeviceDebug.set_settings_Injected(ref value);
			}
		}

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_settings_Injected(out GraphicsDeviceDebugSettings ret);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_settings_Injected([In] ref GraphicsDeviceDebugSettings value);
	}
}
