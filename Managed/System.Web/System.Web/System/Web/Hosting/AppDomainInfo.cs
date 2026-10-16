using System;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides information about the application domain.</summary>
	// Token: 0x02000759 RID: 1881
	public class AppDomainInfo : IAppDomainInfo
	{
		// Token: 0x06004CDD RID: 19677 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal AppDomainInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the application domain ID.</summary>
		/// <returns>The unique application domain ID.</returns>
		// Token: 0x06004CDE RID: 19678 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetId()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the physical path of the application domain.</summary>
		/// <returns>The physical path of the application domain.</returns>
		// Token: 0x06004CDF RID: 19679 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetPhysicalPath()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the site ID of the application domain.</summary>
		/// <returns>The site ID of the application domain.</returns>
		// Token: 0x06004CE0 RID: 19680 RVA: 0x000C8830 File Offset: 0x000C6A30
		public int GetSiteId()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets the root virtual path of the application domain.</summary>
		/// <returns>The root virtual path of the application domain.</returns>
		// Token: 0x06004CE1 RID: 19681 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetVirtualPath()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the idle state of the application domain.</summary>
		/// <returns>true if the application domain is idle; otherwise, false.</returns>
		// Token: 0x06004CE2 RID: 19682 RVA: 0x000C884C File Offset: 0x000C6A4C
		public bool IsIdle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
