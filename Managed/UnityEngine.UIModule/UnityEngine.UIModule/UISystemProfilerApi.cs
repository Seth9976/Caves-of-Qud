using System;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[IgnoredByDeepProfiler]
	[StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/UI/Canvas.h")]
	public static class UISystemProfilerApi
	{
		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(UISystemProfilerApi.SampleType type);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(UISystemProfilerApi.SampleType type);

		// Token: 0x060000E6 RID: 230 RVA: 0x000036EC File Offset: 0x000018EC
		public unsafe static void AddMarker(string name, Object obj)
		{
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
				UISystemProfilerApi.AddMarker_Injected(ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(obj));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddMarker_Injected(ref ManagedSpanWrapper name, IntPtr obj);

		// Token: 0x0200000C RID: 12
		public enum SampleType
		{
			// Token: 0x04000017 RID: 23
			Layout,
			// Token: 0x04000018 RID: 24
			Render
		}
	}
}
