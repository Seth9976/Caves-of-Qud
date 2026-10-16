using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000033 RID: 51
	[Flags]
	public enum FieldAttributes : ushort
	{
		// Token: 0x04000064 RID: 100
		FieldAccessMask = 7,
		// Token: 0x04000065 RID: 101
		CompilerControlled = 0,
		// Token: 0x04000066 RID: 102
		Private = 1,
		// Token: 0x04000067 RID: 103
		FamANDAssem = 2,
		// Token: 0x04000068 RID: 104
		Assembly = 3,
		// Token: 0x04000069 RID: 105
		Family = 4,
		// Token: 0x0400006A RID: 106
		FamORAssem = 5,
		// Token: 0x0400006B RID: 107
		Public = 6,
		// Token: 0x0400006C RID: 108
		Static = 16,
		// Token: 0x0400006D RID: 109
		InitOnly = 32,
		// Token: 0x0400006E RID: 110
		Literal = 64,
		// Token: 0x0400006F RID: 111
		NotSerialized = 128,
		// Token: 0x04000070 RID: 112
		SpecialName = 512,
		// Token: 0x04000071 RID: 113
		PInvokeImpl = 8192,
		// Token: 0x04000072 RID: 114
		RTSpecialName = 1024,
		// Token: 0x04000073 RID: 115
		HasFieldMarshal = 4096,
		// Token: 0x04000074 RID: 116
		HasDefault = 32768,
		// Token: 0x04000075 RID: 117
		HasFieldRVA = 256
	}
}
