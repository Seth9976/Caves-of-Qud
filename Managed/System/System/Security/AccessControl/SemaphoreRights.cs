using System;
using System.Runtime.InteropServices;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system semaphore objects.</summary>
	// Token: 0x02000293 RID: 659
	[Flags]
	[ComVisible(false)]
	public enum SemaphoreRights
	{
		/// <summary>The right to release a named semaphore.</summary>
		// Token: 0x04000BD0 RID: 3024
		Modify = 2,
		/// <summary>The right to delete a named semaphore.</summary>
		// Token: 0x04000BD1 RID: 3025
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named semaphore.</summary>
		// Token: 0x04000BD2 RID: 3026
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named semaphore.</summary>
		// Token: 0x04000BD3 RID: 3027
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named semaphore.</summary>
		// Token: 0x04000BD4 RID: 3028
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named semaphore.</summary>
		// Token: 0x04000BD5 RID: 3029
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named semaphore, and to modify its access rules and audit rules.</summary>
		// Token: 0x04000BD6 RID: 3030
		FullControl = 2031619
	}
}
