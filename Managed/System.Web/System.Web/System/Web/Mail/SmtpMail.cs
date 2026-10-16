using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Web.Mail
{
	/// <summary>Provides properties and methods for sending messages using the Collaboration Data Objects for Windows 2000 (CDOSYS) message component. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000FC RID: 252
	[Obsolete("The recommended alternative is System.Net.Mail.SmtpClient. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class SmtpMail
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x00002050 File Offset: 0x00000250
		private SmtpMail()
		{
		}

		/// <summary>Gets or sets the name of the SMTP relay mail server to use to send e-mail messages. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The name of the e-mail relay server. </returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00023B79 File Offset: 0x00021D79
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00023B80 File Offset: 0x00021D80
		public static string SmtpServer
		{
			get
			{
				return SmtpMail.smtpServer;
			}
			set
			{
				SmtpMail.smtpServer = value;
			}
		}

		/// <summary>Sends an e-mail message using arguments supplied in the properties of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="message">The <see cref="T:System.Web.Mail.MailMessage" /> to send. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The mail cannot be sent.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.Web.Mail.MailMessage)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
		// Token: 0x06000D48 RID: 3400 RVA: 0x00023B88 File Offset: 0x00021D88
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		public static void Send(MailMessage message)
		{
			try
			{
				MailMessageWrapper mailMessageWrapper = new MailMessageWrapper(message);
				SmtpClient smtpClient = new SmtpClient(SmtpMail.smtpServer);
				smtpClient.Send(mailMessageWrapper);
				smtpClient.Close();
			}
			catch (SmtpException ex)
			{
				throw new HttpException(ex.Message, ex);
			}
			catch (IOException ex2)
			{
				throw new HttpException(ex2.Message, ex2);
			}
			catch (FormatException ex3)
			{
				throw new HttpException(ex3.Message, ex3);
			}
			catch (SocketException ex4)
			{
				throw new HttpException(ex4.Message, ex4);
			}
		}

		/// <summary>Sends an e-mail message using the specified destination parameters. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <param name="from">The address of the e-mail sender. </param>
		/// <param name="to">The address of the e-mail recipient. </param>
		/// <param name="subject">The subject line of the e-mail message. </param>
		/// <param name="messageText">The body of the e-mail message. </param>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="M:System.Web.Mail.SmtpMail.Send(System.String,System.String,System.String,System.String)" /> method requires the Microsoft Windows NT, Windows 2000, or Windows XP operating system.</exception>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00023C24 File Offset: 0x00021E24
		public static void Send(string from, string to, string subject, string messageText)
		{
			SmtpMail.Send(new MailMessage
			{
				From = from,
				To = to,
				Subject = subject,
				Body = messageText
			});
		}

		// Token: 0x04001152 RID: 4434
		private static string smtpServer = "localhost";
	}
}
