using System;
using System.Collections;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F6 RID: 246
	internal class MailMessageWrapper
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00022E08 File Offset: 0x00021008
		public MailMessageWrapper(MailMessage message)
		{
			this.message = message;
			if (message.From != null)
			{
				this.from = MailAddress.Parse(message.From);
				this.header.From = this.from.ToString();
			}
			if (message.To != null)
			{
				this.to = MailAddressCollection.Parse(message.To);
				this.header.To = this.to.ToString();
			}
			if (message.Cc != null)
			{
				this.cc = MailAddressCollection.Parse(message.Cc);
				this.header.Cc = this.cc.ToString();
			}
			if (message.Bcc != null)
			{
				this.bcc = MailAddressCollection.Parse(message.Bcc);
				this.header.Bcc = this.bcc.ToString();
			}
			if (message.Subject != null)
			{
				if (MailUtil.NeedEncoding(message.Subject))
				{
					byte[] bytes = message.BodyEncoding.GetBytes(message.Subject);
					this.header.Subject = string.Concat(new string[]
					{
						"=?",
						message.BodyEncoding.BodyName,
						"?B?",
						Convert.ToBase64String(bytes),
						"?="
					});
				}
				else
				{
					this.header.Subject = message.Subject;
				}
			}
			if (message.Body != null)
			{
				this.body = message.Body.Replace("\n.\n", "\n..\n");
				this.body = this.body.Replace("\r\n.\r\n", "\r\n..\r\n");
			}
			if (message.UrlContentBase != null)
			{
				this.header.ContentBase = message.UrlContentBase;
			}
			if (message.UrlContentLocation != null)
			{
				this.header.ContentLocation = message.UrlContentLocation;
			}
			MailFormat bodyFormat = message.BodyFormat;
			if (bodyFormat != MailFormat.Text)
			{
				if (bodyFormat == MailFormat.Html)
				{
					this.header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
				}
				else
				{
					this.header.ContentType = "text/html; charset=\"" + message.BodyEncoding.BodyName + "\"";
				}
			}
			else
			{
				this.header.ContentType = "text/plain; charset=\"" + message.BodyEncoding.BodyName + "\"";
			}
			switch (message.Priority)
			{
			case MailPriority.Normal:
				this.header.Importance = "normal";
				break;
			case MailPriority.Low:
				this.header.Importance = "low";
				break;
			case MailPriority.High:
				this.header.Importance = "high";
				break;
			default:
				this.header.Importance = "normal";
				break;
			}
			this.header.Priority = "normal";
			this.header.MimeVersion = "1.0";
			if (message.BodyEncoding is ASCIIEncoding)
			{
				this.header.ContentTransferEncoding = "7bit";
			}
			else
			{
				this.header.ContentTransferEncoding = "8bit";
			}
			foreach (object obj in message.Headers.Keys)
			{
				string text = (string)obj;
				this.header.Data[text] = (string)this.message.Headers[text];
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000231A4 File Offset: 0x000213A4
		public IList Attachments
		{
			get
			{
				return this.message.Attachments;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x000231B1 File Offset: 0x000213B1
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000231B9 File Offset: 0x000213B9
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x000231C1 File Offset: 0x000213C1
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x000231CA File Offset: 0x000213CA
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x000231D7 File Offset: 0x000213D7
		public Encoding BodyEncoding
		{
			get
			{
				return this.message.BodyEncoding;
			}
			set
			{
				this.message.BodyEncoding = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000231E5 File Offset: 0x000213E5
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x000231F2 File Offset: 0x000213F2
		public MailFormat BodyFormat
		{
			get
			{
				return this.message.BodyFormat;
			}
			set
			{
				this.message.BodyFormat = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00023200 File Offset: 0x00021400
		public MailAddressCollection Cc
		{
			get
			{
				return this.cc;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00023208 File Offset: 0x00021408
		public MailAddress From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00023210 File Offset: 0x00021410
		public MailHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00023218 File Offset: 0x00021418
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x00023225 File Offset: 0x00021425
		public MailPriority Priority
		{
			get
			{
				return this.message.Priority;
			}
			set
			{
				this.message.Priority = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00023233 File Offset: 0x00021433
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x00023240 File Offset: 0x00021440
		public string Subject
		{
			get
			{
				return this.message.Subject;
			}
			set
			{
				this.message.Subject = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0002324E File Offset: 0x0002144E
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00023256 File Offset: 0x00021456
		public string UrlContentBase
		{
			get
			{
				return this.message.UrlContentBase;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00023263 File Offset: 0x00021463
		public string UrlContentLocation
		{
			get
			{
				return this.message.UrlContentLocation;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00023270 File Offset: 0x00021470
		public MailHeader Fields
		{
			get
			{
				MailHeader mailHeader = new MailHeader();
				foreach (object obj in this.message.Fields.Keys)
				{
					string text = (string)obj;
					mailHeader.Data[text] = this.message.Fields[text].ToString();
				}
				return mailHeader;
			}
		}

		// Token: 0x0400113D RID: 4413
		private MailAddressCollection bcc = new MailAddressCollection();

		// Token: 0x0400113E RID: 4414
		private MailAddressCollection cc = new MailAddressCollection();

		// Token: 0x0400113F RID: 4415
		private MailAddress from;

		// Token: 0x04001140 RID: 4416
		private MailAddressCollection to = new MailAddressCollection();

		// Token: 0x04001141 RID: 4417
		private MailHeader header = new MailHeader();

		// Token: 0x04001142 RID: 4418
		private MailMessage message;

		// Token: 0x04001143 RID: 4419
		private string body;
	}
}
