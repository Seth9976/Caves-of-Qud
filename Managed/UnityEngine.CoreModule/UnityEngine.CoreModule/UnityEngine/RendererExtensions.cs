using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200017D RID: 381
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class RendererExtensions
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00019E2B File Offset: 0x0001802B
		public static void UpdateGIMaterials(this Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer(renderer);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00019E38 File Offset: 0x00018038
		[FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")]
		internal static void UpdateGIMaterialsForRenderer(Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer_Injected(Object.MarshalledUnityObject.Marshal<Renderer>(renderer));
		}

		// Token: 0x06000E61 RID: 3681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateGIMaterialsForRenderer_Injected(IntPtr renderer);
	}
}
