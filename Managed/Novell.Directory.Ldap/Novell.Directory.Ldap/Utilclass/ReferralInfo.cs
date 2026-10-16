using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004B RID: 75
	public class ReferralInfo
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000E40A File Offset: 0x0000C60A
		public virtual LdapUrl ReferralUrl
		{
			get
			{
				return this.referralUrl;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000E412 File Offset: 0x0000C612
		public virtual LdapConnection ReferralConnection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000E41A File Offset: 0x0000C61A
		public virtual string[] ReferralList
		{
			get
			{
				return this.referralList;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E422 File Offset: 0x0000C622
		public ReferralInfo(LdapConnection lc, string[] refList, LdapUrl refUrl)
		{
			this.conn = lc;
			this.referralUrl = refUrl;
			this.referralList = refList;
		}

		// Token: 0x040001F0 RID: 496
		private LdapConnection conn;

		// Token: 0x040001F1 RID: 497
		private LdapUrl referralUrl;

		// Token: 0x040001F2 RID: 498
		private string[] referralList;
	}
}
