using System;

namespace UnityEngine.Search
{
	// Token: 0x0200035C RID: 860
	[Flags]
	public enum SearchViewFlags
	{
		// Token: 0x04000B59 RID: 2905
		None = 0,
		// Token: 0x04000B5A RID: 2906
		Debug = 16,
		// Token: 0x04000B5B RID: 2907
		NoIndexing = 32,
		// Token: 0x04000B5C RID: 2908
		Packages = 256,
		// Token: 0x04000B5D RID: 2909
		OpenLeftSidePanel = 2048,
		// Token: 0x04000B5E RID: 2910
		OpenInspectorPreview = 4096,
		// Token: 0x04000B5F RID: 2911
		Centered = 8192,
		// Token: 0x04000B60 RID: 2912
		HideSearchBar = 16384,
		// Token: 0x04000B61 RID: 2913
		CompactView = 32768,
		// Token: 0x04000B62 RID: 2914
		ListView = 65536,
		// Token: 0x04000B63 RID: 2915
		GridView = 131072,
		// Token: 0x04000B64 RID: 2916
		TableView = 262144,
		// Token: 0x04000B65 RID: 2917
		EnableSearchQuery = 524288,
		// Token: 0x04000B66 RID: 2918
		DisableInspectorPreview = 1048576,
		// Token: 0x04000B67 RID: 2919
		DisableSavedSearchQuery = 2097152,
		// Token: 0x04000B68 RID: 2920
		OpenInBuilderMode = 4194304,
		// Token: 0x04000B69 RID: 2921
		OpenInTextMode = 8388608,
		// Token: 0x04000B6A RID: 2922
		DisableBuilderModeToggle = 16777216,
		// Token: 0x04000B6B RID: 2923
		Borderless = 33554432,
		// Token: 0x04000B6C RID: 2924
		DisableQueryHelpers = 67108864,
		// Token: 0x04000B6D RID: 2925
		DisableNoResultTips = 134217728,
		// Token: 0x04000B6E RID: 2926
		IgnoreSavedSearches = 268435456,
		// Token: 0x04000B6F RID: 2927
		ObjectPicker = 536870912,
		// Token: 0x04000B70 RID: 2928
		ObjectPickerAdvancedUI = 1073741824,
		// Token: 0x04000B71 RID: 2929
		ContextSwitchPreservedMask = 33560576
	}
}
