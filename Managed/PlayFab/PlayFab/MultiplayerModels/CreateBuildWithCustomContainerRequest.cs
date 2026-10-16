using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class CreateBuildWithCustomContainerRequest : PlayFabRequestCommon
	{
		// Token: 0x040003BE RID: 958
		public bool? AreAssetsReadonly;

		// Token: 0x040003BF RID: 959
		public string BuildName;

		// Token: 0x040003C0 RID: 960
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x040003C1 RID: 961
		public ContainerImageReference ContainerImageReference;

		// Token: 0x040003C2 RID: 962
		public string ContainerRunCommand;

		// Token: 0x040003C3 RID: 963
		public Dictionary<string, string> CustomTags;

		// Token: 0x040003C4 RID: 964
		public List<AssetReferenceParams> GameAssetReferences;

		// Token: 0x040003C5 RID: 965
		public List<GameCertificateReferenceParams> GameCertificateReferences;

		// Token: 0x040003C6 RID: 966
		public Dictionary<string, string> Metadata;

		// Token: 0x040003C7 RID: 967
		public int MultiplayerServerCountPerVm;

		// Token: 0x040003C8 RID: 968
		public List<Port> Ports;

		// Token: 0x040003C9 RID: 969
		public List<BuildRegionParams> RegionConfigurations;

		// Token: 0x040003CA RID: 970
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x040003CB RID: 971
		public AzureVmSize? VmSize;
	}
}
