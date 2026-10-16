using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000516 RID: 1302
	[Flags]
	internal enum AttributeLocation : short
	{
		// Token: 0x04001B0C RID: 6924
		None = 0,
		// Token: 0x04001B0D RID: 6925
		Assembly = 1,
		// Token: 0x04001B0E RID: 6926
		Module = 2,
		// Token: 0x04001B0F RID: 6927
		Type = 4,
		// Token: 0x04001B10 RID: 6928
		Method = 8,
		// Token: 0x04001B11 RID: 6929
		Field = 16,
		// Token: 0x04001B12 RID: 6930
		Property = 32,
		// Token: 0x04001B13 RID: 6931
		Event = 64,
		// Token: 0x04001B14 RID: 6932
		Parameter = 128,
		// Token: 0x04001B15 RID: 6933
		Return = 256,
		// Token: 0x04001B16 RID: 6934
		TypeParameter = 512,
		// Token: 0x04001B17 RID: 6935
		Unknown = 1024
	}
}
