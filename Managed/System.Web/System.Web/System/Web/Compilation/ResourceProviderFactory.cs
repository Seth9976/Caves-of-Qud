using System;

namespace System.Web.Compilation
{
	/// <summary>Serves as the base class for classes that create resource providers.</summary>
	// Token: 0x02000662 RID: 1634
	public abstract class ResourceProviderFactory
	{
		/// <summary>When overridden in a derived class, creates a global resource provider. </summary>
		/// <returns>A global resource provider.</returns>
		/// <param name="classKey">The name of the resource class.</param>
		// Token: 0x060045F0 RID: 17904
		public abstract IResourceProvider CreateGlobalResourceProvider(string classKey);

		/// <summary>When overridden in a derived class, creates a local resource provider. </summary>
		/// <returns>A local resource provider.</returns>
		/// <param name="virtualPath">The path to a resource file.</param>
		// Token: 0x060045F1 RID: 17905
		public abstract IResourceProvider CreateLocalResourceProvider(string virtualPath);
	}
}
