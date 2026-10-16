using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/Director/ScriptBindings/DataPlayableOutputExtensions.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("DataPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class DataPlayableOutputExtensions
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002510 File Offset: 0x00000710
		[NativeThrows]
		internal unsafe static bool InternalCreateDataOutput(ref PlayableGraph graph, string name, Type type, out PlayableOutputHandle handle)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = DataPlayableOutputExtensions.InternalCreateDataOutput_Injected(ref graph, ref managedSpanWrapper, type, out handle);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateDataOutput_Injected(ref PlayableGraph graph, ref ManagedSpanWrapper name, Type type, out PlayableOutputHandle handle);
	}
}
