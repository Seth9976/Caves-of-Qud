using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200007F RID: 127
	[Flags]
	public enum MethodImplAttributes : ushort
	{
		// Token: 0x04000177 RID: 375
		CodeTypeMask = 3,
		// Token: 0x04000178 RID: 376
		IL = 0,
		// Token: 0x04000179 RID: 377
		Native = 1,
		// Token: 0x0400017A RID: 378
		OPTIL = 2,
		// Token: 0x0400017B RID: 379
		Runtime = 3,
		// Token: 0x0400017C RID: 380
		ManagedMask = 4,
		// Token: 0x0400017D RID: 381
		Unmanaged = 4,
		// Token: 0x0400017E RID: 382
		Managed = 0,
		// Token: 0x0400017F RID: 383
		ForwardRef = 16,
		// Token: 0x04000180 RID: 384
		PreserveSig = 128,
		// Token: 0x04000181 RID: 385
		InternalCall = 4096,
		// Token: 0x04000182 RID: 386
		Synchronized = 32,
		// Token: 0x04000183 RID: 387
		NoOptimization = 64,
		// Token: 0x04000184 RID: 388
		NoInlining = 8
	}
}
