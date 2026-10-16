using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000023 RID: 35
	public class LdapLocalException : LdapException
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00007BB6 File Offset: 0x00005DB6
		public LdapLocalException()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007BBE File Offset: 0x00005DBE
		public LdapLocalException(string messageOrKey, int resultCode)
			: base(messageOrKey, resultCode, null)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007BC9 File Offset: 0x00005DC9
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode)
			: base(messageOrKey, arguments, resultCode, null)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007BD5 File Offset: 0x00005DD5
		public LdapLocalException(string messageOrKey, int resultCode, Exception rootException)
			: base(messageOrKey, resultCode, null, rootException)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007BE1 File Offset: 0x00005DE1
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode, Exception rootException)
			: base(messageOrKey, arguments, resultCode, null, rootException)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007BEF File Offset: 0x00005DEF
		public override string ToString()
		{
			return this.getExceptionString("LdapLocalException");
		}
	}
}
