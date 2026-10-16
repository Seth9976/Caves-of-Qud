using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000037 RID: 55
	public class LdapSearchResultReference : LdapMessage
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A634 File Offset: 0x00008834
		public virtual string[] Referrals
		{
			get
			{
				Asn1Object[] array = ((RfcSearchResultReference)this.message.Response).toArray();
				this.srefs = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.srefs[i] = ((Asn1OctetString)array[i]).stringValue();
				}
				return this.srefs;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A68E File Offset: 0x0000888E
		internal LdapSearchResultReference(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0400015C RID: 348
		private string[] srefs;

		// Token: 0x0400015D RID: 349
		private static object nameLock = new object();

		// Token: 0x0400015E RID: 350
		private static int refNum;

		// Token: 0x0400015F RID: 351
		private string name;
	}
}
