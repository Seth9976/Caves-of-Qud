using System;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class SelectedSolutionsData
	{
		// Token: 0x04000033 RID: 51
		public SelectedSolutionsData.HostingModel SelectedHostingModel;

		// Token: 0x04000034 RID: 52
		public SelectedSolutionsData.NetcodeSolution SelectedNetcodeSolution;

		// Token: 0x02000013 RID: 19
		public enum HostingModel
		{
			// Token: 0x04000040 RID: 64
			None,
			// Token: 0x04000041 RID: 65
			ClientHosted,
			// Token: 0x04000042 RID: 66
			DedicatedServer,
			// Token: 0x04000043 RID: 67
			CloudCode,
			// Token: 0x04000044 RID: 68
			DistributedAuthority
		}

		// Token: 0x02000014 RID: 20
		public enum NetcodeSolution
		{
			// Token: 0x04000046 RID: 70
			None,
			// Token: 0x04000047 RID: 71
			NGO,
			// Token: 0x04000048 RID: 72
			N4E,
			// Token: 0x04000049 RID: 73
			CustomNetcode,
			// Token: 0x0400004A RID: 74
			NoNetcode
		}
	}
}
