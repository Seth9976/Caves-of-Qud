using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x0200046F RID: 1135
	internal class DetourNativeLibcPlatform : IDetourNativePlatform
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x00055A10 File Offset: 0x00053C10
		public DetourNativeLibcPlatform(IDetourNativePlatform inner)
		{
			this.Inner = inner;
			PropertyInfo property = typeof(Environment).GetProperty("SystemPageSize");
			if (property == null)
			{
				throw new NotSupportedException("Unsupported runtime");
			}
			this._Pagesize = (long)((int)property.GetValue(null, new object[0]));
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00055A6C File Offset: 0x00053C6C
		private void SetMemPerms(IntPtr start, ulong len, DetourNativeLibcPlatform.MmapProts prot)
		{
			long pagesize = this._Pagesize;
			long num = (long)start & ~(pagesize - 1L);
			long num2 = ((long)start + (long)len + pagesize - 1L) & ~(pagesize - 1L);
			if (DetourNativeLibcPlatform.mprotect((IntPtr)num, (IntPtr)(num2 - num), prot) != 0)
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00055ABD File Offset: 0x00053CBD
		public void MakeWritable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeLibcPlatform.MmapProts.PROT_READ | DetourNativeLibcPlatform.MmapProts.PROT_WRITE | DetourNativeLibcPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00055ABD File Offset: 0x00053CBD
		public void MakeExecutable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeLibcPlatform.MmapProts.PROT_READ | DetourNativeLibcPlatform.MmapProts.PROT_WRITE | DetourNativeLibcPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00055ABD File Offset: 0x00053CBD
		public void MakeReadWriteExecutable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeLibcPlatform.MmapProts.PROT_READ | DetourNativeLibcPlatform.MmapProts.PROT_WRITE | DetourNativeLibcPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00055AC9 File Offset: 0x00053CC9
		public void FlushICache(IntPtr src, uint size)
		{
			this.Inner.FlushICache(src, size);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00055AD8 File Offset: 0x00053CD8
		public NativeDetourData Create(IntPtr from, IntPtr to, byte? type)
		{
			return this.Inner.Create(from, to, type);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00055AE8 File Offset: 0x00053CE8
		public void Free(NativeDetourData detour)
		{
			this.Inner.Free(detour);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00055AF6 File Offset: 0x00053CF6
		public void Apply(NativeDetourData detour)
		{
			this.Inner.Apply(detour);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00055B04 File Offset: 0x00053D04
		public void Copy(IntPtr src, IntPtr dst, byte type)
		{
			this.Inner.Copy(src, dst, type);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00055B14 File Offset: 0x00053D14
		public IntPtr MemAlloc(uint size)
		{
			return this.Inner.MemAlloc(size);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00055B22 File Offset: 0x00053D22
		public void MemFree(IntPtr ptr)
		{
			this.Inner.MemFree(ptr);
		}

		// Token: 0x06001886 RID: 6278
		[DllImport("libc", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
		private static extern int mprotect(IntPtr start, IntPtr len, DetourNativeLibcPlatform.MmapProts prot);

		// Token: 0x040010B5 RID: 4277
		private readonly IDetourNativePlatform Inner;

		// Token: 0x040010B6 RID: 4278
		private readonly long _Pagesize;

		// Token: 0x02000470 RID: 1136
		[Flags]
		private enum MmapProts
		{
			// Token: 0x040010B8 RID: 4280
			PROT_READ = 1,
			// Token: 0x040010B9 RID: 4281
			PROT_WRITE = 2,
			// Token: 0x040010BA RID: 4282
			PROT_EXEC = 4,
			// Token: 0x040010BB RID: 4283
			PROT_NONE = 0,
			// Token: 0x040010BC RID: 4284
			PROT_GROWSDOWN = 16777216,
			// Token: 0x040010BD RID: 4285
			PROT_GROWSUP = 33554432
		}
	}
}
