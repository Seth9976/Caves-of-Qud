using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using MonoMod.Utils;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x02000471 RID: 1137
	internal class DetourNativeMonoPlatform : IDetourNativePlatform
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x00055B30 File Offset: 0x00053D30
		public DetourNativeMonoPlatform(IDetourNativePlatform inner, string libmono)
		{
			this.Inner = inner;
			Dictionary<string, List<DynDllMapping>> dictionary = new Dictionary<string, List<DynDllMapping>>();
			if (!string.IsNullOrEmpty(libmono))
			{
				dictionary.Add("mono", new List<DynDllMapping> { libmono });
			}
			DynDll.ResolveDynDllImports(this, dictionary);
			this._Pagesize = (long)this.mono_pagesize();
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00055B90 File Offset: 0x00053D90
		private void SetMemPerms(IntPtr start, ulong len, DetourNativeMonoPlatform.MmapProts prot)
		{
			long pagesize = this._Pagesize;
			long num = (long)start & ~(pagesize - 1L);
			long num2 = ((long)start + (long)len + pagesize - 1L) & ~(pagesize - 1L);
			if (this.mono_mprotect((IntPtr)num, (IntPtr)(num2 - num), (int)prot) != 0 && Marshal.GetLastWin32Error() != 0)
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00055BEE File Offset: 0x00053DEE
		public void MakeWritable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeMonoPlatform.MmapProts.PROT_READ | DetourNativeMonoPlatform.MmapProts.PROT_WRITE | DetourNativeMonoPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00055BEE File Offset: 0x00053DEE
		public void MakeExecutable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeMonoPlatform.MmapProts.PROT_READ | DetourNativeMonoPlatform.MmapProts.PROT_WRITE | DetourNativeMonoPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00055BEE File Offset: 0x00053DEE
		public void MakeReadWriteExecutable(IntPtr src, uint size)
		{
			this.SetMemPerms(src, (ulong)size, DetourNativeMonoPlatform.MmapProts.PROT_READ | DetourNativeMonoPlatform.MmapProts.PROT_WRITE | DetourNativeMonoPlatform.MmapProts.PROT_EXEC);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00055BFA File Offset: 0x00053DFA
		public void FlushICache(IntPtr src, uint size)
		{
			this.Inner.FlushICache(src, size);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00055C09 File Offset: 0x00053E09
		public NativeDetourData Create(IntPtr from, IntPtr to, byte? type)
		{
			return this.Inner.Create(from, to, type);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00055C19 File Offset: 0x00053E19
		public void Free(NativeDetourData detour)
		{
			this.Inner.Free(detour);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00055C27 File Offset: 0x00053E27
		public void Apply(NativeDetourData detour)
		{
			this.Inner.Apply(detour);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00055C35 File Offset: 0x00053E35
		public void Copy(IntPtr src, IntPtr dst, byte type)
		{
			this.Inner.Copy(src, dst, type);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00055C45 File Offset: 0x00053E45
		public IntPtr MemAlloc(uint size)
		{
			return this.Inner.MemAlloc(size);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00055C53 File Offset: 0x00053E53
		public void MemFree(IntPtr ptr)
		{
			this.Inner.MemFree(ptr);
		}

		// Token: 0x040010BE RID: 4286
		private readonly IDetourNativePlatform Inner;

		// Token: 0x040010BF RID: 4287
		private readonly long _Pagesize;

		// Token: 0x040010C0 RID: 4288
		[DynDllImport("mono", new string[] { })]
		private DetourNativeMonoPlatform.d_mono_pagesize mono_pagesize;

		// Token: 0x040010C1 RID: 4289
		[DynDllImport("mono", new string[] { })]
		private DetourNativeMonoPlatform.d_mono_mprotect mono_mprotect;

		// Token: 0x02000472 RID: 1138
		// (Invoke) Token: 0x06001894 RID: 6292
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int d_mono_pagesize();

		// Token: 0x02000473 RID: 1139
		// (Invoke) Token: 0x06001898 RID: 6296
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = true)]
		private delegate int d_mono_mprotect(IntPtr addr, IntPtr length, int flags);

		// Token: 0x02000474 RID: 1140
		[Flags]
		private enum MmapProts
		{
			// Token: 0x040010C3 RID: 4291
			PROT_READ = 1,
			// Token: 0x040010C4 RID: 4292
			PROT_WRITE = 2,
			// Token: 0x040010C5 RID: 4293
			PROT_EXEC = 4,
			// Token: 0x040010C6 RID: 4294
			PROT_NONE = 0,
			// Token: 0x040010C7 RID: 4295
			PROT_GROWSDOWN = 16777216,
			// Token: 0x040010C8 RID: 4296
			PROT_GROWSUP = 33554432
		}
	}
}
