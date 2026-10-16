using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public class MemorylessManager
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000020BB File Offset: 0x000002BB
		public static MemorylessMode depthMemorylessMode
		{
			get
			{
				return MemorylessManager.GetFramebufferDepthMemorylessMode();
			}
			set
			{
				MemorylessManager.SetFramebufferDepthMemorylessMode(value);
			}
		}

		// Token: 0x06000016 RID: 22
		[NativeMethod(Name = "GetFramebufferDepthMemorylessMode")]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemorylessMode GetFramebufferDepthMemorylessMode();

		// Token: 0x06000017 RID: 23
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "SetFramebufferDepthMemorylessMode")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetFramebufferDepthMemorylessMode(MemorylessMode mode);
	}
}
