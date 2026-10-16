using System;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002D RID: 45
	public class LdapReferralException : LdapException
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000937B File Offset: 0x0000757B
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00009383 File Offset: 0x00007583
		public virtual string FailedReferral
		{
			get
			{
				return this.failedReferral;
			}
			set
			{
				this.failedReferral = value;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000938C File Offset: 0x0000758C
		public LdapReferralException()
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009394 File Offset: 0x00007594
		public LdapReferralException(string message)
			: base(message, 10, null)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000093A0 File Offset: 0x000075A0
		public LdapReferralException(string message, object[] arguments)
			: base(message, arguments, 10, null)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000093AD File Offset: 0x000075AD
		public LdapReferralException(string message, Exception rootException)
			: base(message, 10, null, rootException)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000093BA File Offset: 0x000075BA
		public LdapReferralException(string message, object[] arguments, Exception rootException)
			: base(message, arguments, 10, null, rootException)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000093C8 File Offset: 0x000075C8
		public LdapReferralException(string message, int resultCode, string serverMessage)
			: base(message, resultCode, serverMessage)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000093D3 File Offset: 0x000075D3
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage)
			: base(message, arguments, resultCode, serverMessage)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000093E0 File Offset: 0x000075E0
		public LdapReferralException(string message, int resultCode, string serverMessage, Exception rootException)
			: base(message, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000093ED File Offset: 0x000075ED
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage, Exception rootException)
			: base(message, arguments, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000093FC File Offset: 0x000075FC
		public virtual string[] getReferrals()
		{
			return this.referrals;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009404 File Offset: 0x00007604
		internal virtual void setReferrals(string[] urls)
		{
			this.referrals = urls;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009410 File Offset: 0x00007610
		public override string ToString()
		{
			string text = this.getExceptionString("LdapReferralException");
			if (this.failedReferral != null)
			{
				string text2 = ResourcesHandler.getMessage("FAILED_REFERRAL", new object[] { "LdapReferralException", this.failedReferral });
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = "LdapReferralException: Failed Referral: " + this.failedReferral;
				}
				text = text + "\n" + text2;
			}
			if (this.referrals != null)
			{
				for (int i = 0; i < this.referrals.Length; i++)
				{
					string text2 = ResourcesHandler.getMessage("REFERRAL_ITEM", new object[]
					{
						"LdapReferralException",
						this.referrals[i]
					});
					if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
					{
						text2 = "LdapReferralException: Referral: " + this.referrals[i];
					}
					text = text + "\n" + text2;
				}
			}
			return text;
		}

		// Token: 0x0400012E RID: 302
		private string failedReferral;

		// Token: 0x0400012F RID: 303
		private string[] referrals;
	}
}
