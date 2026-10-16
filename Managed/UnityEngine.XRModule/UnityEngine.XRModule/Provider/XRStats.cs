using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR.Provider
{
	// Token: 0x02000039 RID: 57
	public static class XRStats
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00006274 File Offset: 0x00004474
		public static bool TryGetStat(IntegratedSubsystem xrSubsystem, string tag, out float value)
		{
			return XRStats.TryGetStat_Internal(xrSubsystem.m_Ptr, tag, out value);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006294 File Offset: 0x00004494
		[NativeMethod("TryGetStatByName_Internal")]
		[StaticAccessor("XRStats::Get()", StaticAccessorType.Dot)]
		[NativeHeader("Modules/XR/Stats/XRStats.h")]
		[NativeConditional("ENABLE_XR")]
		private unsafe static bool TryGetStat_Internal(IntPtr ptr, string tag, out float value)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr2 = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr2, readOnlySpan.Length);
					}
				}
				flag = XRStats.TryGetStat_Internal_Injected(ptr, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TryGetStat_Internal_Injected(IntPtr ptr, ref ManagedSpanWrapper tag, out float value);
	}
}
