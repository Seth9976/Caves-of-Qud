using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000FA RID: 250
	internal class SmtpClient
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x0002348C File Offset: 0x0002168C
		public SmtpClient(string server)
		{
			this.server = server;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000234AC File Offset: 0x000216AC
		private void Connect()
		{
			this.tcpConnection = new TcpClient(this.server, this.port);
			NetworkStream stream = this.tcpConnection.GetStream();
			this.smtp = new SmtpStream(stream);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000234E8 File Offset: 0x000216E8
		private void ChangeToSSLSocket()
		{
			SslStream sslStream = new SslStream(this.smtp.Stream);
			sslStream.AuthenticateAsClient(this.server);
			this.smtp = new SmtpStream(sslStream);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00023520 File Offset: 0x00021720
		private void ReadFields(MailMessageWrapper msg)
		{
			this.username = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"];
			this.password = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"];
			string text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"];
			if (text != null)
			{
				this.authenticate = short.Parse(text);
			}
			text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpusessl"];
			if (text != null)
			{
				this.usessl = bool.Parse(text);
			}
			text = msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpserverport"];
			if (text != null)
			{
				this.port = int.Parse(text);
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000235D4 File Offset: 0x000217D4
		private void StartSend(MailMessageWrapper msg)
		{
			this.ReadFields(msg);
			this.Connect();
			this.smtp.ReadResponse();
			this.smtp.CheckForStatusCode(220);
			if (this.usessl || (this.username != null && this.password != null && this.authenticate != 1))
			{
				this.smtp.WriteEhlo(Dns.GetHostName());
				if (this.usessl && this.smtp.WriteStartTLS())
				{
					this.ChangeToSSLSocket();
				}
				if (this.username != null && this.password != null && this.authenticate != 1)
				{
					this.smtp.WriteAuthLogin();
					if (this.smtp.LastResponse.StatusCode == 334)
					{
						this.smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(this.username)));
						this.smtp.ReadResponse();
						this.smtp.CheckForStatusCode(334);
						this.smtp.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(this.password)));
						this.smtp.ReadResponse();
						this.smtp.CheckForStatusCode(235);
						return;
					}
				}
			}
			else
			{
				this.smtp.WriteHelo(Dns.GetHostName());
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002372C File Offset: 0x0002192C
		public void Send(MailMessageWrapper msg)
		{
			if (msg.From == null)
			{
				throw new SmtpException("From property must be set.");
			}
			if (msg.To == null && msg.To.Count < 1)
			{
				throw new SmtpException("Atleast one recipient must be set.");
			}
			this.StartSend(msg);
			this.smtp.WriteRset();
			this.smtp.WriteMailFrom(msg.From.Address);
			foreach (object obj in msg.To)
			{
				MailAddress mailAddress = (MailAddress)obj;
				this.smtp.WriteRcptTo(mailAddress.Address);
			}
			foreach (object obj2 in msg.Cc)
			{
				MailAddress mailAddress2 = (MailAddress)obj2;
				this.smtp.WriteRcptTo(mailAddress2.Address);
			}
			foreach (object obj3 in msg.Bcc)
			{
				MailAddress mailAddress3 = (MailAddress)obj3;
				this.smtp.WriteRcptTo(mailAddress3.Address);
			}
			this.smtp.WriteData();
			if (msg.Attachments.Count == 0)
			{
				this.SendSinglepartMail(msg);
			}
			else
			{
				this.SendMultipartMail(msg);
			}
			this.smtp.WriteDataEndTag();
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000238C4 File Offset: 0x00021AC4
		private void SendSinglepartMail(MailMessageWrapper msg)
		{
			this.smtp.WriteHeader(msg.Header);
			this.smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000238F4 File Offset: 0x00021AF4
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void SendMultipartMail(MailMessageWrapper msg)
		{
			string text = MailUtil.GenerateBoundary();
			string contentType = msg.Header.ContentType;
			msg.Header.ContentType = "multipart/mixed;\r\n   boundary=" + text;
			this.smtp.WriteHeader(msg.Header);
			this.smtp.WriteBoundary(text);
			MailHeader mailHeader = new MailHeader();
			mailHeader.ContentType = contentType;
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
			}
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendusername"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendusername");
			}
			if (msg.Fields.Data["http://schemas.microsoft.com/cdo/configuration/sendpassword"] != null)
			{
				msg.Fields.Data.Remove("http://schemas.microsoft.com/cdo/configuration/sendpassword");
			}
			mailHeader.Data.Add(msg.Fields.Data);
			this.smtp.WriteHeader(mailHeader);
			this.smtp.WriteBytes(msg.BodyEncoding.GetBytes(msg.Body));
			this.smtp.WriteBoundary(text);
			for (int i = 0; i < msg.Attachments.Count; i++)
			{
				MailAttachment mailAttachment = (MailAttachment)msg.Attachments[i];
				FileInfo fileInfo = new FileInfo(mailAttachment.Filename);
				MailHeader mailHeader2 = new MailHeader();
				mailHeader2.ContentType = MimeTypes.GetMimeType(fileInfo.Name) + "; name=\"" + fileInfo.Name + "\"";
				mailHeader2.ContentDisposition = "attachment; filename=\"" + fileInfo.Name + "\"";
				mailHeader2.ContentTransferEncoding = mailAttachment.Encoding.ToString();
				this.smtp.WriteHeader(mailHeader2);
				FileStream fileStream = fileInfo.OpenRead();
				IAttachmentEncoder attachmentEncoder;
				if (mailAttachment.Encoding == MailEncoding.UUEncode)
				{
					attachmentEncoder = new UUAttachmentEncoder(644, fileInfo.Name);
				}
				else
				{
					attachmentEncoder = new Base64AttachmentEncoder();
				}
				attachmentEncoder.EncodeStream(fileStream, this.smtp.Stream);
				fileStream.Close();
				this.smtp.WriteLine("");
				if (i < msg.Attachments.Count - 1)
				{
					this.smtp.WriteBoundary(text);
				}
				else
				{
					this.smtp.WriteFinalBoundary(text);
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00023B58 File Offset: 0x00021D58
		public void Close()
		{
			this.smtp.WriteQuit();
			this.tcpConnection.Close();
		}

		// Token: 0x0400114A RID: 4426
		private string server;

		// Token: 0x0400114B RID: 4427
		private TcpClient tcpConnection;

		// Token: 0x0400114C RID: 4428
		private SmtpStream smtp;

		// Token: 0x0400114D RID: 4429
		private string username;

		// Token: 0x0400114E RID: 4430
		private string password;

		// Token: 0x0400114F RID: 4431
		private int port = 25;

		// Token: 0x04001150 RID: 4432
		private bool usessl;

		// Token: 0x04001151 RID: 4433
		private short authenticate = 1;
	}
}
