using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/UnityAnalyticsCommon/Public/UnityAnalyticsCommon.h")]
	[ExcludeFromDocs]
	[StructLayout(LayoutKind.Sequential)]
	public static class AnalyticsCommon
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000012 RID: 18
		[StaticAccessor("GetUnityAnalyticsCommon()", StaticAccessorType.Dot)]
		private static extern bool ugsAnalyticsEnabledInternal
		{
			[NativeMethod("UGSAnalyticsUserOptStatus")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeMethod("SetUGSAnalyticsUserOptStatus")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000215C File Offset: 0x0000035C
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002173 File Offset: 0x00000373
		public static bool ugsAnalyticsEnabled
		{
			get
			{
				return AnalyticsCommon.ugsAnalyticsEnabledInternal;
			}
			set
			{
				AnalyticsCommon.ugsAnalyticsEnabledInternal = value;
			}
		}
	}
}
