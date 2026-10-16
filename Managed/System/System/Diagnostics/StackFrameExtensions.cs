using System;

namespace System.Diagnostics
{
	// Token: 0x02000213 RID: 531
	public static class StackFrameExtensions
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x000449EB File Offset: 0x00042BEB
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000449FD File Offset: 0x00042BFD
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00044A0B File Offset: 0x00042C0B
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00044A19 File Offset: 0x00042C19
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00011E15 File Offset: 0x00010015
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00011E15 File Offset: 0x00010015
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}
	}
}
