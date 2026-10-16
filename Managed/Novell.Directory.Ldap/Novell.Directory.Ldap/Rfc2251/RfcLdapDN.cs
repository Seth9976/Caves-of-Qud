using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006A RID: 106
	public class RfcLdapDN : RfcLdapString
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00011B77 File Offset: 0x0000FD77
		public RfcLdapDN(string s)
			: base(s)
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011B80 File Offset: 0x0000FD80
		[CLSCompliant(false)]
		public RfcLdapDN(sbyte[] s)
			: base(s)
		{
		}
	}
}
