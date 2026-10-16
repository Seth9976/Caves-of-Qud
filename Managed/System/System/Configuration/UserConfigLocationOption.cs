using System;

namespace System.Configuration
{
	// Token: 0x020001AE RID: 430
	internal enum UserConfigLocationOption : uint
	{
		// Token: 0x0400077B RID: 1915
		Product = 32U,
		// Token: 0x0400077C RID: 1916
		Product_VersionMajor,
		// Token: 0x0400077D RID: 1917
		Product_VersionMinor,
		// Token: 0x0400077E RID: 1918
		Product_VersionBuild = 36U,
		// Token: 0x0400077F RID: 1919
		Product_VersionRevision = 40U,
		// Token: 0x04000780 RID: 1920
		Company_Product = 48U,
		// Token: 0x04000781 RID: 1921
		Company_Product_VersionMajor,
		// Token: 0x04000782 RID: 1922
		Company_Product_VersionMinor,
		// Token: 0x04000783 RID: 1923
		Company_Product_VersionBuild = 52U,
		// Token: 0x04000784 RID: 1924
		Company_Product_VersionRevision = 56U,
		// Token: 0x04000785 RID: 1925
		Evidence = 64U,
		// Token: 0x04000786 RID: 1926
		Other = 32768U
	}
}
