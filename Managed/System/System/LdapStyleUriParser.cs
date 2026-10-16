using System;

namespace System
{
	/// <summary>A customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
	// Token: 0x02000166 RID: 358
	public class LdapStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
		// Token: 0x06000998 RID: 2456 RVA: 0x0002A2EB File Offset: 0x000284EB
		public LdapStyleUriParser()
			: base(UriParser.LdapUri.Flags)
		{
		}
	}
}
