using System;

namespace System.Net.NetworkInformation
{
	/// <summary>The scope level for an IPv6 address.</summary>
	// Token: 0x02000702 RID: 1794
	public enum ScopeLevel
	{
		/// <summary>The scope level is not specified.</summary>
		// Token: 0x040021D9 RID: 8665
		None,
		/// <summary>The scope is interface-level.</summary>
		// Token: 0x040021DA RID: 8666
		Interface,
		/// <summary>The scope is link-level.</summary>
		// Token: 0x040021DB RID: 8667
		Link,
		/// <summary>The scope is subnet-level.</summary>
		// Token: 0x040021DC RID: 8668
		Subnet,
		/// <summary>The scope is admin-level.</summary>
		// Token: 0x040021DD RID: 8669
		Admin,
		/// <summary>The scope is site-level.</summary>
		// Token: 0x040021DE RID: 8670
		Site,
		/// <summary>The scope is organization-level.</summary>
		// Token: 0x040021DF RID: 8671
		Organization = 8,
		/// <summary>The scope is global.</summary>
		// Token: 0x040021E0 RID: 8672
		Global = 14
	}
}
