using System;
using System.Runtime.InteropServices;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x0200047D RID: 1149
	internal class DetourNativeX86Platform : IDetourNativePlatform
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x000560E4 File Offset: 0x000542E4
		private static bool Is32Bit(long to)
		{
			return (to & 2147483647L) == to;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000560F4 File Offset: 0x000542F4
		private unsafe static DetourNativeX86Platform.DetourType GetDetourType(IntPtr from, IntPtr to, ref IntPtr extra)
		{
			long num = (long)to - ((long)from + 5L);
			if ((DetourNativeX86Platform.Is32Bit(num) || DetourNativeX86Platform.Is32Bit(-num)) && ((byte*)(void*)from)[5] != 95)
			{
				return DetourNativeX86Platform.DetourType.Rel32;
			}
			if (DetourNativeX86Platform.Is32Bit((long)to))
			{
				return DetourNativeX86Platform.DetourType.Abs32;
			}
			IDetourRuntimePlatform runtime = DetourHelper.Runtime;
			if (((runtime != null) ? runtime.TryMemAllocScratchCloseTo(from, out extra, 8) : 0U) >= 8U)
			{
				num = (long)extra - ((long)from + 6L);
				if (DetourNativeX86Platform.Is32Bit(num) || DetourNativeX86Platform.Is32Bit(-num))
				{
					return DetourNativeX86Platform.DetourType.Abs64Split;
				}
			}
			return DetourNativeX86Platform.DetourType.Abs64;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00056180 File Offset: 0x00054380
		public NativeDetourData Create(IntPtr from, IntPtr to, byte? type)
		{
			NativeDetourData nativeDetourData = new NativeDetourData
			{
				Method = from,
				Target = to
			};
			nativeDetourData.Size = DetourNativeX86Platform.DetourSizes[(int)(nativeDetourData.Type = type ?? ((byte)DetourNativeX86Platform.GetDetourType(from, to, ref nativeDetourData.Extra)))];
			return nativeDetourData;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000561E1 File Offset: 0x000543E1
		public void Free(NativeDetourData detour)
		{
			byte type = detour.Type;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000561EC File Offset: 0x000543EC
		public void Apply(NativeDetourData detour)
		{
			int num = 0;
			switch (detour.Type)
			{
			case 0:
				detour.Method.Write(ref num, 233);
				detour.Method.Write(ref num, (uint)((int)((long)detour.Target - ((long)detour.Method + (long)num + 4L))));
				return;
			case 1:
				detour.Method.Write(ref num, 104);
				detour.Method.Write(ref num, (uint)(int)detour.Target);
				detour.Method.Write(ref num, 195);
				return;
			case 2:
			case 3:
				detour.Method.Write(ref num, byte.MaxValue);
				detour.Method.Write(ref num, 37);
				if (detour.Type == 3)
				{
					detour.Method.Write(ref num, (uint)((int)((long)detour.Extra - ((long)detour.Method + (long)num + 4L))));
					num = 0;
					detour.Extra.Write(ref num, (ulong)(long)detour.Target);
					return;
				}
				detour.Method.Write(ref num, 0U);
				detour.Method.Write(ref num, (ulong)(long)detour.Target);
				return;
			default:
				throw new NotSupportedException(string.Format("Unknown detour type {0}", detour.Type));
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00056348 File Offset: 0x00054548
		public unsafe void Copy(IntPtr src, IntPtr dst, byte type)
		{
			switch (type)
			{
			case 0:
				*(UIntPtr)(long)dst = (int)(*(UIntPtr)(long)src);
				*(UIntPtr)((long)dst + 4L) = *(UIntPtr)((long)src + 4L);
				return;
			case 1:
			case 3:
				*(UIntPtr)(long)dst = (int)(*(UIntPtr)(long)src);
				*(UIntPtr)((long)dst + 4L) = (short)(*(UIntPtr)((long)src + 4L));
				return;
			case 2:
				*(UIntPtr)(long)dst = *(UIntPtr)(long)src;
				*(UIntPtr)((long)dst + 8L) = (int)(*(UIntPtr)((long)src + 8L));
				*(UIntPtr)((long)dst + 12L) = (short)(*(UIntPtr)((long)src + 12L));
				return;
			default:
				throw new NotSupportedException(string.Format("Unknown detour type {0}", type));
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00018659 File Offset: 0x00016859
		public void MakeWritable(IntPtr src, uint size)
		{
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00018659 File Offset: 0x00016859
		public void MakeExecutable(IntPtr src, uint size)
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00018659 File Offset: 0x00016859
		public void MakeReadWriteExecutable(IntPtr src, uint size)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00018659 File Offset: 0x00016859
		public void FlushICache(IntPtr src, uint size)
		{
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000559A9 File Offset: 0x00053BA9
		public IntPtr MemAlloc(uint size)
		{
			return Marshal.AllocHGlobal((int)size);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000559B1 File Offset: 0x00053BB1
		public void MemFree(IntPtr ptr)
		{
			Marshal.FreeHGlobal(ptr);
		}

		// Token: 0x0400124E RID: 4686
		private static readonly uint[] DetourSizes = new uint[] { 5U, 6U, 14U, 6U };

		// Token: 0x0200047E RID: 1150
		public enum DetourType : byte
		{
			// Token: 0x04001250 RID: 4688
			Rel32,
			// Token: 0x04001251 RID: 4689
			Abs32,
			// Token: 0x04001252 RID: 4690
			Abs64,
			// Token: 0x04001253 RID: 4691
			Abs64Split
		}
	}
}
