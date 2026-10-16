using System;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200015F RID: 351
	[Flags]
	internal enum AcmFormatChooseStyleFlags
	{
		// Token: 0x040007C9 RID: 1993
		None = 0,
		// Token: 0x040007CA RID: 1994
		ShowHelp = 4,
		// Token: 0x040007CB RID: 1995
		EnableHook = 8,
		// Token: 0x040007CC RID: 1996
		EnableTemplate = 16,
		// Token: 0x040007CD RID: 1997
		EnableTemplateHandle = 32,
		// Token: 0x040007CE RID: 1998
		InitToWfxStruct = 64,
		// Token: 0x040007CF RID: 1999
		ContextHelp = 128
	}
}
