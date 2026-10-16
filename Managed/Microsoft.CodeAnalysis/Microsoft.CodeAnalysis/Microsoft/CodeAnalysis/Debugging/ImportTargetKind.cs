using System;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x02000360 RID: 864
	internal enum ImportTargetKind
	{
		// Token: 0x04000FDC RID: 4060
		Namespace,
		// Token: 0x04000FDD RID: 4061
		Type,
		// Token: 0x04000FDE RID: 4062
		NamespaceOrType,
		// Token: 0x04000FDF RID: 4063
		Assembly,
		// Token: 0x04000FE0 RID: 4064
		XmlNamespace,
		// Token: 0x04000FE1 RID: 4065
		MethodToken,
		// Token: 0x04000FE2 RID: 4066
		CurrentNamespace,
		// Token: 0x04000FE3 RID: 4067
		DefaultNamespace,
		// Token: 0x04000FE4 RID: 4068
		Defunct
	}
}
