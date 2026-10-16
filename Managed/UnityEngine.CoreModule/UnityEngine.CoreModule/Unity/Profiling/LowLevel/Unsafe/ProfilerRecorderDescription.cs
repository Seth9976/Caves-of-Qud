using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000076 RID: 118
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerRecorderDescription
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000047CD File Offset: 0x000029CD
		public ProfilerCategory Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000047D5 File Offset: 0x000029D5
		public MarkerFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000047DD File Offset: 0x000029DD
		public ProfilerMarkerDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000047E5 File Offset: 0x000029E5
		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				return this.unitType;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000047ED File Offset: 0x000029ED
		public int NameUtf8Len
		{
			get
			{
				return this.nameUtf8Len;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000047F5 File Offset: 0x000029F5
		public unsafe byte* NameUtf8
		{
			get
			{
				return this.nameUtf8;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000047FD File Offset: 0x000029FD
		public string Name
		{
			get
			{
				return ProfilerUnsafeUtility.Utf8ToString(this.nameUtf8, this.nameUtf8Len);
			}
		}

		// Token: 0x040001B7 RID: 439
		[FieldOffset(0)]
		private readonly ProfilerCategory category;

		// Token: 0x040001B8 RID: 440
		[FieldOffset(2)]
		private readonly MarkerFlags flags;

		// Token: 0x040001B9 RID: 441
		[FieldOffset(4)]
		private readonly ProfilerMarkerDataType dataType;

		// Token: 0x040001BA RID: 442
		[FieldOffset(5)]
		private readonly ProfilerMarkerDataUnit unitType;

		// Token: 0x040001BB RID: 443
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x040001BC RID: 444
		[FieldOffset(12)]
		private readonly int nameUtf8Len;

		// Token: 0x040001BD RID: 445
		[FieldOffset(16)]
		private unsafe readonly byte* nameUtf8;
	}
}
