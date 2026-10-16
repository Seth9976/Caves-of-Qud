using System;

namespace Mono.Cecil
{
	// Token: 0x0200026C RID: 620
	[Flags]
	public enum TypeAttributes : uint
	{
		// Token: 0x04000509 RID: 1289
		VisibilityMask = 7U,
		// Token: 0x0400050A RID: 1290
		NotPublic = 0U,
		// Token: 0x0400050B RID: 1291
		Public = 1U,
		// Token: 0x0400050C RID: 1292
		NestedPublic = 2U,
		// Token: 0x0400050D RID: 1293
		NestedPrivate = 3U,
		// Token: 0x0400050E RID: 1294
		NestedFamily = 4U,
		// Token: 0x0400050F RID: 1295
		NestedAssembly = 5U,
		// Token: 0x04000510 RID: 1296
		NestedFamANDAssem = 6U,
		// Token: 0x04000511 RID: 1297
		NestedFamORAssem = 7U,
		// Token: 0x04000512 RID: 1298
		LayoutMask = 24U,
		// Token: 0x04000513 RID: 1299
		AutoLayout = 0U,
		// Token: 0x04000514 RID: 1300
		SequentialLayout = 8U,
		// Token: 0x04000515 RID: 1301
		ExplicitLayout = 16U,
		// Token: 0x04000516 RID: 1302
		ClassSemanticMask = 32U,
		// Token: 0x04000517 RID: 1303
		Class = 0U,
		// Token: 0x04000518 RID: 1304
		Interface = 32U,
		// Token: 0x04000519 RID: 1305
		Abstract = 128U,
		// Token: 0x0400051A RID: 1306
		Sealed = 256U,
		// Token: 0x0400051B RID: 1307
		SpecialName = 1024U,
		// Token: 0x0400051C RID: 1308
		Import = 4096U,
		// Token: 0x0400051D RID: 1309
		Serializable = 8192U,
		// Token: 0x0400051E RID: 1310
		WindowsRuntime = 16384U,
		// Token: 0x0400051F RID: 1311
		StringFormatMask = 196608U,
		// Token: 0x04000520 RID: 1312
		AnsiClass = 0U,
		// Token: 0x04000521 RID: 1313
		UnicodeClass = 65536U,
		// Token: 0x04000522 RID: 1314
		AutoClass = 131072U,
		// Token: 0x04000523 RID: 1315
		BeforeFieldInit = 1048576U,
		// Token: 0x04000524 RID: 1316
		RTSpecialName = 2048U,
		// Token: 0x04000525 RID: 1317
		HasSecurity = 262144U,
		// Token: 0x04000526 RID: 1318
		Forwarder = 2097152U
	}
}
