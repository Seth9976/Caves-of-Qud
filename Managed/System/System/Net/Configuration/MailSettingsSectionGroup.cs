using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
	// Token: 0x0200076F RID: 1903
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the SMTP settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSection" /> object that contains configuration information for the local computer.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x000CD3B6 File Offset: 0x000CB5B6
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
