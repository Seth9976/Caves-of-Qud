using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000079 RID: 121
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerCategoryDescription
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000049FF File Offset: 0x00002BFF
		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.NameUtf8, this.NameUtf8Len);
			}
		}

		// Token: 0x040001C5 RID: 453
		[FieldOffset(0)]
		public readonly ushort Id;

		// Token: 0x040001C6 RID: 454
		[FieldOffset(2)]
		public readonly ushort Flags;

		// Token: 0x040001C7 RID: 455
		[FieldOffset(4)]
		public readonly Color32 Color;

		// Token: 0x040001C8 RID: 456
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x040001C9 RID: 457
		[FieldOffset(12)]
		public readonly int NameUtf8Len;

		// Token: 0x040001CA RID: 458
		[FieldOffset(16)]
		public unsafe readonly byte* NameUtf8;
	}
}
