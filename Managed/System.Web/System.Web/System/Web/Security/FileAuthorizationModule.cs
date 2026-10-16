using System;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>Verifies that the user has permission to access the file requested. This class cannot be inherited.</summary>
	// Token: 0x020004B8 RID: 1208
	[global::System.MonoTODO("that's only a stub")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FileAuthorizationModule : IHttpModule
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.FileAuthorizationModule" /> class.</summary>
		// Token: 0x06003646 RID: 13894 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public FileAuthorizationModule()
		{
		}

		/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FileAuthorizationModule" />.</summary>
		// Token: 0x06003647 RID: 13895 RVA: 0x00003946 File Offset: 0x00001B46
		public void Dispose()
		{
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FileAuthorizationModule" /> object.</summary>
		/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
		// Token: 0x06003648 RID: 13896 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void Init(HttpApplication app)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the user has access to the requested file.</summary>
		/// <returns>true if the current Windows user represented by <paramref name="token" /> has access to the file using the specified HTTP verb or if the <see cref="T:System.Web.Security.FileAuthorizationModule" /> module is not defined in the application's configuration file; otherwise, false.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="token">A Windows access token representing the user.</param>
		/// <param name="verb">The HTTP verb used to make the request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.-or-<paramref name="token" /> is <see cref="F:System.IntPtr.Zero" />.-or-<paramref name="verb" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="virtualPath" /> is not in the application directory structure of the Web application.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="virtualPath" /> does not exist.</exception>
		// Token: 0x06003649 RID: 13897 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public static bool CheckFileAccessForUser(string virtualPath, IntPtr token, string verb)
		{
			throw new NotImplementedException();
		}
	}
}
