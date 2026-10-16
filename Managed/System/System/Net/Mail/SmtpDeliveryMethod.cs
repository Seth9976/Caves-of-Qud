using System;

namespace System.Net.Mail
{
	/// <summary>Specifies how email messages are delivered.</summary>
	// Token: 0x02000810 RID: 2064
	public enum SmtpDeliveryMethod
	{
		/// <summary>Email is sent through the network to an SMTP server.</summary>
		// Token: 0x0400279D RID: 10141
		Network,
		/// <summary>Email is copied to the directory specified by the <see cref="P:System.Net.Mail.SmtpClient.PickupDirectoryLocation" /> property for delivery by an external application.</summary>
		// Token: 0x0400279E RID: 10142
		SpecifiedPickupDirectory,
		/// <summary>Email is copied to the pickup directory used by a local Internet Information Services (IIS) for delivery.</summary>
		// Token: 0x0400279F RID: 10143
		PickupDirectoryFromIis
	}
}
