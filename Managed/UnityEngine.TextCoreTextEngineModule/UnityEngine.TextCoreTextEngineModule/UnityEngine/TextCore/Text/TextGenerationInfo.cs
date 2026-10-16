using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005D RID: 93
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class TextGenerationInfo
	{
		// Token: 0x060002A9 RID: 681
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create();

		// Token: 0x060002AA RID: 682
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(IntPtr ptr);
	}
}
