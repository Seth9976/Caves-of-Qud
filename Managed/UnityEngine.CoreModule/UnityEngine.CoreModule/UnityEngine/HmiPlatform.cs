using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000203 RID: 515
	[NativeHeader("Runtime/Export/Hmi/HmiPlatform.bindings.h")]
	public class HmiPlatform
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x00030B14 File Offset: 0x0002ED14
		[ExcludeFromDocs]
		public unsafe static void LogStartupTiming(string tag)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				HmiPlatform.LogStartupTiming_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001B48 RID: 6984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LogStartupTiming_Injected(ref ManagedSpanWrapper tag);
	}
}
