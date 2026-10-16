using System;

namespace System.Web.Configuration
{
	/// <summary>Defines whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date for forms authentication.</summary>
	// Token: 0x02000575 RID: 1397
	public enum TicketCompatibilityMode
	{
		/// <summary>Specifies that the ticket expiration date is stored as local time. This is the default value.</summary>
		// Token: 0x04002065 RID: 8293
		Framework20,
		/// <summary>Specifies that the ticket expiration date is stored as UTC.</summary>
		// Token: 0x04002066 RID: 8294
		Framework40
	}
}
