using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000135 RID: 309
	internal static class NativeMethods
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00013870 File Offset: 0x00011A70
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, SafeHandle hSourceHandle, HandleRef hTargetProcess, out SafeWaitHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			bool flag = false;
			bool flag3;
			try
			{
				hSourceHandle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				MonoIOError monoIOError;
				bool flag2 = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.DangerousGetHandle(), hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(monoIOError);
				}
				targetHandle = new SafeWaitHandle(intPtr, true);
				flag3 = flag2;
			}
			finally
			{
				if (flag)
				{
					hSourceHandle.DangerousRelease();
				}
			}
			return flag3;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000138E4 File Offset: 0x00011AE4
		public static bool DuplicateHandle(HandleRef hSourceProcessHandle, HandleRef hSourceHandle, HandleRef hTargetProcess, out SafeProcessHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions)
		{
			IntPtr intPtr;
			MonoIOError monoIOError;
			bool flag = MonoIO.DuplicateHandle(hSourceProcessHandle.Handle, hSourceHandle.Handle, hTargetProcess.Handle, out intPtr, dwDesiredAccess, bInheritHandle ? 1 : 0, dwOptions, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(monoIOError);
			}
			targetHandle = new SafeProcessHandle(intPtr, true);
			return flag;
		}

		// Token: 0x0600071D RID: 1821
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x0600071E RID: 1822
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetExitCodeProcess(IntPtr processHandle, out int exitCode);

		// Token: 0x0600071F RID: 1823 RVA: 0x00013930 File Offset: 0x00011B30
		public static bool GetExitCodeProcess(SafeProcessHandle processHandle, out int exitCode)
		{
			bool flag = false;
			bool exitCodeProcess;
			try
			{
				processHandle.DangerousAddRef(ref flag);
				exitCodeProcess = NativeMethods.GetExitCodeProcess(processHandle.DangerousGetHandle(), out exitCode);
			}
			finally
			{
				if (flag)
				{
					processHandle.DangerousRelease();
				}
			}
			return exitCodeProcess;
		}

		// Token: 0x06000720 RID: 1824
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TerminateProcess(IntPtr processHandle, int exitCode);

		// Token: 0x06000721 RID: 1825 RVA: 0x00013974 File Offset: 0x00011B74
		public static bool TerminateProcess(SafeProcessHandle processHandle, int exitCode)
		{
			bool flag = false;
			bool flag2;
			try
			{
				processHandle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.TerminateProcess(processHandle.DangerousGetHandle(), exitCode);
			}
			finally
			{
				if (flag)
				{
					processHandle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06000722 RID: 1826
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int WaitForInputIdle(IntPtr handle, int milliseconds);

		// Token: 0x06000723 RID: 1827 RVA: 0x000139B8 File Offset: 0x00011BB8
		public static int WaitForInputIdle(SafeProcessHandle handle, int milliseconds)
		{
			bool flag = false;
			int num;
			try
			{
				handle.DangerousAddRef(ref flag);
				num = NativeMethods.WaitForInputIdle(handle.DangerousGetHandle(), milliseconds);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06000724 RID: 1828
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetProcessWorkingSetSize(IntPtr handle, out IntPtr min, out IntPtr max);

		// Token: 0x06000725 RID: 1829 RVA: 0x000139FC File Offset: 0x00011BFC
		public static bool GetProcessWorkingSetSize(SafeProcessHandle handle, out IntPtr min, out IntPtr max)
		{
			bool flag = false;
			bool processWorkingSetSize;
			try
			{
				handle.DangerousAddRef(ref flag);
				processWorkingSetSize = NativeMethods.GetProcessWorkingSetSize(handle.DangerousGetHandle(), out min, out max);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return processWorkingSetSize;
		}

		// Token: 0x06000726 RID: 1830
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);

		// Token: 0x06000727 RID: 1831 RVA: 0x00013A40 File Offset: 0x00011C40
		public static bool SetProcessWorkingSetSize(SafeProcessHandle handle, IntPtr min, IntPtr max)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.SetProcessWorkingSetSize(handle.DangerousGetHandle(), min, max);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x06000728 RID: 1832
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetProcessTimes(IntPtr handle, out long creation, out long exit, out long kernel, out long user);

		// Token: 0x06000729 RID: 1833 RVA: 0x00013A84 File Offset: 0x00011C84
		public static bool GetProcessTimes(SafeProcessHandle handle, out long creation, out long exit, out long kernel, out long user)
		{
			bool flag = false;
			bool processTimes;
			try
			{
				handle.DangerousAddRef(ref flag);
				processTimes = NativeMethods.GetProcessTimes(handle.DangerousGetHandle(), out creation, out exit, out kernel, out user);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return processTimes;
		}

		// Token: 0x0600072A RID: 1834
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentProcessId();

		// Token: 0x0600072B RID: 1835
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetPriorityClass(IntPtr handle);

		// Token: 0x0600072C RID: 1836 RVA: 0x00013ACC File Offset: 0x00011CCC
		public static int GetPriorityClass(SafeProcessHandle handle)
		{
			bool flag = false;
			int priorityClass;
			try
			{
				handle.DangerousAddRef(ref flag);
				priorityClass = NativeMethods.GetPriorityClass(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return priorityClass;
		}

		// Token: 0x0600072D RID: 1837
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetPriorityClass(IntPtr handle, int priorityClass);

		// Token: 0x0600072E RID: 1838 RVA: 0x00013B0C File Offset: 0x00011D0C
		public static bool SetPriorityClass(SafeProcessHandle handle, int priorityClass)
		{
			bool flag = false;
			bool flag2;
			try
			{
				handle.DangerousAddRef(ref flag);
				flag2 = NativeMethods.SetPriorityClass(handle.DangerousGetHandle(), priorityClass);
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return flag2;
		}

		// Token: 0x0600072F RID: 1839
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CloseProcess(IntPtr handle);

		// Token: 0x04000519 RID: 1305
		public const int E_ABORT = -2147467260;

		// Token: 0x0400051A RID: 1306
		public const int PROCESS_TERMINATE = 1;

		// Token: 0x0400051B RID: 1307
		public const int PROCESS_CREATE_THREAD = 2;

		// Token: 0x0400051C RID: 1308
		public const int PROCESS_SET_SESSIONID = 4;

		// Token: 0x0400051D RID: 1309
		public const int PROCESS_VM_OPERATION = 8;

		// Token: 0x0400051E RID: 1310
		public const int PROCESS_VM_READ = 16;

		// Token: 0x0400051F RID: 1311
		public const int PROCESS_VM_WRITE = 32;

		// Token: 0x04000520 RID: 1312
		public const int PROCESS_DUP_HANDLE = 64;

		// Token: 0x04000521 RID: 1313
		public const int PROCESS_CREATE_PROCESS = 128;

		// Token: 0x04000522 RID: 1314
		public const int PROCESS_SET_QUOTA = 256;

		// Token: 0x04000523 RID: 1315
		public const int PROCESS_SET_INFORMATION = 512;

		// Token: 0x04000524 RID: 1316
		public const int PROCESS_QUERY_INFORMATION = 1024;

		// Token: 0x04000525 RID: 1317
		public const int PROCESS_QUERY_LIMITED_INFORMATION = 4096;

		// Token: 0x04000526 RID: 1318
		public const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000527 RID: 1319
		public const int SYNCHRONIZE = 1048576;

		// Token: 0x04000528 RID: 1320
		public const int PROCESS_ALL_ACCESS = 2035711;

		// Token: 0x04000529 RID: 1321
		public const int DUPLICATE_CLOSE_SOURCE = 1;

		// Token: 0x0400052A RID: 1322
		public const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x0400052B RID: 1323
		public const int STILL_ACTIVE = 259;

		// Token: 0x0400052C RID: 1324
		public const int WAIT_OBJECT_0 = 0;

		// Token: 0x0400052D RID: 1325
		public const int WAIT_FAILED = -1;

		// Token: 0x0400052E RID: 1326
		public const int WAIT_TIMEOUT = 258;

		// Token: 0x0400052F RID: 1327
		public const int WAIT_ABANDONED = 128;

		// Token: 0x04000530 RID: 1328
		public const int WAIT_ABANDONED_0 = 128;

		// Token: 0x04000531 RID: 1329
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000532 RID: 1330
		public const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x04000533 RID: 1331
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04000534 RID: 1332
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000535 RID: 1333
		public const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000536 RID: 1334
		public const int ERROR_INVALID_NAME = 123;

		// Token: 0x04000537 RID: 1335
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000538 RID: 1336
		public const int ERROR_FILENAME_EXCED_RANGE = 206;
	}
}
