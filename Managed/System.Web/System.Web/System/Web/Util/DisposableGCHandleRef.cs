using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000119 RID: 281
	internal class DisposableGCHandleRef<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002582E File Offset: 0x00023A2E
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public DisposableGCHandleRef(T t)
		{
			this._handle = GCHandle.Alloc(t);
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00025847 File Offset: 0x00023A47
		public T Target
		{
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				return (T)((object)this._handle.Target);
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00025859 File Offset: 0x00023A59
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public void Dispose()
		{
			this.Target.Dispose();
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
		}

		// Token: 0x040011B2 RID: 4530
		private GCHandle _handle;
	}
}
