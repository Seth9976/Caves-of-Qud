using System;

namespace NUnit.Framework
{
	// Token: 0x02000028 RID: 40
	[Flags]
	public enum ParallelScope
	{
		// Token: 0x04000027 RID: 39
		None = 0,
		// Token: 0x04000028 RID: 40
		Self = 1,
		// Token: 0x04000029 RID: 41
		Children = 2,
		// Token: 0x0400002A RID: 42
		Fixtures = 4
	}
}
