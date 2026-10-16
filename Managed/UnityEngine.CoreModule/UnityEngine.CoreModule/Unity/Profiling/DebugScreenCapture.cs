using System;
using Unity.Collections;
using UnityEngine;

namespace Unity.Profiling
{
	// Token: 0x02000073 RID: 115
	public struct DebugScreenCapture
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00004789 File Offset: 0x00002989
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00004791 File Offset: 0x00002991
		public NativeArray<byte> RawImageDataReference { readonly get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000479A File Offset: 0x0000299A
		// (set) Token: 0x060001FF RID: 511 RVA: 0x000047A2 File Offset: 0x000029A2
		public TextureFormat ImageFormat { readonly get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000047AB File Offset: 0x000029AB
		// (set) Token: 0x06000201 RID: 513 RVA: 0x000047B3 File Offset: 0x000029B3
		public int Width { readonly get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000047BC File Offset: 0x000029BC
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000047C4 File Offset: 0x000029C4
		public int Height { readonly get; set; }
	}
}
