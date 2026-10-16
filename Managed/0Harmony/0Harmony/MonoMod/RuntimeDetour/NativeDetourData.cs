using System;

namespace MonoMod.RuntimeDetour
{
	// Token: 0x0200046B RID: 1131
	internal struct NativeDetourData
	{
		// Token: 0x040010A6 RID: 4262
		public IntPtr Method;

		// Token: 0x040010A7 RID: 4263
		public IntPtr Target;

		// Token: 0x040010A8 RID: 4264
		public byte Type;

		// Token: 0x040010A9 RID: 4265
		public uint Size;

		// Token: 0x040010AA RID: 4266
		public IntPtr Extra;
	}
}
