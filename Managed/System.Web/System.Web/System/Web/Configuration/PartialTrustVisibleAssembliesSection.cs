using System;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures a set of partial-trust assemblies. This class cannot be inherited.</summary>
	// Token: 0x020006A7 RID: 1703
	public sealed class PartialTrustVisibleAssembliesSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembliesSection" /> class.</summary>
		// Token: 0x060047EC RID: 18412 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PartialTrustVisibleAssembliesSection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.PartialTrustVisibleAssembliesSection" /> collection of APTCA-marked assemblies to use in partial-trust ASP.NET applications.</summary>
		/// <returns>A collection of the APTCA-marked assemblies to use in partial-trust ASP.NET applications.</returns>
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public PartialTrustVisibleAssemblyCollection PartialTrustVisibleAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
