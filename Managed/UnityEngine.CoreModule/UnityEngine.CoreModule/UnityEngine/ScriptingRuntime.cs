using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020002A8 RID: 680
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Export/Scripting/ScriptingRuntime.h")]
	internal class ScriptingRuntime
	{
		// Token: 0x0600219C RID: 8604
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetAllUserAssemblies();
	}
}
