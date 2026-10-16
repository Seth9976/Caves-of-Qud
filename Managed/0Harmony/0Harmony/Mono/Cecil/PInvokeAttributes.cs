using System;

namespace Mono.Cecil
{
	// Token: 0x02000256 RID: 598
	[Flags]
	public enum PInvokeAttributes : ushort
	{
		// Token: 0x0400049A RID: 1178
		NoMangle = 1,
		// Token: 0x0400049B RID: 1179
		CharSetMask = 6,
		// Token: 0x0400049C RID: 1180
		CharSetNotSpec = 0,
		// Token: 0x0400049D RID: 1181
		CharSetAnsi = 2,
		// Token: 0x0400049E RID: 1182
		CharSetUnicode = 4,
		// Token: 0x0400049F RID: 1183
		CharSetAuto = 6,
		// Token: 0x040004A0 RID: 1184
		SupportsLastError = 64,
		// Token: 0x040004A1 RID: 1185
		CallConvMask = 1792,
		// Token: 0x040004A2 RID: 1186
		CallConvWinapi = 256,
		// Token: 0x040004A3 RID: 1187
		CallConvCdecl = 512,
		// Token: 0x040004A4 RID: 1188
		CallConvStdCall = 768,
		// Token: 0x040004A5 RID: 1189
		CallConvThiscall = 1024,
		// Token: 0x040004A6 RID: 1190
		CallConvFastcall = 1280,
		// Token: 0x040004A7 RID: 1191
		BestFitMask = 48,
		// Token: 0x040004A8 RID: 1192
		BestFitEnabled = 16,
		// Token: 0x040004A9 RID: 1193
		BestFitDisabled = 32,
		// Token: 0x040004AA RID: 1194
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x040004AB RID: 1195
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x040004AC RID: 1196
		ThrowOnUnmappableCharDisabled = 8192
	}
}
