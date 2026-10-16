using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	/// <summary>Creates a new <see cref="T:System.AppDomain" /> instance for the Web application. This class cannot be inherited. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
	// Token: 0x02000541 RID: 1345
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AppDomainFactory : IAppDomainFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppDomainFactory" /> class. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
		// Token: 0x06003A4B RID: 14923 RVA: 0x00002050 File Offset: 0x00000250
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public AppDomainFactory()
		{
		}

		/// <summary>Returns a new application domain for the specified Web application. This class was used by earlier versions of the .NET Framework than version 2.0, which uses the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class instead.</summary>
		/// <returns>A new application domain.</returns>
		/// <param name="module">The module containing the Web application.</param>
		/// <param name="typeName">The type of the Web application.</param>
		/// <param name="appId">The unique identifier for the Web application.</param>
		/// <param name="appPath">The path to the Web application's files.</param>
		/// <param name="strUrlOfAppOrigin">The URL of origin for the Web application.</param>
		/// <param name="iZone">The zone of origin for the Web application.</param>
		// Token: 0x06003A4C RID: 14924 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public object Create(string module, string typeName, string appId, string appPath, string strUrlOfAppOrigin, int iZone)
		{
			throw new NotImplementedException();
		}
	}
}
