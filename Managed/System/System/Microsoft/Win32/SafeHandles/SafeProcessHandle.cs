using System;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000136 RID: 310
	[SuppressUnmanagedCodeSecurity]
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x00013B50 File Offset: 0x00011D50
		internal SafeProcessHandle()
			: base(true)
		{
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00013B59 File Offset: 0x00011D59
		internal SafeProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00013B69 File Offset: 0x00011D69
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00013B79 File Offset: 0x00011D79
		internal void InitialSetHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00013B82 File Offset: 0x00011D82
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseProcess(this.handle);
		}

		// Token: 0x04000539 RID: 1337
		internal static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);
	}
}
