using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class GetBuildResponse : PlayFabResultCommon
	{
		// Token: 0x04000475 RID: 1141
		public bool? AreAssetsReadonly;

		// Token: 0x04000476 RID: 1142
		public string BuildId;

		// Token: 0x04000477 RID: 1143
		public string BuildName;

		// Token: 0x04000478 RID: 1144
		public string BuildStatus;

		// Token: 0x04000479 RID: 1145
		public ContainerFlavor? ContainerFlavor;

		// Token: 0x0400047A RID: 1146
		public string ContainerRunCommand;

		// Token: 0x0400047B RID: 1147
		public DateTime? CreationTime;

		// Token: 0x0400047C RID: 1148
		public ContainerImageReference CustomGameContainerImage;

		// Token: 0x0400047D RID: 1149
		public List<AssetReference> GameAssetReferences;

		// Token: 0x0400047E RID: 1150
		public List<GameCertificateReference> GameCertificateReferences;

		// Token: 0x0400047F RID: 1151
		public InstrumentationConfiguration InstrumentationConfiguration;

		// Token: 0x04000480 RID: 1152
		public Dictionary<string, string> Metadata;

		// Token: 0x04000481 RID: 1153
		public int MultiplayerServerCountPerVm;

		// Token: 0x04000482 RID: 1154
		public string OsPlatform;

		// Token: 0x04000483 RID: 1155
		public List<Port> Ports;

		// Token: 0x04000484 RID: 1156
		public List<BuildRegion> RegionConfigurations;

		// Token: 0x04000485 RID: 1157
		public string ServerType;

		// Token: 0x04000486 RID: 1158
		public string StartMultiplayerServerCommand;

		// Token: 0x04000487 RID: 1159
		public bool? UseStreamingForAssetDownloads;

		// Token: 0x04000488 RID: 1160
		public AzureVmSize? VmSize;
	}
}
