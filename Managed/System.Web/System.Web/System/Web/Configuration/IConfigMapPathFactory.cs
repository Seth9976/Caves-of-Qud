using System;

namespace System.Web.Configuration
{
	/// <summary>Maps the configuration file virtual and physical paths.</summary>
	// Token: 0x02000564 RID: 1380
	public interface IConfigMapPathFactory
	{
		/// <summary>Creates the interface for the mapping between configuration-file virtual and physical paths. </summary>
		/// <returns>The <see cref="T:System.Web.Configuration.IConfigMapPath" /> object associated with the specified configuration-file path mapping.</returns>
		/// <param name="virtualPath">The configuration-file virtual path.</param>
		/// <param name="physicalPath">The configuration-file physical path.</param>
		// Token: 0x06003B1B RID: 15131
		IConfigMapPath Create(string virtualPath, string physicalPath);
	}
}
