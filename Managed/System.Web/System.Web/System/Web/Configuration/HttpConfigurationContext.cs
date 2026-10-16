using System;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Supplies current context information to configuration-section handlers in ASP.NET applications.</summary>
	// Token: 0x020005A4 RID: 1444
	public class HttpConfigurationContext
	{
		// Token: 0x06003DCD RID: 15821 RVA: 0x000A1EBB File Offset: 0x000A00BB
		internal HttpConfigurationContext(string virtualPath)
		{
			this.virtualPath = virtualPath;
		}

		/// <summary>Gets the virtual path to the Web.config configuration file.</summary>
		/// <returns>The virtual path to the Web.config file. Null when evaluating Machine.config; an empty string ("") when evaluating the root Web.config file for the site.</returns>
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000A1ECA File Offset: 0x000A00CA
		public string VirtualPath
		{
			get
			{
				return this.virtualPath;
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal HttpConfigurationContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021FB RID: 8699
		private string virtualPath;
	}
}
