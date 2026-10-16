using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Specifies the target machine's CPU architecture.</summary>
	// Token: 0x0200002E RID: 46
	public enum Machine : ushort
	{
		/// <summary>The target CPU is unknown or not specified.</summary>
		// Token: 0x040000C6 RID: 198
		Unknown,
		/// <summary>Intel 386.</summary>
		// Token: 0x040000C7 RID: 199
		I386 = 332,
		/// <summary>MIPS little-endian WCE v2.</summary>
		// Token: 0x040000C8 RID: 200
		WceMipsV2 = 361,
		/// <summary>Alpha.</summary>
		// Token: 0x040000C9 RID: 201
		Alpha = 388,
		/// <summary>Hitachi SH3 little endian.</summary>
		// Token: 0x040000CA RID: 202
		SH3 = 418,
		/// <summary>Hitachi SH3 DSP.</summary>
		// Token: 0x040000CB RID: 203
		SH3Dsp,
		/// <summary>Hitachi SH3 little endian.</summary>
		// Token: 0x040000CC RID: 204
		SH3E,
		/// <summary>Hitachi SH4 little endian.</summary>
		// Token: 0x040000CD RID: 205
		SH4 = 422,
		/// <summary>Hitachi SH5.</summary>
		// Token: 0x040000CE RID: 206
		SH5 = 424,
		/// <summary>ARM little endian.</summary>
		// Token: 0x040000CF RID: 207
		Arm = 448,
		/// <summary>Thumb.</summary>
		// Token: 0x040000D0 RID: 208
		Thumb = 450,
		/// <summary>ARM Thumb-2 little endian.</summary>
		// Token: 0x040000D1 RID: 209
		ArmThumb2 = 452,
		/// <summary>Matsushita AM33.</summary>
		// Token: 0x040000D2 RID: 210
		AM33 = 467,
		/// <summary>IBM PowerPC little endian.</summary>
		// Token: 0x040000D3 RID: 211
		PowerPC = 496,
		/// <summary>PowerPCFP.</summary>
		// Token: 0x040000D4 RID: 212
		PowerPCFP,
		/// <summary>Intel 64.</summary>
		// Token: 0x040000D5 RID: 213
		IA64 = 512,
		/// <summary>MIPS.</summary>
		// Token: 0x040000D6 RID: 214
		MIPS16 = 614,
		/// <summary>ALPHA64.</summary>
		// Token: 0x040000D7 RID: 215
		Alpha64 = 644,
		/// <summary>MIPS with FPU.</summary>
		// Token: 0x040000D8 RID: 216
		MipsFpu = 870,
		/// <summary>MIPS16 with FPU.</summary>
		// Token: 0x040000D9 RID: 217
		MipsFpu16 = 1126,
		/// <summary>Infineon.</summary>
		// Token: 0x040000DA RID: 218
		Tricore = 1312,
		/// <summary>EFI Byte Code.</summary>
		// Token: 0x040000DB RID: 219
		Ebc = 3772,
		/// <summary>AMD64 (K8).</summary>
		// Token: 0x040000DC RID: 220
		Amd64 = 34404,
		/// <summary>M32R little-endian.</summary>
		// Token: 0x040000DD RID: 221
		M32R = 36929,
		/// <summary>ARM64.</summary>
		// Token: 0x040000DE RID: 222
		Arm64 = 43620
	}
}
