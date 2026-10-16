using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000026 RID: 38
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class AssetImportAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000254F File Offset: 0x0000074F
		public AssetImportAnalytic()
			: base("assetImport", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002568 File Offset: 0x00000768
		[RequiredByNativeCode]
		public static AssetImportAnalytic CreateAssetImportAnalytic()
		{
			return new AssetImportAnalytic();
		}

		// Token: 0x04000062 RID: 98
		public string package_name;

		// Token: 0x04000063 RID: 99
		public int package_import_choice;
	}
}
