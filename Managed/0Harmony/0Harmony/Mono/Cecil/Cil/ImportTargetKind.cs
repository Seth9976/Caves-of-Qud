using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002DD RID: 733
	public enum ImportTargetKind : byte
	{
		// Token: 0x04000972 RID: 2418
		ImportNamespace = 1,
		// Token: 0x04000973 RID: 2419
		ImportNamespaceInAssembly,
		// Token: 0x04000974 RID: 2420
		ImportType,
		// Token: 0x04000975 RID: 2421
		ImportXmlNamespaceWithAlias,
		// Token: 0x04000976 RID: 2422
		ImportAlias,
		// Token: 0x04000977 RID: 2423
		DefineAssemblyAlias,
		// Token: 0x04000978 RID: 2424
		DefineNamespaceAlias,
		// Token: 0x04000979 RID: 2425
		DefineNamespaceInAssemblyAlias,
		// Token: 0x0400097A RID: 2426
		DefineTypeAlias
	}
}
