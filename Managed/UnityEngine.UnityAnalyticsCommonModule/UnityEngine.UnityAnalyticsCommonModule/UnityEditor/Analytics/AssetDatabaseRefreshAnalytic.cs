using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000013 RID: 19
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetDatabaseRefreshAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000221F File Offset: 0x0000041F
		public AssetDatabaseRefreshAnalytic()
			: base("assetDatabaseInitRefresh", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002238 File Offset: 0x00000438
		[RequiredByNativeCode]
		internal static AssetDatabaseRefreshAnalytic CreateAssetDatabaseRefreshAnalytic()
		{
			return new AssetDatabaseRefreshAnalytic();
		}

		// Token: 0x0400001B RID: 27
		[SerializeField]
		public bool isV2;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		public long Imports_Imported;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		public long Imports_ImportedInProcess;

		// Token: 0x0400001E RID: 30
		[SerializeField]
		public long Imports_ImportedOutOfProcess;

		// Token: 0x0400001F RID: 31
		[SerializeField]
		public long Imports_Refresh;

		// Token: 0x04000020 RID: 32
		[SerializeField]
		public long Imports_DomainReload;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		public long CacheServer_MetadataRequested;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		public long CacheServer_MetadataDownloaded;

		// Token: 0x04000023 RID: 35
		[SerializeField]
		public long CacheServer_MetadataFailedToDownload;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		public long CacheServer_MetadataUploaded;

		// Token: 0x04000025 RID: 37
		[SerializeField]
		public long CacheServer_ArtifactsFailedToUpload;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		public long CacheServer_MetadataVersionsDownloaded;

		// Token: 0x04000027 RID: 39
		[SerializeField]
		public long CacheServer_MetadataMatched;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		public long CacheServer_ArtifactsDownloaded;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		public long CacheServer_ArtifactFilesDownloaded;

		// Token: 0x0400002A RID: 42
		[SerializeField]
		public long CacheServer_ArtifactFilesFailedToDownload;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		public long CacheServer_ArtifactsUploaded;

		// Token: 0x0400002C RID: 44
		[SerializeField]
		public long CacheServer_ArtifactFilesUploaded;

		// Token: 0x0400002D RID: 45
		[SerializeField]
		public long CacheServer_ArtifactFilesFailedToUpload;

		// Token: 0x0400002E RID: 46
		[SerializeField]
		public long CacheServer_Connects;

		// Token: 0x0400002F RID: 47
		[SerializeField]
		public long CacheServer_Disconnects;
	}
}
