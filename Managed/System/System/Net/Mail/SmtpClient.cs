using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace System.Net.Mail
{
	/// <summary>Allows applications to send e-mail by using the Simple Mail Transfer Protocol (SMTP).</summary>
	// Token: 0x02000807 RID: 2055
	[Obsolete("SmtpClient and its network of types are poorly designed, we strongly recommend you use https://github.com/jstedfast/MailKit and https://github.com/jstedfast/MimeKit instead")]
	public class SmtpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class by using configuration file settings. </summary>
		// Token: 0x060041B2 RID: 16818 RVA: 0x000E27EB File Offset: 0x000E09EB
		public SmtpClient()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server. </summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
		// Token: 0x060041B3 RID: 16819 RVA: 0x000E27F5 File Offset: 0x000E09F5
		public SmtpClient(string host)
			: this(host, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server and port.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
		/// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> cannot be less than zero.</exception>
		// Token: 0x060041B4 RID: 16820 RVA: 0x000E2800 File Offset: 0x000E0A00
		public SmtpClient(string host, int port)
		{
			SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			if (smtpSection != null)
			{
				this.host = smtpSection.Network.Host;
				this.port = smtpSection.Network.Port;
				this.enableSsl = smtpSection.Network.EnableSsl;
				this.TargetName = smtpSection.Network.TargetName;
				if (this.TargetName == null)
				{
					this.TargetName = "SMTPSVC/" + ((host != null) ? host : "");
				}
				if (smtpSection.Network.UserName != null)
				{
					string text = string.Empty;
					if (smtpSection.Network.Password != null)
					{
						text = smtpSection.Network.Password;
					}
					this.Credentials = new CCredentialsByHost(smtpSection.Network.UserName, text);
				}
				if (!string.IsNullOrEmpty(smtpSection.From))
				{
					this.defaultFrom = new MailAddress(smtpSection.From);
				}
			}
			if (!string.IsNullOrEmpty(host))
			{
				this.host = host;
			}
			if (port != 0)
			{
				this.port = port;
				return;
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
		}

		/// <summary>Specify which certificates should be used to establish the Secure Sockets Layer (SSL) connection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, holding one or more client certificates. The default value is derived from the mail configuration attributes in a configuration file.</returns>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x000E2931 File Offset: 0x000E0B31
		[MonoTODO("Client certificates not used")]
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the SPN to use for extended protection. The default value for this SPN is of the form "SMTPSVC/&lt;host&gt;" where &lt;host&gt; is the hostname of the SMTP mail server. </returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000E294C File Offset: 0x000E0B4C
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000E2954 File Offset: 0x000E0B54
		public string TargetName { get; set; }

		/// <summary>Gets or sets the credentials used to authenticate the sender.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentialsByHost" /> that represents the credentials to use for authentication; or null if no credentials have been specified.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x000E295D File Offset: 0x000E0B5D
		// (set) Token: 0x060041B9 RID: 16825 RVA: 0x000E2965 File Offset: 0x000E0B65
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckState();
				this.credentials = value;
			}
		}

		/// <summary>Specifies how outgoing email messages will be handled.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpDeliveryMethod" /> that indicates how email messages are delivered.</returns>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x000E2974 File Offset: 0x000E0B74
		// (set) Token: 0x060041BB RID: 16827 RVA: 0x000E297C File Offset: 0x000E0B7C
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
			set
			{
				this.CheckState();
				this.deliveryMethod = value;
			}
		}

		/// <summary>Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the connection.</summary>
		/// <returns>true if the <see cref="T:System.Net.Mail.SmtpClient" /> uses SSL; otherwise, false. The default is false.</returns>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x000E298B File Offset: 0x000E0B8B
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x000E2993 File Offset: 0x000E0B93
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckState();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets or sets the name or IP address of the host used for SMTP transactions.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name or IP address of the computer to use for SMTP transactions.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000E29A2 File Offset: 0x000E0BA2
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000E29AA File Offset: 0x000E0BAA
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("An empty string is not allowed.", "value");
				}
				this.CheckState();
				this.host = value;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the pickup directory for mail messages.</returns>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000E29DF File Offset: 0x000E0BDF
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000E29E7 File Offset: 0x000E0BE7
		public string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
			set
			{
				this.pickupDirectoryLocation = value;
			}
		}

		/// <summary>Gets or sets the port used for SMTP transactions.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the port number on the SMTP host. The default value is 25.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000E29F0 File Offset: 0x000E0BF0
		// (set) Token: 0x060041C3 RID: 16835 RVA: 0x000E29F8 File Offset: 0x000E0BF8
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.port = value;
			}
		}

		/// <summary>Gets or sets the delivery format used by <see cref="T:System.Net.Mail.SmtpClient" /> to send e-mail.  </summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.The delivery format used by <see cref="T:System.Net.Mail.SmtpClient" />.</returns>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x000E2A16 File Offset: 0x000E0C16
		// (set) Token: 0x060041C5 RID: 16837 RVA: 0x000E2A1E File Offset: 0x000E0C1E
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
			set
			{
				this.CheckState();
				this.deliveryFormat = value;
			}
		}

		/// <summary>Gets the network connection used to transmit the e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that connects to the <see cref="P:System.Net.Mail.SmtpClient.Host" /> property used for SMTP.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null or the empty string ("").-or-<see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public ServicePoint ServicePoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> call times out.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation was less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060041C7 RID: 16839 RVA: 0x000E2A2D File Offset: 0x000E0C2D
		// (set) Token: 0x060041C8 RID: 16840 RVA: 0x000E2A35 File Offset: 0x000E0C35
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an e-mail is being sent.</exception>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060041CA RID: 16842 RVA: 0x000E2A53 File Offset: 0x000E0C53
		public bool UseDefaultCredentials
		{
			get
			{
				return false;
			}
			[MonoNotSupported("no DefaultCredential support in Mono")]
			set
			{
				if (value)
				{
					throw new NotImplementedException("Default credentials are not supported");
				}
				this.CheckState();
			}
		}

		/// <summary>Occurs when an asynchronous e-mail send operation completes.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060041CB RID: 16843 RVA: 0x000E2A6C File Offset: 0x000E0C6C
		// (remove) Token: 0x060041CC RID: 16844 RVA: 0x000E2AA4 File Offset: 0x000E0CA4
		public event SendCompletedEventHandler SendCompleted;

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
		// Token: 0x060041CD RID: 16845 RVA: 0x000E2AD9 File Offset: 0x000E0CD9
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
		// Token: 0x060041CE RID: 16846 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO("Does nothing at the moment.")]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000E2AE2 File Offset: 0x000E0CE2
		private void CheckState()
		{
			if (this.messageInProcess != null)
			{
				throw new InvalidOperationException("Cannot set Timeout while Sending a message");
			}
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000E2AF8 File Offset: 0x000E0CF8
		private static string EncodeAddress(MailAddress address)
		{
			if (!string.IsNullOrEmpty(address.DisplayName))
			{
				string text = MailMessage.EncodeSubjectRFC2047(address.DisplayName, Encoding.UTF8);
				return string.Concat(new string[] { "\"", text, "\" <", address.Address, ">" });
			}
			return address.ToString();
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000E2B5C File Offset: 0x000E0D5C
		private static string EncodeAddresses(MailAddressCollection addresses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (MailAddress mailAddress in addresses)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SmtpClient.EncodeAddress(mailAddress));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000E2BCC File Offset: 0x000E0DCC
		private string EncodeSubjectRFC2047(MailMessage message)
		{
			return MailMessage.EncodeSubjectRFC2047(message.Subject, message.SubjectEncoding);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000E2BE0 File Offset: 0x000E0DE0
		private string EncodeBody(MailMessage message)
		{
			string body = message.Body;
			Encoding bodyEncoding = message.BodyEncoding;
			TransferEncoding contentTransferEncoding = message.ContentTransferEncoding;
			if (contentTransferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(bodyEncoding.GetBytes(body), Base64FormattingOptions.InsertLineBreaks);
			}
			if (contentTransferEncoding == TransferEncoding.SevenBit)
			{
				return body;
			}
			return this.ToQuotedPrintable(body, bodyEncoding);
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000E2C24 File Offset: 0x000E0E24
		private string EncodeBody(AlternateView av)
		{
			byte[] array = new byte[av.ContentStream.Length];
			av.ContentStream.Read(array, 0, array.Length);
			TransferEncoding transferEncoding = av.TransferEncoding;
			if (transferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks);
			}
			if (transferEncoding == TransferEncoding.SevenBit)
			{
				return Encoding.ASCII.GetString(array);
			}
			return this.ToQuotedPrintable(array);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000E2C7E File Offset: 0x000E0E7E
		private void EndSection(string section)
		{
			this.SendData(string.Format("--{0}--", section));
			this.SendData(string.Empty);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000E2C9C File Offset: 0x000E0E9C
		private string GenerateBoundary()
		{
			string text = SmtpClient.GenerateBoundary(this.boundaryIndex);
			this.boundaryIndex++;
			return text;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000E2CB8 File Offset: 0x000E0EB8
		private static string GenerateBoundary(int index)
		{
			return string.Format("--boundary_{0}_{1}", index, Guid.NewGuid().ToString("D"));
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000E2CE7 File Offset: 0x000E0EE7
		private bool IsError(SmtpClient.SmtpResponse status)
		{
			return status.StatusCode >= (SmtpStatusCode)400;
		}

		/// <summary>Raises the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains event data.</param>
		// Token: 0x060041D9 RID: 16857 RVA: 0x000E2CFC File Offset: 0x000E0EFC
		protected void OnSendCompleted(AsyncCompletedEventArgs e)
		{
			try
			{
				if (this.SendCompleted != null)
				{
					this.SendCompleted(this, e);
				}
			}
			finally
			{
				this.worker = null;
				this.user_async_state = null;
			}
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000E2D40 File Offset: 0x000E0F40
		private void CheckCancellation()
		{
			if (this.worker != null && this.worker.CancellationPending)
			{
				throw new SmtpClient.CancellationException();
			}
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000E2D60 File Offset: 0x000E0F60
		private SmtpClient.SmtpResponse Read()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				this.CheckCancellation();
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 <= 0)
				{
					break;
				}
				int num3 = num + num2 - 1;
				if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
				{
					int num4 = num3 - 3;
					while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
					{
						num4--;
					}
					flag = array[num4 + 4] == 32;
				}
				num += num2;
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
			}
			while (!flag);
			if (num > 0)
			{
				return SmtpClient.SmtpResponse.Parse(new ASCIIEncoding().GetString(array, 0, num - 1));
			}
			throw new IOException("Connection closed");
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000E2E34 File Offset: 0x000E1034
		private void ResetExtensions()
		{
			this.authMechs = SmtpClient.AuthMechs.None;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000E2E40 File Offset: 0x000E1040
		private void ParseExtensions(string extens)
		{
			foreach (string text in extens.Split('\n', StringSplitOptions.None))
			{
				if (text.Length >= 4)
				{
					string text2 = text.Substring(4);
					if (text2.StartsWith("AUTH ", StringComparison.Ordinal))
					{
						string[] array2 = text2.Split(' ', StringSplitOptions.None);
						for (int j = 1; j < array2.Length; j++)
						{
							string text3 = array2[j].Trim();
							if (!(text3 == "LOGIN"))
							{
								if (text3 == "PLAIN")
								{
									this.authMechs |= SmtpClient.AuthMechs.Plain;
								}
							}
							else
							{
								this.authMechs |= SmtpClient.AuthMechs.Login;
							}
						}
					}
				}
			}
		}

		/// <summary>Sends the specified message to an SMTP server for delivery.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.MailMessage.From" /> is null.-or- There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060041DE RID: 16862 RVA: 0x000E2EF4 File Offset: 0x000E10F4
		public void Send(MailMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.Network && (this.Host == null || this.Host.Trim().Length == 0))
			{
				throw new InvalidOperationException("The SMTP host was not specified");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
			{
				throw new NotSupportedException("IIS delivery is not supported");
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
			this.mutex.WaitOne();
			try
			{
				this.messageInProcess = message;
				if (this.deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
				{
					this.SendToFile(message);
				}
				else
				{
					this.SendInternal(message);
				}
			}
			catch (SmtpClient.CancellationException)
			{
			}
			catch (SmtpException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new SmtpException("Message could not be sent.", ex);
			}
			finally
			{
				this.mutex.ReleaseMutex();
				this.messageInProcess = null;
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000E2FE8 File Offset: 0x000E11E8
		private void SendInternal(MailMessage message)
		{
			this.CheckCancellation();
			try
			{
				this.client = new TcpClient(this.host, this.port);
				this.stream = this.client.GetStream();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				this.SendCore(message);
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				if (this.reader != null)
				{
					this.reader.Close();
				}
				if (this.stream != null)
				{
					this.stream.Close();
				}
				if (this.client != null)
				{
					this.client.Close();
				}
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000E30AC File Offset: 0x000E12AC
		private void SendToFile(MailMessage message)
		{
			if (!Path.IsPathRooted(this.pickupDirectoryLocation))
			{
				throw new SmtpException("Only absolute directories are allowed for pickup directory.");
			}
			string text = Path.Combine(this.pickupDirectoryLocation, Guid.NewGuid().ToString() + ".eml");
			try
			{
				this.writer = new StreamWriter(text);
				MailAddress from = message.From;
				if (from == null)
				{
					from = this.defaultFrom;
				}
				string text2 = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
				text2 = text2.Remove(text2.Length - 3, 1);
				this.SendHeader("Date", text2);
				this.SendHeader("From", SmtpClient.EncodeAddress(from));
				this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
				if (message.CC.Count > 0)
				{
					this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
				}
				this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
				foreach (string text3 in message.Headers.AllKeys)
				{
					this.SendHeader(text3, message.Headers[text3]);
				}
				this.AddPriorityHeader(message);
				this.boundaryIndex = 0;
				if (message.Attachments.Count > 0)
				{
					this.SendWithAttachments(message);
				}
				else
				{
					this.SendWithoutAttachments(message, null, false);
				}
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				this.writer = null;
			}
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000E324C File Offset: 0x000E144C
		private void SendCore(MailMessage message)
		{
			SmtpClient.SmtpResponse smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text = Dns.GetHostName();
			try
			{
				text = Dns.GetHostEntry(text).HostName;
			}
			catch (SocketException)
			{
			}
			smtpResponse = this.SendCommand("EHLO " + text);
			if (this.IsError(smtpResponse))
			{
				smtpResponse = this.SendCommand("HELO " + text);
				if (this.IsError(smtpResponse))
				{
					throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
				}
			}
			else
			{
				string description = smtpResponse.Description;
				if (description != null)
				{
					this.ParseExtensions(description);
				}
			}
			if (this.enableSsl)
			{
				this.InitiateSecureConnection();
				this.ResetExtensions();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				smtpResponse = this.SendCommand("EHLO " + text);
				if (this.IsError(smtpResponse))
				{
					smtpResponse = this.SendCommand("HELO " + text);
					if (this.IsError(smtpResponse))
					{
						throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
					}
				}
				else
				{
					string description2 = smtpResponse.Description;
					if (description2 != null)
					{
						this.ParseExtensions(description2);
					}
				}
			}
			if (this.authMechs != SmtpClient.AuthMechs.None)
			{
				this.Authenticate();
			}
			MailAddress mailAddress = message.Sender;
			if (mailAddress == null)
			{
				mailAddress = message.From;
			}
			if (mailAddress == null)
			{
				mailAddress = this.defaultFrom;
			}
			smtpResponse = this.SendCommand("MAIL FROM:<" + mailAddress.Address + ">");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			List<SmtpFailedRecipientException> list = new List<SmtpFailedRecipientException>();
			for (int i = 0; i < message.To.Count; i++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.To[i].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.To[i].Address));
				}
			}
			for (int j = 0; j < message.CC.Count; j++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.CC[j].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.CC[j].Address));
				}
			}
			for (int k = 0; k < message.Bcc.Count; k++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.Bcc[k].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.Bcc[k].Address));
				}
			}
			if (list.Count > 0)
			{
				throw new SmtpFailedRecipientsException("failed recipients", list.ToArray());
			}
			smtpResponse = this.SendCommand("DATA");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text2 = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
			text2 = text2.Remove(text2.Length - 3, 1);
			this.SendHeader("Date", text2);
			MailAddress from = message.From;
			if (from == null)
			{
				from = this.defaultFrom;
			}
			this.SendHeader("From", SmtpClient.EncodeAddress(from));
			this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
			if (message.CC.Count > 0)
			{
				this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
			}
			this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
			string text3 = "normal";
			switch (message.Priority)
			{
			case MailPriority.Normal:
				text3 = "normal";
				break;
			case MailPriority.Low:
				text3 = "non-urgent";
				break;
			case MailPriority.High:
				text3 = "urgent";
				break;
			}
			this.SendHeader("Priority", text3);
			if (message.Sender != null)
			{
				this.SendHeader("Sender", SmtpClient.EncodeAddress(message.Sender));
			}
			if (message.ReplyToList.Count > 0)
			{
				this.SendHeader("Reply-To", SmtpClient.EncodeAddresses(message.ReplyToList));
			}
			foreach (string text4 in message.Headers.AllKeys)
			{
				this.SendHeader(text4, MailMessage.EncodeSubjectRFC2047(message.Headers[text4], message.HeadersEncoding));
			}
			this.AddPriorityHeader(message);
			this.boundaryIndex = 0;
			if (message.Attachments.Count > 0)
			{
				this.SendWithAttachments(message);
			}
			else
			{
				this.SendWithoutAttachments(message, null, false);
			}
			this.SendDot();
			smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			try
			{
				smtpResponse = this.SendCommand("QUIT");
			}
			catch (IOException)
			{
			}
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipients" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The message could not be delivered to one or more of the recipients in <paramref name="recipients" />. </exception>
		// Token: 0x060041E2 RID: 16866 RVA: 0x000E3778 File Offset: 0x000E1978
		public void Send(string from, string recipients, string subject, string body)
		{
			this.Send(new MailMessage(from, recipients, subject, body));
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.</exception>
		// Token: 0x060041E3 RID: 16867 RVA: 0x000E378C File Offset: 0x000E198C
		public Task SendMailAsync(MailMessage message)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			SendCompletedEventHandler handler = null;
			handler = delegate(object s, AsyncCompletedEventArgs e)
			{
				SmtpClient.SendMailAsyncCompletedHandler(tcs, e, handler, this);
			};
			this.SendCompleted += handler;
			this.SendAsync(message, tcs);
			return tcs.Task;
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation. . The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipients" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060041E4 RID: 16868 RVA: 0x000E37EE File Offset: 0x000E19EE
		public Task SendMailAsync(string from, string recipients, string subject, string body)
		{
			return this.SendMailAsync(new MailMessage(from, recipients, subject, body));
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000E3800 File Offset: 0x000E1A00
		private static void SendMailAsyncCompletedHandler(TaskCompletionSource<object> source, AsyncCompletedEventArgs e, SendCompletedEventHandler handler, SmtpClient client)
		{
			if (source != e.UserState)
			{
				return;
			}
			client.SendCompleted -= handler;
			if (e.Error != null)
			{
				source.SetException(e.Error);
				return;
			}
			if (e.Cancelled)
			{
				source.SetCanceled();
				return;
			}
			source.SetResult(null);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000E383E File Offset: 0x000E1A3E
		private void SendDot()
		{
			this.writer.Write(".\r\n");
			this.writer.Flush();
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000E385C File Offset: 0x000E1A5C
		private void SendData(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				this.writer.Write("\r\n");
				this.writer.Flush();
				return;
			}
			StringReader stringReader = new StringReader(data);
			bool flag = this.deliveryMethod == SmtpDeliveryMethod.Network;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				this.CheckCancellation();
				if (flag && text.Length > 0 && text[0] == '.')
				{
					text = "." + text;
				}
				this.writer.Write(text);
				this.writer.Write("\r\n");
			}
			this.writer.Flush();
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes. </summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is null.-or-<see cref="P:System.Net.Mail.MailMessage.From" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.-or-The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060041E8 RID: 16872 RVA: 0x000E38FC File Offset: 0x000E1AFC
		public void SendAsync(MailMessage message, object userToken)
		{
			if (this.worker != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new BackgroundWorker();
			this.worker.DoWork += delegate(object o, DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					this.Send(message);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
					throw ex;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, RunWorkerCompletedEventArgs ea)
			{
				this.OnSendCompleted(new AsyncCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Sends an e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the address that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipient" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.-or- <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.-or-<see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to true, but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.-or-The message could not be delivered to one or more of the recipients in <paramref name="recipients" />.</exception>
		// Token: 0x060041E9 RID: 16873 RVA: 0x000E3981 File Offset: 0x000E1B81
		public void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			this.SendAsync(new MailMessage(from, recipients, subject, body), userToken);
		}

		/// <summary>Cancels an asynchronous operation to send an e-mail message.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x060041EA RID: 16874 RVA: 0x000E3995 File Offset: 0x000E1B95
		public void SendAsyncCancel()
		{
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000E39B8 File Offset: 0x000E1BB8
		private void AddPriorityHeader(MailMessage message)
		{
			MailPriority priority = message.Priority;
			if (priority != MailPriority.Low)
			{
				if (priority == MailPriority.High)
				{
					this.SendHeader("Priority", "Urgent");
					this.SendHeader("Importance", "high");
					this.SendHeader("X-Priority", "1");
					return;
				}
			}
			else
			{
				this.SendHeader("Priority", "Non-Urgent");
				this.SendHeader("Importance", "low");
				this.SendHeader("X-Priority", "5");
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000E3A38 File Offset: 0x000E1C38
		private void SendSimpleBody(MailMessage message)
		{
			this.SendHeader("Content-Type", message.BodyContentType.ToString());
			if (message.ContentTransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(message.ContentTransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(message));
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000E3A94 File Offset: 0x000E1C94
		private void SendBodylessSingleAlternate(AlternateView av)
		{
			this.SendHeader("Content-Type", av.ContentType.ToString());
			if (av.TransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(av.TransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(av));
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000E3AF0 File Offset: 0x000E1CF0
		private void SendWithoutAttachments(MailMessage message, string boundary, bool attachmentExists)
		{
			if (message.Body == null && message.AlternateViews.Count == 1)
			{
				this.SendBodylessSingleAlternate(message.AlternateViews[0]);
				return;
			}
			if (message.AlternateViews.Count > 0)
			{
				this.SendBodyWithAlternateViews(message, boundary, attachmentExists);
				return;
			}
			this.SendSimpleBody(message);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000E3B48 File Offset: 0x000E1D48
		private void SendWithAttachments(MailMessage message)
		{
			string text = this.GenerateBoundary();
			this.SendHeader("Content-Type", new ContentType
			{
				Boundary = text,
				MediaType = "multipart/mixed",
				CharSet = null
			}.ToString());
			this.SendData(string.Empty);
			Attachment attachment = null;
			if (message.AlternateViews.Count > 0)
			{
				this.SendWithoutAttachments(message, text, true);
			}
			else
			{
				attachment = Attachment.CreateAttachmentFromString(message.Body, null, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				message.Attachments.Insert(0, attachment);
			}
			try
			{
				this.SendAttachments(message, attachment, text);
			}
			finally
			{
				if (attachment != null)
				{
					message.Attachments.Remove(attachment);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000E3C1C File Offset: 0x000E1E1C
		private void SendBodyWithAlternateViews(MailMessage message, string boundary, bool attachmentExists)
		{
			AlternateViewCollection alternateViews = message.AlternateViews;
			string text = this.GenerateBoundary();
			ContentType contentType = new ContentType();
			contentType.Boundary = text;
			contentType.MediaType = "multipart/alternative";
			if (!attachmentExists)
			{
				this.SendHeader("Content-Type", contentType.ToString());
				this.SendData(string.Empty);
			}
			AlternateView alternateView = null;
			if (message.Body != null)
			{
				alternateView = AlternateView.CreateAlternateViewFromString(message.Body, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				alternateViews.Insert(0, alternateView);
				this.StartSection(boundary, contentType);
			}
			try
			{
				foreach (AlternateView alternateView2 in alternateViews)
				{
					string text2 = null;
					if (alternateView2.LinkedResources.Count > 0)
					{
						text2 = this.GenerateBoundary();
						ContentType contentType2 = new ContentType("multipart/related");
						contentType2.Boundary = text2;
						contentType2.Parameters["type"] = alternateView2.ContentType.ToString();
						this.StartSection(text, contentType2);
						this.StartSection(text2, alternateView2.ContentType, alternateView2);
					}
					else
					{
						ContentType contentType2 = new ContentType(alternateView2.ContentType.ToString());
						this.StartSection(text, contentType2, alternateView2);
					}
					switch (alternateView2.TransferEncoding)
					{
					case TransferEncoding.Unknown:
					case TransferEncoding.SevenBit:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Encoding.ASCII.GetString(array));
						break;
					}
					case TransferEncoding.QuotedPrintable:
					{
						byte[] array2 = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array2, 0, array2.Length);
						this.SendData(this.ToQuotedPrintable(array2));
						break;
					}
					case TransferEncoding.Base64:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
						break;
					}
					}
					if (alternateView2.LinkedResources.Count > 0)
					{
						this.SendLinkedResources(message, alternateView2.LinkedResources, text2);
						this.EndSection(text2);
					}
					if (!attachmentExists)
					{
						this.SendData(string.Empty);
					}
				}
			}
			finally
			{
				if (alternateView != null)
				{
					alternateViews.Remove(alternateView);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000E3EB4 File Offset: 0x000E20B4
		private void SendLinkedResources(MailMessage message, LinkedResourceCollection resources, string boundary)
		{
			foreach (LinkedResource linkedResource in resources)
			{
				this.StartSection(boundary, linkedResource.ContentType, linkedResource);
				switch (linkedResource.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				}
				case TransferEncoding.QuotedPrintable:
				{
					byte[] array2 = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array2, 0, array2.Length);
					this.SendData(this.ToQuotedPrintable(array2));
					break;
				}
				case TransferEncoding.Base64:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				}
			}
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000E3FC8 File Offset: 0x000E21C8
		private void SendAttachments(MailMessage message, Attachment body, string boundary)
		{
			foreach (Attachment attachment in message.Attachments)
			{
				ContentType contentType = new ContentType(attachment.ContentType.ToString());
				if (attachment.Name != null)
				{
					contentType.Name = attachment.Name;
					if (attachment.NameEncoding != null)
					{
						contentType.CharSet = attachment.NameEncoding.HeaderName;
					}
					attachment.ContentDisposition.FileName = attachment.Name;
				}
				this.StartSection(boundary, contentType, attachment, attachment != body);
				byte[] array = new byte[attachment.ContentStream.Length];
				attachment.ContentStream.Read(array, 0, array.Length);
				switch (attachment.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				case TransferEncoding.QuotedPrintable:
					this.SendData(this.ToQuotedPrintable(array));
					break;
				case TransferEncoding.Base64:
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				this.SendData(string.Empty);
			}
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000E40F4 File Offset: 0x000E22F4
		private SmtpClient.SmtpResponse SendCommand(string command)
		{
			this.writer.Write(command);
			this.writer.Write("\r\n");
			this.writer.Flush();
			return this.Read();
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000E4123 File Offset: 0x000E2323
		private void SendHeader(string name, string value)
		{
			this.SendData(string.Format("{0}: {1}", name, value));
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000E4137 File Offset: 0x000E2337
		private void StartSection(string section, ContentType sectionContentType)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendData(string.Empty);
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000E4168 File Offset: 0x000E2368
		private void StartSection(string section, ContentType sectionContentType, AttachmentBase att)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendData(string.Empty);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000E41E8 File Offset: 0x000E23E8
		private void StartSection(string section, ContentType sectionContentType, Attachment att, bool sendDisposition)
		{
			this.SendData(string.Format("--{0}", section));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (sendDisposition)
			{
				this.SendHeader("content-disposition", att.ContentDisposition.ToString());
			}
			this.SendData(string.Empty);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000E4280 File Offset: 0x000E2480
		private string ToQuotedPrintable(string input, Encoding enc)
		{
			byte[] bytes = enc.GetBytes(input);
			return this.ToQuotedPrintable(bytes);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000E429C File Offset: 0x000E249C
		private string ToQuotedPrintable(byte[] bytes)
		{
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("=", 3);
			byte b = 61;
			char c = '\0';
			int i = 0;
			while (i < bytes.Length)
			{
				byte b2 = bytes[i];
				int num2;
				if (b2 > 127 || b2 == b)
				{
					stringBuilder.Length = 1;
					stringBuilder.Append(Convert.ToString(b2, 16).ToUpperInvariant());
					num2 = 3;
					goto IL_007C;
				}
				c = Convert.ToChar(b2);
				if (c != '\r' && c != '\n')
				{
					num2 = 1;
					goto IL_007C;
				}
				stringWriter.Write(c);
				num = 0;
				IL_00AC:
				i++;
				continue;
				IL_007C:
				num += num2;
				if (num > 75)
				{
					stringWriter.Write("=\r\n");
					num = num2;
				}
				if (num2 == 1)
				{
					stringWriter.Write(c);
					goto IL_00AC;
				}
				stringWriter.Write(stringBuilder.ToString());
				goto IL_00AC;
			}
			return stringWriter.ToString();
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000E436C File Offset: 0x000E256C
		private static string GetTransferEncodingName(TransferEncoding encoding)
		{
			switch (encoding)
			{
			case TransferEncoding.QuotedPrintable:
				return "quoted-printable";
			case TransferEncoding.Base64:
				return "base64";
			case TransferEncoding.SevenBit:
				return "7bit";
			default:
				return "unknown";
			}
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000E439C File Offset: 0x000E259C
		private void InitiateSecureConnection()
		{
			SmtpClient.SmtpResponse smtpResponse = this.SendCommand("STARTTLS");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(SmtpStatusCode.GeneralFailure, "Server does not support secure connections.");
			}
			MobileTlsProvider providerInternal = Mono.Net.Security.MonoTlsProviderFactory.GetProviderInternal();
			MonoTlsSettings monoTlsSettings = MonoTlsSettings.CopyDefaultSettings();
			monoTlsSettings.UseServicePointManagerCallback = new bool?(true);
			SslStream sslStream = new SslStream(this.stream, false, providerInternal, monoTlsSettings);
			this.CheckCancellation();
			sslStream.AuthenticateAsClient(this.Host, this.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			this.stream = sslStream;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000E4418 File Offset: 0x000E2618
		private void Authenticate()
		{
			string text;
			string text2;
			if (this.UseDefaultCredentials)
			{
				text = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").UserName;
				text2 = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").Password;
			}
			else
			{
				if (this.Credentials == null)
				{
					return;
				}
				text = this.Credentials.GetCredential(this.host, this.port, "smtp").UserName;
				text2 = this.Credentials.GetCredential(this.host, this.port, "smtp").Password;
			}
			this.Authenticate(text, text2);
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000E44DE File Offset: 0x000E26DE
		private void CheckStatus(SmtpClient.SmtpResponse status, int i)
		{
			if (status.StatusCode != (SmtpStatusCode)i)
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000E44FB File Offset: 0x000E26FB
		private void ThrowIfError(SmtpClient.SmtpResponse status)
		{
			if (this.IsError(status))
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000E4518 File Offset: 0x000E2718
		private void Authenticate(string user, string password)
		{
			if (this.authMechs == SmtpClient.AuthMechs.None)
			{
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Login) != SmtpClient.AuthMechs.None)
			{
				SmtpClient.SmtpResponse smtpResponse = this.SendCommand("AUTH LOGIN");
				this.CheckStatus(smtpResponse, 334);
				smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(user)));
				this.CheckStatus(smtpResponse, 334);
				smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
				this.CheckStatus(smtpResponse, 235);
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Plain) != SmtpClient.AuthMechs.None)
			{
				string text = string.Format("\0{0}\0{1}", user, password);
				text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
				SmtpClient.SmtpResponse smtpResponse = this.SendCommand("AUTH PLAIN " + text);
				this.CheckStatus(smtpResponse, 235);
				return;
			}
			throw new SmtpException("AUTH types PLAIN, LOGIN not supported by the server");
		}

		// Token: 0x04002769 RID: 10089
		private string host;

		// Token: 0x0400276A RID: 10090
		private int port;

		// Token: 0x0400276B RID: 10091
		private int timeout = 100000;

		// Token: 0x0400276C RID: 10092
		private ICredentialsByHost credentials;

		// Token: 0x0400276D RID: 10093
		private string pickupDirectoryLocation;

		// Token: 0x0400276E RID: 10094
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x0400276F RID: 10095
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x04002770 RID: 10096
		private bool enableSsl;

		// Token: 0x04002771 RID: 10097
		private X509CertificateCollection clientCertificates;

		// Token: 0x04002772 RID: 10098
		private TcpClient client;

		// Token: 0x04002773 RID: 10099
		private Stream stream;

		// Token: 0x04002774 RID: 10100
		private StreamWriter writer;

		// Token: 0x04002775 RID: 10101
		private StreamReader reader;

		// Token: 0x04002776 RID: 10102
		private int boundaryIndex;

		// Token: 0x04002777 RID: 10103
		private MailAddress defaultFrom;

		// Token: 0x04002778 RID: 10104
		private MailMessage messageInProcess;

		// Token: 0x04002779 RID: 10105
		private BackgroundWorker worker;

		// Token: 0x0400277A RID: 10106
		private object user_async_state;

		// Token: 0x0400277B RID: 10107
		private SmtpClient.AuthMechs authMechs;

		// Token: 0x0400277C RID: 10108
		private Mutex mutex = new Mutex();

		// Token: 0x02000808 RID: 2056
		[Flags]
		private enum AuthMechs
		{
			// Token: 0x04002780 RID: 10112
			None = 0,
			// Token: 0x04002781 RID: 10113
			Login = 1,
			// Token: 0x04002782 RID: 10114
			Plain = 2
		}

		// Token: 0x02000809 RID: 2057
		private class CancellationException : Exception
		{
		}

		// Token: 0x0200080A RID: 2058
		private struct HeaderName
		{
			// Token: 0x04002783 RID: 10115
			public const string ContentTransferEncoding = "Content-Transfer-Encoding";

			// Token: 0x04002784 RID: 10116
			public const string ContentType = "Content-Type";

			// Token: 0x04002785 RID: 10117
			public const string Bcc = "Bcc";

			// Token: 0x04002786 RID: 10118
			public const string Cc = "Cc";

			// Token: 0x04002787 RID: 10119
			public const string From = "From";

			// Token: 0x04002788 RID: 10120
			public const string Subject = "Subject";

			// Token: 0x04002789 RID: 10121
			public const string To = "To";

			// Token: 0x0400278A RID: 10122
			public const string MimeVersion = "MIME-Version";

			// Token: 0x0400278B RID: 10123
			public const string MessageId = "Message-ID";

			// Token: 0x0400278C RID: 10124
			public const string Priority = "Priority";

			// Token: 0x0400278D RID: 10125
			public const string Importance = "Importance";

			// Token: 0x0400278E RID: 10126
			public const string XPriority = "X-Priority";

			// Token: 0x0400278F RID: 10127
			public const string Date = "Date";
		}

		// Token: 0x0200080B RID: 2059
		private struct SmtpResponse
		{
			// Token: 0x06004201 RID: 16897 RVA: 0x000E45E8 File Offset: 0x000E27E8
			public static SmtpClient.SmtpResponse Parse(string line)
			{
				SmtpClient.SmtpResponse smtpResponse = default(SmtpClient.SmtpResponse);
				if (line.Length < 4)
				{
					throw new SmtpException("Response is to short " + line.Length.ToString() + ".");
				}
				if (line[3] != ' ' && line[3] != '-')
				{
					throw new SmtpException("Response format is wrong.(" + line + ")");
				}
				smtpResponse.StatusCode = (SmtpStatusCode)int.Parse(line.Substring(0, 3));
				smtpResponse.Description = line;
				return smtpResponse;
			}

			// Token: 0x04002790 RID: 10128
			public SmtpStatusCode StatusCode;

			// Token: 0x04002791 RID: 10129
			public string Description;
		}
	}
}
