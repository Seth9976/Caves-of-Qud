using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class CreateBuildWithManagedContainerRequest : PlayFabRequestCommon
	{
		// Token: 0x040003DD RID: 989
		public bool? AreAssetsReadonly;

		// Token: 0x040003DE RID: 990
		public string BuildName;

		// Token: 0x040003DF RID: 991
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x040003E0 RID: 992
		public Dictionary<string, string> CustomTags;

		// Token: 0x040003E1 RID: 993
		public List<AssetReferenceParams> GameAssetReferences;

		// Token: 0x040003E2 RID: 994
		public List<GameCertificateReferenceParams> GameCertificateReferences;

		// Token: 0x040003E3 RID: 995
		public string GameWorkingDirectory;

		// Token: 0x040003E4 RID: 996
		public InstrumentationConfiguration InstrumentationConfiguration;

		// Token: 0x040003E5 RID: 997
		public Dictionary<string, string> Metadata;

		// Token: 0x040003E6 RID: 998
		public int MultiplayerServerCountPerVm;

		// Token: 0x040003E7 RID: 999
		public List<Port> Ports;

		// Token: 0x040003E8 RID: 1000
		public List<BuildRegionParams> RegionConfigurations;

		// Token: 0x040003E9 RID: 1001
		public string StartMultiplayerServerCommand;

		// Token: 0x040003EA RID: 1002
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x040003EB RID: 1003
		public AzureVmSize? VmSize;
	}
}
