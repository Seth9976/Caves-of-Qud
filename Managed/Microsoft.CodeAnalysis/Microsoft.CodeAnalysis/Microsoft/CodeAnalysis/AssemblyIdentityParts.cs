using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B4 RID: 436
	[Flags]
	public enum AssemblyIdentityParts
	{
		// Token: 0x0400057B RID: 1403
		Name = 1,
		// Token: 0x0400057C RID: 1404
		Version = 30,
		// Token: 0x0400057D RID: 1405
		VersionMajor = 2,
		// Token: 0x0400057E RID: 1406
		VersionMinor = 4,
		// Token: 0x0400057F RID: 1407
		VersionBuild = 8,
		// Token: 0x04000580 RID: 1408
		VersionRevision = 16,
		// Token: 0x04000581 RID: 1409
		Culture = 32,
		// Token: 0x04000582 RID: 1410
		PublicKey = 64,
		// Token: 0x04000583 RID: 1411
		PublicKeyToken = 128,
		// Token: 0x04000584 RID: 1412
		PublicKeyOrToken = 192,
		// Token: 0x04000585 RID: 1413
		Retargetability = 256,
		// Token: 0x04000586 RID: 1414
		ContentType = 512,
		// Token: 0x04000587 RID: 1415
		Unknown = 1024
	}
}
