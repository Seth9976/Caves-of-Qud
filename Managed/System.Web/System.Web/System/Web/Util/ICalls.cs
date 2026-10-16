using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Web.Util
{
	// Token: 0x0200013B RID: 315
	internal class ICalls
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x00002050 File Offset: 0x00000250
		private ICalls()
		{
		}

		// Token: 0x06000E5F RID: 3679
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMachineConfigPath();

		// Token: 0x06000E60 RID: 3680
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetMachineInstallDirectory();

		// Token: 0x06000E61 RID: 3681
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetUnmanagedResourcesPtr(Assembly assembly, out IntPtr ptr, out int length);
	}
}
