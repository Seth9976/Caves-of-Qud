using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Provides helper functions for the process host.</summary>
	// Token: 0x02000539 RID: 1337
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[SuppressUnmanagedCodeSecurity]
	[Guid("35f9c4c1-3800-4d17-99bc-018a62243687")]
	[ComImport]
	public interface IProcessHostSupportFunctions
	{
		/// <summary>Gets the properties from the application's metabase.</summary>
		/// <param name="appId">The ID of the application.</param>
		/// <param name="virtualPath">The root virtual path of the application.</param>
		/// <param name="physicalPath">The root physical path of the application.</param>
		/// <param name="siteName">The display name of the application.</param>
		/// <param name="siteId">The site ID.</param>
		// Token: 0x06003A39 RID: 14905
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void GetApplicationProperties([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, out string virtualPath, out string physicalPath, out string siteName, out string siteId);

		/// <summary>Gets the physical path of a relative URL.</summary>
		/// <param name="appId">The application ID.</param>
		/// <param name="virtualPath">The relative URL to map.</param>
		/// <param name="physicalPath">The physical path of the relative URL.</param>
		// Token: 0x06003A3A RID: 14906
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		void MapPath([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string virtualPath, out string physicalPath);

		/// <summary>Gets a Windows security token for the specified application's root directory.</summary>
		/// <returns>A Windows handle that contains a Windows security token for the specified application's root directory.</returns>
		/// <param name="appId">The unique identifier of the application.</param>
		// Token: 0x06003A3B RID: 14907
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetConfigToken([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		/// <summary>Gets the application host configuration (.config) file path.</summary>
		/// <returns>The physical path (including the file name) to the application host configuration file.</returns>
		// Token: 0x06003A3C RID: 14908
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetAppHostConfigFilename();

		/// <summary>Gets the physical path for the ApplicationHost.config file.</summary>
		/// <returns>The physical path for the ApplicationHost.config file.</returns>
		// Token: 0x06003A3D RID: 14909
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetRootWebConfigFilename();

		/// <summary>Retrieves the INativeConfigurationSystem interface.</summary>
		/// <returns>A pointer to the INativeConfigurationSystem interface.</returns>
		// Token: 0x06003A3E RID: 14910
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetNativeConfigurationSystem();
	}
}
