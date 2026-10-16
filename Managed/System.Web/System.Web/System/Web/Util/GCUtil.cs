using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000118 RID: 280
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal static class GCUtil
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x000257DE File Offset: 0x000239DE
		public static IntPtr RootObject(object obj)
		{
			if (obj == null)
			{
				return IntPtr.Zero;
			}
			return (IntPtr)GCHandle.Alloc(obj);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000257F4 File Offset: 0x000239F4
		public static object UnrootObject(IntPtr pointer)
		{
			if (pointer != IntPtr.Zero)
			{
				GCHandle gchandle = (GCHandle)pointer;
				if (gchandle.IsAllocated)
				{
					object target = gchandle.Target;
					gchandle.Free();
					return target;
				}
			}
			return null;
		}
	}
}
