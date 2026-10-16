using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200002B RID: 43
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VCProviderAnalytics : AnalyticsEventBase
	{
		// Token: 0x0600004F RID: 79 RVA: 0x0000263F File Offset: 0x0000083F
		public VCProviderAnalytics()
			: base("versioncontrol_ProviderSettings_OnUpdate", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002658 File Offset: 0x00000858
		[RequiredByNativeCode]
		internal static VCProviderAnalytics CreateVCProviderAnalytics()
		{
			return new VCProviderAnalytics();
		}

		// Token: 0x0400006E RID: 110
		public string Mode;
	}
}
