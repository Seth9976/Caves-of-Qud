using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000027 RID: 39
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetExportAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000257F File Offset: 0x0000077F
		public AssetExportAnalytic()
			: base("assetExport", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002598 File Offset: 0x00000798
		[RequiredByNativeCode]
		public static AssetExportAnalytic CreateAssetExportAnalytic()
		{
			return new AssetExportAnalytic();
		}

		// Token: 0x04000064 RID: 100
		public string package_name;

		// Token: 0x04000065 RID: 101
		public string error_message;

		// Token: 0x04000066 RID: 102
		public int items_count;

		// Token: 0x04000067 RID: 103
		public string[] asset_extensions;

		// Token: 0x04000068 RID: 104
		public bool include_upm_dependencies;
	}
}
