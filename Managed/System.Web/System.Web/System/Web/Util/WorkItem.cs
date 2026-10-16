using System;
using System.Security.Permissions;

namespace System.Web.Util
{
	/// <summary>Provides the ability to move work items to another thread for execution.</summary>
	// Token: 0x0200014D RID: 333
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WorkItem
	{
		/// <summary>Moves a work item to a separate thread for execution.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Util.WorkItemCallback" /> that represents the method that is to be called on a separate thread.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
		// Token: 0x06000EDC RID: 3804 RVA: 0x00027E40 File Offset: 0x00026040
		[global::System.MonoTODO("Not implemented, not currently supported by Mono")]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void Post(WorkItemCallback callback)
		{
			throw new PlatformNotSupportedException("Not supported on mono");
		}
	}
}
