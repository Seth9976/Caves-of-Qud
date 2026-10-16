using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the outcome of sending e-mail by using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000816 RID: 2070
	public enum SmtpStatusCode
	{
		/// <summary>The commands were sent in the incorrect sequence.</summary>
		// Token: 0x040027A8 RID: 10152
		BadCommandSequence = 503,
		/// <summary>The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at http://www.ietf.org.</summary>
		// Token: 0x040027A9 RID: 10153
		CannotVerifyUserWillAttemptDelivery = 252,
		/// <summary>The client was not authenticated or is not allowed to send mail using the specified SMTP host.</summary>
		// Token: 0x040027AA RID: 10154
		ClientNotPermitted = 454,
		/// <summary>The SMTP service does not implement the specified command.</summary>
		// Token: 0x040027AB RID: 10155
		CommandNotImplemented = 502,
		/// <summary>The SMTP service does not implement the specified command parameter.</summary>
		// Token: 0x040027AC RID: 10156
		CommandParameterNotImplemented = 504,
		/// <summary>The SMTP service does not recognize the specified command.</summary>
		// Token: 0x040027AD RID: 10157
		CommandUnrecognized = 500,
		/// <summary>The message is too large to be stored in the destination mailbox.</summary>
		// Token: 0x040027AE RID: 10158
		ExceededStorageAllocation = 552,
		/// <summary>The transaction could not occur. You receive this error when the specified SMTP host cannot be found.</summary>
		// Token: 0x040027AF RID: 10159
		GeneralFailure = -1,
		/// <summary>A Help message was returned by the service.</summary>
		// Token: 0x040027B0 RID: 10160
		HelpMessage = 214,
		/// <summary>The SMTP service does not have sufficient storage to complete the request.</summary>
		// Token: 0x040027B1 RID: 10161
		InsufficientStorage = 452,
		/// <summary>The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited e-mail (spam). For details, see RFC 2505, which is available at http://www.ietf.org.</summary>
		// Token: 0x040027B2 RID: 10162
		LocalErrorInProcessing = 451,
		/// <summary>The destination mailbox is in use.</summary>
		// Token: 0x040027B3 RID: 10163
		MailboxBusy = 450,
		/// <summary>The syntax used to specify the destination mailbox is incorrect.</summary>
		// Token: 0x040027B4 RID: 10164
		MailboxNameNotAllowed = 553,
		/// <summary>The destination mailbox was not found or could not be accessed.</summary>
		// Token: 0x040027B5 RID: 10165
		MailboxUnavailable = 550,
		/// <summary>The email was successfully sent to the SMTP service.</summary>
		// Token: 0x040027B6 RID: 10166
		Ok = 250,
		/// <summary>The SMTP service is closing the transmission channel.</summary>
		// Token: 0x040027B7 RID: 10167
		ServiceClosingTransmissionChannel = 221,
		/// <summary>The SMTP service is not available; the server is closing the transmission channel.</summary>
		// Token: 0x040027B8 RID: 10168
		ServiceNotAvailable = 421,
		/// <summary>The SMTP service is ready.</summary>
		// Token: 0x040027B9 RID: 10169
		ServiceReady = 220,
		/// <summary>The SMTP service is ready to receive the e-mail content.</summary>
		// Token: 0x040027BA RID: 10170
		StartMailInput = 354,
		/// <summary>The syntax used to specify a command or parameter is incorrect.</summary>
		// Token: 0x040027BB RID: 10171
		SyntaxError = 501,
		/// <summary>A system status or system Help reply.</summary>
		// Token: 0x040027BC RID: 10172
		SystemStatus = 211,
		/// <summary>The transaction failed.</summary>
		// Token: 0x040027BD RID: 10173
		TransactionFailed = 554,
		/// <summary>The user mailbox is not located on the receiving server. You should resend using the supplied address information.</summary>
		// Token: 0x040027BE RID: 10174
		UserNotLocalTryAlternatePath = 551,
		/// <summary>The user mailbox is not located on the receiving server; the server forwards the e-mail.</summary>
		// Token: 0x040027BF RID: 10175
		UserNotLocalWillForward = 251,
		/// <summary>The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.</summary>
		// Token: 0x040027C0 RID: 10176
		MustIssueStartTlsFirst = 530
	}
}
