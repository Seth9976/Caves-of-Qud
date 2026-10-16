using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class CreateBuildWithManagedContainerResponse : PlayFabResultCommon
	{
		// Token: 0x040003EC RID: 1004
		public bool? AreAssetsReadonly;

		// Token: 0x040003ED RID: 1005
		public string BuildId;

		// Token: 0x040003EE RID: 1006
		public string BuildName;

		// Token: 0x040003EF RID: 1007
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x040003F0 RID: 1008
		public DateTime? CreationTime;

		// Token: 0x040003F1 RID: 1009
		public List<AssetReference> GameAssetReferences;

		// Token: 0x040003F2 RID: 1010
		public List<GameCertificateReference> GameCertificateReferences;

		// Token: 0x040003F3 RID: 1011
		public string GameWorkingDirectory;

		// Token: 0x040003F4 RID: 1012
		public InstrumentationConfiguration InstrumentationConfiguration;

		// Token: 0x040003F5 RID: 1013
		public Dictionary<string, string> Metadata;

		// Token: 0x040003F6 RID: 1014
		public int MultiplayerServerCountPerVm;

		// Token: 0x040003F7 RID: 1015
		public string OsPlatform;

		// Token: 0x040003F8 RID: 1016
		public List<Port> Ports;

		// Token: 0x040003F9 RID: 1017
		public List<BuildRegion> RegionConfigurations;

		// Token: 0x040003FA RID: 1018
		public string ServerType;

		// Token: 0x040003FB RID: 1019
		public string StartMultiplayerServerCommand;

		// Token: 0x040003FC RID: 1020
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x040003FD RID: 1021
		public AzureVmSize? VmSize;
	}
}
