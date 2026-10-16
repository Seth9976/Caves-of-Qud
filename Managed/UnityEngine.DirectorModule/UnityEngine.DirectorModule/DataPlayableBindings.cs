using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("DataPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayable.bindings.h")]
	internal static class DataPlayableBindings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[NativeThrows]
		public static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return DataPlayableBindings.CreateHandleInternal_Injected(ref graph, ref handle);
		}

		// Token: 0x06000002 RID: 2
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle);
	}
}
