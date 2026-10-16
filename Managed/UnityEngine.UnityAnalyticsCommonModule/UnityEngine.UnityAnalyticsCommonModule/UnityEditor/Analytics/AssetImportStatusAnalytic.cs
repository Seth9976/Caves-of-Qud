using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000025 RID: 37
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetImportStatusAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0000251F File Offset: 0x0000071F
		public AssetImportStatusAnalytic()
			: base("assetImportStatus", 1, SendEventOptions.kAppendBuildTarget, "")
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002538 File Offset: 0x00000738
		[RequiredByNativeCode]
		public static AssetImportStatusAnalytic CreateAssetImportStatusAnalytic()
		{
			return new AssetImportStatusAnalytic();
		}

		// Token: 0x04000058 RID: 88
		public string package_name;

		// Token: 0x04000059 RID: 89
		public int package_items_count;

		// Token: 0x0400005A RID: 90
		public int package_import_status;

		// Token: 0x0400005B RID: 91
		public string error_message;

		// Token: 0x0400005C RID: 92
		public int project_assets_count;

		// Token: 0x0400005D RID: 93
		public int unselected_assets_count;

		// Token: 0x0400005E RID: 94
		public int selected_new_assets_count;

		// Token: 0x0400005F RID: 95
		public int selected_changed_assets_count;

		// Token: 0x04000060 RID: 96
		public int unchanged_assets_count;

		// Token: 0x04000061 RID: 97
		public string[] selected_asset_extensions;
	}
}
