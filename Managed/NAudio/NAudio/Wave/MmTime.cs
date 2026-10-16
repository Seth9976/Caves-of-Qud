using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000176 RID: 374
	[StructLayout(2)]
	internal struct MmTime
	{
		// Token: 0x040008E9 RID: 2281
		public const int TIME_MS = 1;

		// Token: 0x040008EA RID: 2282
		public const int TIME_SAMPLES = 2;

		// Token: 0x040008EB RID: 2283
		public const int TIME_BYTES = 4;

		// Token: 0x040008EC RID: 2284
		[FieldOffset(0)]
		public uint wType;

		// Token: 0x040008ED RID: 2285
		[FieldOffset(4)]
		public uint ms;

		// Token: 0x040008EE RID: 2286
		[FieldOffset(4)]
		public uint sample;

		// Token: 0x040008EF RID: 2287
		[FieldOffset(4)]
		public uint cb;

		// Token: 0x040008F0 RID: 2288
		[FieldOffset(4)]
		public uint ticks;

		// Token: 0x040008F1 RID: 2289
		[FieldOffset(4)]
		public byte smpteHour;

		// Token: 0x040008F2 RID: 2290
		[FieldOffset(5)]
		public byte smpteMin;

		// Token: 0x040008F3 RID: 2291
		[FieldOffset(6)]
		public byte smpteSec;

		// Token: 0x040008F4 RID: 2292
		[FieldOffset(7)]
		public byte smpteFrame;

		// Token: 0x040008F5 RID: 2293
		[FieldOffset(8)]
		public byte smpteFps;

		// Token: 0x040008F6 RID: 2294
		[FieldOffset(9)]
		public byte smpteDummy;

		// Token: 0x040008F7 RID: 2295
		[FieldOffset(10)]
		public byte smptePad0;

		// Token: 0x040008F8 RID: 2296
		[FieldOffset(11)]
		public byte smptePad1;

		// Token: 0x040008F9 RID: 2297
		[FieldOffset(4)]
		public uint midiSongPtrPos;
	}
}
