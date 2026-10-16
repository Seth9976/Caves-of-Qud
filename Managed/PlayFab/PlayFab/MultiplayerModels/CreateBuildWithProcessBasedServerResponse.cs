using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public class CreateBuildWithProcessBasedServerResponse : PlayFabResultCommon
	{
		// Token: 0x0400040C RID: 1036
		public bool? AreAssetsReadonly;

		// Token: 0x0400040D RID: 1037
		public string BuildId;

		// Token: 0x0400040E RID: 1038
		public string BuildName;

		// Token: 0x0400040F RID: 1039
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x04000410 RID: 1040
		public DateTime? CreationTime;

		// Token: 0x04000411 RID: 1041
		public List<AssetReference> GameAssetReferences;

		// Token: 0x04000412 RID: 1042
		public List<GameCertificateReference> GameCertificateReferences;

		// Token: 0x04000413 RID: 1043
		public string GameWorkingDirectory;

		// Token: 0x04000414 RID: 1044
		public Dictionary<string, string> Metadata;

		// Token: 0x04000415 RID: 1045
		public int MultiplayerServerCountPerVm;

		// Token: 0x04000416 RID: 1046
		public string OsPlatform;

		// Token: 0x04000417 RID: 1047
		public List<Port> Ports;

		// Token: 0x04000418 RID: 1048
		public List<BuildRegion> RegionConfigurations;

		// Token: 0x04000419 RID: 1049
		public string ServerType;

		// Token: 0x0400041A RID: 1050
		public string StartMultiplayerServerCommand;

		// Token: 0x0400041B RID: 1051
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x0400041C RID: 1052
		public AzureVmSize? VmSize;
	}
}
