using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000079 RID: 121
	[Flags]
	public enum MethodAttributes : ushort
	{
		// Token: 0x04000140 RID: 320
		MemberAccessMask = 7,
		// Token: 0x04000141 RID: 321
		CompilerControlled = 0,
		// Token: 0x04000142 RID: 322
		Private = 1,
		// Token: 0x04000143 RID: 323
		FamANDAssem = 2,
		// Token: 0x04000144 RID: 324
		Assembly = 3,
		// Token: 0x04000145 RID: 325
		Family = 4,
		// Token: 0x04000146 RID: 326
		FamORAssem = 5,
		// Token: 0x04000147 RID: 327
		Public = 6,
		// Token: 0x04000148 RID: 328
		Static = 16,
		// Token: 0x04000149 RID: 329
		Final = 32,
		// Token: 0x0400014A RID: 330
		Virtual = 64,
		// Token: 0x0400014B RID: 331
		HideBySig = 128,
		// Token: 0x0400014C RID: 332
		VtableLayoutMask = 256,
		// Token: 0x0400014D RID: 333
		ReuseSlot = 0,
		// Token: 0x0400014E RID: 334
		NewSlot = 256,
		// Token: 0x0400014F RID: 335
		CheckAccessOnOverride = 512,
		// Token: 0x04000150 RID: 336
		Abstract = 1024,
		// Token: 0x04000151 RID: 337
		SpecialName = 2048,
		// Token: 0x04000152 RID: 338
		PInvokeImpl = 8192,
		// Token: 0x04000153 RID: 339
		UnmanagedExport = 8,
		// Token: 0x04000154 RID: 340
		RTSpecialName = 4096,
		// Token: 0x04000155 RID: 341
		HasSecurity = 16384,
		// Token: 0x04000156 RID: 342
		RequireSecObject = 32768
	}
}
