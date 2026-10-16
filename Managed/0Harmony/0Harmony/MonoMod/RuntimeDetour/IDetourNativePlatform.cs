using System;

namespace MonoMod.RuntimeDetour
{
	// Token: 0x02000468 RID: 1128
	internal interface IDetourNativePlatform
	{
		// Token: 0x06001850 RID: 6224
		NativeDetourData Create(IntPtr from, IntPtr to, byte? type = null);

		// Token: 0x06001851 RID: 6225
		void Free(NativeDetourData detour);

		// Token: 0x06001852 RID: 6226
		void Apply(NativeDetourData detour);

		// Token: 0x06001853 RID: 6227
		void Copy(IntPtr src, IntPtr dst, byte type);

		// Token: 0x06001854 RID: 6228
		void MakeWritable(IntPtr src, uint size);

		// Token: 0x06001855 RID: 6229
		void MakeExecutable(IntPtr src, uint size);

		// Token: 0x06001856 RID: 6230
		void MakeReadWriteExecutable(IntPtr src, uint size);

		// Token: 0x06001857 RID: 6231
		void FlushICache(IntPtr src, uint size);

		// Token: 0x06001858 RID: 6232
		IntPtr MemAlloc(uint size);

		// Token: 0x06001859 RID: 6233
		void MemFree(IntPtr ptr);
	}
}
