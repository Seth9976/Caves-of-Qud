using System;

namespace Mono.Cecil
{
	// Token: 0x02000236 RID: 566
	[Flags]
	public enum MethodAttributes : ushort
	{
		// Token: 0x04000390 RID: 912
		MemberAccessMask = 7,
		// Token: 0x04000391 RID: 913
		CompilerControlled = 0,
		// Token: 0x04000392 RID: 914
		Private = 1,
		// Token: 0x04000393 RID: 915
		FamANDAssem = 2,
		// Token: 0x04000394 RID: 916
		Assembly = 3,
		// Token: 0x04000395 RID: 917
		Family = 4,
		// Token: 0x04000396 RID: 918
		FamORAssem = 5,
		// Token: 0x04000397 RID: 919
		Public = 6,
		// Token: 0x04000398 RID: 920
		Static = 16,
		// Token: 0x04000399 RID: 921
		Final = 32,
		// Token: 0x0400039A RID: 922
		Virtual = 64,
		// Token: 0x0400039B RID: 923
		HideBySig = 128,
		// Token: 0x0400039C RID: 924
		VtableLayoutMask = 256,
		// Token: 0x0400039D RID: 925
		ReuseSlot = 0,
		// Token: 0x0400039E RID: 926
		NewSlot = 256,
		// Token: 0x0400039F RID: 927
		CheckAccessOnOverride = 512,
		// Token: 0x040003A0 RID: 928
		Abstract = 1024,
		// Token: 0x040003A1 RID: 929
		SpecialName = 2048,
		// Token: 0x040003A2 RID: 930
		PInvokeImpl = 8192,
		// Token: 0x040003A3 RID: 931
		UnmanagedExport = 8,
		// Token: 0x040003A4 RID: 932
		RTSpecialName = 4096,
		// Token: 0x040003A5 RID: 933
		HasSecurity = 16384,
		// Token: 0x040003A6 RID: 934
		RequireSecObject = 32768
	}
}
