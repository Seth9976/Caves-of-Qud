using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000115 RID: 277
	public enum ImportTargetKind : byte
	{
		// Token: 0x0400056C RID: 1388
		ImportNamespace = 1,
		// Token: 0x0400056D RID: 1389
		ImportNamespaceInAssembly,
		// Token: 0x0400056E RID: 1390
		ImportType,
		// Token: 0x0400056F RID: 1391
		ImportXmlNamespaceWithAlias,
		// Token: 0x04000570 RID: 1392
		ImportAlias,
		// Token: 0x04000571 RID: 1393
		DefineAssemblyAlias,
		// Token: 0x04000572 RID: 1394
		DefineNamespaceAlias,
		// Token: 0x04000573 RID: 1395
		DefineNamespaceInAssemblyAlias,
		// Token: 0x04000574 RID: 1396
		DefineTypeAlias
	}
}
