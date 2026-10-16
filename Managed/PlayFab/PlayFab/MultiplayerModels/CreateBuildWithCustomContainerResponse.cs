using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	public class CreateBuildWithCustomContainerResponse : PlayFabResultCommon
	{
		// Token: 0x040003CC RID: 972
		public bool? AreAssetsReadonly;

		// Token: 0x040003CD RID: 973
		public string BuildId;

		// Token: 0x040003CE RID: 974
		public string BuildName;

		// Token: 0x040003CF RID: 975
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x040003D0 RID: 976
		public string ContainerRunCommand;

		// Token: 0x040003D1 RID: 977
		public DateTime? CreationTime;

		// Token: 0x040003D2 RID: 978
		public ContainerImageReference CustomGameContainerImage;

		// Token: 0x040003D3 RID: 979
		public List<AssetReference> GameAssetReferences;

		// Token: 0x040003D4 RID: 980
		public List<GameCertificateReference> GameCertificateReferences;

		// Token: 0x040003D5 RID: 981
		public Dictionary<string, string> Metadata;

		// Token: 0x040003D6 RID: 982
		public int MultiplayerServerCountPerVm;

		// Token: 0x040003D7 RID: 983
		public string OsPlatform;

		// Token: 0x040003D8 RID: 984
		public List<Port> Ports;

		// Token: 0x040003D9 RID: 985
		public List<BuildRegion> RegionConfigurations;

		// Token: 0x040003DA RID: 986
		public string ServerType;

		// Token: 0x040003DB RID: 987
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x040003DC RID: 988
		public AzureVmSize? VmSize;
	}
}
