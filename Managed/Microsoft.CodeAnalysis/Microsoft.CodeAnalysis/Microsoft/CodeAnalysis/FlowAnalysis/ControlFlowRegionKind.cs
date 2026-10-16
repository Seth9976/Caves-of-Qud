using System;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002E0 RID: 736
	public enum ControlFlowRegionKind
	{
		// Token: 0x04000DD5 RID: 3541
		Root,
		// Token: 0x04000DD6 RID: 3542
		LocalLifetime,
		// Token: 0x04000DD7 RID: 3543
		Try,
		// Token: 0x04000DD8 RID: 3544
		Filter,
		// Token: 0x04000DD9 RID: 3545
		Catch,
		// Token: 0x04000DDA RID: 3546
		FilterAndHandler,
		// Token: 0x04000DDB RID: 3547
		TryAndCatch,
		// Token: 0x04000DDC RID: 3548
		Finally,
		// Token: 0x04000DDD RID: 3549
		TryAndFinally,
		// Token: 0x04000DDE RID: 3550
		StaticLocalInitializer,
		// Token: 0x04000DDF RID: 3551
		ErroneousBody
	}
}
