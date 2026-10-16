using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x02000479 RID: 1145
	internal class DetourNativeWindowsPlatform : IDetourNativePlatform
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x00055D89 File Offset: 0x00053F89
		public DetourNativeWindowsPlatform(IDetourNativePlatform inner)
		{
			this.Inner = inner;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00055D98 File Offset: 0x00053F98
		public void MakeWritable(IntPtr src, uint size)
		{
			DetourNativeWindowsPlatform.PAGE page;
			if (!DetourNativeWindowsPlatform.VirtualProtect(src, (IntPtr)((long)((ulong)size)), DetourNativeWindowsPlatform.PAGE.EXECUTE_READWRITE, out page))
			{
				throw this.LogAllSections(Marshal.GetLastWin32Error(), "MakeWriteable", src, size);
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00055DCC File Offset: 0x00053FCC
		public void MakeExecutable(IntPtr src, uint size)
		{
			DetourNativeWindowsPlatform.PAGE page;
			if (!DetourNativeWindowsPlatform.VirtualProtect(src, (IntPtr)((long)((ulong)size)), DetourNativeWindowsPlatform.PAGE.EXECUTE_READWRITE, out page))
			{
				throw this.LogAllSections(Marshal.GetLastWin32Error(), "MakeExecutable", src, size);
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00055E00 File Offset: 0x00054000
		public void MakeReadWriteExecutable(IntPtr src, uint size)
		{
			DetourNativeWindowsPlatform.PAGE page;
			if (!DetourNativeWindowsPlatform.VirtualProtect(src, (IntPtr)((long)((ulong)size)), DetourNativeWindowsPlatform.PAGE.EXECUTE_READWRITE, out page))
			{
				throw this.LogAllSections(Marshal.GetLastWin32Error(), "MakeExecutable", src, size);
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00055E33 File Offset: 0x00054033
		public void FlushICache(IntPtr src, uint size)
		{
			if (!DetourNativeWindowsPlatform.FlushInstructionCache(DetourNativeWindowsPlatform.GetCurrentProcess(), src, (UIntPtr)size))
			{
				throw this.LogAllSections(Marshal.GetLastWin32Error(), "FlushICache", src, size);
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00055E5C File Offset: 0x0005405C
		private unsafe Exception LogAllSections(int error, string from, IntPtr src, uint size)
		{
			Exception ex = new Win32Exception(error);
			if (MMDbgLog.Writer == null)
			{
				return ex;
			}
			MMDbgLog.Log(string.Format("{0} failed for 0x{1:X16} + {2} - logging all memory sections", from, (long)src, size));
			MMDbgLog.Log("reason: " + ex.Message);
			try
			{
				IntPtr currentProcess = DetourNativeWindowsPlatform.GetCurrentProcess();
				IntPtr intPtr = (IntPtr)65536;
				int num = 0;
				DetourNativeWindowsPlatform.MEMORY_BASIC_INFORMATION memory_BASIC_INFORMATION;
				while (DetourNativeWindowsPlatform.VirtualQueryEx(currentProcess, intPtr, out memory_BASIC_INFORMATION, sizeof(DetourNativeWindowsPlatform.MEMORY_BASIC_INFORMATION)) != 0)
				{
					ulong num2 = (ulong)(long)src;
					ulong num3 = num2 + (ulong)size;
					long num4 = (long)memory_BASIC_INFORMATION.BaseAddress;
					ulong num5 = (ulong)(num4 + (long)memory_BASIC_INFORMATION.RegionSize);
					bool flag = num4 <= (long)num3 && num2 <= num5;
					MMDbgLog.Log(string.Format("{0} #{1}", flag ? "*" : "-", num++));
					MMDbgLog.Log(string.Format("addr: 0x{0:X16}", (long)memory_BASIC_INFORMATION.BaseAddress));
					MMDbgLog.Log(string.Format("size: 0x{0:X16}", (long)memory_BASIC_INFORMATION.RegionSize));
					MMDbgLog.Log(string.Format("aaddr: 0x{0:X16}", (long)memory_BASIC_INFORMATION.AllocationBase));
					MMDbgLog.Log(string.Format("state: {0}", memory_BASIC_INFORMATION.State));
					MMDbgLog.Log(string.Format("type: {0}", memory_BASIC_INFORMATION.Type));
					MMDbgLog.Log(string.Format("protect: {0}", memory_BASIC_INFORMATION.Protect));
					MMDbgLog.Log(string.Format("aprotect: {0}", memory_BASIC_INFORMATION.AllocationProtect));
					try
					{
						IntPtr intPtr2 = intPtr;
						intPtr = (IntPtr)((long)memory_BASIC_INFORMATION.BaseAddress + (long)memory_BASIC_INFORMATION.RegionSize);
						if ((long)intPtr > (long)intPtr2)
						{
							continue;
						}
					}
					catch (OverflowException)
					{
						MMDbgLog.Log("overflow");
					}
					break;
				}
			}
			catch
			{
				throw ex;
			}
			return ex;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0005608C File Offset: 0x0005428C
		public NativeDetourData Create(IntPtr from, IntPtr to, byte? type)
		{
			return this.Inner.Create(from, to, type);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005609C File Offset: 0x0005429C
		public void Free(NativeDetourData detour)
		{
			this.Inner.Free(detour);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000560AA File Offset: 0x000542AA
		public void Apply(NativeDetourData detour)
		{
			this.Inner.Apply(detour);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000560B8 File Offset: 0x000542B8
		public void Copy(IntPtr src, IntPtr dst, byte type)
		{
			this.Inner.Copy(src, dst, type);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000560C8 File Offset: 0x000542C8
		public IntPtr MemAlloc(uint size)
		{
			return this.Inner.MemAlloc(size);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000560D6 File Offset: 0x000542D6
		public void MemFree(IntPtr ptr)
		{
			this.Inner.MemFree(ptr);
		}

		// Token: 0x060018B8 RID: 6328
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool VirtualProtect(IntPtr lpAddress, IntPtr dwSize, DetourNativeWindowsPlatform.PAGE flNewProtect, out DetourNativeWindowsPlatform.PAGE lpflOldProtect);

		// Token: 0x060018B9 RID: 6329
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x060018BA RID: 6330
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool FlushInstructionCache(IntPtr hProcess, IntPtr lpBaseAddress, UIntPtr dwSize);

		// Token: 0x060018BB RID: 6331
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out DetourNativeWindowsPlatform.MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);

		// Token: 0x04001231 RID: 4657
		private readonly IDetourNativePlatform Inner;

		// Token: 0x0200047A RID: 1146
		[Flags]
		private enum PAGE : uint
		{
			// Token: 0x04001233 RID: 4659
			UNSET = 0U,
			// Token: 0x04001234 RID: 4660
			NOACCESS = 1U,
			// Token: 0x04001235 RID: 4661
			READONLY = 2U,
			// Token: 0x04001236 RID: 4662
			READWRITE = 4U,
			// Token: 0x04001237 RID: 4663
			WRITECOPY = 8U,
			// Token: 0x04001238 RID: 4664
			EXECUTE = 16U,
			// Token: 0x04001239 RID: 4665
			EXECUTE_READ = 32U,
			// Token: 0x0400123A RID: 4666
			EXECUTE_READWRITE = 64U,
			// Token: 0x0400123B RID: 4667
			EXECUTE_WRITECOPY = 128U,
			// Token: 0x0400123C RID: 4668
			GUARD = 256U,
			// Token: 0x0400123D RID: 4669
			NOCACHE = 512U,
			// Token: 0x0400123E RID: 4670
			WRITECOMBINE = 1024U
		}

		// Token: 0x0200047B RID: 1147
		private enum MEM : uint
		{
			// Token: 0x04001240 RID: 4672
			UNSET,
			// Token: 0x04001241 RID: 4673
			MEM_COMMIT = 4096U,
			// Token: 0x04001242 RID: 4674
			MEM_RESERVE = 8192U,
			// Token: 0x04001243 RID: 4675
			MEM_FREE = 65536U,
			// Token: 0x04001244 RID: 4676
			MEM_PRIVATE = 131072U,
			// Token: 0x04001245 RID: 4677
			MEM_MAPPED = 262144U,
			// Token: 0x04001246 RID: 4678
			MEM_IMAGE = 16777216U
		}

		// Token: 0x0200047C RID: 1148
		private struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x04001247 RID: 4679
			public IntPtr BaseAddress;

			// Token: 0x04001248 RID: 4680
			public IntPtr AllocationBase;

			// Token: 0x04001249 RID: 4681
			public DetourNativeWindowsPlatform.PAGE AllocationProtect;

			// Token: 0x0400124A RID: 4682
			public IntPtr RegionSize;

			// Token: 0x0400124B RID: 4683
			public DetourNativeWindowsPlatform.MEM State;

			// Token: 0x0400124C RID: 4684
			public DetourNativeWindowsPlatform.PAGE Protect;

			// Token: 0x0400124D RID: 4685
			public DetourNativeWindowsPlatform.MEM Type;
		}
	}
}
