using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace System.Web.Mail
{
	/// <summary>Provides properties and methods for constructing an e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
	// Token: 0x020000F5 RID: 245
	[Obsolete("The recommended alternative is System.Net.Mail.MailMessage. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class MailMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Mail.MailMessage" /> class. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00022CDC File Offset: 0x00020EDC
		public MailMessage()
		{
			this.attachments = new ArrayList(8);
			this.headers = new ListDictionary();
			this.bodyEncoding = Encoding.Default;
			this.fields = new Hashtable();
		}

		/// <summary>Specifies the collection of attachments that are transmitted with the message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> collection of <see cref="T:System.Web.Mail.MailAttachment" /> objects.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00022D32 File Offset: 0x00020F32
		public IList Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of email addresses that receive a blind carbon copy (BCC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses that receive a blind carbon copy (BCC) of the e-mail message.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00022D3A File Offset: 0x00020F3A
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00022D42 File Offset: 0x00020F42
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		/// <summary>Gets or sets the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The body of the e-mail message.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00022D4B File Offset: 0x00020F4B
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00022D53 File Offset: 0x00020F53
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

		/// <summary>Gets or sets the encoding type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Text.Encoding" /> values that indicates the encoding type of the body of the e-mail message.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00022D5C File Offset: 0x00020F5C
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x00022D64 File Offset: 0x00020F64
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		/// <summary>Gets or sets the content type of the body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Mail.MailFormat" /> values.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00022D6D File Offset: 0x00020F6D
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x00022D75 File Offset: 0x00020F75
		public MailFormat BodyFormat
		{
			get
			{
				return this.bodyFormat;
			}
			set
			{
				this.bodyFormat = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses that receive a carbon copy (CC) of the e-mail message.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00022D7E File Offset: 0x00020F7E
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00022D86 File Offset: 0x00020F86
		public string Cc
		{
			get
			{
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		/// <summary>Gets or sets the e-mail address of the sender. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The sender's e-mail address.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00022D8F File Offset: 0x00020F8F
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00022D97 File Offset: 0x00020F97
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		/// <summary>Specifies the custom headers that are transmitted with the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of custom headers.</returns>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00022DA0 File Offset: 0x00020FA0
		public IDictionary Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets the priority of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>One of the <see cref="T:System.Web.Mail.MailPriority" /> values.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00022DA8 File Offset: 0x00020FA8
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00022DB0 File Offset: 0x00020FB0
		public MailPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		/// <summary>Gets or sets the subject line of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The subject line of the e-mail message.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00022DB9 File Offset: 0x00020FB9
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00022DC1 File Offset: 0x00020FC1
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of recipient e-mail addresses. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>A semicolon-delimited list of e-mail addresses.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00022DCA File Offset: 0x00020FCA
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00022DD2 File Offset: 0x00020FD2
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		/// <summary>Gets or sets the Content-Base HTTP header, the URL base of all relative URLs used within the HTML-encoded body of the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The URL base.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00022DDB File Offset: 0x00020FDB
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00022DE3 File Offset: 0x00020FE3
		public string UrlContentBase
		{
			get
			{
				return this.urlContentBase;
			}
			set
			{
				this.urlContentBase = value;
			}
		}

		/// <summary>Gets or sets the Content-Location HTTP header for the e-mail message. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>The content-base header.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00022DEC File Offset: 0x00020FEC
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00022DF4 File Offset: 0x00020FF4
		public string UrlContentLocation
		{
			get
			{
				return this.urlContentLocation;
			}
			set
			{
				this.urlContentLocation = value;
			}
		}

		/// <summary>Gets a collection of objects that map to Microsoft Collaboration Data Objects (CDO) fields. Recommended alternative: <see cref="N:System.Net.Mail" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> collection of objects that map to Collaboration Data Objects (CDO) fields.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00022DFD File Offset: 0x00020FFD
		public IDictionary Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x0400112F RID: 4399
		private ArrayList attachments;

		// Token: 0x04001130 RID: 4400
		private string bcc;

		// Token: 0x04001131 RID: 4401
		private string body = string.Empty;

		// Token: 0x04001132 RID: 4402
		private Encoding bodyEncoding;

		// Token: 0x04001133 RID: 4403
		private MailFormat bodyFormat;

		// Token: 0x04001134 RID: 4404
		private string cc;

		// Token: 0x04001135 RID: 4405
		private string from;

		// Token: 0x04001136 RID: 4406
		private ListDictionary headers;

		// Token: 0x04001137 RID: 4407
		private MailPriority priority;

		// Token: 0x04001138 RID: 4408
		private string subject = string.Empty;

		// Token: 0x04001139 RID: 4409
		private string to;

		// Token: 0x0400113A RID: 4410
		private string urlContentBase;

		// Token: 0x0400113B RID: 4411
		private string urlContentLocation;

		// Token: 0x0400113C RID: 4412
		private Hashtable fields;
	}
}
