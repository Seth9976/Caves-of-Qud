using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[StaticAccessor("SpriteUtilityBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/SpriteMask/Public/ScriptBindings/SpriteMask.bindings.h")]
	internal static class SpriteMaskUtility
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002338 File Offset: 0x00000538
		internal static bool HasSpriteMaskInLayerRange(SortingLayerRange range)
		{
			return SpriteMaskUtility.HasSpriteMaskInLayerRange_Injected(ref range);
		}

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasSpriteMaskInLayerRange_Injected([In] ref SortingLayerRange range);
	}
}
