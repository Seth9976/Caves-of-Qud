using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000014 RID: 20
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class BuildAssetBundleAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000224F File Offset: 0x0000044F
		public BuildAssetBundleAnalytic()
			: base("unity5BuildAssetBundles", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002268 File Offset: 0x00000468
		[RequiredByNativeCode]
		internal static BuildAssetBundleAnalytic CreateBuildAssetBundleAnalytic()
		{
			return new BuildAssetBundleAnalytic();
		}

		// Token: 0x04000030 RID: 48
		public bool success;

		// Token: 0x04000031 RID: 49
		public string error;
	}
}
