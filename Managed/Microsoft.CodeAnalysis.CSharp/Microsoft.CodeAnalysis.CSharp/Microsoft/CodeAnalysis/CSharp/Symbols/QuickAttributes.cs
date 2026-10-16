using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000527 RID: 1319
	[Flags]
	internal enum QuickAttributes : byte
	{
		// Token: 0x04001B3F RID: 6975
		None = 0,
		// Token: 0x04001B40 RID: 6976
		TypeIdentifier = 1,
		// Token: 0x04001B41 RID: 6977
		TypeForwardedTo = 2,
		// Token: 0x04001B42 RID: 6978
		AssemblyKeyName = 4,
		// Token: 0x04001B43 RID: 6979
		AssemblyKeyFile = 8,
		// Token: 0x04001B44 RID: 6980
		AssemblySignatureKey = 16
	}
}
