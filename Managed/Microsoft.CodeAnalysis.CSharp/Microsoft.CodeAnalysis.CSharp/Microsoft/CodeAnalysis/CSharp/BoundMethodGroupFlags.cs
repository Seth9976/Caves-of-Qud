using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B5 RID: 181
	[Flags]
	internal enum BoundMethodGroupFlags
	{
		// Token: 0x0400054C RID: 1356
		None = 0,
		// Token: 0x0400054D RID: 1357
		SearchExtensionMethods = 1,
		// Token: 0x0400054E RID: 1358
		HasImplicitReceiver = 2
	}
}
