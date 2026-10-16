using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an e-mail message that can be sent using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000803 RID: 2051
	public class MailMessage : IDisposable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		// Token: 0x06004181 RID: 16769 RVA: 0x000E22B8 File Offset: 0x000E04B8
		public MailMessage()
		{
			this.to = new MailAddressCollection();
			this.alternateViews = new AlternateViewCollection();
			this.attachments = new AttachmentCollection();
			this.bcc = new MailAddressCollection();
			this.cc = new MailAddressCollection();
			this.replyTo = new MailAddressCollection();
			this.headers = new NameValueCollection();
			this.headers.Add("MIME-Version", "1.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.Net.Mail.MailAddress" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the recipient of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06004182 RID: 16770 RVA: 0x000E2338 File Offset: 0x000E0538
		public MailMessage(MailAddress from, MailAddress to)
			: this()
		{
			if (from == null || to == null)
			{
				throw new ArgumentNullException();
			}
			this.From = from;
			this.to.Add(to);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.String" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06004183 RID: 16771 RVA: 0x000E2360 File Offset: 0x000E0560
		public MailMessage(string from, string to)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the address of the recipient of the e-mail message.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject text.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x06004184 RID: 16772 RVA: 0x000E23F0 File Offset: 0x000E05F0
		public MailMessage(string from, string to, string subject, string body)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
			this.Body = body;
			this.Subject = subject;
		}

		/// <summary>Gets the attachment collection used to store alternate forms of the message body.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AlternateViewCollection" />.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000E248E File Offset: 0x000E068E
		public AlternateViewCollection AlternateViews
		{
			get
			{
				return this.alternateViews;
			}
		}

		/// <summary>Gets the attachment collection used to store data attached to this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AttachmentCollection" />.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x000E2496 File Offset: 0x000E0696
		public AttachmentCollection Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000E249E File Offset: 0x000E069E
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		/// <summary>Gets or sets the message body.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the body text.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004188 RID: 16776 RVA: 0x000E24A6 File Offset: 0x000E06A6
		// (set) Token: 0x06004189 RID: 16777 RVA: 0x000E24AE File Offset: 0x000E06AE
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				if (value != null && this.bodyEncoding == null)
				{
					this.bodyEncoding = this.GuessEncoding(value) ?? Encoding.ASCII;
				}
				this.body = value;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x000E24D8 File Offset: 0x000E06D8
		internal ContentType BodyContentType
		{
			get
			{
				return new ContentType(this.isHtml ? "text/html" : "text/plain")
				{
					CharSet = (this.BodyEncoding ?? Encoding.ASCII).HeaderName
				};
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x000E250D File Offset: 0x000E070D
		internal TransferEncoding ContentTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
		}

		/// <summary>Gets or sets the encoding used to encode the message body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x000E251A File Offset: 0x000E071A
		// (set) Token: 0x0600418D RID: 16781 RVA: 0x000E2522 File Offset: 0x000E0722
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

		/// <summary>Gets or sets the transfer encoding used to encode the message body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Mime.TransferEncoding" />.A <see cref="T:System.Net.Mime.TransferEncoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x0600418E RID: 16782 RVA: 0x000E250D File Offset: 0x000E070D
		// (set) Token: 0x0600418F RID: 16783 RVA: 0x0000829A File Offset: 0x0000649A
		public TransferEncoding BodyTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000E252B File Offset: 0x000E072B
		public MailAddressCollection CC
		{
			get
			{
				return this.cc;
			}
		}

		/// <summary>Gets or sets the delivery notifications for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.DeliveryNotificationOptions" /> value that contains the delivery notifications for this message.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000E2533 File Offset: 0x000E0733
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x000E253B File Offset: 0x000E073B
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryNotificationOptions;
			}
			set
			{
				this.deliveryNotificationOptions = value;
			}
		}

		/// <summary>Gets or sets the from address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the from address information.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000E2544 File Offset: 0x000E0744
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x000E254C File Offset: 0x000E074C
		public MailAddress From
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

		/// <summary>Gets the e-mail headers that are transmitted with this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the e-mail headers.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000E2555 File Offset: 0x000E0755
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets a value indicating whether the mail message body is in Html.</summary>
		/// <returns>true if the message body is in Html; else false. The default is false.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x000E255D File Offset: 0x000E075D
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x000E2565 File Offset: 0x000E0765
		public bool IsBodyHtml
		{
			get
			{
				return this.isHtml;
			}
			set
			{
				this.isHtml = value;
			}
		}

		/// <summary>Gets or sets the priority of this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailPriority" /> that contains the priority of this message.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x000E256E File Offset: 0x000E076E
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x000E2576 File Offset: 0x000E0776
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

		/// <summary>Gets or sets the encoding used for the user-defined custom headers for this e-mail message.</summary>
		/// <returns>The encoding used for user-defined custom headers for this e-mail message.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x000E257F File Offset: 0x000E077F
		// (set) Token: 0x0600419B RID: 16795 RVA: 0x000E2587 File Offset: 0x000E0787
		public Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		/// <summary>Gets or sets the list of addresses to reply to for the mail message.</summary>
		/// <returns>The list of the addresses to reply to for the mail message.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x000E2590 File Offset: 0x000E0790
		public MailAddressCollection ReplyToList
		{
			get
			{
				return this.replyTo;
			}
		}

		/// <summary>Gets or sets the ReplyTo address for the mail message.</summary>
		/// <returns>A MailAddress that indicates the value of the <see cref="P:System.Net.Mail.MailMessage.ReplyTo" /> field.</returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000E2598 File Offset: 0x000E0798
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000E25B5 File Offset: 0x000E07B5
		[Obsolete("Use ReplyToList instead")]
		public MailAddress ReplyTo
		{
			get
			{
				if (this.replyTo.Count == 0)
				{
					return null;
				}
				return this.replyTo[0];
			}
			set
			{
				this.replyTo.Clear();
				this.replyTo.Add(value);
			}
		}

		/// <summary>Gets or sets the sender's address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the sender's address information.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000E25CE File Offset: 0x000E07CE
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x000E25D6 File Offset: 0x000E07D6
		public MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		/// <summary>Gets or sets the subject line for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the subject content.</returns>
		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000E25DF File Offset: 0x000E07DF
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x000E25E7 File Offset: 0x000E07E7
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (value != null && this.subjectEncoding == null)
				{
					this.subjectEncoding = this.GuessEncoding(value);
				}
				this.subject = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the subject content for this e-mail message.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that was used to encode the <see cref="P:System.Net.Mail.MailMessage.Subject" /> property.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000E2608 File Offset: 0x000E0808
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x000E2610 File Offset: 0x000E0810
		public Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		/// <summary>Gets the address collection that contains the recipients of this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000E2619 File Offset: 0x000E0819
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.MailMessage" />. </summary>
		// Token: 0x060041A6 RID: 16806 RVA: 0x000E2621 File Offset: 0x000E0821
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060041A7 RID: 16807 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E2630 File Offset: 0x000E0830
		private Encoding GuessEncoding(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					return MailMessage.UTF8Unmarked;
				}
			}
			return null;
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000E2664 File Offset: 0x000E0864
		internal static TransferEncoding GuessTransferEncoding(Encoding enc)
		{
			if (Encoding.ASCII.Equals(enc))
			{
				return TransferEncoding.SevenBit;
			}
			if (Encoding.UTF8.CodePage == enc.CodePage || Encoding.Unicode.CodePage == enc.CodePage || Encoding.UTF32.CodePage == enc.CodePage)
			{
				return TransferEncoding.Base64;
			}
			return TransferEncoding.QuotedPrintable;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E26BC File Offset: 0x000E08BC
		internal static string To2047(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				if (b < 33 || b > 126 || b == 63 || b == 61 || b == 95)
				{
					stringBuilder.Append('=');
					stringBuilder.Append(MailMessage.hex[(b >> 4) & 15]);
					stringBuilder.Append(MailMessage.hex[(int)(b & 15)]);
				}
				else
				{
					stringBuilder.Append((char)b);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000E273C File Offset: 0x000E093C
		internal static string EncodeSubjectRFC2047(string s, Encoding enc)
		{
			if (s == null || Encoding.ASCII.Equals(enc))
			{
				return s;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					string text = MailMessage.To2047(enc.GetBytes(s));
					return string.Concat(new string[] { "=?", enc.HeaderName, "?Q?", text, "?=" });
				}
			}
			return s;
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x000E27B9 File Offset: 0x000E09B9
		private static Encoding UTF8Unmarked
		{
			get
			{
				if (MailMessage.utf8unmarked == null)
				{
					MailMessage.utf8unmarked = new UTF8Encoding(false);
				}
				return MailMessage.utf8unmarked;
			}
		}

		// Token: 0x0400274E RID: 10062
		private AlternateViewCollection alternateViews;

		// Token: 0x0400274F RID: 10063
		private AttachmentCollection attachments;

		// Token: 0x04002750 RID: 10064
		private MailAddressCollection bcc;

		// Token: 0x04002751 RID: 10065
		private MailAddressCollection replyTo;

		// Token: 0x04002752 RID: 10066
		private string body;

		// Token: 0x04002753 RID: 10067
		private MailPriority priority;

		// Token: 0x04002754 RID: 10068
		private MailAddress sender;

		// Token: 0x04002755 RID: 10069
		private DeliveryNotificationOptions deliveryNotificationOptions;

		// Token: 0x04002756 RID: 10070
		private MailAddressCollection cc;

		// Token: 0x04002757 RID: 10071
		private MailAddress from;

		// Token: 0x04002758 RID: 10072
		private NameValueCollection headers;

		// Token: 0x04002759 RID: 10073
		private MailAddressCollection to;

		// Token: 0x0400275A RID: 10074
		private string subject;

		// Token: 0x0400275B RID: 10075
		private Encoding subjectEncoding;

		// Token: 0x0400275C RID: 10076
		private Encoding bodyEncoding;

		// Token: 0x0400275D RID: 10077
		private Encoding headersEncoding = Encoding.UTF8;

		// Token: 0x0400275E RID: 10078
		private bool isHtml;

		// Token: 0x0400275F RID: 10079
		private static char[] hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x04002760 RID: 10080
		private static Encoding utf8unmarked;
	}
}
