using System;

namespace System.Web.Management
{
	/// <summary>Specifies the ASP.Net features to install or remove using the methods provided by the <see cref="T:System.Web.Management.SqlServices" /> class.</summary>
	// Token: 0x02000529 RID: 1321
	[Flags]
	public enum SqlFeatures
	{
		/// <summary>No features.</summary>
		// Token: 0x04001F62 RID: 8034
		None = 0,
		/// <summary>The membership feature.</summary>
		// Token: 0x04001F63 RID: 8035
		Membership = 1,
		/// <summary>The profile feature.</summary>
		// Token: 0x04001F64 RID: 8036
		Profile = 2,
		/// <summary>The role manager feature.</summary>
		// Token: 0x04001F65 RID: 8037
		RoleManager = 4,
		/// <summary>The personalization feature.</summary>
		// Token: 0x04001F66 RID: 8038
		Personalization = 8,
		/// <summary>The Web event provider feature.</summary>
		// Token: 0x04001F67 RID: 8039
		SqlWebEventProvider = 16,
		/// <summary>All features.</summary>
		// Token: 0x04001F68 RID: 8040
		All = 1073741855
	}
}
