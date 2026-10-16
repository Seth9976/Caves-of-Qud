using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public class CreateBuildWithProcessBasedServerRequest : PlayFabRequestCommon
	{
		// Token: 0x040003FE RID: 1022
		public bool? AreAssetsReadonly;

		// Token: 0x040003FF RID: 1023
		public string BuildName;

		// Token: 0x04000400 RID: 1024
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000401 RID: 1025
		public List<AssetReferenceParams> GameAssetReferences;

		// Token: 0x04000402 RID: 1026
		public List<GameCertificateReferenceParams> GameCertificateReferences;

		// Token: 0x04000403 RID: 1027
		public string GameWorkingDirectory;

		// Token: 0x04000404 RID: 1028
		public Dictionary<string, string> Metadata;

		// Token: 0x04000405 RID: 1029
		public int MultiplayerServerCountPerVm;

		// Token: 0x04000406 RID: 1030
		public string OsPlatform;

		// Token: 0x04000407 RID: 1031
		public List<Port> Ports;

		// Token: 0x04000408 RID: 1032
		public List<BuildRegionParams> RegionConfigurations;

		// Token: 0x04000409 RID: 1033
		public string StartMultiplayerServerCommand;

		// Token: 0x0400040A RID: 1034
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x0400040B RID: 1035
		public AzureVmSize? VmSize;
	}
}
